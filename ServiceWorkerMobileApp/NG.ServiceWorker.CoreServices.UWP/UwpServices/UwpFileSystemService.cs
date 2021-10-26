using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NG.ServiceWorker.CoreServices.UwpServices
{
    public class UwpFileSystemService : IFileSystemService
    {
        #region Base

        /// <summary>The local path, where databases and synced files should be kept.</summary>
        private string m_rootDirpath = null;

        /// <summary>The database directory.</summary>
        private string m_databaseDirpath = null;
        /// <summary>The temp directory.</summary>
        private string m_tempDirpath = null;

        /// <summary>Constructor.</summary>
        public UwpFileSystemService()
        {
            m_rootDirpath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
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
                    m_databaseDirpath = System.IO.Path.Combine(m_rootDirpath, "SwData");
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
                    m_tempDirpath = System.IO.Path.Combine(m_rootDirpath, "SwTemp");
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
            return new Dictionary<string, string> { { "Documents", m_rootDirpath } };
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
