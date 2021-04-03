namespace SyncFlash
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.List_Projects = new System.Windows.Forms.ListBox();
            this.contextprojects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПроектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПроектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.синхронизироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьСписокПроектовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.list_dirs = new System.Windows.Forms.ListView();
            this.contextdirs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьЭтуПапкуВОстальныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listExceptions = new System.Windows.Forms.ListBox();
            this.contextExceptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btSelectUSB = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tblog = new System.Windows.Forms.DataGridView();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.cbSilent = new System.Windows.Forms.CheckBox();
            this.btLog = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextprojects.SuspendLayout();
            this.contextdirs.SuspendLayout();
            this.contextExceptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblog)).BeginInit();
            this.SuspendLayout();
            // 
            // List_Projects
            // 
            this.List_Projects.ContextMenuStrip = this.contextprojects;
            this.List_Projects.FormattingEnabled = true;
            resources.ApplyResources(this.List_Projects, "List_Projects");
            this.List_Projects.Name = "List_Projects";
            // 
            // contextprojects
            // 
            this.contextprojects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПроектToolStripMenuItem,
            this.удалитьПроектToolStripMenuItem,
            this.синхронизироватьToolStripMenuItem,
            this.переименоватьToolStripMenuItem,
            this.обновитьСписокПроектовToolStripMenuItem});
            this.contextprojects.Name = "contextprojects";
            resources.ApplyResources(this.contextprojects, "contextprojects");
            // 
            // добавитьПроектToolStripMenuItem
            // 
            this.добавитьПроектToolStripMenuItem.Name = "добавитьПроектToolStripMenuItem";
            resources.ApplyResources(this.добавитьПроектToolStripMenuItem, "добавитьПроектToolStripMenuItem");
            this.добавитьПроектToolStripMenuItem.Click += new System.EventHandler(this.добавитьПроектToolStripMenuItem_Click);
            // 
            // удалитьПроектToolStripMenuItem
            // 
            this.удалитьПроектToolStripMenuItem.Name = "удалитьПроектToolStripMenuItem";
            resources.ApplyResources(this.удалитьПроектToolStripMenuItem, "удалитьПроектToolStripMenuItem");
            this.удалитьПроектToolStripMenuItem.Click += new System.EventHandler(this.удалитьПроектToolStripMenuItem_Click);
            // 
            // синхронизироватьToolStripMenuItem
            // 
            this.синхронизироватьToolStripMenuItem.Name = "синхронизироватьToolStripMenuItem";
            resources.ApplyResources(this.синхронизироватьToolStripMenuItem, "синхронизироватьToolStripMenuItem");
            this.синхронизироватьToolStripMenuItem.Click += new System.EventHandler(this.синхронизироватьToolStripMenuItem_Click);
            // 
            // переименоватьToolStripMenuItem
            // 
            this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
            resources.ApplyResources(this.переименоватьToolStripMenuItem, "переименоватьToolStripMenuItem");
            this.переименоватьToolStripMenuItem.Click += new System.EventHandler(this.переименоватьToolStripMenuItem_Click);
            // 
            // обновитьСписокПроектовToolStripMenuItem
            // 
            this.обновитьСписокПроектовToolStripMenuItem.Name = "обновитьСписокПроектовToolStripMenuItem";
            resources.ApplyResources(this.обновитьСписокПроектовToolStripMenuItem, "обновитьСписокПроектовToolStripMenuItem");
            this.обновитьСписокПроектовToolStripMenuItem.Click += new System.EventHandler(this.обновитьСписокПроектовToolStripMenuItem_Click);
            // 
            // list_dirs
            // 
            resources.ApplyResources(this.list_dirs, "list_dirs");
            this.list_dirs.ContextMenuStrip = this.contextdirs;
            this.list_dirs.HideSelection = false;
            this.list_dirs.MultiSelect = false;
            this.list_dirs.Name = "list_dirs";
            this.list_dirs.UseCompatibleStateImageBehavior = false;
            this.list_dirs.View = System.Windows.Forms.View.List;
            // 
            // contextdirs
            // 
            this.contextdirs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПапкуToolStripMenuItem,
            this.удалитьПапкуToolStripMenuItem,
            this.копироватьЭтуПапкуВОстальныеToolStripMenuItem});
            this.contextdirs.Name = "contextdirs";
            resources.ApplyResources(this.contextdirs, "contextdirs");
            // 
            // добавитьПапкуToolStripMenuItem
            // 
            this.добавитьПапкуToolStripMenuItem.Name = "добавитьПапкуToolStripMenuItem";
            resources.ApplyResources(this.добавитьПапкуToolStripMenuItem, "добавитьПапкуToolStripMenuItem");
            this.добавитьПапкуToolStripMenuItem.Click += new System.EventHandler(this.добавитьПапкуToolStripMenuItem_Click);
            // 
            // удалитьПапкуToolStripMenuItem
            // 
            this.удалитьПапкуToolStripMenuItem.Name = "удалитьПапкуToolStripMenuItem";
            resources.ApplyResources(this.удалитьПапкуToolStripMenuItem, "удалитьПапкуToolStripMenuItem");
            this.удалитьПапкуToolStripMenuItem.Click += new System.EventHandler(this.удалитьПапкуToolStripMenuItem_Click);
            // 
            // копироватьЭтуПапкуВОстальныеToolStripMenuItem
            // 
            this.копироватьЭтуПапкуВОстальныеToolStripMenuItem.Name = "копироватьЭтуПапкуВОстальныеToolStripMenuItem";
            resources.ApplyResources(this.копироватьЭтуПапкуВОстальныеToolStripMenuItem, "копироватьЭтуПапкуВОстальныеToolStripMenuItem");
            this.копироватьЭтуПапкуВОстальныеToolStripMenuItem.Click += new System.EventHandler(this.копироватьЭтуПапкуВОстальныеToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Name = "checkBox1";
            this.toolTip1.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged_1);
            // 
            // listExceptions
            // 
            resources.ApplyResources(this.listExceptions, "listExceptions");
            this.listExceptions.ContextMenuStrip = this.contextExceptions;
            this.listExceptions.FormattingEnabled = true;
            this.listExceptions.Name = "listExceptions";
            // 
            // contextExceptions
            // 
            this.contextExceptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextExceptions.Name = "contextdirs";
            resources.ApplyResources(this.contextExceptions, "contextExceptions");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btSelectUSB
            // 
            resources.ApplyResources(this.btSelectUSB, "btSelectUSB");
            this.btSelectUSB.Name = "btSelectUSB";
            this.toolTip1.SetToolTip(this.btSelectUSB, resources.GetString("btSelectUSB.ToolTip"));
            this.btSelectUSB.UseVisualStyleBackColor = true;
            this.btSelectUSB.Click += new System.EventHandler(this.btSelectUSB_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.Color.Aqua;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button1.FlatAppearance.BorderSize = 4;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tblog
            // 
            this.tblog.AllowUserToAddRows = false;
            this.tblog.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.tblog, "tblog");
            this.tblog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tblog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tblog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tblog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tblog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.tblog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tblog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblog.ColumnHeadersVisible = false;
            this.tblog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.data});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tblog.DefaultCellStyle = dataGridViewCellStyle1;
            this.tblog.EnableHeadersVisualStyles = false;
            this.tblog.Name = "tblog";
            this.tblog.ReadOnly = true;
            this.tblog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tblog.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            this.tblog.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.tblog.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.tblog.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tblog.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Navy;
            this.tblog.RowTemplate.Height = 10;
            this.tblog.RowTemplate.ReadOnly = true;
            this.tblog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // data
            // 
            this.data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.data, "data");
            this.data.Name = "data";
            this.data.ReadOnly = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.toolTip1.SetToolTip(this.checkBox2, resources.GetString("checkBox2.ToolTip"));
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // cbSilent
            // 
            resources.ApplyResources(this.cbSilent, "cbSilent");
            this.cbSilent.Name = "cbSilent";
            this.toolTip1.SetToolTip(this.cbSilent, resources.GetString("cbSilent.ToolTip"));
            this.cbSilent.UseVisualStyleBackColor = true;
            this.cbSilent.CheckedChanged += new System.EventHandler(this.cbSilent_CheckedChanged);
            // 
            // btLog
            // 
            resources.ApplyResources(this.btLog, "btLog");
            this.btLog.Name = "btLog";
            this.btLog.UseVisualStyleBackColor = true;
            this.btLog.Click += new System.EventHandler(this.btLog_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btLog);
            this.Controls.Add(this.cbSilent);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.tblog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btSelectUSB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listExceptions);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.list_dirs);
            this.Controls.Add(this.List_Projects);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextprojects.ResumeLayout(false);
            this.contextdirs.ResumeLayout(false);
            this.contextExceptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox List_Projects;
        private System.Windows.Forms.ListView list_dirs;
        private System.Windows.Forms.ContextMenuStrip contextprojects;
        private System.Windows.Forms.ToolStripMenuItem добавитьПроектToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьПроектToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextdirs;
        private System.Windows.Forms.ToolStripMenuItem добавитьПапкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьПапкуToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listExceptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextExceptions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem синхронизироватьToolStripMenuItem;
        private System.Windows.Forms.Button btSelectUSB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView tblog;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox cbSilent;
        private System.Windows.Forms.Button btLog;
        private System.Windows.Forms.ToolStripMenuItem копироватьЭтуПапкуВОстальныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьСписокПроектовToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

