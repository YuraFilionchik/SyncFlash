using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Management;

namespace SyncFlash
{
    public partial class Form1 : Form
    {
        configmanager cfg;
        const string cfg_file = "conf.ini";
        string pc_name = Environment.MachineName;
        List<Project> Projects;
        Thread SyncThread;
        public Form1()
        {
            InitializeComponent();
            cfg = new configmanager(cfg_file);
            Projects = cfg.ReadAllProjects() ?? new List<Project>();
            foreach (var p in Projects)
            {
                List_Projects.Items.Add(p.ToString());
            }
            this.FormClosing += Form1_FormClosing;
            List_Projects.SelectedIndexChanged += List_Projects_SelectedIndexChanged;
            list_dirs.SelectedIndexChanged += List_dirs_SelectedIndexChanged;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            
        }
        
        //selected DIR
        private void List_dirs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (List_Projects.SelectedItem == null)
            { list_dirs.Items.Clear(); listExceptions.Items.Clear(); return; } //clear lists
            if (list_dirs.SelectedItems.Count == 0)
            {   listExceptions.Items.Clear(); //clear list
                return;            }
            var selectedProj = Projects.FirstOrDefault(x => x.Name == List_Projects.SelectedItem.ToString());
            var selectedDir = selectedProj.AllProjectDirs.FirstOrDefault(x => x.Dir == list_dirs.SelectedItems[0].Text);
            //show Dir info
            listExceptions.Items.AddRange(selectedDir.ExceptDirs.ToArray());
            if (list_dirs.SelectedItems.Count==1)
            {
                tblog.Text = selectedProj.Name + Environment.NewLine;
                if(selectedDir!=null && selectedDir.Info1()!=null)
                foreach (var str in selectedDir.Info1())
                {
                    tblog.Text += str+Environment.NewLine;
                }
            }
            if(list_dirs.SelectedItems.Count==2)
            {

            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DisplayDirs();
        }


        //select project
        private void List_Projects_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDirs();
        }

