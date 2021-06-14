using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.LogsModel
{
    public class LogEntryDetailJson
    {
        /// <summary>The exception stack.</summary>
        public List<LogEntryDetailJsonException> ExceptionStack { get; set; }
    }

    public class LogEntryDetailJsonException
    {
        /// <summary>The full class type of the exception.</summary>
        public string ExceptionType { get; set; }

        /// <summary>A message of the exception.</summary>
        public string Message { get; set; }

        /// <summary>A stack trace of the exception.</summary>
        public string StackTrace { get; set; }
    }
}
