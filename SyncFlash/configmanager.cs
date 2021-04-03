using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SyncFlash
{
    public static class CONSTS
    {
        public const string RootXMLProject = "Projects";
        public const string ProjXML = "Project";
        public const string DirXML = "directory";
        public const string AutoSync = "Autosync";
        public const string AttDirName = "path";
        public const string PC_XML = "NetBios";
        public const string ExceptXML = "ExceptionDir";
        public const string FlashDrive = "FLASHDRIVE";
        public const string btSyncText1 = "StartSync";
        public const string btSyncText2 = "StopSync";
        public static void AddNewLine(DataGridView control, string text)
        {
            if (text == null)
            {

            }
            else
            {
                int last = control.Rows.Count - 1;
                var value = last >= 0 ? control.Rows[last].Cells["data"].Value : null;
                if (last >= 0 && value != null && value.ToString().Contains("Skipped"))
                {//уже есть временная строка
                    control.Invoke(new MethodInvoker(delegate ()
                    {

                    }));
                    if (control.InvokeRequired)
                        control.Invoke(new MethodInvoker(delegate ()
                            { control.Rows[last].Cells["data"].Value = text; }));
                    else control.Rows[last].Cells["data"].Value = text;
                }
                else//создаем новую строку
                {
                    int lastrow = 0;
                    if (control.InvokeRequired)
                        control.Invoke(new MethodInvoker(delegate () { lastrow = control.Rows.Add(); }));
                    else lastrow = control.Rows.Add();
                    if (control.InvokeRequired)
                        control.Invoke(new MethodInvoker(delegate () { control.Rows[lastrow].Cells["data"].Value = text; }));
                    else control.Rows[lastrow].Cells["data"].Value = text;
                }

                int offset = 10;
                if (control.RowCount > offset)
                {
                    if (control.InvokeRequired)
                        control.Invoke(new MethodInvoker(delegate ()
                            { control.FirstDisplayedScrollingRowIndex = control.RowCount - offset; }));
                    else
                        control.FirstDisplayedScrollingRowIndex = control.RowCount - offset;
                }

            }

        }

        public static void AddToLastLine(DataGridView control, string text)
        {
            if (text == null)
            {

            }
            else
            {

                if (control.InvokeRequired)
                    control.Invoke(new MethodInvoker(delegate ()
                        { control.Rows[control.Rows.Count - 1].Cells["data"].Value += text; }));
                else control.Rows[control.Rows.Count - 1].Cells["data"].Value += text;
            }

        }
        public static void AddToTempLine(DataGridView control, string text)
        {
            if (text == null)
            {

            }
            else
            {
                int last = control.Rows.Count - 1;
                var value = last >= 0 ? control.Rows[last].Cells["data"].Value : null;
                if (last >= 0 && value != null && value.ToString().Contains("Skipped"))
                {//already exist
                    if (control.InvokeRequired)
                        control.Invoke(new MethodInvoker(delegate ()
                        {
                            control.Rows[last].Cells["data"].Value = "Skipped:\t" + text;
                        }));
                    else control.Rows[last].Cells["data"].Value = "Skipped:\t" + text;
                }
                else
                {//add new templine
                    AddNewLine(control, "Skipped:\t" + text);
                }


            }

        }

        public static void EnableButton(Button bt)
        {

            if (bt.InvokeRequired) bt.Invoke(new Action<string>(s => bt.Text = (s)), CONSTS.btSyncText1);
            else bt.Text = CONSTS.btSyncText1;
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
        public static void invokeTBAppendText(TextBox tb, string text)
        {
            if (tb.InvokeRequired) tb.Invoke(new Action<string>(s => tb.AppendText(s)), text + "\r\n");
            else tb.AppendText(text + "\r\n");
        }

        public static void invokeTBClearText(TextBox tb)
        {
            if (tb.InvokeRequired) tb.Invoke(new Action<string>(s => tb.Clear()));
            else tb.Clear();
        }
        public static void invokeEnableControl(Control control, bool enabled)
        {
            if (control.InvokeRequired) control.Invoke(new Action<bool>(s => control.Enabled = s), enabled);
            else control.Enabled = enabled;
        }

        /// <summary>
        /// Get NAme of Removable drive on computer
        /// </summary>
        /// <returns>"D:"</returns>
        private static string GetDriveLetter()
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
        private static string _driveletter;
        public static string DriveLetter
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_driveletter)) return GetDriveLetter();
                else return _driveletter;
            }
            set
            {
                _driveletter = value;
            }
        }

    }

    public class configmanager
    {
        string Filepath; //Имя файла.
        private XDocument doc;
        private const string RootXMLProject = CONSTS.RootXMLProject;
        private const string ProjXML = CONSTS.ProjXML;
        private const string DirXML = CONSTS.DirXML;

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
            catch (Exception)
            {
                if (doc == null)
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
                doc = XDocument.Load(Filepath);
                var projs = from pr in doc.Descendants(ProjXML)
                            select new
                            {
                                NAME = pr.Attribute("name")?.Value,
                                AutoSync = pr.Attribute(CONSTS.AutoSync)?.Value,
                                Dirs = pr.Descendants(DirXML),
                                ExcDirs = pr.Descendants(CONSTS.ExceptXML)
                            };
                foreach (var project in projs) //read each project
                {
                    var p = new Project(project.NAME);
                    if (project.AutoSync != null) p.AutoSync = Boolean.Parse(project.AutoSync);
                    else p.AutoSync = false;
                    foreach (var e in project.ExcDirs)
                    {//Adding exceptions Dirs
                        string exDir = e.Attribute(CONSTS.AttDirName).Value.ToString(); //Exception DIR path from XML file
                        p.ExceptionDirs.Add(exDir);
                    }
                    foreach (var d in project.Dirs)
                    {   //read directory for the project
                        var projDir = new Projdir(d.Attribute(CONSTS.AttDirName).Value.TrimEnd('\\'), p, d.Attribute(CONSTS.PC_XML).Value);
                        p.AllProjectDirs.Add(projDir);
                    }
                    Result.Add(p);
                }
                return Result;
            }
            catch (Exception)
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
        private Project ClearDublicates(Project p)
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
                    doc = new XDocument(new XElement(RootXMLProject));
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
