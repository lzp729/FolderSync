using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FolderSync
{
    class LocLocal : LocBase
    {
        static private class NativeMethods
        {
            [DllImport("Shlwapi.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool PathIsUNC(String pszPath);
            [DllImport("Shlwapi.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool PathIsNetworkPath(String pszPath);
            static public bool IsLocalPath(String path)
            {
                if (!PathIsUNC(path))
                {
                    return !PathIsNetworkPath(path);
                }

                return false;
            }
        }


        static private char _locDelim = Path.DirectorySeparatorChar;
        static public bool CheckPath(string path)
        {
            return Directory.Exists(path);
        }

        static public void FileCopy(LocSync syncControl, string source, string destination, int bufsize = 524288) //512K
        {
            long file_size = new System.IO.FileInfo(source).Length;
            int array_length = bufsize;
            byte[] dataArray = new byte[array_length];

            string destination_mask = destination + ".ing";

            using (FileStream fsread = new FileStream
            (source, FileMode.Open, FileAccess.Read, FileShare.None, array_length))
            {
                using (BinaryReader bwread = new BinaryReader(fsread))
                {
                    using (FileStream fswrite = new FileStream
                    (destination_mask, FileMode.Create, FileAccess.Write, FileShare.None, array_length))
                    {
                        using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                        {
                            long copied_size = 0;
                            int retris = 0;
                            while (copied_size < file_size)
                            {
                                if (!syncControl.syncing)
                                    throw new IOException("Stop to sync file");

                                int read = bwread.Read(dataArray, 0, array_length);
                                copied_size += read;
                                if (0 == read)
                                {
                                    if (copied_size == file_size)
                                        break;
                                    else
                                    {
                                        if (retris == 10)
                                            throw new IOException("Failed to read source");
                                        ++retris;
                                        Thread.Sleep(500);
                                    }
                                }
                                else
                                    retris = 0;
                                bwwrite.Write(dataArray, 0, read);
                                bwwrite.Flush();
                            }
                        }
                    }
                }
            }
            if (File.Exists(destination))
                File.Delete(destination);
            File.Move(destination_mask, destination);

            if (syncControl.SyncTimestamp)
            {
                try
                {
                    DateTime dt = File.GetLastWriteTime(source);
                    File.SetLastWriteTime(destination, dt);
                }
                catch
                {
                    throw new IOException("Failed to sync timestamp");
                }
            }
        }

        public LocLocal(string path)
        {
            this._originalPath = path;
            this.ResetLoc();
        }
        
        
        public override char LocDelim
        {
            get { return LocLocal._locDelim; }
            set { LocLocal._locDelim = value; }
        }
        
        
        public override void ResetLoc()
        {
            this.RootLoc = Path.GetFullPath(_originalPath);
            this.CurrentURN = "";
        }
        public override void StepIn(string loc)
        {
            CurrentURN += LocDelim + loc;
            CurrentURN = CurrentURN.Trim( new char[] { LocDelim});
        }

        public override void StepOut()
        {
            CurrentURN = CurrentURN.Substring(0, CurrentURN.LastIndexOf(LocDelim) + 1).Trim(new char[] { LocDelim });
        }


        public override List<string> GetFiles()
        {
            return Directory.GetFiles(CurrentURL).Select((t) => { return Path.GetFileName(t); }).ToList();
        }

        public override List<string> GetFolders()
        {
            return Directory.GetDirectories(CurrentURL).Select((t) => { return Path.GetFileName(t); }).ToList();
        }

        public override void CreateFolder(string subLoc)
        {
            if (Directory.Exists(CurrentURL))
                Directory.CreateDirectory(CurrentURL + LocDelim + subLoc);
        }

        private void _DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }

                foreach (string folder in Directory.GetDirectories(path))
                {
                    _DeleteFolder(folder);
                }

                Directory.Delete(path);
            }
        }

        public override void RemoveFolder(string subLoc)
        {
            if (Directory.Exists(CurrentURL + LocDelim + subLoc))
                _DeleteFolder(CurrentURL + LocDelim + subLoc);
        }
        
        public override void RemoveFile(string fileName)
        {
            if (File.Exists(CurrentURL + LocDelim + fileName))
                File.Delete(CurrentURL + LocDelim + fileName);
        }

        public override DateTime GetFileLastUpdateTimestamp(string fileName)
        {
            DateTime dt = File.GetLastWriteTime(CurrentURL + LocDelim + fileName);
            return dt.AddTicks(0 - dt.Ticks % TimeSpan.TicksPerSecond);
        }
    }
}
