using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.SystemUI.LogsUI
{
    public class SystemLogsPage : ContentPage
    {
        public SystemLogsPage()
        {
            this.Title = "Logs";

            // Get the sessions
            IEnumerable<LogsModel.LogsSession> sessionList = Services.LogService.GetSessions();

            UI.ListUI.ListViewModel listViewModel = new UI.ListUI.ListViewModel();
            listViewModel.SectionList.Add(new UI.ListUI.ListSectionViewModel());
            foreach (LogsModel.LogsSession session in sessionList)
            {
                string title = session.StartDateTimeUtc.ToLocalTime().ToString();
                string subtitle = $"{session.EntryNum} Entries";
                listViewModel.SectionList[0].Add(UI.ListUI.ListItemViewModel.CreateSubtitledCommand(title, subtitle, async (view) => { await GotoSession(view, session); }));
            }
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GotoSession(View view, LogsModel.LogsSession session)
        {
            await view.Navigation.PushAsync(new SystemLogsSessionPage(session));
        }
    }
}
