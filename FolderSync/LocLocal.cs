using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
            //if (LocLocal.NativeMethods.IsLocalPath(_originalPath))
            //    this.LocDelim = Path.DirectorySeparatorChar;
            //else
            //    this.LocDelim = Path.AltDirectorySeparatorChar;
            this.RootLoc = Path.GetFullPath(_originalPath);
            this.CurrentLoc = "";
        }
        public override void StepIn(string loc)
        {
            CurrentLoc += LocDelim + loc;
            CurrentLoc = CurrentLoc.Trim( new char[] { LocDelim});
        }
        public override void StepOut()
        {
            CurrentLoc = CurrentLoc.Substring(0, CurrentLoc.LastIndexOf(LocDelim) + 1).Trim(new char[] { LocDelim });
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
