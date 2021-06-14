using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NG.ServiceWorker.SystemUI.LogsUI
{
    public partial class SystemLogsEntry : ContentPage
    {
        public SystemLogsEntry(LogsModel.LogsEntry entry)
        {
            InitializeComponent();

            // Get detail
            LogsModel.LogEntryDetailJson entryDetail = Services.LogService.GetEntryDetailForEntryId(entry.LogEntryId);

            if (entryDetail.ExceptionStack != null)
            {
                foreach (LogsModel.LogEntryDetailJsonException exception in entryDetail.ExceptionStack)
                {
                    this.DetailStack.Children.Add(new SystemLogsEntryException(exception));
                }
            }
        }
    }
}
