using System;
using System.Collections.Generic;
using System.IO;

using Foundation;

namespace NG.ServiceWorker.CoreServices.iOSServices
{
    public class iOSFileSystemService : IFileSystemService
    {
        #region Base

        /// <summary>The documents directory.</summary>
        private string m_documentsDirpath = null;
        /// <summary>The library directory.</summary>
        private string m_libraryDirpath = null;

        /// <summary>The database directory.</summary>
        private string m_databaseDirpath = null;
        /// <summary>The temp directory.</summary>
        private string m_tempDirpath = null;

        /// <summary>Constructor.</summary>
        public iOSFileSystemService()
        {
            m_documentsDirpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            m_libraryDirpath = Path.GetFullPath(Path.Combine(m_documentsDirpath, "..", "Library"));
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
                    m_databaseDirpath = Path.Combine(m_libraryDirpath, "SwData");
                    if (Directory.Exists(m_databaseDirpath) == false)
                    {
                        Directory.CreateDirectory(m_databaseDirpath);
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
                    m_tempDirpath = Path.GetFullPath(System.IO.Path.Combine(m_documentsDirpath, "..", "tmp", "SwTemp"));
                    if (Directory.Exists(m_tempDirpath) == false)
                    {
                        Directory.CreateDirectory(m_tempDirpath);
                    }
                }
                return m_tempDirpath;
            }
        }

        /// <summary>Getter for the directories to show in the debug menu.</summary>
        public Dictionary<string, string> GetRootDirectories()
        {
            return new Dictionary<string, string>
            {
                { "Documents", m_documentsDirpath },
                { "Library", m_libraryDirpath },
                { "Temp", Path.GetFullPath(Path.Combine(m_documentsDirpath, "..", "tmp")) }
            };
        }

        /// <summary>Getter for whether the given file will be backed up to the platform's cloud.</summary>
        public bool GetFileBackedUp(string filepath)
        {
            NSUrl url = NSUrl.FromFilename(filepath);
            NSObject value = null;
            NSError error = null;
            if (url.TryGetResource(NSUrl.IsExcludedFromBackupKey, out value, out error))
            {
#if DEBUG
                string typeName = value.GetType().FullName;
#endif
                NSNumber valueNumber = (NSNumber)value;
                return valueNumber.Int32Value == 0;
            }
            return false;
        }

        /// <summary>Setter for whether a file should be backed up.</summary>
        public void SetFileBackedup(string filepath, bool shouldBackup)
        {
            NSError error = NSFileManager.SetSkipBackupAttribute(filepath, !shouldBackup);
            if (error != null)
            {
                string filename = Path.GetFileName(filepath);
                if (shouldBackup)
                    Console.WriteLine($"Cannot set the file '{filename}' to be backed up: {error.LocalizedFailureReason}");
                else
                    Console.WriteLine($"Cannot set the file '{filename}' to not be backed up: {error.LocalizedFailureReason}");
            }
        }

        #endregion
    }
}
