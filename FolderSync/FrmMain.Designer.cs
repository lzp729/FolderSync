namespace FolderSync
{
    partial class FrmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.btnDirect = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.clmStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSourceRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTargetRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.chkDelFolder = new System.Windows.Forms.CheckBox();
            this.chkDelFile = new System.Windows.Forms.CheckBox();
            this.chkAddFolder = new System.Windows.Forms.CheckBox();
            this.chkAddFile = new System.Windows.Forms.CheckBox();
            this.chkSyncProperties = new System.Windows.Forms.CheckBox();
            this.chkSyncTimestamp = new System.Windows.Forms.CheckBox();
            this.chkDryRun = new System.Windows.Forms.CheckBox();
            this.lblStat = new System.Windows.Forms.Label();
            this.chkTrimRoot = new System.Windows.Forms.CheckBox();
            this.dgvTask = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckedListBoxColumn1 = new FolderSync.DataGridViewCheckedComboxBoxColumn();
            this.clmTaskEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmTaskSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTaskDirc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmTaskTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTaskSetting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(3, 17);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(350, 20);
            this.txtSource.TabIndex = 0;
            this.txtSource.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtSource_MouseDoubleClick);
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(407, 17);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(350, 20);
            this.txtTarget.TabIndex = 1;
            this.txtTarget.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtTarget_MouseDoubleClick);
            // 
            // btnDirect
            // 
            this.btnDirect.Location = new System.Drawing.Point(359, 15);
            this.btnDirect.Name = "btnDirect";
            this.btnDirect.Size = new System.Drawing.Size(42, 23);
            this.btnDirect.TabIndex = 2;
            this.btnDirect.Text = "=>";
            this.btnDirect.UseVisualStyleBackColor = true;
            this.btnDirect.Click += new System.EventHandler(this.btnDirect_Click);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(12, 246);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(760, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click2);
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToOrderColumns = true;
            this.dgvLog.AllowUserToResizeRows = false;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmStatus,
            this.clmAction,
            this.clmSource,
            this.clmSourceRoot,
            this.clmTarget,
            this.clmTargetRoot});
            this.dgvLog.Location = new System.Drawing.Point(12, 275);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.Size = new System.Drawing.Size(760, 304);
            this.dgvLog.TabIndex = 4;
            // 
            // clmStatus
            // 
            this.clmStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmStatus.FillWeight = 10F;
            this.clmStatus.HeaderText = "Status";
            this.clmStatus.Name = "clmStatus";
            this.clmStatus.ReadOnly = true;
            // 
            // clmAction
            // 
            this.clmAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmAction.FillWeight = 10F;
            this.clmAction.HeaderText = "Action";
            this.clmAction.Name = "clmAction";
            this.clmAction.ReadOnly = true;
            // 
            // clmSource
            // 
            this.clmSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmSource.FillWeight = 40F;
            this.clmSource.HeaderText = "Source";
            this.clmSource.Name = "clmSource";
            this.clmSource.ReadOnly = true;
            // 
            // clmSourceRoot
            // 
            this.clmSourceRoot.HeaderText = "SourceRoot";
            this.clmSourceRoot.Name = "clmSourceRoot";
            this.clmSourceRoot.ReadOnly = true;
            // 
            // clmTarget
            // 
            this.clmTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmTarget.FillWeight = 40F;
            this.clmTarget.HeaderText = "Target";
            this.clmTarget.Name = "clmTarget";
            this.clmTarget.ReadOnly = true;
            // 
            // clmTargetRoot
            // 
            this.clmTargetRoot.HeaderText = "TargetRoot";
            this.clmTargetRoot.Name = "clmTargetRoot";
            this.clmTargetRoot.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.lblTarget);
            this.panel1.Controls.Add(this.lblSource);
            this.panel1.Controls.Add(this.chkDelFolder);
            this.panel1.Controls.Add(this.chkDelFile);
            this.panel1.Controls.Add(this.chkAddFolder);
            this.panel1.Controls.Add(this.chkAddFile);
            this.panel1.Controls.Add(this.chkSyncProperties);
            this.panel1.Controls.Add(this.chkSyncTimestamp);
            this.panel1.Controls.Add(this.txtTarget);
            this.panel1.Controls.Add(this.txtSource);
            this.panel1.Controls.Add(this.btnDirect);
            this.panel1.Location = new System.Drawing.Point(12, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 86);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.ForeColor = System.Drawing.Color.Red;
            this.lblTarget.Location = new System.Drawing.Point(563, 1);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(38, 13);
            this.lblTarget.TabIndex = 10;
            this.lblTarget.Text = "Target";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.ForeColor = System.Drawing.Color.Green;
            this.lblSource.Location = new System.Drawing.Point(155, 1);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(41, 13);
            this.lblSource.TabIndex = 9;
            this.lblSource.Text = "Source";
            // 
            // chkDelFolder
            // 
            this.chkDelFolder.AutoSize = true;
            this.chkDelFolder.Checked = true;
            this.chkDelFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelFolder.Location = new System.Drawing.Point(433, 66);
            this.chkDelFolder.Name = "chkDelFolder";
            this.chkDelFolder.Size = new System.Drawing.Size(240, 17);
            this.chkDelFolder.TabIndex = 8;
            this.chkDelFolder.Text = "Delete folder from target if not exists in source";
            this.chkDelFolder.UseVisualStyleBackColor = true;
            // 
            // chkDelFile
            // 
            this.chkDelFile.AutoSize = true;
            this.chkDelFile.Checked = true;
            this.chkDelFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelFile.Location = new System.Drawing.Point(433, 43);
            this.chkDelFile.Name = "chkDelFile";
            this.chkDelFile.Size = new System.Drawing.Size(224, 17);
            this.chkDelFile.TabIndex = 7;
            this.chkDelFile.Text = "Delete file from target if not exists in souce";
            this.chkDelFile.UseVisualStyleBackColor = true;
            // 
            // chkAddFolder
            // 
            this.chkAddFolder.AutoSize = true;
            this.chkAddFolder.Checked = true;
            this.chkAddFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddFolder.Location = new System.Drawing.Point(200, 66);
            this.chkAddFolder.Name = "chkAddFolder";
            this.chkAddFolder.Size = new System.Drawing.Size(152, 17);
            this.chkAddFolder.TabIndex = 6;
            this.chkAddFolder.Text = "Add new folder if not exists";
            this.chkAddFolder.UseVisualStyleBackColor = true;
            // 
            // chkAddFile
            // 
            this.chkAddFile.AutoSize = true;
            this.chkAddFile.Checked = true;
            this.chkAddFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddFile.Location = new System.Drawing.Point(200, 43);
            this.chkAddFile.Name = "chkAddFile";
            this.chkAddFile.Size = new System.Drawing.Size(181, 17);
            this.chkAddFile.TabIndex = 5;
            this.chkAddFile.Text = "Add new file to target if not exists";
            this.chkAddFile.UseVisualStyleBackColor = true;
            // 
            // chkSyncProperties
            // 
            this.chkSyncProperties.AutoSize = true;
            this.chkSyncProperties.Enabled = false;
            this.chkSyncProperties.Location = new System.Drawing.Point(3, 66);
            this.chkSyncProperties.Name = "chkSyncProperties";
            this.chkSyncProperties.Size = new System.Drawing.Size(161, 17);
            this.chkSyncProperties.TabIndex = 4;
            this.chkSyncProperties.Text = "Sync all properties if possible";
            this.chkSyncProperties.UseVisualStyleBackColor = true;
            // 
            // chkSyncTimestamp
            // 
            this.chkSyncTimestamp.AutoSize = true;
            this.chkSyncTimestamp.Enabled = false;
            this.chkSyncTimestamp.Location = new System.Drawing.Point(3, 43);
            this.chkSyncTimestamp.Name = "chkSyncTimestamp";
            this.chkSyncTimestamp.Size = new System.Drawing.Size(164, 17);
            this.chkSyncTimestamp.TabIndex = 3;
            this.chkSyncTimestamp.Text = "Sync all timeStamp if possible";
            this.chkSyncTimestamp.UseVisualStyleBackColor = true;
            // 
            // chkDryRun
            // 
            this.chkDryRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDryRun.AutoSize = true;
            this.chkDryRun.Checked = true;
            this.chkDryRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDryRun.Location = new System.Drawing.Point(567, 12);
            this.chkDryRun.Name = "chkDryRun";
            this.chkDryRun.Size = new System.Drawing.Size(205, 17);
            this.chkDryRun.TabIndex = 9;
            this.chkDryRun.Text = "Dry Run (Logging only, no file actions)";
            this.chkDryRun.UseVisualStyleBackColor = true;
            // 
            // lblStat
            // 
            this.lblStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStat.AutoSize = true;
            this.lblStat.Location = new System.Drawing.Point(12, 582);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(42, 13);
            this.lblStat.TabIndex = 10;
            this.lblStat.Text = "statistic";
            // 
            // chkTrimRoot
            // 
            this.chkTrimRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTrimRoot.AutoSize = true;
            this.chkTrimRoot.Location = new System.Drawing.Point(681, 585);
            this.chkTrimRoot.Name = "chkTrimRoot";
            this.chkTrimRoot.Size = new System.Drawing.Size(91, 17);
            this.chkTrimRoot.TabIndex = 11;
            this.chkTrimRoot.Text = "Trim root path";
            this.chkTrimRoot.UseVisualStyleBackColor = true;
            this.chkTrimRoot.CheckedChanged += new System.EventHandler(this.chkTrimRoot_CheckedChanged);
            // 
            // dgvTask
            // 
            this.dgvTask.AllowUserToResizeRows = false;
            this.dgvTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTask.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTaskEnable,
            this.clmTaskSource,
            this.clmTaskDirc,
            this.clmTaskTarget,
            this.clmTaskSetting});
            this.dgvTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvTask.Location = new System.Drawing.Point(12, 35);
            this.dgvTask.Name = "dgvTask";
            this.dgvTask.RowHeadersWidth = 24;
            this.dgvTask.Size = new System.Drawing.Size(760, 205);
            this.dgvTask.TabIndex = 12;
            this.dgvTask.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellContentDoubleClick);
            this.dgvTask.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellDoubleClick);
            this.dgvTask.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvTask_RowsAdded);
            // 
            // dataGridViewCheckedListBoxColumn1
            // 
            this.dataGridViewCheckedListBoxColumn1.HeaderText = "Setting";
            this.dataGridViewCheckedListBoxColumn1.Name = "dataGridViewCheckedListBoxColumn1";
            // 
            // clmTaskEnable
            // 
            this.clmTaskEnable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmTaskEnable.FillWeight = 5F;
            this.clmTaskEnable.HeaderText = "?";
            this.clmTaskEnable.Name = "clmTaskEnable";
            this.clmTaskEnable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmTaskEnable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmTaskSource
            // 
            this.clmTaskSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.clmTaskSource.DefaultCellStyle = dataGridViewCellStyle21;
            this.clmTaskSource.FillWeight = 35F;
            this.clmTaskSource.HeaderText = "Source";
            this.clmTaskSource.Name = "clmTaskSource";
            this.clmTaskSource.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmTaskDirc
            // 
            this.clmTaskDirc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmTaskDirc.DefaultCellStyle = dataGridViewCellStyle22;
            this.clmTaskDirc.FillWeight = 10F;
            this.clmTaskDirc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clmTaskDirc.HeaderText = "Direction";
            this.clmTaskDirc.Items.AddRange(new object[] {
            "=>",
            "<=",
            "<=>"});
            this.clmTaskDirc.Name = "clmTaskDirc";
            this.clmTaskDirc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmTaskTarget
            // 
            this.clmTaskTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Red;
            this.clmTaskTarget.DefaultCellStyle = dataGridViewCellStyle23;
            this.clmTaskTarget.FillWeight = 35F;
            this.clmTaskTarget.HeaderText = "Target";
            this.clmTaskTarget.Name = "clmTaskTarget";
            this.clmTaskTarget.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmTaskTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmTaskSetting
            // 
            this.clmTaskSetting.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmTaskSetting.DefaultCellStyle = dataGridViewCellStyle24;
            this.clmTaskSetting.FillWeight = 20F;
            this.clmTaskSetting.HeaderText = "Setting";
            this.clmTaskSetting.Name = "clmTaskSetting";
            this.clmTaskSetting.ReadOnly = true;
            this.clmTaskSetting.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmTaskSetting.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 26);
            this.label1.TabIndex = 13;
            this.label1.Text = "Sync folders. Supported path includes: local, remote share, ftp://user:pass@serve" +
    "r/uri/\r\nDouble-click to choose path or open settings.";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 621);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTask);
            this.Controls.Add(this.chkTrimRoot);
            this.Controls.Add(this.lblStat);
            this.Controls.Add(this.chkDryRun);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folder Sync";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button btnDirect;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkSyncTimestamp;
        private System.Windows.Forms.CheckBox chkSyncProperties;
        private System.Windows.Forms.CheckBox chkAddFile;
        private System.Windows.Forms.CheckBox chkAddFolder;
        private System.Windows.Forms.CheckBox chkDelFile;
        private System.Windows.Forms.CheckBox chkDelFolder;
        private System.Windows.Forms.CheckBox chkDryRun;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSourceRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTargetRoot;
        private System.Windows.Forms.CheckBox chkTrimRoot;
        private System.Windows.Forms.DataGridView dgvTask;
        private DataGridViewCheckedComboxBoxColumn dataGridViewCheckedListBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmTaskEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaskSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmTaskDirc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaskTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaskSetting;
        private System.Windows.Forms.Label label1;
    }
}

