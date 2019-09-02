using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Windows.Forms;

namespace SyncFlash
{ public static class CONSTS
    {
        public const string RootXMLProject = "Projects";
        public const string ProjXML = "Project";
        public const string DirXML = "directory";
        public const string AttDirName = "path";
        public const string PC_XML = "NetBios";
        public const string ExceptXML = "ExceptionDir";
        public const string FlashDrive = "FLASHDRIVE";
        public  const string btSyncText1="StartSync";
        public const string btSyncText2 = "StopSync";
        public static void invokeControlTextNewLine(TextBox control, string text)
        {
            if (text == null)
            {

            }
            else
            {
                if (control.InvokeRequired) control.Invoke(new Action<string>(s => control.AppendText(s)), text+Environment.NewLine);
                else control.AppendText(text + Environment.NewLine); 
            }

        }
        public static void invokeAddTempLine(TextBox control, string text)
        {
            if (text == null)
            {

            }
            else
            {
                var size = control.Lines.Length;
                var lastLine = control.Lines[size-1];
                if (lastLine.Contains(("Skipped")))//Already exist
                {
                    if (control.InvokeRequired) control.Invoke(new Action<string>(s => control.Lines[size - 1] = s), "Skipped:\t" + text);
                    else lastLine = "Skipped:\t" + text;
                }else {//first temp line
                    if (control.InvokeRequired) control.Invoke(new Action<string>(s => control.AppendText(s)), "Skipped:\t" + text);
                    else control.AppendText("Skipped:\t" + text);
                }
                    
            }

        }
        private static void invokeDeleteTempLine(TextBox control)
        {
            var size = control.Lines.Length;
            var lastLine = control.Lines[size - 1];
            if (lastLine.Contains(("Skipped")))
            {
                if (control.InvokeRequired) control.Invoke(new Action<string>(s => control.Lines[size-1]=s),"");
                else control.Lines[size - 1] ="";
            }

        }
        public static void invokeControlText(TextBox control, string text)
        {
            if (text == null)
            {

            }
            else
            {
                invokeDeleteTempLine(control);
                if (control.InvokeRequired) control.Invoke(new Action<string>(s => control.AppendText(s)), text );
                else control.AppendText(text);
            }

        }
        public static void EnableButton(Button bt)
        {
            
                if (bt.InvokeRequired) bt.Invoke(new Action<string>(s => bt.Text=(s)), CONSTS.btSyncText1);
                else bt.Text=CONSTS.btSyncText1;
           // invokeEnableControl(bt,true);
            

        }
        public static void DisableButton(Button bt)
        {

            if (bt.InvokeRequired) bt.Invoke(new Action<string>(s => bt.Text = (s)), CONSTS.btSyncText2);
            else bt.Text = CONSTS.btSyncText2;
           // invokeEnableControl(bt, false);


        }
        public static void invokeProgress(ProgressBar bar, int value)
        {
            if (value > 100) return;
            if (bar.InvokeRequired) bar.Invoke(new Action<int>(s => bar.Value = s), value);
            else bar.Value = value;
        }

        public static void invokeEnableControl(Control control, bool enabled)
        {
            if (control.InvokeRequired) control.Invoke(new Action<bool>(s => control.Enabled=s), enabled);
            else control.Enabled=enabled;
        }

        /// <summary>
        /// Get NAme of Removable drive on computer
        /// </summary>
        /// <returns>"D:"</returns>
        public static string GetDriveLetter()
        {
            string drive = "";
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true && d.DriveType == DriveType.Removable && d.TotalSize > 1600000)
                {
                    drive = d.Name.TrimEnd('\\');
                }

            }
            return drive;
        }
    }
   
    class configmanager
    {
        string Filepath; //Имя файла.
        private XDocument doc;
        private const string RootXMLProject = CONSTS.RootXMLProject;
        private const string ProjXML = CONSTS.ProjXML;
        private const string DirXML = CONSTS.DirXML;
        
        #region inifile
        //[DllImport("kernel32", CharSet = CharSet.Unicode)] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
        //static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        //[DllImport("kernel32", CharSet = CharSet.Unicode)] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        //static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        //[DllImport("kernel32", CharSet = CharSet.Unicode)] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        //static extern int GetPrivateProfileString(string Section, string Key, string Default, IntPtr RetVal, int Size, string FilePath);

        //public string[] GetAllKeys(string Section)
        //{
        //    IntPtr RetVal = Marshal.AllocHGlobal(4096 * sizeof(char));
        //    // GetPrivateProfileString(Section, null, "", RetVal, 255, Path);
        //    string t = "";
        //    List<string> result = new List<string>();
        //    int n = GetPrivateProfileString(Section, null, null, RetVal, 4096 * sizeof(char), Path) - 1;
        //    if (n > 0)
        //        t = Marshal.PtrToStringUni(RetVal, n);

        //    Marshal.FreeHGlobal(RetVal);

        //    return t.Split('\0');

        //}

        //// С помощью конструктора записываем путь до файла и его имя.
        //public configmanager(string IniPath)
        //{

        //    Path = new FileInfo(IniPath).FullName.ToString();
        //}

        ////Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
        //public string ReadINI(string Section, string Key)
        //{
        //    var RetVal = new StringBuilder(255);
        //    GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);

        //    return RetVal.ToString();
        //}
        ////Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
        //public void Write(string Section, string Key, string Value)
        //{

        //    WritePrivateProfileString(Section, Key, Value, Path);
        //}

        ////Удаляем ключ из выбранной секции.
        //public void DeleteKey(string Key, string Section = null)
        //{
        //    Write(Section, Key, null);
        //}
        ////Удаляем выбранную секцию
        //public void DeleteSection(string Section = null)
        //{
        //    Write(Section, null, null);
        //}
        ////Проверяем, есть ли такой ключ, в этой секции
        //public bool KeyExists(string Key, string Section = null)
        //{
        //    return ReadINI(Section, Key).Length > 0;
        //}
        #endregion
        public configmanager(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    doc = XDocument.Load(file);
                }
                else
                {
                    Filepath = file;
                    doc = new XDocument(new XElement(RootXMLProject));
                    doc.Save(file);
                }
               
            }
            catch(Exception)
            {
                if(doc==null)
                {
                    doc = new XDocument(new XElement(RootXMLProject));
                    doc.Save(file);
                }
            }
            finally { Filepath = file; }
           
        }
        public List<Project> ReadAllProjects()
        {
            List<Project> Result = new List<Project>();
            try
            {
                var projs = from pr in doc.Descendants(ProjXML)
                            select new
                            {
                                NAME = pr.Attribute("name").Value,
                                Dirs = pr.Descendants(DirXML),
                                ExcDirs = pr.Descendants(CONSTS.ExceptXML)
                            };
                foreach (var project in projs) //read each project
                {
                    var p = new Project(project.NAME);
                    foreach (var e in project.ExcDirs)
                    {//Adding exceptions Dirs
                        string exDir = e.Attribute(CONSTS.AttDirName).Value.ToString(); //Exception DIR path from XML file
                            p.ExceptionDirs.Add(exDir);
                    }
                    foreach (var d in project.Dirs)
                    {   //read directory for the project
                        var projDir = new Projdir(d.Attribute(CONSTS.AttDirName).Value.TrimEnd('\\'),p, d.Attribute(CONSTS.PC_XML).Value);
                        p.AllProjectDirs.Add(projDir);
                     }
                    Result.Add(p);
                }
                return Result;
            }
            catch(Exception)
            { return Result; }
        }
        public void SaveProjects(IEnumerable<Project> projects)
        {
            doc.Element(RootXMLProject).Elements().Remove();
            foreach (var p in projects)
            {
                doc.Element(RootXMLProject).Add(p.ToXElement());
            }
            foreach (var p in projects)
            {
             SaveProject(p);
            }
        }
        private Project ClearDublicates(Project   p)
        {
            for (int i = 0; i < p.AllProjectDirs.Count; i++)
            {
                var idir = p.AllProjectDirs[i];
                if (p.AllProjectDirs.Count(x => x.Dir == idir.Dir) > 1)
                    p.AllProjectDirs.Remove(idir);
               
            }
            return p;
        }
        public void SaveProject(Project project)
        {
            if (!File.Exists(Filepath))
            {
                File.Create(Filepath);
                if (doc.Elements(RootXMLProject).Count() == 0)
                    doc=new XDocument(new XElement(RootXMLProject));
            }
            ClearDublicates(project);
            var xp = project.ToXElement();
            var projects = doc.Element(RootXMLProject).Elements();
            if (projects.Any(x => x.Attribute("name").Value == project.Name))
                projects.First(x => x.Attribute("name").Value == project.Name).Remove();
            doc.Element(RootXMLProject).Add(xp);
            doc.Save(Filepath, SaveOptions.OmitDuplicateNamespaces);
        }
    }

}
