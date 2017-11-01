using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Drawing;

namespace FolderSync
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.clmSourceRoot.Visible = false;
            this.clmTargetRoot.Visible = false;
            loadTasks();
            
            Point mouseSet = default(Point);
            this.MouseDown += (sender, me) =>
            {
                if (me.Button == MouseButtons.Left)
                {
                    mouseSet = me.Location;
                }
            };
            this.lblLine.MouseMove += (sender, me) =>
            {
                // limited by this.panelTestNotes.MinimumSize
                if (me.Button == MouseButtons.Left)
                {
                    int yy = me.Location.Y - mouseSet.Y;
                    if (this.dgvTask.Height + yy > this.dgvTask.MinimumSize.Height 
                        && this.dgvLog.Height - yy > this.dgvLog.MinimumSize.Height)
                    {
                        this.dgvTask.Height = this.dgvTask.Height + yy;
                        this.lblLine.Location = new Point(this.lblLine.Location.X, this.lblLine.Location.Y + yy);
                        this.btnGo.Location = new Point(this.btnGo.Location.X, this.btnGo.Location.Y + yy);
                        this.dgvLog.Location = new Point(this.dgvLog.Location.X, this.dgvLog.Location.Y + yy);
                        this.dgvLog.Height = this.dgvLog.Height - yy;
                    }
                }
            };
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (trdSync == null)
            {
                LocBase locSource = LocBase.GetInstance(this.txtSource.Text);
                LocBase locTarget = LocBase.GetInstance(this.txtTarget.Text);

                if (locSource == null)
                {
                    MessageBox.Show("Unsupported source path");
                    return;
                }

                if (locTarget == null)
                {
                    MessageBox.Show("Unsupported target path");
                    return;
                }

                if (locSource.RootLoc.ToLower().Contains(locTarget.RootLoc.ToLower()) ||
                    locTarget.RootLoc.ToLower().Contains(locSource.RootLoc.ToLower()))
                {
                    MessageBox.Show("Nested path");
                    return;
                }

                LocSyncDirection dirc = LocSyncDirection.EM_TO;

                switch (this.btnDirect.Text)
                {
                    case "=>":
                        dirc = LocSyncDirection.EM_TO;
                        break;
                    case "<=":
                        dirc = LocSyncDirection.EM_FROM;
                        break;
                    case "<=>":
                        dirc = LocSyncDirection.EM_BI;
                        break;
                }

                if (dirc == LocSyncDirection.EM_BI)
                {
                    MessageBox.Show("Bi-direction sync not implemented");
                    return;
                }

                currentTask = new LocSync(locSource, locTarget, dirc);
                currentTask.Dryrun = chkDryRun.Checked;
                currentTask.AddFile = chkAddFile.Checked;
                currentTask.DelFile = chkDelFile.Checked;
                currentTask.AddFolder = chkAddFolder.Checked;
                currentTask.DelFolder = chkDelFolder.Checked;
                currentTask.SyncTimestamp = chkSyncTimestamp.Checked;
                currentTask.SyncProperties = chkSyncProperties.Checked;

                if (currentTask.Dryrun && currentTask.AddFolder)
                    MessageBox.Show("folders will be created still even in dryrun");

                if (currentTask.SyncTimestamp || currentTask.SyncProperties)
                    MessageBox.Show("timestamp or properties sync is not implemented yet");

                if (MessageBox.Show("Good to go?", "Folder Sync", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                this.dgvLog.Rows.Clear();
                this.lblStat.Text = "";

                Dictionary<LocSyncStatus, int> countStatus = new Dictionary<LocSyncStatus, int>()
            {
                { LocSyncStatus.EM_ERROR, 0 },
                { LocSyncStatus.EM_WARNING, 0 },
                { LocSyncStatus.EM_OK, 0 },
            };

                Dictionary<LocSyncAction, int> countAction = new Dictionary<LocSyncAction, int>()
            {
                { LocSyncAction.EM_IGNORE_TIMESTAMP, 0 },
                { LocSyncAction.EM_REPLACE, 0 },
                { LocSyncAction.EM_ADD_FILE, 0 },
                { LocSyncAction.EM_ADD_FOLDER, 0 },
                { LocSyncAction.EM_DELETE_FILE, 0 },
                { LocSyncAction.EM_DELETE_FOLDER, 0 },
            };

                currentTask.SyncUpdateEvent += (senderr, ee) =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (!this.chkTrimRoot.Checked)
                            this.dgvLog.Rows.Add(
                                LocSync.LocSyncStatusString[ee.status],
                                LocSync.LocSyncActionString[ee.action],
                                ee.source,
                                ee.sourceLoc.RootLoc,
                                ee.target,
                                ee.targetLoc.RootLoc
                                );
                        else
                            this.dgvLog.Rows.Add(
                                LocSync.LocSyncStatusString[ee.status],
                                LocSync.LocSyncActionString[ee.action],
                                ee.source.StartsWith(ee.sourceLoc.RootLoc) ?
                                    ee.source.Substring(ee.sourceLoc.RootLoc.Length) :
                                    ee.source,
                                ee.sourceLoc.RootLoc,
                                ee.target.Substring(ee.targetLoc.RootLoc.Length),
                                ee.targetLoc.RootLoc
                                );

                        ++countStatus[ee.status];
                        ++countAction[ee.action];

                        this.dgvLog.FirstDisplayedScrollingRowIndex = this.dgvLog.Rows.Count - 1;
                        this.lblStat.Text = string.Format
                        ("Total: Error {0,6},    Warning {1,6},    OK {2,6}\n" +
                         "Ignored: {3,10},    Replace: {4, 10},    File Added: {5, 10},    Folder Added: {6, 10},    File Deleted: {7, 10},    Folder Deleted: {8, 10}",
                         countStatus[LocSyncStatus.EM_ERROR], countStatus[LocSyncStatus.EM_WARNING], countStatus[LocSyncStatus.EM_OK],
                         countAction[LocSyncAction.EM_IGNORE_TIMESTAMP], countAction[LocSyncAction.EM_REPLACE], countAction[LocSyncAction.EM_ADD_FILE],
                         countAction[LocSyncAction.EM_ADD_FOLDER], countAction[LocSyncAction.EM_DELETE_FILE], countAction[LocSyncAction.EM_DELETE_FOLDER]
                         );
                    });
                };

                this.btnGo.Text = "Stop";
                syncing = true;

                trdSync = new Thread(new ThreadStart(
                    () =>
                    {
                        currentTask.Start();
                        EndSync();
                    }));

                trdSync.Start();
            }
            else
            {
                syncing = false;
                currentTask.Stop();
                new Thread(new ThreadStart(
                    () =>
                    {
                        trdSync.Join();
                        EndSync();
                    }
                )).Start();
            }
        }

        private void btnGo_Click2(object sender, EventArgs e)
        {
            if (trdSync == null)
            {
                List<LocSync> tasks = new List<LocSync>();

                Dictionary<LocSyncStatus, int> countStatus = new Dictionary<LocSyncStatus, int>()
                        {
                            { LocSyncStatus.EM_ERROR, 0 },
                            { LocSyncStatus.EM_WARNING, 0 },
                            { LocSyncStatus.EM_OK, 0 },
                        };

                Dictionary<LocSyncAction, int> countAction = new Dictionary<LocSyncAction, int>()
                        {
                            { LocSyncAction.EM_IGNORE_TIMESTAMP, 0 },
                            { LocSyncAction.EM_REPLACE, 0 },
                            { LocSyncAction.EM_ADD_FILE, 0 },
                            { LocSyncAction.EM_ADD_FOLDER, 0 },
                            { LocSyncAction.EM_DELETE_FILE, 0 },
                            { LocSyncAction.EM_DELETE_FOLDER, 0 },
                        };

                foreach (DataGridViewRow row in this.dgvTask.Rows)
                {
                    if (row.Cells["clmTaskEnable"].Value != null &&
                        (bool)(row.Cells["clmTaskEnable"] as DataGridViewCheckBoxCell).Value == true)
                    {
                        LocBase locSource = LocBase.GetInstance(row.Cells["clmTaskSource"].Value.ToString());
                        LocBase locTarget = LocBase.GetInstance(row.Cells["clmTaskTarget"].Value.ToString());

                        if (locSource == null)
                        {
                            MessageBox.Show("Unsupported source path");
                            this.dgvTask.ClearSelection();
                            row.Selected = true;
                            return;
                        }

                        if (locTarget == null)
                        {
                            MessageBox.Show("Unsupported target path");
                            this.dgvTask.ClearSelection();
                            row.Selected = true;
                            return;
                        }

                        if (locSource.RootLoc.ToLower().Contains(locTarget.RootLoc.ToLower()) ||
                            locTarget.RootLoc.ToLower().Contains(locSource.RootLoc.ToLower()))
                        {
                            MessageBox.Show("Nested path");
                            this.dgvTask.ClearSelection();
                            row.Selected = true;
                            return;
                        }

                        LocSyncDirection dirc = LocSyncDirection.EM_TO;

                        switch (row.Cells["clmTaskDirc"].Value.ToString().Trim())
                        {
                            case "=>":
                                dirc = LocSyncDirection.EM_TO;
                                break;
                            case "<=":
                                dirc = LocSyncDirection.EM_FROM;
                                break;
                            case "<=>":
                                dirc = LocSyncDirection.EM_BI;
                                break;
                        }

                        if (dirc == LocSyncDirection.EM_BI)
                        {
                            MessageBox.Show("Bi-direction sync not implemented");
                            this.dgvTask.ClearSelection();
                            row.Selected = true;
                            return;
                        }
                        
                        List<string> settings = row.Cells["clmTaskSetting"].Value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => t.Trim().ToLower()).Distinct().ToList();

                        LocSync sync = new LocSync(locSource, locTarget, dirc);
                        
                        sync.Dryrun = chkDryRun.Checked;
                        sync.AddFile = settings.Contains("addfile");
                        sync.DelFile = settings.Contains("delfile");
                        sync.AddFolder = settings.Contains("addfolder");
                        sync.DelFolder = settings.Contains("delfolder");
                        sync.SyncTimestamp = settings.Contains("synctimestamp");
                        sync.SyncProperties = settings.Contains("syncproperties");

                        if (sync.SyncTimestamp || sync.SyncProperties)
                        {
                            MessageBox.Show("timestamp or properties sync is not implemented yet");
                            this.dgvTask.ClearSelection();
                            row.Selected = true;
                            return;
                        }

                        sync.SyncUpdateEvent += (senderr, ee) =>
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                if (!this.chkTrimRoot.Checked)
                                    this.dgvLog.Rows.Add(
                                        LocSync.LocSyncStatusString[ee.status],
                                        LocSync.LocSyncActionString[ee.action],
                                        ee.source,
                                        ee.sourceLoc.RootLoc,
                                        ee.target,
                                        ee.targetLoc.RootLoc
                                        );
                                else
                                    this.dgvLog.Rows.Add(
                                        LocSync.LocSyncStatusString[ee.status],
                                        LocSync.LocSyncActionString[ee.action],
                                        ee.source.StartsWith(ee.sourceLoc.RootLoc) ?
                                            ee.source.Substring(ee.sourceLoc.RootLoc.Length) :
                                            ee.source,
                                        ee.sourceLoc.RootLoc,
                                        ee.target.Substring(ee.targetLoc.RootLoc.Length),
                                        ee.targetLoc.RootLoc
                                        );

                                this.dgvLog.FirstDisplayedScrollingRowIndex = this.dgvLog.Rows.Count - 1;


                                ++countStatus[ee.status];
                                ++countAction[ee.action];
                                
                                this.lblStat.Text = string.Format
                                ("Total: Error {0,-6},    Warning {1,-6},    OK {2,-6}\n" +
                                    "Ignored: {3,-6},    Replace: {4, -6},    File Added: {5, -6},    Folder Added: {6, -6},    File Deleted: {7, -6},    Folder Deleted: {8, -6}",
                                    countStatus[LocSyncStatus.EM_ERROR], countStatus[LocSyncStatus.EM_WARNING], countStatus[LocSyncStatus.EM_OK],
                                    countAction[LocSyncAction.EM_IGNORE_TIMESTAMP], countAction[LocSyncAction.EM_REPLACE], countAction[LocSyncAction.EM_ADD_FILE],
                                    countAction[LocSyncAction.EM_ADD_FOLDER], countAction[LocSyncAction.EM_DELETE_FILE], countAction[LocSyncAction.EM_DELETE_FOLDER]
                                    );
                            });
                        };

                        tasks.Add(sync);
                    }
                }

                if (tasks.Count == 0)
                    return;

                if (MessageBox.Show("Good to go?", "Folder Sync", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                saveTasks();

                this.dgvLog.Rows.Clear();
                this.lblStat.Text = "";
                syncing = true;
                this.btnGo.Text = "Stop";

                trdSync = new Thread(new ThreadStart(
                    () =>
                    {
                        foreach (LocSync onesync in tasks)
                        {
                            if (!syncing)
                                break;

                            currentTask = onesync;
                            currentTask.Start();
                        }

                        EndSync();
                    }));

                trdSync.Start();
            }
            else
            {
                syncing = false;
                currentTask.Stop();
                new Thread(new ThreadStart(
                    () =>
                    {
                        trdSync.Join();
                        EndSync();
                    }
                )).Start();
            }
        }

        private void btnDirect_Click(object sender, EventArgs e)
        {
            switch (this.btnDirect.Text)
            {
                case "=>":
                    this.btnDirect.Text = "<=";
                    break;
                case "<=":
                    this.btnDirect.Text = "<=>";
                    break;
                case "<=>":
                    this.btnDirect.Text = "=>";
                    break;
            }
        }
        
        private void txtSource_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderSelectDialog folderBrowserDialog = new FolderSelectDialog();
            folderBrowserDialog.Title = "Choose source folder";

            if (folderBrowserDialog.ShowDialog(this.Handle))
            {
                this.txtSource.Text = folderBrowserDialog.FileName;
            }
        }

        private void txtTarget_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderSelectDialog folderBrowserDialog = new FolderSelectDialog();
            folderBrowserDialog.Title = "Choose target folder";

            if (folderBrowserDialog.ShowDialog(this.Handle))
            {
                this.txtTarget.Text = folderBrowserDialog.FileName;
            }
        }

        private void chkTrimRoot_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvLog.Rows)
            {
                if (!this.chkTrimRoot.Checked)
                {
                    if (!row.Cells["clmSource"].Value.ToString().StartsWith(row.Cells["clmSourceRoot"].Value.ToString()))
                        row.Cells["clmSource"].Value = row.Cells["clmSourceRoot"].Value.ToString() + row.Cells["clmSource"].Value.ToString();

                    if (!row.Cells["clmTarget"].Value.ToString().StartsWith(row.Cells["clmTargetRoot"].Value.ToString()))
                        row.Cells["clmTarget"].Value = row.Cells["clmTargetRoot"].Value.ToString() + row.Cells["clmTarget"].Value.ToString();
                }
                else
                {
                    if (row.Cells["clmSource"].Value.ToString().StartsWith(row.Cells["clmSourceRoot"].Value.ToString()))
                        row.Cells["clmSource"].Value = row.Cells["clmSource"].Value.ToString().Substring(row.Cells["clmSourceRoot"].Value.ToString().Length);

                    if (row.Cells["clmTarget"].Value.ToString().StartsWith(row.Cells["clmTargetRoot"].Value.ToString()))
                        row.Cells["clmTarget"].Value = row.Cells["clmTarget"].Value.ToString().Substring(row.Cells["clmTargetRoot"].Value.ToString().Length);
                }
            }
        }


        bool syncing = true;
        Thread trdSync = null;
        LocSync currentTask = null;
        string taskFileName = @"FolderSync.task";
        char taskFieldDelim = '\x1234';

        private void EndSync()
        {
            currentTask = null;
            trdSync = null;
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate
                {
                    this.btnGo.Text = "Go";
                });
            else
                this.btnGo.Text = "Go";
        }

        private void dgvTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == this.dgvTask.Columns["clmTaskSource"].Index
                 || e.ColumnIndex == this.dgvTask.Columns["clmTaskTarget"].Index)
                {
                    FolderSelectDialog folderBrowserDialog = new FolderSelectDialog();
                    folderBrowserDialog.Title = "Choose folder to deploy";

                    if (folderBrowserDialog.ShowDialog(this.Handle))
                    {
                        string target = folderBrowserDialog.FileName;
                        this.dgvTask[e.ColumnIndex, e.RowIndex].Value = target;
                        this.dgvTask.EndEdit();
                    }
                }


                if (e.ColumnIndex == this.dgvTask.Columns["clmTaskSetting"].Index)
                {
                    string setting = "";
                    if (this.dgvTask[e.ColumnIndex, e.RowIndex].Value != null)
                        setting = this.dgvTask[e.ColumnIndex, e.RowIndex].Value.ToString();

                    SyncSetting setup = new SyncSetting(setting);
                    if (setup.ShowDialog() == DialogResult.OK)
                        this.dgvTask[e.ColumnIndex, e.RowIndex].Value = setup.Value;
                    setup.Dispose();
                    this.dgvTask.EndEdit();
                }
            }
        }
        
        private void dgvTask_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvTask_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.dgvTask["clmTaskEnable", e.RowIndex - 1].Value =
                this.dgvTask["clmTaskEnable", e.RowIndex - 1].Value==null?
                true : this.dgvTask["clmTaskEnable", e.RowIndex - 1].Value;

            this.dgvTask["clmTaskSource", e.RowIndex - 1].Value =
                this.dgvTask["clmTaskSource", e.RowIndex - 1].Value == null?
                ""   : this.dgvTask["clmTaskSource", e.RowIndex - 1].Value;

            this.dgvTask["clmTaskDirc", e.RowIndex - 1].Value =
                this.dgvTask["clmTaskDirc", e.RowIndex - 1].Value == null?
                "=>" : this.dgvTask["clmTaskDirc", e.RowIndex - 1].Value;

            this.dgvTask["clmTaskTarget", e.RowIndex - 1].Value =
                this.dgvTask["clmTaskTarget", e.RowIndex - 1].Value == null ?
                ""  : this.dgvTask["clmTaskTarget", e.RowIndex - 1].Value;

            this.dgvTask["clmTaskSetting", e.RowIndex - 1].Value =
                this.dgvTask["clmTaskSetting", e.RowIndex - 1].Value == null ?
                "AddFile,AddFolder,DelFile,DelFolder," : this.dgvTask["clmTaskSetting", e.RowIndex - 1].Value;
        }

        private void saveTasks()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(taskFileName, false))
            {
                foreach (DataGridViewRow row in this.dgvTask.Rows)
                {
                    if (row.Index < this.dgvTask.RowCount - 1)
                    {
                        string line = "";
                        line += row.Cells["clmTaskSource"].Value.ToString() + taskFieldDelim;
                        line += row.Cells["clmTaskDirc"].Value.ToString() + taskFieldDelim;
                        line += row.Cells["clmTaskTarget"].Value.ToString() + taskFieldDelim;
                        line += row.Cells["clmTaskSetting"].Value.ToString() + taskFieldDelim;
                        file.WriteLine(line);
                    }
                }
            }
        }

        private void loadTasks()
        {
            if (File.Exists(taskFileName))
            {
                foreach (string line in File.ReadAllLines(taskFileName))
                {
                    try
                    {
                        List<string> fields = line.Split(new char[] { taskFieldDelim }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        this.dgvTask.Rows.Add(
                            true,
                            fields[0],
                            fields[1],
                            fields[2],
                            fields[3]
                            );
                    }
                    catch { }
                }
            }
        }

        private void lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/lzp729/FolderSync");
        }
    }
}
