using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FolderSync
{
    abstract class LocBase
    {
        static public LocBase GetInstance(string path)
        {
            if (LocLocal.CheckPath(path))
                return new LocLocal(path);

            if (LocFTP.CheckPath(path))
                return new LocFTP(path);

            return null;
        }

        static public void SyncFile(LocBase source, LocBase target, string fileName)
        {
            if (source is LocLocal)
            {
                if (target is LocLocal)
                {
                    File.Copy(source.CurrentURL + source.LocDelim + fileName,
                        target.CurrentURL + source.LocDelim + fileName,true);
                    return;
                }

                if (target is LocFTP)
                {
                    LocFTP ftp = target as LocFTP;

                    ftp._session.PutFiles(
                        source.CurrentURL + source.LocDelim + fileName,
                        ftp.LocDelim + ftp.RootLoc + ftp.LocDelim + ftp.CurrentLoc + ftp.LocDelim + fileName,
                        false).Check();
                    return;
                }
            }

            throw new NotImplementedException();
        }


        protected string _originalPath = "";
        protected string _rootLocation = "";
        protected string _currentLocation = "";

        abstract public char LocDelim
        { get; set; }
        public virtual string RootLoc
        {
            get { return _rootLocation; }
            set { _rootLocation = value; }
        }
        public virtual string CurrentLoc
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }
        public virtual string CurrentURL
        {
            get { return RootLoc + this.LocDelim + CurrentLoc; }
        }
        public virtual string OriginalPath
        {
            get { return _originalPath; }
        }


        abstract public void ResetLoc();
        abstract public void StepIn(string loc);
        abstract public void StepOut();


        abstract public List<string> GetFiles();
        abstract public List<string> GetFolders();
        abstract public void CreateFolder(string subLoc);
        abstract public void RemoveFolder(string subLoc);
        abstract public void RemoveFile(string fileName);
        abstract public DateTime GetFileLastUpdateTimestamp(string fileName);

    }
}
