namespace SyncFlash
{
    partial class MsgDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgDialog));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arrow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьФайлВИсключенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменитьДобавленныеИсключенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поменятьМестамиИсточникИНазначениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьФайлВоВсехПапкахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cbfilter = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            resources.ApplyResources(this.btOK, "btOK");
            this.btOK.BackColor = System.Drawing.Color.Goldenrod;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Name = "btOK";
            this.btOK.Tag = "Копировать";
            this.btOK.UseVisualStyleBackColor = false;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            resources.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.Name = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // dgv
            // 
            resources.ApplyResources(this.dgv, "dgv");
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.Number,
            this.Source,
            this.DateSource,
            this.Arrow,
            this.DateTarget,
            this.Target});
            this.dgv.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // check
            // 
            resources.ApplyResources(this.check, "check");
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Number
            // 
            this.Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.Number, "Number");
            this.Number.Name = "Number";
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Source
            // 
            this.Source.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Source, "Source");
            this.Source.Name = "Source";
            // 
            // DateSource
            // 
            this.DateSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.DateSource, "DateSource");
            this.DateSource.Name = "DateSource";
            // 
            // Arrow
            // 
            resources.ApplyResources(this.Arrow, "Arrow");
            this.Arrow.Name = "Arrow";
            this.Arrow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DateTarget
            // 
            this.DateTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.DateTarget, "DateTarget");
            this.DateTarget.Name = "DateTarget";
            // 
            // Target
            // 
            this.Target.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Target, "Target");
            this.Target.Name = "Target";
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьФайлВИсключенияToolStripMenuItem,
            this.отменитьДобавленныеИсключенияToolStripMenuItem,
            this.поменятьМестамиИсточникИНазначениеToolStripMenuItem,
            this.удалитьФайлВоВсехПапкахToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // добавитьФайлВИсключенияToolStripMenuItem
            // 
            resources.ApplyResources(this.добавитьФайлВИсключенияToolStripMenuItem, "добавитьФайлВИсключенияToolStripMenuItem");
            this.добавитьФайлВИсключенияToolStripMenuItem.Name = "добавитьФайлВИсключенияToolStripMenuItem";
            this.добавитьФайлВИсключенияToolStripMenuItem.Click += new System.EventHandler(this.добавитьФайлВИсключенияToolStripMenuItem_Click);
            // 
            // отменитьДобавленныеИсключенияToolStripMenuItem
            // 
            resources.ApplyResources(this.отменитьДобавленныеИсключенияToolStripMenuItem, "отменитьДобавленныеИсключенияToolStripMenuItem");
            this.отменитьДобавленныеИсключенияToolStripMenuItem.Name = "отменитьДобавленныеИсключенияToolStripMenuItem";
            this.отменитьДобавленныеИсключенияToolStripMenuItem.Click += new System.EventHandler(this.отменитьДобавленныеИсключенияToolStripMenuItem_Click);
            // 
            // поменятьМестамиИсточникИНазначениеToolStripMenuItem
            // 
            resources.ApplyResources(this.поменятьМестамиИсточникИНазначениеToolStripMenuItem, "поменятьМестамиИсточникИНазначениеToolStripMenuItem");
            this.поменятьМестамиИсточникИНазначениеToolStripMenuItem.Name = "поменятьМестамиИсточникИНазначениеToolStripMenuItem";
            this.поменятьМестамиИсточникИНазначениеToolStripMenuItem.Click += new System.EventHandler(this.поменятьМестамиИсточникИНазначениеToolStripMenuItem_Click);
            // 
            // удалитьФайлВоВсехПапкахToolStripMenuItem
            // 
            resources.ApplyResources(this.удалитьФайлВоВсехПапкахToolStripMenuItem, "удалитьФайлВоВсехПапкахToolStripMenuItem");
            this.удалитьФайлВоВсехПапкахToolStripMenuItem.Name = "удалитьФайлВоВсехПапкахToolStripMenuItem";
            this.удалитьФайлВоВсехПапкахToolStripMenuItem.Click += new System.EventHandler(this.удалитьФайлВоВсехПапкахToolStripMenuItem_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbfilter
            // 
            resources.ApplyResources(this.cbfilter, "cbfilter");
            this.cbfilter.Name = "cbfilter";
            this.cbfilter.UseVisualStyleBackColor = true;
            this.cbfilter.CheckedChanged += new System.EventHandler(this.cbfilter_CheckedChanged);
            // 
            // MsgDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbfilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MsgDialog";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьФайлВИсключенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменитьДобавленныеИсключенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поменятьМестамиИсточникИНазначениеToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem удалитьФайлВоВсехПапкахToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbfilter;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arrow;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn Target;
    }
}