namespace FolderSync
{
    partial class SyncSetting
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkSyncProperties = new System.Windows.Forms.CheckBox();
            this.chkSyncTimestamp = new System.Windows.Forms.CheckBox();
            this.chkAddFolder = new System.Windows.Forms.CheckBox();
            this.chkAddFile = new System.Windows.Forms.CheckBox();
            this.chkDelFolder = new System.Windows.Forms.CheckBox();
            this.chkDelFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(31, 166);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(161, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkSyncProperties
            // 
            this.chkSyncProperties.AutoSize = true;
            this.chkSyncProperties.Enabled = false;
            this.chkSyncProperties.Location = new System.Drawing.Point(12, 35);
            this.chkSyncProperties.Name = "chkSyncProperties";
            this.chkSyncProperties.Size = new System.Drawing.Size(161, 17);
            this.chkSyncProperties.TabIndex = 6;
            this.chkSyncProperties.Text = "Sync all properties if possible";
            this.chkSyncProperties.UseVisualStyleBackColor = true;
            // 
            // chkSyncTimestamp
            // 
            this.chkSyncTimestamp.AutoSize = true;
            this.chkSyncTimestamp.Enabled = false;
            this.chkSyncTimestamp.Location = new System.Drawing.Point(12, 12);
            this.chkSyncTimestamp.Name = "chkSyncTimestamp";
            this.chkSyncTimestamp.Size = new System.Drawing.Size(164, 17);
            this.chkSyncTimestamp.TabIndex = 5;
            this.chkSyncTimestamp.Text = "Sync all timeStamp if possible";
            this.chkSyncTimestamp.UseVisualStyleBackColor = true;
            // 
            // chkAddFolder
            // 
            this.chkAddFolder.AutoSize = true;
            this.chkAddFolder.Checked = true;
            this.chkAddFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddFolder.Location = new System.Drawing.Point(12, 81);
            this.chkAddFolder.Name = "chkAddFolder";
            this.chkAddFolder.Size = new System.Drawing.Size(152, 17);
            this.chkAddFolder.TabIndex = 8;
            this.chkAddFolder.Text = "Add new folder if not exists";
            this.chkAddFolder.UseVisualStyleBackColor = true;
            // 
            // chkAddFile
            // 
            this.chkAddFile.AutoSize = true;
            this.chkAddFile.Checked = true;
            this.chkAddFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddFile.Location = new System.Drawing.Point(12, 58);
            this.chkAddFile.Name = "chkAddFile";
            this.chkAddFile.Size = new System.Drawing.Size(181, 17);
            this.chkAddFile.TabIndex = 7;
            this.chkAddFile.Text = "Add new file to target if not exists";
            this.chkAddFile.UseVisualStyleBackColor = true;
            // 
            // chkDelFolder
            // 
            this.chkDelFolder.AutoSize = true;
            this.chkDelFolder.Checked = true;
            this.chkDelFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelFolder.Location = new System.Drawing.Point(12, 127);
            this.chkDelFolder.Name = "chkDelFolder";
            this.chkDelFolder.Size = new System.Drawing.Size(240, 17);
            this.chkDelFolder.TabIndex = 10;
            this.chkDelFolder.Text = "Delete folder from target if not exists in source";
            this.chkDelFolder.UseVisualStyleBackColor = true;
            // 
            // chkDelFile
            // 
            this.chkDelFile.AutoSize = true;
            this.chkDelFile.Checked = true;
            this.chkDelFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelFile.Location = new System.Drawing.Point(12, 104);
            this.chkDelFile.Name = "chkDelFile";
            this.chkDelFile.Size = new System.Drawing.Size(224, 17);
            this.chkDelFile.TabIndex = 9;
            this.chkDelFile.Text = "Delete file from target if not exists in souce";
            this.chkDelFile.UseVisualStyleBackColor = true;
            // 
            // SyncSetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(264, 201);
            this.Controls.Add(this.chkDelFolder);
            this.Controls.Add(this.chkDelFile);
            this.Controls.Add(this.chkAddFolder);
            this.Controls.Add(this.chkAddFile);
            this.Controls.Add(this.chkSyncProperties);
            this.Controls.Add(this.chkSyncTimestamp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SyncSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SyncSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkSyncProperties;
        private System.Windows.Forms.CheckBox chkSyncTimestamp;
        private System.Windows.Forms.CheckBox chkAddFolder;
        private System.Windows.Forms.CheckBox chkAddFile;
        private System.Windows.Forms.CheckBox chkDelFolder;
        private System.Windows.Forms.CheckBox chkDelFile;
    }
}