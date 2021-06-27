using System;

using Serilog;

namespace NG.ServiceWorker.CoreServices.Serilog
{
    public class SerilogLogger : ILogger
    {
        #region Base

        /// <summary>The name of the logger.</summary>
        private string m_loggerName = null;

        /// <summary>Constructor.</summary>
        public SerilogLogger(string loggerName)
        {
            m_loggerName = loggerName;
        }

        #endregion

        #region ILogger implementation

        public void Trace(string message) { Log.Verbose($"{m_loggerName}: {message}"); }
        public void Info(string message) { Log.Information($"{m_loggerName}: {message}"); }
        public void Warn(string message) { Log.Warning($"{m_loggerName}: {message}"); }
        public void Error(string message) { Log.Error($"{m_loggerName}: {message}"); }
        public void Fatal(string message) { Log.Fatal($"{m_loggerName}: {message}"); }
        public void Warn(string message, Exception ex) { Log.Warning(ex, $"{m_loggerName}: {message}"); }
        public void Error(string message, Exception ex) { Log.Error(ex, $"{m_loggerName}: {message}"); }
        public void Fatal(string message, Exception ex) { Log.Fatal(ex, $"{m_loggerName}: {message}"); }

        #endregion
    }
}