        /// <summary>
        /// Sync All online directories of project
        /// </summary>
        /// <param name="project"></param>
        private void StartSync(Project project)
        {
            if (List_Projects.SelectedItem == null) return;
            var selectedProj = Projects.First(x => x.Name == List_Projects.SelectedItem.ToString());
            SyncThread = new Thread(delegate() 
            {
                int newfiles = 0;
                int updatedfiles = 0;
                int errorCopy = 0;
                var OnlineDirs = selectedProj.AllProjectDirs.Where(x=>x.IsOnline);
                if (OnlineDirs.Count() == 0) return;
                if (OnlineDirs.Count() == 1)
                {
                    foreach (var t in OnlineDirs.First().Info2())
                    {
                        CONSTS.invokeControlText(tblog, t);
                    }
                    return;
                }
                if(OnlineDirs.Count()>1)
                {
                    int count = 1;
                    var sourceDir = OnlineDirs.First(x=>x.LastMod== OnlineDirs.Max(d => d.LastMod));// откуда будем копировать
                    var DestDirs = OnlineDirs.Where(x => x.Dir != sourceDir.Dir);       //в эти папки копирует новые данные
                    if(DestDirs.All(x=>x.LastMod==sourceDir.LastMod)) { MessageBox.Show("Все папки одинаковые, нечего синхронизировать"); return; }
                    string DirsToString = "";
                    CONSTS.invokeControlText(tblog, "From \t" + sourceDir.LastMod.ToString("dd.MM.yyyy hh:mm:ss") + "<=>" + sourceDir.Dir + " \t\t{" + sourceDir.PC_Name + "}");
                    foreach (var d in DestDirs)
                    {
                        DirsToString += d.Dir + "; ";
                        CONSTS.invokeControlText(tblog, "To \t" + d.LastMod.ToString("dd.MM.yyyy hh:mm:ss") + "<=>" + d.Dir + " \t\t{" + d.PC_Name + "}");
                    }
                    var dr = MessageBox.Show("Будут скопированы обновленные и новые файлы из " + sourceDir.Dir + "\r\n в \r\n" + DirsToString,
                        project.Name, MessageBoxButtons.YesNo);
                    
                    if (dr == DialogResult.No) return;
                    foreach (var file in sourceDir.AllFiles)//процесс копирования
                    {
                        count++;
                        foreach (var destdir in DestDirs)
                        {//TODO check copiing
                            if (!Directory.Exists(destdir.Dir)) Directory.CreateDirectory(destdir.Dir);
                            
                            if(destdir.AllFiles.Any(x=>GetRelationPath(x.Key,destdir.Dir)==GetRelationPath(file.Key,sourceDir.Dir)))//file exist
                            {
                                var dfile = destdir.AllFiles.First(x => GetRelationPath(x.Key, destdir.Dir) == GetRelationPath(file.Key, sourceDir.Dir));
                                if(dfile.Value<file.Value)//файл назначения старше исходника
                                {
                                    try { File.Copy(file.Key, dfile.Key, true); }
                                    catch (Exception) { errorCopy++; }
                                    updatedfiles++;
                                }
                                
                                CONSTS.invokeProgress(progressBar1, (int)(count *100/ sourceDir.AllFiles.Count));
                                
                            }
                            else //copy new file
                            {
                                string newfile = destdir.Dir + GetRelationPath(file.Key, sourceDir.Dir);
                                string filedir = newfile.Remove(newfile.Length- newfile.Split('\\').Last().Length);
                                if (!Directory.Exists(filedir)) Directory.CreateDirectory(filedir);
                                try {File.Copy(file.Key, newfile, true); }
                                catch (Exception) { errorCopy++; }
                                CONSTS.invokeProgress(progressBar1, (int)(count*100 / sourceDir.AllFiles.Count));
                                newfiles++;
                            }
                        }

                    }//конец перебора исходных файлов
                    CONSTS.invokeControlText(tblog,project.Name+" синхронизирован.");
                    CONSTS.invokeControlText(tblog, "Новых файлов: \t"+newfiles.ToString());
                    CONSTS.invokeControlText(tblog, "Обновлено файлов:\t" + updatedfiles.ToString());
                    CONSTS.invokeControlText(tblog, "Ошибок копирования:\t" + errorCopy.ToString());
                }


            });
            SyncThread.Start();
        }
        /// <summary>
        /// Выделяет одинаковую часть пусти для файлов одного проекта
        /// </summary>
        /// <param name="fullpath"></param>
        /// <param name="projDir"></param>
        /// <returns></returns>
        public string GetRelationPath(string fullpath,string projDir)
        {
            string result = fullpath.Remove(0, projDir.Length);
            return result;
        }
        /// <summary>
        /// Write dirs of selected project into list_dirs
        /// </summary>
        public void DisplayDirs()
        {
            list_dirs.Items.Clear();
            listExceptions.Items.Clear();
            if (List_Projects.SelectedItem == null)
            {  return; }
            
            var selectedProj = Projects.First(x => x.Name == List_Projects.SelectedItem.ToString());
            foreach (var item in selectedProj.AllProjectDirs)
            {
                if (!checkBox1.Checked || item.IsOnline && (item.PC_Name == pc_name|| item.PC_Name==CONSTS.FlashDrive))
                {
                    list_dirs.Items.Add(item.Dir);
                }

            }
        }
        //Завершение программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllProjects();
        }

        private void SaveAllProjects()
        {
          if(Projects!=null && Projects.Count!=0)  cfg.SaveProjects(Projects);
        }
        #region contextMenu
        private void добавитьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input input = new Input();
            input.ShowDialog();
            if (!String.IsNullOrWhiteSpace(input.TEXT) && !List_Projects.Items.Contains(input.TEXT))
            {
                List_Projects.Items.Add(input.TEXT);
                var proj = new Project(input.TEXT);
               Projects.Add(proj);
                SaveAllProjects();
            }
        }
        //добавление папки на флешке
        private void добавитьПапкуНаFlashDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input input = new Input();
            input.ShowDialog();
            var selected = List_Projects.SelectedItem;
            if (string.IsNullOrWhiteSpace(input.TEXT) ||
                list_dirs.Items.Contains(new ListViewItem(input.TEXT.TrimEnd('\\'))) ||
                selected == null) return;

            if (!list_dirs.Items.Contains(new ListViewItem(input.TEXT.TrimEnd('\\')))) list_dirs.Items.Add(input.TEXT.TrimEnd('\\'));
            Projects.First(x => x.Name == selected.ToString()).AllProjectDirs.Add(new Projdir(input.TEXT.TrimEnd('\\'),CONSTS.FlashDrive));
            SaveAllProjects();
        }
        private void добавитьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input input = new Input();
            input.ShowDialog();
            var selected = List_Projects.SelectedItem;
            if (string.IsNullOrWhiteSpace(input.TEXT) ||
                list_dirs.Items.Contains(new ListViewItem(input.TEXT.TrimEnd('\\'))) ||
                selected == null) return;
           
            if(!list_dirs.Items.Contains(new ListViewItem(input.TEXT.TrimEnd('\\')))) list_dirs.Items.Add(input.TEXT.TrimEnd('\\'));
            Projects.First(x => x.Name == selected.ToString()).AllProjectDirs.Add(new Projdir(input.TEXT.TrimEnd('\\')));
            SaveAllProjects();
        }

        private void удалитьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var selectedDir = list_dirs.SelectedItems;
            if (selectedDir.Count == 0) return;
            Projects.First(x => x.Name == selected.ToString()).RemoveDir(selectedDir[0].Text);
            list_dirs.Items.Remove(selectedDir[0]);
            SaveAllProjects();
        }

        private void удалитьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            Projects.Remove(Projects.First(x => x.Name == selected.ToString()));
            List_Projects.Items.Remove(selected);
           SaveAllProjects();
        }
        //Add Except Dir
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var selectedDir = list_dirs.SelectedItems;
            if (selectedDir.Count == 0) return;
            var selectedExc = listExceptions.SelectedItems;
            Input input = new Input();
            if (selectedExc.Count == 1) input.TEXT = selectedExc[0].ToString();
            var dg = input.ShowDialog();
            if (dg != DialogResult.OK) return;
            if (string.IsNullOrWhiteSpace(input.TEXT) ||
                listExceptions.Items.Contains(input.TEXT)) return;
            var d = Projects.First(x => x.Name == selected.ToString()). //selected dir
                 AllProjectDirs.First(c => c.Dir == selectedDir[0].Text);//DIR
            if (d.ExceptDirs.Contains(input.TEXT.TrimEnd('\\'))) return;
            d.ExceptDirs.Add(input.TEXT.TrimEnd('\\'));
            listExceptions.Items.Clear();
            listExceptions.Items.AddRange(d.ExceptDirs.ToArray());
            SaveAllProjects();
        }

        //remove Except dir
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var selectedDir = list_dirs.SelectedItems;
            if (selectedDir.Count == 0) return;
            var selectedExc = listExceptions.SelectedItems;
            if (selectedExc.Count == 0) return;
            var d = Projects.First(x => x.Name == selected.ToString()).
                AllProjectDirs.First(c => c.Dir == selectedDir[0].Text);
            string selExc = selectedExc[0].ToString();
            if (!d.ExceptDirs.Contains(selExc))
                return;
            d.ExceptDirs.Remove(selExc);
            listExceptions.Items.Clear();
            listExceptions.Items.AddRange(d.ExceptDirs.ToArray());
            SaveAllProjects();
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var P = Projects.First(x => x.Name == selected.ToString());
            tblog.Clear();
            StartSync(P);
        }

        private void синхронизироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var P = Projects.First(x => x.Name == selected.ToString());
            tblog.Clear();
            StartSync(P);
        }
    }
}
