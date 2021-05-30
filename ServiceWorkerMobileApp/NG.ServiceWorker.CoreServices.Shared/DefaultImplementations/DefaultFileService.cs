using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultFileServiceFile : IFile
    {
        #region Base

        /// <summary>The filepath.</summary>
        private string m_filepath = null;

        /// <summary>The image source.</summary>
        private Xamarin.Forms.ImageSource m_imageSource = null;

        /// <summary>Constructor.</summary>
        public DefaultFileServiceFile(string filepath)
        {
            m_filepath = filepath;
        }

        #endregion

        #region IFile implementation

        /// <summary>Getter for the file data as an image source.</summary>
        public Xamarin.Forms.ImageSource AsImageSource
        {
            get
            {
                if (m_imageSource == null)
                {
                    m_imageSource = Xamarin.Forms.ImageSource.FromFile(m_filepath);
                }
                return m_imageSource;
            }
        }

        #endregion
    }

    public class DefaultFileService : IFileService
    {
        #region Base

        /// <summary>The index of the last temp file.</summary>
        private int m_tempFileIndex = 0;

        /// <summary>Constructor.</summary>
        public DefaultFileService()
        {
            // Temp file cleanup
            string tempDirpath = Services.FileSystemService.TempDirectory;
            foreach (string tempFilepath in System.IO.Directory.GetFiles(tempDirpath))
            {
                System.IO.File.Delete(tempFilepath);
            }
        }

        #endregion

        #region IFileService implementation

        /// <summary>Creates a temp file.</summary>
        public IFile CreateTempFile(IFileWriter writer)
        {
            // Work out path
            string tempDirpath = Services.FileSystemService.TempDirectory;
            int tempFileIndex = ++m_tempFileIndex;
            string filename = System.IO.Path.ChangeExtension(tempFileIndex.ToString(), writer.FileDataExtension);
            string tempFilepath = System.IO.Path.Combine(tempDirpath, filename);

            // Create the file
            using (System.IO.Stream fileStream = System.IO.File.OpenWrite(tempFilepath))
            {
                writer.WriteDataToStream(fileStream);
            }

            // Return
            return new DefaultFileServiceFile(tempFilepath);
        }

        #endregion
    }
}
