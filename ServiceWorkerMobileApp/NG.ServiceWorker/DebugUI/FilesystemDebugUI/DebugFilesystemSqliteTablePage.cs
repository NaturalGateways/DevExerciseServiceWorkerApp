using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.DebugUI.FilesystemDebugUI
{
    public class DebugFilesystemSqliteTablePage : ContentPage
    {
        public DebugFilesystemSqliteTablePage(string filepath, string tableName)
        {
            // Set title
            this.Title = System.IO.Path.GetFileName(tableName);

            // Create view model
            UI.ListUI.ListViewModel listViewModel = new UI.ListUI.ListViewModel();

            // Connect to database
            using (ISqliteConnection connection = Services.SqliteService.ConnectToFilepath(filepath))
            {
                // Create column list
                List<(string columnName, string dataType)> columnList = new List<(string columnName, string dataType)>();
                connection.Query($"pragma table_info({tableName});", null, (row) =>
                {
                    row.SkipColumn();
                    string columnName = row.GetString();
                    string dataType = row.GetString();
                    columnList.Add((columnName, dataType));
                });

                // Create header section
                {
                    UI.ListUI.ListSectionViewModel sectionViewModel = new UI.ListUI.ListSectionViewModel { Title = "Columns" };
                    foreach ((string columnName, string dataType) column in columnList)
                    {
                        sectionViewModel.Add(UI.ListUI.ListItemViewModel.CreateSubtitled(column.columnName, column.dataType));
                    }
                    listViewModel.SectionList.Add(sectionViewModel);
                }

                // Create rows
                string columnsCsv = string.Join(",", columnList.Select(x => x.columnName));
                string getValuesSql = $"SELECT {columnsCsv} FROM {tableName};";
                int rowIndex = 0;
                connection.Query(getValuesSql, null, (row) =>
                {
                    UI.ListUI.ListSectionViewModel sectionViewModel = new UI.ListUI.ListSectionViewModel { Title = $"Row {++rowIndex}" };
                    foreach ((string columnName, string dataType) column in columnList)
                    {
                        string valueString = row.GetString();
                        if ((valueString.StartsWith("{") && valueString.EndsWith("}")) || (valueString.StartsWith("[") && valueString.EndsWith("]")))
                        {
                            sectionViewModel.Add(UI.ListUI.ListItemViewModel.CreateAttribute(column.columnName, "JSON"));
                        }
                        else
                        {
                            sectionViewModel.Add(UI.ListUI.ListItemViewModel.CreateAttribute(column.columnName, valueString));
                        }
                    }
                    listViewModel.SectionList.Add(sectionViewModel);
                });
            }

            // Set content
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }
    }
}
