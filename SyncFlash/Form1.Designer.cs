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
            this.List_Projects = new System.Windows.Forms.ListBox();
            this.list_dirs = new System.Windows.Forms.ListView();
            this.tblog = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // List_Projects
            // 
            this.List_Projects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.List_Projects.FormattingEnabled = true;
            this.List_Projects.Location = new System.Drawing.Point(3, 2);
            this.List_Projects.Name = "List_Projects";
            this.List_Projects.Size = new System.Drawing.Size(148, 147);
            this.List_Projects.TabIndex = 0;
            // 
            // list_dirs
            // 
            this.list_dirs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_dirs.Location = new System.Drawing.Point(157, 2);
            this.list_dirs.MultiSelect = false;
            this.list_dirs.Name = "list_dirs";
            this.list_dirs.Size = new System.Drawing.Size(447, 147);
            this.list_dirs.TabIndex = 2;
            this.list_dirs.UseCompatibleStateImageBehavior = false;
            this.list_dirs.View = System.Windows.Forms.View.Details;
            // 
            // tblog
            // 
            this.tblog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblog.Location = new System.Drawing.Point(3, 159);
            this.tblog.Multiline = true;
            this.tblog.Name = "tblog";
            this.tblog.Size = new System.Drawing.Size(601, 109);
            this.tblog.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 275);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(609, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 298);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblog);
            this.Controls.Add(this.list_dirs);
            this.Controls.Add(this.List_Projects);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox List_Projects;
        private System.Windows.Forms.ListView list_dirs;
        private System.Windows.Forms.TextBox tblog;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

