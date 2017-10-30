using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FolderSync
{
    enum LocSyncDirection
    {
        EM_TO,
        EM_FROM,
        EM_BI
    }
    enum LocSyncStatus
    {
        EM_ERROR,
        EM_WARNING,
        EM_OK
    }

    enum LocSyncAction
    {
        EM_IGNORE_TIMESTAMP,
        EM_REPLACE,
        EM_ADD_FILE,
        EM_ADD_FOLDER,
        EM_DELETE_FILE,
        EM_DELETE_FOLDER

    }

    class LocSyncEventArgs : EventArgs
    {
        public LocSyncStatus status = LocSyncStatus.EM_ERROR;
        public LocSyncAction action = LocSyncAction.EM_IGNORE_TIMESTAMP;
        public string source = "";
        public string target = "";
        public LocBase sourceLoc = null;
        public LocBase targetLoc = null;
    }

    delegate void LocSyncUpdateHandler(object sender, LocSyncEventArgs e);

    class LocSync
    {
        static public Dictionary<LocSyncStatus, string> LocSyncStatusString = new Dictionary<LocSyncStatus, string>()
        {
            { LocSyncStatus.EM_ERROR,   "Error"},
            { LocSyncStatus.EM_WARNING, "Warning"},
            { LocSyncStatus.EM_OK,      "OK"},
        };
        
        static public Dictionary<LocSyncAction, string> LocSyncActionString = new Dictionary<LocSyncAction, string>()
        {
            { LocSyncAction.EM_IGNORE_TIMESTAMP,  "Ignored Newer"},
            { LocSyncAction.EM_REPLACE,           "Replaced"},
            { LocSyncAction.EM_ADD_FILE,          "File Added"},
            { LocSyncAction.EM_ADD_FOLDER,        "Folder Added"},
            { LocSyncAction.EM_DELETE_FILE,       "File Deleted"},
            { LocSyncAction.EM_DELETE_FOLDER,     "Folder Deleted"},
        };
        
        public event LocSyncUpdateHandler SyncUpdateEvent;
        public bool Dryrun = true;
        public bool AddFile = true;
        public bool AddFolder = true;
        public bool DelFile = true;
        public bool DelFolder = true;
        public bool SyncTimestamp = true;
        public bool SyncProperties = true;

        
        LocBase source = null;
        LocBase target = null;
        LocSyncDirection direction = LocSyncDirection.EM_TO;
        bool syncing = false;

        public LocSync(LocBase s, LocBase t, LocSyncDirection drc)
        {
            source = s;
            target = t;
            direction = drc;
        }

        public void Start()
        {
            if (source == null || target == null)
                return;

            source.ResetLoc();
            target.ResetLoc();
            syncing = true;

            switch (direction)
            {
                case LocSyncDirection.EM_TO:
                    Sync();
                    break;
                case LocSyncDirection.EM_FROM:
                    LocBase t = source;
                    source = target;
                    target = t;
                    Sync();
                    break;
                case LocSyncDirection.EM_BI:
                    return;
            }
        }

        public void Stop()
        {
            syncing = false;
        }

        private void Sync()
        {
            if (!syncing)
                return;

            List<string> sourceFiles = new List<string>();
            List<string> targetFiles = new List<string>();

            sourceFiles = source.GetFiles();
            targetFiles = target.GetFiles();
            
            foreach (string sourceFile in sourceFiles)
            {
                if (!syncing)
                    break;

                // target location contains the file
                if (targetFiles.Contains(sourceFile, StringComparer.OrdinalIgnoreCase))
                {
                    DateTime dt = target.GetFileLastUpdateTimestamp(sourceFile);
                    // target file is older
                    if (source.GetFileLastUpdateTimestamp(sourceFile)
                        > dt)
                    {
                        try
                        {
                            if (!Dryrun)
                                LocBase.SyncFile(source, target, sourceFile);

                            SyncUpdateEvent(this, new LocSyncEventArgs()
                            {
                                status = LocSyncStatus.EM_OK,
                                action = LocSyncAction.EM_REPLACE,
                                source = Regex.Replace(source.CurrentURL + source.LocDelim + sourceFile, source.LocDelim + "+", source.LocDelim.ToString()),
                                target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFile, target.LocDelim + "+", target.LocDelim.ToString()),
                                sourceLoc = source,
                                targetLoc = target
                            });
                        }
                        catch(Exception ex)
                        {
                            SyncUpdateEvent(this, new LocSyncEventArgs()
                            {
                                status = LocSyncStatus.EM_ERROR,
                                action = LocSyncAction.EM_REPLACE,
                                source = Regex.Replace(source.CurrentURL + source.LocDelim + sourceFile, source.LocDelim + "+", source.LocDelim.ToString()),
                                target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFile, target.LocDelim + "+", target.LocDelim.ToString()),
                                sourceLoc = source,
                                targetLoc = target
                            });
                        }
                    }
                    // target file is newer
                    else
                    {
                        SyncUpdateEvent(this, new LocSyncEventArgs()
                        {
                            status = LocSyncStatus.EM_WARNING,
                            action = LocSyncAction.EM_IGNORE_TIMESTAMP,
                            source = Regex.Replace(source.CurrentURL + source.LocDelim + sourceFile, source.LocDelim + "+", source.LocDelim.ToString()),
                            target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFile, target.LocDelim + "+", target.LocDelim.ToString()),
                            sourceLoc = source,
                            targetLoc = target
                        });
                    }
                }
                // target file not exists
                else
                {
                    if (AddFile)
                    {
                        try
                        {
                            if (!Dryrun)
                                LocBase.SyncFile(source, target, sourceFile);

                            SyncUpdateEvent(this, new LocSyncEventArgs()
                            {
                                status = LocSyncStatus.EM_OK,
                                action = LocSyncAction.EM_ADD_FILE,
                                source = Regex.Replace(source.CurrentURL + source.LocDelim + sourceFile, source.LocDelim + "+", source.LocDelim.ToString()),
                                target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFile, target.LocDelim + "+", target.LocDelim.ToString()),
                                sourceLoc = source,
                                targetLoc = target
                            });
                        }
                        catch
                        {
                            SyncUpdateEvent(this, new LocSyncEventArgs()
                            {
                                status = LocSyncStatus.EM_ERROR,
                                action = LocSyncAction.EM_ADD_FILE,
                                source = Regex.Replace(source.CurrentURL + source.LocDelim + sourceFile, source.LocDelim + "+", source.LocDelim.ToString()),
                                target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFile, target.LocDelim + "+", target.LocDelim.ToString()),
                                sourceLoc = source,
                                targetLoc = target
                            });
                        }
                    }
                }
                targetFiles.Remove(sourceFile);
            }


            if (!syncing)
                return;

            // remove not exists source folder from target locations
            if (DelFile)
            {
                foreach (string targetFile in targetFiles)
                {
                    if (!syncing)
                        break;
                    try
                    {
                        if (!Dryrun)
                            target.RemoveFile(targetFile);

                        SyncUpdateEvent(this, new LocSyncEventArgs()
                        {
                            status = LocSyncStatus.EM_OK,
                            action = LocSyncAction.EM_DELETE_FILE,
                            source = "",
                            target = Regex.Replace(target.CurrentURL + target.LocDelim + targetFile, target.LocDelim + "+", target.LocDelim.ToString()),
                            sourceLoc = source,
                            targetLoc = target
                        });
                    }
                    catch
                    {
                        SyncUpdateEvent(this, new LocSyncEventArgs()
                        {
                            status = LocSyncStatus.EM_ERROR,
                            action = LocSyncAction.EM_DELETE_FILE,
                            source = "",
                            target = Regex.Replace(target.CurrentURL + target.LocDelim + targetFile, target.LocDelim + "+", target.LocDelim.ToString()),
                            sourceLoc = source,
                            targetLoc = target
                        });
                    }
                }
            }

            List<string> sourceFolders = new List<string>();
            List<string> targetFolders = new List<string>();

            if (!syncing)
                return;

            sourceFolders = source.GetFolders();
            targetFolders = target.GetFolders();

            foreach (string sourceFolder in sourceFolders)
            {
                if (!syncing)
                    break;

                if (targetFolders.Contains(sourceFolder, StringComparer.OrdinalIgnoreCase)) // to ignore case
                {
                    target.StepIn(sourceFolder);
                    source.StepIn(sourceFolder);
                    Sync();
                    source.StepOut();
                    target.StepOut();
                }
                else
                {
                    if (AddFolder)
                    {
                        try
                        {
                            target.CreateFolder(sourceFolder);
                            SyncUpdateEvent(this, new LocSyncEventArgs()
                            {
                                status = LocSyncStatus.EM_OK,
                                action = LocSyncAction.EM_ADD_FOLDER,
                                source = "",
                                target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFolder, target.LocDelim + "+", target.LocDelim.ToString()),
                                sourceLoc = source,
                                targetLoc = target
                            });
                        }
                        catch
                        {
                            SyncUpdateEvent(this, new LocSyncEventArgs()
                            {
                                status = LocSyncStatus.EM_ERROR,
                                action = LocSyncAction.EM_ADD_FOLDER,
                                source = "",
                                target = Regex.Replace(target.CurrentURL + target.LocDelim + sourceFolder, target.LocDelim + "+", target.LocDelim.ToString()),
                                sourceLoc = source,
                                targetLoc = target
                            });
                        }

                        target.StepIn(sourceFolder);
                        source.StepIn(sourceFolder);
                        Sync();
                        source.StepOut();
                        target.StepOut();
                    }
                }

                targetFolders.Remove(sourceFolder);
            }

            if (!syncing)
                return;

            if (DelFolder)
            {
                foreach (string targetFolder in targetFolders)
                {
                    if (!syncing)
                        break;

                    try
                    {
                        if (!Dryrun)
                            target.RemoveFolder(targetFolder);

                        SyncUpdateEvent(this, new LocSyncEventArgs()
                        {
                            status = LocSyncStatus.EM_OK,
                            action = LocSyncAction.EM_DELETE_FOLDER,
                            source = "",
                            target = Regex.Replace(target.CurrentURL + target.LocDelim + targetFolder, target.LocDelim + "+", target.LocDelim.ToString()),
                            sourceLoc = source,
                            targetLoc = target
                        });
                    }
                    catch
                    {
                        SyncUpdateEvent(this, new LocSyncEventArgs()
                        {
                            status = LocSyncStatus.EM_ERROR,
                            action = LocSyncAction.EM_DELETE_FOLDER,
                            source = "",
                            target = Regex.Replace(target.CurrentURL + target.LocDelim + targetFolder, target.LocDelim + "+", target.LocDelim.ToString()),
                            sourceLoc = source,
                            targetLoc = target
                        });
                    }
                }
            }
        }
    }
}
