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
        public MsgDialog(List<Queue> queues)
        {
            try
            {
InitializeComponent();
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

        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgv.Rows) //Удаляем из очереди файлы, которые не отмечены
                {
                    if (!(bool)row.Cells["check"].Value)
                    {
                        Queue q = ReturnedQueue.Find(x => x.Number == (int)row.Cells["Number"].Value);
                        ReturnedQueue.Remove(q);
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
    }
}
