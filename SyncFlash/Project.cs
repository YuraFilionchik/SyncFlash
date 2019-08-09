using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using filesdates = System.Collections.Generic.Dictionary<string, System.DateTime>;
namespace SyncFlash
{
    class Project
    {
        
    }
    class Projdir
    {
        private string _dir;
        public string Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }
        public bool IsOnline
        {
            get { return Directory.Exists(Dir); }
        }
        /// <summary>
        /// Dictionary<files,datetime last write> всех файлов в Projdir
        /// </summary>
        public  filesdates AllFiles
        {
            get
            {
                if (!IsOnline) return new filesdates();
                List<string> files1 = new List<string>();//all files in Dir
                files1.AddRange(Directory.GetFiles(Dir));//all files in Dir
                string[] subdirs = Directory.GetDirectories(Dir);//all dirs in Dir
                if (subdirs.Length != 0)//find all files in subdirs
                    foreach (var subdir in subdirs)
                    {
                        var files2 = Directory.GetFiles(subdir);
                        if (files2.Length != 0) files1.AddRange(files2);
                    }
                filesdates res = new filesdates();
                
                foreach (var file in files1)
                {
                    res.Add(file, File.GetLastWriteTime(file));
                }
                return res;
            }
        }
        /// <summary>
        /// Показывает время подификации самого нового файла в папке Dir и одной подпапке внутрь
        /// </summary>
        public DateTime LastMod
        {
            get
            {
                
                DateTime lastmod = new DateTime(2000, 1, 1);
                if (!IsOnline) return lastmod;
                var files1 = AllFiles;
                if (files1.Count == 0) return lastmod;//no one file in Dir and subdirs
                return files1.Max(x => File.GetLastWriteTime(x));
            }
        }
    }
}
