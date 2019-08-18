using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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

        private void StartSync(Project project)
        {
            if (List_Projects.SelectedItem == null) return;
            var selectedProj = Projects.First(x => x.Name == List_Projects.SelectedItem.ToString());
            SyncThread = new Thread(delegate() 
            {
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
               //onlinedirs >=2

            });
            SyncThread.Start();
        }
        /// <summary>
        /// Write dirs of selected project into list_dirs
        /// </summary>
        public void DisplayDirs()
        {

            if (List_Projects.SelectedItem == null)
            { list_dirs.Items.Clear();listExceptions.Items.Clear(); return; }
            list_dirs.Items.Clear();
            listExceptions.Items.Clear();
            var selectedProj = Projects.First(x => x.Name == List_Projects.SelectedItem.ToString());
            foreach (var item in selectedProj.AllProjectDirs)
            {
                if (!checkBox1.Checked || item.IsOnline && item.PC_Name == pc_name)
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

        private void добавитьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input input = new Input();
            input.ShowDialog();
            var selected = List_Projects.SelectedItem;
            if (string.IsNullOrWhiteSpace(input.TEXT) ||
                list_dirs.Items.Contains(new ListViewItem(input.TEXT)) ||
                selected == null) return;
            //TODO check existing dir
            if(!list_dirs.Items.Contains(new ListViewItem(input.TEXT))) list_dirs.Items.Add(input.TEXT);
            Projects.First(x => x.Name == selected.ToString()).AllProjectDirs.Add(new Projdir(input.TEXT));
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

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selected = List_Projects.SelectedItem;
            if (selected == null) return;
            var P = Projects.First(x => x.Name == selected.ToString());
            StartSync(P);
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
            var dg=input.ShowDialog();
            if (dg != DialogResult.OK) return;
            if (string.IsNullOrWhiteSpace(input.TEXT) ||
                listExceptions.Items.Contains(input.TEXT)) return;
            //TODO check existing exc dir
            var d = Projects.First(x => x.Name == selected.ToString()).
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
    }
}
