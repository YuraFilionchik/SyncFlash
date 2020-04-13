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
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(22, 327);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(93, 33);
            this.btOK.TabIndex = 0;
            this.btOK.Tag = "Копировать";
            this.btOK.Text = "Копировать";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(655, 327);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(95, 33);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.Number,
            this.Source,
            this.DateSource,
            this.Arrow,
            this.DateTarget,
            this.Target});
            this.dgv.Location = new System.Drawing.Point(1, -2);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(775, 323);
            this.dgv.TabIndex = 2;
            // 
            // check
            // 
            this.check.HeaderText = "V";
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.check.Width = 24;
            // 
            // Number
            // 
            this.Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Number.HeaderText = "№";
            this.Number.Name = "Number";
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Number.Width = 43;
            // 
            // Source
            // 
            this.Source.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Source.HeaderText = "Источник";
            this.Source.MinimumWidth = 100;
            this.Source.Name = "Source";
            // 
            // DateSource
            // 
            this.DateSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DateSource.HeaderText = "Изменен";
            this.DateSource.Name = "DateSource";
            this.DateSource.Width = 78;
            // 
            // Arrow
            // 
            this.Arrow.HeaderText = "-->";
            this.Arrow.Name = "Arrow";
            this.Arrow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Arrow.ToolTipText = "Направление";
            this.Arrow.Width = 30;
            // 
            // DateTarget
            // 
            this.DateTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DateTarget.HeaderText = "Изменен";
            this.DateTarget.Name = "DateTarget";
            this.DateTarget.Width = 78;
            // 
            // Target
            // 
            this.Target.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Target.HeaderText = "Назначение";
            this.Target.Name = "Target";
            // 
            // MsgDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 362);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(1400, 700);
            this.Name = "MsgDialog";
            this.Text = "Синхронизация каталогов";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arrow;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn Target;
    }
}