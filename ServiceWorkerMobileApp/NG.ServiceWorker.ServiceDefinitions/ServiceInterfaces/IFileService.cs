using System;

namespace NG.ServiceWorker
{
    public interface IFile
    {
        /// <summary>Getter for the file data as an image source.</summary>
        Xamarin.Forms.ImageSource AsImageSource { get; }

        /// <summary>Setter for whetehr the file should be backed up or not.</summary>
        void SetBackedUp(bool shouldBackup);
    }

    public interface IFileWriter
    {
        /// <summary>The extension of the file.</summary>
        string FileDataExtension { get; }

        /// <summary>Called to write the data to a created file.</summary>
        void WriteDataToStream(System.IO.Stream stream);
    }

    public interface IFileService
    {
        /// <summary>Creates a temp file.</summary>
        IFile CreateTempFile(IFileWriter writer);

        /// <summary>Getter for a database filepath.</summary>
        string GetDatabaseFilepath(string filename);
    }
}
