using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.AndroidServices
{
    public class AndroidFileSystemService : IFileSystemService
    {
        #region Base

        /// <summary>The documents directory.</summary>
        private string m_documentsDirpath = null;
        /// <summary>The database directory.</summary>
        private string m_databaseDirpath = null;
        /// <summary>The temp directory.</summary>
        private string m_tempDirpath = null;

        /// <summary>Constructor.</summary>
        public AndroidFileSystemService()
        {
            m_documentsDirpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        #endregion

        #region IFileSystemService

        /// <summary>Getter for the directory to put database files in.</summary>
        public string DatabaseDirpath
        {
            get
            {
                if (string.IsNullOrEmpty(m_databaseDirpath))
                {
                    m_databaseDirpath = System.IO.Path.Combine(m_documentsDirpath, "SwData");
                    if (System.IO.Directory.Exists(m_databaseDirpath) == false)
                    {
                        System.IO.Directory.CreateDirectory(m_databaseDirpath);
                    }
                }
                return m_databaseDirpath;
            }
        }

        /// <summary>Getter for the directory to put temp files in.</summary>
        public string TempDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(m_tempDirpath))
                {
                    m_tempDirpath = System.IO.Path.Combine(m_documentsDirpath, "SwTemp");
                    if (System.IO.Directory.Exists(m_tempDirpath) == false)
                    {
                        System.IO.Directory.CreateDirectory(m_tempDirpath);
                    }
                }
                return m_tempDirpath;
            }
        }

        /// <summary>Getter for the directories to show in the debug menu.</summary>
        public Dictionary<string, string> GetRootDirectories()
        {
            return new Dictionary<string, string> { { "Documents", m_documentsDirpath } };
        }

        /// <summary>Getter for whether the given file will be backed up to the platform's cloud.</summary>
        public bool GetFileBackedUp(string filepath)
        {
            return false;
        }

        /// <summary>Setter for whether a file should be backed up.</summary>
        public void SetFileBackedup(string filepath, bool shouldBackup)
        {
            //
        }

        #endregion
    }
}
