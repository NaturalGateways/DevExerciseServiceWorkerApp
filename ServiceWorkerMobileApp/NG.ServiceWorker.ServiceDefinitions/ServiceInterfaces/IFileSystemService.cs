using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public interface IFileSystemService
    {
        /// <summary>Getter for the directory to put temp files in.</summary>
        string TempDirectory { get; }

        /// <summary>Getter for the directories to show in the debug menu.</summary>
        Dictionary<string, string> GetRootDirectories();

        /// <summary>Getter for whether the given file will be backed up to the platform's cloud.</summary>
        bool GetFileBackedUp(string filepath);

        /// <summary>Setter for whether a file should be backed up.</summary>
        void SetFileBackedup(string filepath, bool shouldBackup);
    }
}
