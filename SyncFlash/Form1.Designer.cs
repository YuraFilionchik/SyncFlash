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
            this.List_Projects = new System.Windows.Forms.ListBox();
            this.contextprojects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПроектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПроектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.list_dirs = new System.Windows.Forms.ListView();
            this.contextdirs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tblog = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listExceptions = new System.Windows.Forms.ListBox();
            this.contextExceptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.добавитьПапкуНаFlashDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.синхронизироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextprojects.SuspendLayout();
            this.contextdirs.SuspendLayout();
            this.contextExceptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // List_Projects
            // 
            this.List_Projects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.List_Projects.ContextMenuStrip = this.contextprojects;
            this.List_Projects.FormattingEnabled = true;
            this.List_Projects.Location = new System.Drawing.Point(3, 2);
            this.List_Projects.Name = "List_Projects";
            this.List_Projects.Size = new System.Drawing.Size(148, 199);
            this.List_Projects.TabIndex = 0;
            // 
            // contextprojects
            // 
            this.contextprojects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПроектToolStripMenuItem,
            this.удалитьПроектToolStripMenuItem,
            this.синхронизироватьToolStripMenuItem});
            this.contextprojects.Name = "contextprojects";
            this.contextprojects.Size = new System.Drawing.Size(216, 70);
            // 
            // добавитьПроектToolStripMenuItem
            // 
            this.добавитьПроектToolStripMenuItem.Name = "добавитьПроектToolStripMenuItem";
            this.добавитьПроектToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.добавитьПроектToolStripMenuItem.Text = "Добавить проект";
            this.добавитьПроектToolStripMenuItem.Click += new System.EventHandler(this.добавитьПроектToolStripMenuItem_Click);
            // 
            // удалитьПроектToolStripMenuItem
            // 
            this.удалитьПроектToolStripMenuItem.Name = "удалитьПроектToolStripMenuItem";
            this.удалитьПроектToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.удалитьПроектToolStripMenuItem.Text = "Удалить проект из списка";
            this.удалитьПроектToolStripMenuItem.Click += new System.EventHandler(this.удалитьПроектToolStripMenuItem_Click);
            // 
            // list_dirs
            // 
            this.list_dirs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_dirs.ContextMenuStrip = this.contextdirs;
            this.list_dirs.Location = new System.Drawing.Point(157, 2);
            this.list_dirs.MultiSelect = false;
            this.list_dirs.Name = "list_dirs";
            this.list_dirs.Size = new System.Drawing.Size(368, 105);
            this.list_dirs.TabIndex = 2;
            this.list_dirs.UseCompatibleStateImageBehavior = false;
            this.list_dirs.View = System.Windows.Forms.View.List;
            // 
            // contextdirs
            // 
            this.contextdirs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПапкуToolStripMenuItem,
            this.удалитьПапкуToolStripMenuItem,
            this.добавитьПапкуНаFlashDriveToolStripMenuItem});
            this.contextdirs.Name = "contextdirs";
            this.contextdirs.Size = new System.Drawing.Size(235, 70);
            // 
            // добавитьПапкуToolStripMenuItem
            // 
            this.добавитьПапкуToolStripMenuItem.Name = "добавитьПапкуToolStripMenuItem";
            this.добавитьПапкуToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.добавитьПапкуToolStripMenuItem.Text = "Добавить папку ПК";
            this.добавитьПапкуToolStripMenuItem.Click += new System.EventHandler(this.добавитьПапкуToolStripMenuItem_Click);
            // 
            // удалитьПапкуToolStripMenuItem
            // 
            this.удалитьПапкуToolStripMenuItem.Name = "удалитьПапкуToolStripMenuItem";
            this.удалитьПапкуToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.удалитьПапкуToolStripMenuItem.Text = "Удалить папку";
            this.удалитьПапкуToolStripMenuItem.Click += new System.EventHandler(this.удалитьПапкуToolStripMenuItem_Click);
            // 
            // tblog
            // 
            this.tblog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblog.Location = new System.Drawing.Point(3, 207);
            this.tblog.Multiline = true;
            this.tblog.Name = "tblog";
            this.tblog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tblog.Size = new System.Drawing.Size(626, 151);
            this.tblog.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 365);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(634, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(531, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(77, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "OnlineOnly";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // listExceptions
            // 
            this.listExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listExceptions.ContextMenuStrip = this.contextExceptions;
            this.listExceptions.FormattingEnabled = true;
            this.listExceptions.Location = new System.Drawing.Point(157, 132);
            this.listExceptions.Name = "listExceptions";
            this.listExceptions.Size = new System.Drawing.Size(367, 69);
            this.listExceptions.TabIndex = 7;
            // 
            // contextExceptions
            // 
            this.contextExceptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextExceptions.Name = "contextdirs";
            this.contextExceptions.Size = new System.Drawing.Size(162, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem1.Text = "Добавить папку";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem2.Text = "Удалить папку";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Исключения:";
            // 
            // добавитьПапкуНаFlashDriveToolStripMenuItem
            // 
            this.добавитьПапкуНаFlashDriveToolStripMenuItem.Name = "добавитьПапкуНаFlashDriveToolStripMenuItem";
            this.добавитьПапкуНаFlashDriveToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.добавитьПапкуНаFlashDriveToolStripMenuItem.Text = "Добавить папку на FlashDrive";
            this.добавитьПапкуНаFlashDriveToolStripMenuItem.Click += new System.EventHandler(this.добавитьПапкуНаFlashDriveToolStripMenuItem_Click);
            // 
            // синхронизироватьToolStripMenuItem
            // 
            this.синхронизироватьToolStripMenuItem.Name = "синхронизироватьToolStripMenuItem";
            this.синхронизироватьToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.синхронизироватьToolStripMenuItem.Text = "-=Синхронизировать=-";
            this.синхронизироватьToolStripMenuItem.Click += new System.EventHandler(this.синхронизироватьToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 388);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listExceptions);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblog);
            this.Controls.Add(this.list_dirs);
            this.Controls.Add(this.List_Projects);
            this.Name = "Form1";
            this.Text = "SyncFlash";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextprojects.ResumeLayout(false);
            this.contextdirs.ResumeLayout(false);
            this.contextExceptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox List_Projects;
        private System.Windows.Forms.ListView list_dirs;
        private System.Windows.Forms.TextBox tblog;
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
        private System.Windows.Forms.ToolStripMenuItem добавитьПапкуНаFlashDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem синхронизироватьToolStripMenuItem;
    }
}

