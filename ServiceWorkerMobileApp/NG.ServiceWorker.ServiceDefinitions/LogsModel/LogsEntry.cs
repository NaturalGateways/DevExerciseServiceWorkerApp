using System;

namespace NG.ServiceWorker.LogsModel
{
    public class LogsEntry
    {
        public int LogEntryId { get; set; }

        public string ComponentName { get; set; }

        public DateTime DateTimeUtc { get; set; }

        public string LogLevelName { get; set; }

        public string Message { get; set; }

        public bool HasDetail { get; set; }
    }
}

