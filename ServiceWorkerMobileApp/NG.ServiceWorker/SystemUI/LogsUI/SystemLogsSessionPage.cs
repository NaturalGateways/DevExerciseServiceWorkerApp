using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.SystemUI.LogsUI
{
    public class SystemLogsSessionPage : ContentPage
    {
        public SystemLogsSessionPage(LogsModel.LogsSession session)
        {
            this.Title = $"Logs Session {session.StartDateTimeUtc.ToLocalTime().ToString()}";

            // Get the sessions
            IEnumerable<LogsModel.LogsEntry> entryList = Services.LogService.GetEntriesForSessionId(session.SessionId);

            UI.ListUI.ListViewModel listViewModel = new UI.ListUI.ListViewModel();
            listViewModel.SectionList.Add(new UI.ListUI.ListSectionViewModel());
            foreach (LogsModel.LogsEntry entry in entryList)
            {
                string title = entry.Message;
                string subtitle = $"{entry.DateTimeUtc.ToLocalTime()} {entry.LogLevelName} {entry.ComponentName}";
                if (entry.HasDetail)
                {
                    listViewModel.SectionList[0].Add(UI.ListUI.ListItemViewModel.CreateSubtitledCommand(title, subtitle, async (view) => { await GotoEntry(view, entry); }));
                }
                else
                {
                    listViewModel.SectionList[0].Add(UI.ListUI.ListItemViewModel.CreateSubtitled(title, subtitle));
                }
            }
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GotoEntry(View view, LogsModel.LogsEntry entry)
        {
            await view.Navigation.PushAsync(new SystemLogsEntry(entry));
        }
    }
}

