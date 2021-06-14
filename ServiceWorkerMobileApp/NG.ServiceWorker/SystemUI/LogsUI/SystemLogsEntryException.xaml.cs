using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.ServiceWorker.SystemUI.LogsUI
{
    public partial class SystemLogsEntryException : ContentView
    {
        public SystemLogsEntryException(LogsModel.LogEntryDetailJsonException exJson)
        {
            InitializeComponent();

            this.NameLabel.Text = exJson.ExceptionType;
            this.MessageLabel.Text = exJson.Message;
            this.StackTraceLabel.Text = exJson.StackTrace;
        }
    }
}
