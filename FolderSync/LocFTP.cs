using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinSCP;
using System.Linq;

namespace FolderSync
{
    class LocFTP : LocBase
    {
        static private char _locDelim = '/';
        static private string _ftpURLPreifx = "ftp://";
        static private string _urlPattern = @"^" + _ftpURLPreifx + "(?<serverpath>.+?)/(?<rootpath>.+)*$";

        public static bool CheckPath(string path)
        {
            path = path.EndsWith(_locDelim.ToString()) ? path : path + _locDelim;
            return Regex.Matches(path, _urlPattern, RegexOptions.IgnoreCase).Count > 0;
        }

        private SessionOptions _sessionOptions = new SessionOptions();
        public Session _session = new Session();

        public LocFTP(string path)
        {
            this._originalPath = path;
            this.ResetLoc();
        }


        public override char LocDelim
        {
            get { return LocFTP._locDelim; }
            set { LocFTP._locDelim = value; }
        }
        public override string CurrentURL
        {
            get { return _ftpURLPreifx + _sessionOptions.HostName + LocDelim + RootLoc + LocDelim + CurrentURN; }
        }


        public override void ResetLoc()
        {
            string path = this._originalPath;
            path = path.EndsWith(LocDelim.ToString()) ? path : path + LocDelim;

            MatchCollection mc = Regex.Matches(path, _urlPattern, RegexOptions.IgnoreCase);
            _sessionOptions.ParseUrl(_ftpURLPreifx + mc[0].Groups["serverpath"].ToString());

            this.RootLoc = mc[0].Groups["rootpath"].ToString().Trim(LocDelim);
            this.CurrentURN = "";

            this.GetFiles();
        }
        public override void StepIn(string loc)
        {
            CurrentURN += LocDelim + loc;
            CurrentURN = CurrentURN.Trim(new char[] { LocDelim });
        }
        public override void StepOut()
        {
            CurrentURN = CurrentURN.Substring(0, CurrentURN.LastIndexOf(LocDelim) + 1).Trim(new char[] { LocDelim });
        }


        public override List<string> GetFiles()
        {
            if (!_session.Opened)
                _session.Open(_sessionOptions);

            List<string> files = new List<string>();

            RemoteDirectoryInfo info = _session.ListDirectory(LocDelim + RootLoc + LocDelim + CurrentURN);
            foreach (RemoteFileInfo file in info.Files)
            {
                if (!file.IsDirectory)
                    files.Add(file.Name);
            }

            return files;
        }

        public override List<string> GetFolders()
        {
            if (!_session.Opened)
                _session.Open(_sessionOptions);

            List<string> folders = new List<string>();

            RemoteDirectoryInfo info = _session.ListDirectory(LocDelim + RootLoc + LocDelim + CurrentURN);
            foreach (RemoteFileInfo folder in info.Files)
            {
                if (folder.IsDirectory && folder.Name != ".." && folder.Name != ".")
                    folders.Add(folder.Name);
            }

            return folders;
        }

        public override void CreateFolder(string subLoc)
        {
            if (!_session.Opened)
                _session.Open(_sessionOptions);
            
            _session.CreateDirectory(LocDelim + RootLoc + LocDelim + CurrentURN + LocDelim + subLoc);
        }

        public override void RemoveFolder(string subLoc)
        {
            if (!_session.Opened)
                _session.Open(_sessionOptions);

            _session.RemoveFiles(LocDelim + RootLoc + LocDelim + CurrentURN + LocDelim + subLoc);
        }

        public override void RemoveFile(string fileName)
        {
            if (!_session.Opened)
                _session.Open(_sessionOptions);

            _session.RemoveFiles(LocDelim + RootLoc + LocDelim + CurrentURN + LocDelim + fileName);
        }

        public override DateTime GetFileLastUpdateTimestamp(string fileName)
        {
            if (!_session.Opened)
                _session.Open(_sessionOptions);

            DateTime dt = _session.GetFileInfo(LocDelim + RootLoc + LocDelim + CurrentURN + LocDelim + fileName).LastWriteTime;
            return dt.AddTicks(0 - dt.Ticks % TimeSpan.TicksPerSecond);
        }
    }
}
