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
        public string DriveLette;
        const string cfg_file = "conf.ini";
        string pc_name = Environment.MachineName;
        List<Project> Projects;
        private bool IsRunningSync=false; // if sync is running
        Thread SyncThread; 
        public Form1()
        { //TODO SYNC SOME PROJECTS 
            InitializeComponent();
            cfg = new configmanager(cfg_file);
            Projects = cfg.ReadAllProjects() ?? new List<Project>();
            foreach (var p in Projects)
            {
                List_Projects.Items.Add(p.ToString());
            }
            DriveLette = CONSTS.GetDriveLetter();
            button1.Text = CONSTS.btSyncText1;
            this.FormClosing += Form1_FormClosing;
            List_Projects.SelectedIndexChanged += List_Projects_SelectedIndexChanged;
            list_dirs.SelectedIndexChanged += List_dirs_SelectedIndexChanged;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            CONSTS.AddNewLine(tblog,"USB-Flash: "+DriveLette);
            
        }

        #region Events Handlers

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
        private void Form1_Load(object sender, EventArgs e)
        {
            StartAutoSync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var P = Projects.First(x => x.Name == selected.ToString());
            tblog.Rows.Clear();
            StartSync(P);
        }
        private void btSelectUSB_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Выберите любую папку на USB/Select any folder on USB.";
            var dr = fd.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (!Directory.Exists(fd.SelectedPath)) return;
            string newLette = fd.SelectedPath.Split('\\').First();
            DriveLette = newLette;
            CONSTS.AddNewLine(tblog, "New USB letter: " + DriveLette);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (button1.Text == CONSTS.btSyncText2)
            {
                SyncThread.Abort();
                CONSTS.EnableButton(button1);
                CONSTS.AddNewLine(tblog, "Прервано пользователем");
                return;
            }
            SyncSelectedProjects();
        }



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (List_Projects.SelectedItems.Count != 1) return;
            var selectedProj = Projects.FirstOrDefault(x => x.Name == List_Projects.SelectedItem.ToString());
            if (selectedProj != null) selectedProj.AutoSync = checkBox2.Checked;
            SaveAllProjects();
        }
        #endregion



        /// <summary>
        /// Sync All online directories of project
        /// </summary>
        /// <param name="project"></param>
        /// /// <param name="silentMode">true - without dialog boxes</param>
        private void StartSync(Project project, bool silentMode=false)
        {
           // if (List_Projects.SelectedItem == null) return;
            
            CONSTS.DisableButton(button1);
           // var selectedProj = Projects.First(x => x.Name == List_Projects.SelectedItem.ToString());
            SyncThread = new Thread(delegate()
                {
                    try
                    {
                        SetSyncStatus(true);
                        int newfiles = 0;
                        int updatedfiles = 0;
                        int errorCopy = 0;
                        var OnlineDirs = project.AllProjectDirs.Where(x => x.IsOnline);
                        if (OnlineDirs.Count() == 0) {SetSyncStatus(false); return;}
                        if (OnlineDirs.Count() == 1)
                        {
                            foreach (var t in OnlineDirs.First().Info2())
                            {
                                CONSTS.AddNewLine(tblog, t);
                            }
                            SetSyncStatus(false);
                            return;
                        }

                        if (OnlineDirs.Count() > 1)
                        {
                            //DateTime t = DateTime.Now;
                            int count = 0; //count of files in dir
                            int cTotal = 0;
                            Projdir sourceDir;
                            //ищем папки с максимальной датой изменения файлов
                            var Equaldirs = OnlineDirs.Where(x => x.LastMod == OnlineDirs.Max(d => d.LastMod));
                            if (Equaldirs.Count() > 1)
                            {
                                //если по датам папки одинаковые, то выбираем ту, у которой файлов больше
                                sourceDir = Equaldirs.First(x =>
                                    x.AllFiles().Count == Equaldirs.Max(c => c.AllFiles().Count));
                            }
                            else //только одна папка с максимальной датой
                                sourceDir = Equaldirs.First(); // откуда будем копировать

                            var DestDirs =
                                OnlineDirs.Where(x => x.Dir != sourceDir.Dir); //в эти папки копирует новые данные
                            if (DestDirs.All(x =>
                                x.LastMod == sourceDir.LastMod && x.AllFiles().Count == sourceDir.AllFiles().Count))
                            {
                                //MessageBox.Show("Все папки одинаковые, нечего синхронизировать");
                                CONSTS.AddNewLine(tblog,project.Name+": Нечего синхронизировать.");
                                SetSyncStatus(false);
                                return;
                            }

                            string DirsToString = "";
                            CONSTS.AddNewLine(tblog,
                                "From \t" + sourceDir.LastMod.ToString("dd.MM.yyyy hh:mm:ss") + "<=>" + sourceDir.Dir +
                                " \t\t{" + sourceDir.PC_Name + "}");
                            foreach (var d in DestDirs)
                            {
                                DirsToString += d.Dir + "; ";
                                CONSTS.AddNewLine(tblog,
                                    "To \t" + d.LastMod.ToString("dd.MM.yyyy hh:mm:ss") + "<=>" + d.Dir + " \t\t{" +
                                    d.PC_Name + "}");
                            }

                            if (!silentMode)// not silent
                            {
                                var dr = MessageBox.Show(
                                "Будут скопированы обновленные и новые файлы из " + sourceDir.Dir + "\r\n в \r\n" +
                                DirsToString,
                                project.Name, MessageBoxButtons.YesNo);
                            if (dr == DialogResult.No) {SetSyncStatus(false);return;}
                            }
                           
                            CONSTS.AddNewLine(tblog, "--------------------------------------------");
                            foreach (var destdir in DestDirs) //перебор папок назначения
                            {
                                if (!Directory.Exists(destdir.Dir)) Directory.CreateDirectory(destdir.Dir);
                                Dictionary<string, DateTime> AllSourceFiles = sourceDir.AllFiles();
                                Dictionary<string, DateTime> AllDestFiles = destdir.AllFiles();
                                int cAllSource = AllSourceFiles.Count;
                                cTotal += AllSourceFiles.Count;
                                count = 0;
                                int upd = 0;
                                int nfile = 0;
                                foreach (var file in AllSourceFiles) //процесс копирования
                                {
                                    count++;
                                    var relfile = GetRelationPath(file.Key, sourceDir.Dir);
                                    if (AllDestFiles.Any(x => GetRelationPath(x.Key, destdir.Dir) == relfile)
                                    ) //file exist
                                    {
                                        var dfile = AllDestFiles.First(x =>
                                            GetRelationPath(x.Key, destdir.Dir) == relfile);
                                        if (dfile.Value < file.Value) //файл назначения старше исходника
                                        {
                                            try
                                            {
                                                //update file
                                                upd++;
                                                var size = new FileInfo(file.Key).Length;
                                                CONSTS.AddNewLine(tblog,
                                                    "upd:> " + upd.ToString() + "). " + relfile);
                                                File.Copy(file.Key, dfile.Key, true);
                                                CONSTS.AddToLastLine(tblog,
                                                    "\t(" + ((double) size / 1000) + "kbit)");
                                            }
                                            catch (Exception ex)
                                            {
                                                errorCopy++;
                                                CONSTS.AddNewLine(tblog,
                                                    "err:>\t" + relfile + "\t" + ex.Message);
                                            }

                                            updatedfiles++;
                                        }
                                        else
                                        {
                                           CONSTS.AddToTempLine(tblog, relfile);
                                        }

                                        CONSTS.invokeProgress(progressBar1, (int) (count * 100 / cAllSource));

                                    }
                                    else //copy new file
                                    {
                                        string newfile = destdir.Dir + relfile;
                                        string filedir =
                                            newfile.Remove(newfile.Length - newfile.Split('\\').Last().Length);
                                        if (!Directory.Exists(filedir)) Directory.CreateDirectory(filedir);
                                        try
                                        {
                                            nfile++;
                                            var size = new FileInfo(file.Key).Length;
                                            CONSTS.AddNewLine(tblog,
                                                "new:>" + nfile.ToString() + "). " + relfile);
                                            File.Copy(file.Key, newfile, true);
                                            CONSTS.AddToLastLine(tblog,
                                                "\t(" + ((double) size / 1000) + "kbit)");
                                        }
                                        catch (Exception ex)
                                        {
                                            errorCopy++;
                                            CONSTS.AddNewLine(tblog,
                                                "err:>\t" + relfile + "\t" + ex.Message);
                                        }

                                        CONSTS.invokeProgress(progressBar1, (int) (count * 100 / cAllSource));
                                        newfiles++;
                                    }
                                } //конец перебора исходных файлов

                            }

                            CONSTS.AddNewLine(tblog, "--------------------------------");
                            CONSTS.AddNewLine(tblog, project.Name + " синхронизирован.");
                            CONSTS.AddNewLine(tblog, "Новых файлов: \t" + newfiles.ToString());
                            CONSTS.AddNewLine(tblog, "Обновлено файлов:\t" + updatedfiles.ToString());
                            CONSTS.AddNewLine(tblog, "Всего файлов:\t" + cTotal.ToString());
                            CONSTS.AddNewLine(tblog, "Ошибок копирования:\t" + errorCopy.ToString());
                            CONSTS.EnableButton(button1);
                           
                        }


                    }
                    finally     
                    {
                        CONSTS.EnableButton(button1);
                        SetSyncStatus(false);
                    }
                }


            );
            SyncThread.Start();
        }
        /// <summary>
        /// Выделяет одинаковую часть пусти для файлов одного проекта
        /// </summary>
        /// <param name="fullpath"></param>
        /// <param name="projDir"></param>
        /// <returns></returns>
        public static string GetRelationPath(string fullpath,string projDir)
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
            checkBox2.Checked = selectedProj.AutoSync;
            foreach (var item in selectedProj.AllProjectDirs)
            {
                if (!checkBox1.Checked || item.IsOnline && (item.PC_Name == pc_name|| item.PC_Name==CONSTS.FlashDrive))
                {
                    list_dirs.Items.Add(item.Dir);
                }

            }
            listExceptions.Items.AddRange(selectedProj.ExceptionDirs.ToArray());
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

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (List_Projects.SelectedItems.Count != 1) return;
            var selectedProj = List_Projects.SelectedItem.ToString();
            var inputDialog = new Input();
            inputDialog.button1.Visible = false;
            inputDialog.TEXT = selectedProj;
            var dr = inputDialog.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (List_Projects.Items.Contains(inputDialog.TEXT) && inputDialog.TEXT != selectedProj)
            {
                MessageBox.Show("Такое имя уже есть в списке.");
                return;
            }

            Projects.First(x => x.Name == selectedProj).Name = inputDialog.TEXT;
            List_Projects.Items[List_Projects.SelectedIndex] = inputDialog.TEXT;
            SaveAllProjects();

        }
        private void синхронизироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncSelectedProjects();
        }
        private void добавитьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input input = new Input();
            input.ShowDialog();
            var selected = List_Projects.SelectedItem;
            string inputDir = input.TEXT.TrimEnd('\\');
            if (string.IsNullOrWhiteSpace(input.TEXT) ||
                list_dirs.Items.Contains(new ListViewItem(inputDir)) ||
                selected == null) return;
           
            if(!list_dirs.Items.Contains(new ListViewItem(inputDir))) list_dirs.Items.Add(inputDir);
           var p= Projects.First(x => x.Name == selected.ToString());
            var lette = input.TEXT.Split('\\')[0];
            if (lette == DriveLette)
            {//removable disk
                string dir = input.TEXT.TrimEnd('\\').Substring(lette.Length);
                p.AllProjectDirs.Add(new Projdir(dir, p, CONSTS.FlashDrive));
            }
            else //HDD disk
            {
                p.AllProjectDirs.Add(new Projdir(inputDir, p));
            }
            
            SaveAllProjects();
        }

        private void удалитьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var selectedDir = list_dirs.SelectedItems;
            if (selectedDir.Count == 0) return;
            string DirRemove = selectedDir[0].Text;
            if (DriveLette == DirRemove.Split('\\')[0])//FlashDrive
            {
                DirRemove = GetRelationPath(DirRemove, DriveLette);
            }
                Projects.First(x => x.Name == selected.ToString()).RemoveDir(DirRemove);
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
            var pr = Projects.First(x => x.Name == selected.ToString()); //selected proj

            string relpath = input.TEXT.TrimEnd('\\').Contains(":\\") ?
                GetRelationPath(input.TEXT.TrimEnd('\\'), selectedDir[0].Text) : input.TEXT.TrimEnd('\\');
            if (pr.ExceptionDirs.Contains(relpath)) return;
            pr.ExceptionDirs.Add(relpath);//добавление относительного пути
            listExceptions.Items.Clear(); 
            listExceptions.Items.AddRange(pr.ExceptionDirs.ToArray());
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
            var pr = Projects.First(x => x.Name == selected.ToString());
            string selExc = selectedExc[0].ToString();
            if (!pr.ExceptionDirs.Contains(selExc))
                return;
            pr.ExceptionDirs.Remove(selExc);
            listExceptions.Items.Clear();
            listExceptions.Items.AddRange(pr.ExceptionDirs.ToArray());
            SaveAllProjects();
        }
        #endregion


        public void SetSyncStatus(bool status)
        {
            IsRunningSync = status;
        }
        /// <summary>
        /// Запускает процесс синхронизации выделенных проектов
        /// </summary>
        private void SyncSelectedProjects()
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var P = Projects.First(x => x.Name == selected.ToString());
            tblog.Rows.Clear();
            StartSync(P);
        }
        //TODO Menu/edit dirs

        /// <summary>
        /// Run Sync projects with attribute AutoSync=True
        /// </summary>
        public void StartAutoSync()
        {
            Thread autoSyncThread=new Thread(delegate()
            {
var projAuto = Projects.Where(x => x.AutoSync);//All autosync projects
                if (projAuto.Count() == 0)
                {
                    CONSTS.AddNewLine(tblog,"Nothing for autosync");
                    return;
                }
                CONSTS.AddNewLine(tblog,"For AutoSync "+projAuto.Count()+" projects");
            foreach (var project in projAuto)
            {
                if(SyncThread!=null)
                while (IsRunningSync || SyncThread.IsAlive)
                {
                    Thread.Sleep(1000);
                }
                StartSync(project,true);

            }
            });
            autoSyncThread.Start();
            
        }
    }
}
