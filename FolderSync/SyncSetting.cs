using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderSync
{
    public partial class SyncSetting : Form
    {
        public SyncSetting(string value)
        {
            InitializeComponent();

            List<string> settings = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim().ToLower()).Distinct().ToList();

            if (!settings.Contains("synctimestamp"))
                this.chkSyncTimestamp.Checked = false;

            if (!settings.Contains("syncproperties"))
                this.chkSyncProperties.Checked = false;

            if (!settings.Contains("addfile"))
                this.chkAddFile.Checked = false;

            if (!settings.Contains("addfolder"))
                this.chkAddFolder.Checked = false;

            if (!settings.Contains("delfile"))
                this.chkDelFile.Checked = false;

            if (!settings.Contains("delfolder"))
                this.chkDelFolder.Checked = false;
        }

        public string Value = "";
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Value += this.chkSyncTimestamp.Checked ? "SyncTimestamp," : "";
            this.Value += this.chkSyncProperties.Checked ? "syncproperties," : "";
            this.Value += this.chkAddFile.Checked ? "AddFile," : "";
            this.Value += this.chkAddFolder.Checked ? "AddFolder," : "";
            this.Value += this.chkDelFile.Checked ? "DelFile," : "";
            this.Value += this.chkDelFolder.Checked ? "DelFolder," : "";
        }
    }
}
