using System;

namespace NG.ServiceWorker.CoreServices.AndroidServices
{
    public class AndroidFileSystemService : IFileSystemService
    {
        #region Base

        /// <summary>The documents directory.</summary>
        private string m_documentsDirpath = null;
        /// <summary>The documents directory.</summary>
        private string m_tempDirpath = null;

        /// <summary>Constructor.</summary>
        public AndroidFileSystemService()
        {
            m_documentsDirpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        #endregion

        #region IFileSystemService

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

        #endregion
    }
}
