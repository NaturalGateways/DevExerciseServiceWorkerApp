using System;

namespace NG.ServiceWorker.LogsModel
{
    public class LogsSession
    {
        public string SessionId { get; set; }

        public DateTime StartDateTimeUtc { get; set; }

        public int EntryNum { get; set; }
    }
}
