using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncFlash
{
    public partial class MsgDialog : Form
    {
       public List<Queue> ReturnedQueue;
        public List<Queue> ExceptionsList = new List<Queue>();
        private bool AddExceptions = false;
        
        public MsgDialog(List<Queue> queues)
        {
            try
            {
InitializeComponent();
                dgv.SelectionChanged += Dgv_SelectionChanged;

            //Fill datagridview
            foreach (var q in queues)
            {
               int i=dgv.Rows.Add();
                dgv.Rows[i].Cells["check"].Value = q.Active;
                dgv.Rows[i].Cells["Number"].Value = q.Number;
                dgv.Rows[i].Cells["Source"].Value = q.SourceFile;
                dgv.Rows[i].Cells["Target"].Value = q.TargetFile;
                dgv.Rows[i].Cells["DateSource"].Value = q.DateSource;
                dgv.Rows[i].Cells["DateTarget"].Value = q.DateTarget;
                    dgv.Rows[i].Cells["Arrow"].Value = "-->";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MsgDialog");
            }
            
            ReturnedQueue = queues;
        }

        /// <summary>
        /// Select row in datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                //add exceptions
                if(AddExceptions)
                {
                    configmanager cfg = new configmanager(Form1.cfg_file); //manager of file config
                    var Projects = cfg.ReadAllProjects();
                    var pr = Projects.First(x => x.Alldirs.Any(c => c.Contains(ExceptionsList[0].SourceFileProjectDir))); //selected proj
                    foreach (Queue q in ExceptionsList)
                    {
                        string relpath = Form1.GetRelationPath(q.SourceFile,q.SourceFileProjectDir);
                        if (pr.ExceptionDirs.Contains(relpath)) continue ;
                        pr.ExceptionDirs.Add(relpath);//добавление относительного пути
                        _ = ReturnedQueue.Remove(q);
                    }
                    //save changes
                    cfg.SaveProject(pr);
                }
                foreach (DataGridViewRow row in dgv.Rows) //Удаляем из очереди файлы, которые не отмечены
                {
                    if (!(bool)row.Cells["check"].Value)
                    {
                        Queue q = ReturnedQueue.Find(x => x.Number == (int)row.Cells["Number"].Value);
                       if(q!=null) ReturnedQueue.Remove(q);
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add selected source file to ExceptionsList but not confirm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void добавитьФайлВИсключенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.SelectedRows.Count == 0) return;
                DataGridViewRow selectedrow = dgv.SelectedRows[0];
                int Number = (int)selectedrow.Cells["Number"].Value;//number of selected queue
                Queue selectedQueue = ReturnedQueue.First(x => x.Number == Number);
                if (ExceptionsList.Contains(selectedQueue)) return;
                ExceptionsList.Add(selectedQueue);

                AddExceptions = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add exception");
            }
        }

        private void отменитьДобавленныеИсключенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddExceptions = false;
        }

        private void поменятьМестамиИсточникИНазначениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.SelectedRows.Count == 0) return;
                DataGridViewRow selectedrow = dgv.SelectedRows[0];
                int Number = (int)selectedrow.Cells["Number"].Value;//number of selected queue
                Queue selectedQueue = ReturnedQueue.Find(x => x.Number == Number);
                string oldsource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Swap Target and Source");
            }
        }
    }
}
