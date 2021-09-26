using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.DebugUI.FilesystemDebugUI
{
    public class DebugFilesystemSqlitePage : ContentPage
    {
        public DebugFilesystemSqlitePage(string filepath)
        {
            // Set title
            this.Title = System.IO.Path.GetFileName(filepath);

            // Create view model
            UI.ListUI.ListViewModel listViewModel = new UI.ListUI.ListViewModel();
            UI.ListUI.ListSectionViewModel sectionViewModel = new UI.ListUI.ListSectionViewModel { Title = "Tables" };
            listViewModel.SectionList.Add(sectionViewModel);

            // Connect to database
            using (ISqliteConnection connection = Services.SqliteService.ConnectToFilepath(filepath))
            {
                connection.Query("SELECT NAME FROM sqlite_master WHERE type='table' ORDER BY NAME;", null, (row) =>
                {
                    string tableName = row.GetString();
                    sectionViewModel.Add(UI.ListUI.ListItemViewModel.CreateCommand(tableName, async (view) => { await GoToTableAsync(view, filepath, tableName); }));
                });
            }

            // Set content
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GoToTableAsync(View view, string filepath, string tableName)
        {
            await view.Navigation.PushAsync(new DebugFilesystemSqliteTablePage(filepath, tableName));
        }
    }
}
