﻿using System;
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
        MyTimer tmr;
       public static LogForm log;
        public Form1()
        { //TODO SYNC SOME PROJECTS 
            InitializeComponent();
           log = new LogForm();
            tmr = new MyTimer(log);
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
           // StartAutoSync();
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
                tmr.Start("Aborting process");
                SyncThread.Abort();
                CONSTS.EnableButton(button1);
                CONSTS.AddNewLine(tblog, "Прервано пользователем");
                tmr.Stop();
                return;
            }
            log.ClearLog();
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

                        var OnlineDirs = project.OnlineDirs;//.AllProjectDirs.Where(x => x.IsOnline); 
                        if (OnlineDirs.Count() == 0) { SetSyncStatus(false); return;}
                        if (OnlineDirs.Count() == 1)
                        {
                           
                            foreach (var t in OnlineDirs.First().Info2())
                            {
                                tmr.Start("Вывод инфо директории");
                                CONSTS.AddNewLine(tblog, t);
                                tmr.Stop();
                            }
                            SetSyncStatus(false);
                            return;
                        }

                        
                        if (OnlineDirs.Count() > 1) //run SYNC
                        {
                           
                            //DateTime t = DateTime.Now;
                            int filesCount = 0; //count of files in dir
                            int cTotal = 0;
                            Dictionary<string, List<string>> Queue = new Dictionary<string, List<string>>();//очередь файлов источника и назначений
                            //перебор всех Онлайн директорий
                            List<string> skippedFiles = new List<string>(); //relativ path!!!
                            foreach (var Dir in OnlineDirs) //перебор все папок проекта
                            {
                                //каждый файл ищется в других папках проекта и добавляется в очередь
                                foreach (var dateFile in Dir.AllFiles()) 
                                {   
                                   //получаем относительный путь файла
                                    string relatePath = GetRelationPath(dateFile.Key, Dir.Dir);
                                    //проверялся ли такой файл раньше?
                                    if (skippedFiles.Contains(relatePath)) continue; // ДА - пропускаем
                                    //проверяем, нет ли такого файла уже в очереди 
                                    //поиск файла в очереди
                                    if (Queue.Keys.Any(x=>x.Contains(relatePath))) continue; //dataFile.Key - Full filePath

                                    var newest = dateFile; //самый свежий файл

                                    //создаем список файлов, которые нужно заменить файлом dateFile, файл назначения
                                    Dictionary<string, DateTime> otherFiles = new Dictionary<string, DateTime>();
                                    foreach (var otherDir in OnlineDirs) //Поиск текущего файла dateFile в остальных директориях
                                    {                                        
                                        if (otherDir == Dir) continue; //пропуск текущей директории
                                      var file = otherDir.FindFile(relatePath);//поиск такого же файла
                                        //если такого файла в Директории нет
                                        if (String.IsNullOrEmpty(file.Key)) //создадим путь для копирования в эту директорию
                                        {
                                            //пара значений (Путь файла, Время последнего изменения)
                                            file = new KeyValuePair<string, DateTime>
                                            (newest.Key.Replace(Dir.Dir, otherDir.Dir), newest.Value);
                                            otherFiles.Add(file.Key, file.Value);//добавление в список назначения
                                                                                 //нужно проверить существование папки назначения и создать ее, если нужно
                                            #region выделение директории из пути файла
                                            var splits = file.Key.Split('\\');
                                            string newDir = ""; //new Directory ??
                                            for (int i = 0; i < splits.Length - 1; i++)
                                            {
                                                if (i == splits.Length - 2) newDir += splits[i];
                                                else newDir += splits[i] + "\\";
                                            }
                                            if (!Directory.Exists(newDir)) Directory.CreateDirectory(newDir);
                                            #endregion
                                            continue;
                                        }
                                        if (newest.Value > file.Value)
                                            otherFiles.Add(file.Key, file.Value);//добавление файла в список файлов
                                        else if (newest.Value == file.Value)
                                        {
                                            continue; //SKIP  SAME FILES, DONT COPY!!!
                                        }else
                                        //меняем переменную новейшего файла с текущим
                                        {
                                            if (!otherFiles.ContainsKey(newest.Key))
                                                otherFiles.Add(newest.Key, newest.Value);
                                            newest = file;
                                        }
                                        }
                                    // tmr.Stop(1);
                                    //нашли самый новый файл dateFile среди остальных директорий
                                    //теперь добавляем в очередь
                                    if (otherFiles.Count() != 0) 
                                        Queue.Add(newest.Key, otherFiles.Keys.ToList());
                                    else skippedFiles.Add(GetRelationPath(newest.Key,Dir.Dir)); //если все файлы одинаковые как newest - пропускаем
                                }
                            }//проверили все файлы и добавили их в очередь
                            //проверяем есть ли что копировать
                            if (Queue.Count==0)
                            {
                               if(!silentMode) MessageBox.Show("Все папки одинаковые, нечего синхронизировать");
                                CONSTS.AddNewLine(tblog,project.Name+": Нечего синхронизировать.");
                                SetSyncStatus(false);
                                return;
                            }


                            if (!silentMode)// not silent
                            {
                                //TODO размер окна бывает слишком больщой
                                tmr.Start("Подготовка msg");
                                string msg = "Будут скопированы следующие файлы:\r\n";
                                foreach (var file in Queue)
                                {
                                    msg += file.Key + "\r\n";
                                }
                                tmr.Stop();
                                var msgBox = new MsgDialog(msg);
                               var dr= msgBox.ShowDialog();
                            if (dr != DialogResult.OK) {SetSyncStatus(false);return;}
                            }
                           
                            CONSTS.AddNewLine(tblog, "--------------------------------------------");
                            cTotal = Queue.Count(); //count of SrcFiles
                            filesCount = 0; //total copied DestFiles 
                            int nUpd = 0;//count of updated files
                            int nNew = 0; //count of new files
                            foreach (var File in Queue) //перебор файлов, которые надо копировать                            {
                            { string SrcFile = File.Key;
                               // if (!Directory.Exists(SrcFile)) Directory.CreateDirectory(SrcFile);
                                var AllDestFiles = File.Value; //список куда копировать
                                int cAllSource = AllDestFiles.Count;
                                //cTotal += AllDestFiles.Count;
                                //================COPY========    
                                int ID = 1000;
                                foreach (var DstFile in AllDestFiles) //процесс копирования
                                {

                                    tmr.Start(DstFile, ID);
                                    filesCount++;
                                    if(System.IO.File.Exists(DstFile))//File exist
                                    { 
                                    try
                                    {//TODO time of copy files
                                        //update file
                                        var size = new FileInfo(SrcFile).Length;
                                            CONSTS.AddNewLine(tblog, filesCount.ToString() + "). " + "Обновленный :> " + SrcFile+
                                                "\t(" + ((double)size / 1000) + "kbit)");
                                            CONSTS.AddToTempLine(tblog, "   Запуск  :> " + SrcFile+" ==>"+ DstFile);
                                        System.IO.File.Copy(SrcFile, DstFile, true);
                                            CONSTS.AddNewLine(tblog,    "   Updated :> " +  DstFile);
                                            nUpd++;
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        errorCopy++;
                                        CONSTS.AddNewLine(tblog,
                                            "err:>\t" + ex.Message);
                                    }
                                    

                                }
                                    else //copy new file
                                    {
                                     string filedir = SrcFile.Remove(SrcFile.Length - SrcFile.Split('\\').Last().Length);
                                        if (!Directory.Exists(filedir)) Directory.CreateDirectory(filedir);
                                        try
                                        {
                                            
                                            var size = new FileInfo(SrcFile).Length;
                                            CONSTS.AddNewLine(tblog, filesCount.ToString() + "). " + "Новый :> " + SrcFile +
                                                "\t(" + ((double)size / 1000) + "kbit)");
                                            CONSTS.AddToTempLine(tblog, "   Запуск  :> " + SrcFile + " ==>" + DstFile);
                                            System.IO.File.Copy(SrcFile, DstFile, true);
                                            CONSTS.AddNewLine(tblog,    "   Copied  :> " +  DstFile);
                                            nNew++;
                                        }
                                        catch (Exception ex)
                                        {
                                            errorCopy++;
                                            CONSTS.AddNewLine(tblog,
                                                "err:>\t" + DstFile + "\t" + ex.Message);
                                        }

                                   }//end IF Else
                                    CONSTS.invokeProgress(progressBar1, (int)(filesCount * 100 / cTotal));
                                    tmr.Stop(ID); ID++;
                                } //конец перебора файлов назначения

                            }//конец перебора исходных файлов

                            CONSTS.AddNewLine(tblog, "--------------------------------");
                            CONSTS.AddNewLine(tblog, project.Name + " синхронизирован.");
                            CONSTS.AddNewLine(tblog, "Новых файлов: \t\t" + nNew.ToString());
                            CONSTS.AddNewLine(tblog, "Обновлено файлов:  \t" + nUpd.ToString());
                            CONSTS.AddNewLine(tblog, "Всего исходных файлов:\t" + cTotal.ToString());
                            CONSTS.AddNewLine(tblog, "Всего скопировано: \t" + cTotal.ToString());
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
            var selected = List_Projects.SelectedItem;//selected project in List
            if (selected == null) return;
            var selectedDir = list_dirs.SelectedItems; //Selected directory into project
            if (selectedDir.Count == 0)
            {
                CONSTS.AddNewLine(tblog, "Для добавления исключений" +
                    " нужно выбрать одну из папок проекта, в котором будете указывать исключения");
                return;
            }
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
            tmr.Start("Подготовка синхронизации");
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var P = Projects.First(x => x.Name == selected.ToString());
            tblog.Rows.Clear();
            tmr.Stop();
            StartSync(P,cbSilent.Checked);
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

        private void btLog_Click(object sender, EventArgs e)
        {
            if (log.IsDisposed) log = new LogForm();
            if (log.Visible) log.Visible = false;
            else log.Show();
        }

        //add text info to LogForm
        public void Addlog(string text)
        {
            log.AddLine(text);
        }
        public void ClearLog()
        {
            log.ClearLog();
        }
    }
}
