using System;

namespace NG.ServiceWorker
{
    public interface IFileSystemService
    {
        /// <summary>Getter for the directory to put temp files in.</summary>
        string TempDirectory { get; }
    }
}
