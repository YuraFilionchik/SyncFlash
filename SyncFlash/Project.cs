using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using filesdates = System.Collections.Generic.Dictionary<string, System.DateTime>;
using System.Xml.Linq;

namespace SyncFlash
{
    class Project
    {
        public List<Projdir> AllProjectDirs;
        public List<string> ExceptionDirs;
        public List<Projdir> OnlineDirs
        {
            get
            {
                return AllProjectDirs.Where(x => x.IsOnline).ToList();
            }
        }
        public List<string> Alldirs
        {
            get
            {
                var list = new List<string>();
                foreach (var pd in AllProjectDirs)
                {
                    list.Add(pd.Dir);
                }
                return list;
            }
        }
        
       
        
        public string Name;
        public override string ToString()
        {
            return Name;
        }
        public Project(string name)
        {
            this.Name = name;
            AllProjectDirs = new List<Projdir>();
            ExceptionDirs = new List<string>();
        }

        public void RemoveDir(string dir)
        {
            if (AllProjectDirs.Any(x => x.Dir == dir)) AllProjectDirs.Remove(AllProjectDirs.First(c=>c.Dir==dir));
        }
        public void StartSync()
        {

        }
        /// <summary>
        /// <Project name = Name>
        ///     <directory> dirpath</directory>
        ///     ...
        ///     <directory> dirpath</directory>
        /// </Project>
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {//TODO modify Exceptdir to XML
            var xel = new XElement(CONSTS.ProjXML);
            xel.SetAttributeValue("name", Name);
            
            foreach (var dir in AllProjectDirs)
            {
                XElement xdir = new XElement(CONSTS.DirXML);
                xdir.SetAttributeValue(CONSTS.PC_XML, dir.PC_Name);//имя компа в атрибутах
                xdir.SetAttributeValue(CONSTS.AttDirName, dir.Dir);//путь к папке в атрибутах
                xel.Add(xdir);
            }
            //папки исключения
            foreach (var exc in ExceptionDirs)
            {
                XElement xdir = new XElement(CONSTS.ExceptXML);
                xdir.SetAttributeValue(CONSTS.AttDirName, exc);//Папка исключения в атрибутах
                xel.Add(xdir);
            }

            return xel;
        }
    }
    /// <summary>
    /// Класс представляющий одну папку для данного проекта
    /// </summary>
    class Projdir
    {
        private string _dir;
        private Project FromProject;
        private string pc_name;
        /// <summary>
        /// Имя компъютера NetBios, на которой находится папка
        /// </summary>
        public string PC_Name
        {
            get
            {
                return pc_name;
            }
            set
            {
                pc_name = value;
            }
        }
        public string Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }
        public bool IsOnline
        {
            get { return Directory.Exists(Dir); }
        }

        private DateTime DefaultDate= new DateTime(2000, 1, 1);
        public Projdir(string dir,Project project)
        {
            Dir = dir;
            pc_name= System.Environment.MachineName;
            FromProject = project;
        }
        public Projdir(string dir,Project project, string pc)
        {
            Dir = dir;
            pc_name = pc;
            FromProject = project;
        }
        /// <summary>
        /// Dictionary<files,datetime last write> всех файлов в Projdir
        /// </summary>
        public  filesdates AllFiles()
        {
            
            
                if (!IsOnline) return new filesdates();
                //List<string> files1 = new List<string>();//all files in Dir
                //files1.AddRange(Directory.GetFiles(Dir));//all files in Dir
                //string[] subdirs = Directory.GetDirectories(Dir);//all dirs in Dir
                //if (subdirs.Length != 0)//find all files in subdirs
                //    foreach (var subdir in subdirs)
                //    {
                //        var files2 = Directory.GetFiles(subdir);
                //        if (files2.Length != 0) files1.AddRange(files2);
                //    }
                filesdates res = new filesdates();
                var AllFiles = GetfilesIndir(Dir);
                foreach (var file in AllFiles)
                {
                    res.Add(file, File.GetLastWriteTime(file));
                }
                return res;
            
        }
        private string[] GetfilesIndir(string dir)
        {
            string relativeDir = dir.Contains(":\\") ? Form1.GetRelationPath(dir, this.Dir) : dir;
            var res = new string[0];
            if (!Directory.Exists(dir) || FromProject.ExceptionDirs.Contains(relativeDir))//filter by ExceptionDirs
                return res;
            if (Directory.GetDirectories(dir).Count() == 0) return Directory.GetFiles(dir);

            else
            {
                res = res.Concat(Directory.GetFiles(dir)).ToArray();
                foreach (var D in Directory.GetDirectories(dir))
                {
                    res = res.Concat(GetfilesIndir(D)).ToArray();
                }
            }
            return res;
        }
        /// <summary>
        /// Показывает время подификации самого нового файла в папке Dir и одной подпапке внутрь
        /// </summary>
        public DateTime LastMod
        {
            get
            {

                DateTime lastmod = DefaultDate; ;
                if (!IsOnline) return lastmod;
                var files1 = AllFiles();
                if (files1.Count == 0) return lastmod;//no one file in Dir and subdirs
                return files1.Max(x => x.Value);
            }
        }

        /// <summary>
        /// Сравнение с другой папкой проекта пофайлово
        /// </summary>
        /// <param name="Dir2"></param>
        /// <returns>[0]-кол-во файлов новее чем в Dir2, [1]-старше чем в Dir2,
        /// [2]-новых файлов, [3]-отсутсвующих файлов по сравнению с Dir2</returns>
        public int[] CompairDir(Projdir Dir2)
        {
            int[] res = new int[2];
            var files2 = Dir2.AllFiles();
            //key - filepath
            //value - datetime
            var all = AllFiles();
            foreach (var filedate in all)
            {
                var n0 = all.Count(x=>files2.Any(c=>c.Key==x.Key && c.Value<x.Value));
                var n1 = all.Count(x => files2.Any(c => c.Key == x.Key && c.Value > x.Value));
                var n2 = all.Count(x =>!files2.Any(c=>c.Key==x.Key));
                var n3 = files2.Count(x => !all.Any(c => c.Key == x.Key));
            }

            return res;
        }

        public override string ToString()
        {
            return Dir;
        }
        /// <summary>
        /// Предоставляет информацию о папке
        /// </summary>
        /// <returns></returns>
        public List<string> Info2()
        {
            var res = new List<string>();
            
            if (IsOnline) res.Add("ONLINE == " + Dir); else res.Add("OFFLINE == " + Dir);
            if (LastMod == DefaultDate) res.Add("Dir Last Modif.: недоступно");
            else res.Add("Dir Last Modif.: "+LastMod.ToString("dd.MM.yyyy HH:mm:ss"));
            res.Add("Файлов \t:" + AllFiles().Count);
            return res;
        }
        public List<string> Info1()
        {
            var res = new List<string>();

            if (IsOnline) res.Add("ONLINE == " + Dir); else res.Add("OFFLINE == " + Dir);
           // if (LastMod == DefaultDate) res.Add("Dir Last Modif.: недоступно");
            //else res.Add("Dir Last Modif.: " + LastMod.ToString("dd.MM.yyyy HH:mm:ss"));
            //res.Add("Файлов \t:" + AllFiles.Count);
            return res;
        }
    }
}
