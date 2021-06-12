using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.DebugUI.FilesystemDebugUI
{
    public partial class DebugFilesystemPage : ContentPage
    {
        public DebugFilesystemPage()
        {
            InitializeComponent();

            this.Title = "Filesystem";

            // Get dirpaths
            Dictionary<string, string> dirpathsByName = Services.FileSystemService.GetRootDirectories();
            UI.ListUI.ListViewModel listViewModel = new UI.ListUI.ListViewModel();
            foreach (KeyValuePair<string, string> dirpathByName in dirpathsByName)
            {
                string[] subDirpathArray = System.IO.Directory.GetDirectories(dirpathByName.Value);
                string[] subFilepathArray = System.IO.Directory.GetFiles(dirpathByName.Value);
                if (subDirpathArray.Any())
                {
                    UI.ListUI.ListSectionViewModel sectionViewModel = new UI.ListUI.ListSectionViewModel { Title = $"{dirpathByName.Key} Directories" };
                    foreach (string subDirpath in subDirpathArray.OrderBy(x => x))
                    {
                        string subDirname = System.IO.Path.GetFileName(subDirpath);
                        sectionViewModel.Add(UI.ListUI.ListItemViewModel.CreateCommand(subDirname, async (view) => { await GoToDirectoryAsync(view, subDirpath); }));
                    }
                    listViewModel.SectionList.Add(sectionViewModel);
                }
                if (subFilepathArray.Any())
                {
                    UI.ListUI.ListSectionViewModel sectionViewModel = new UI.ListUI.ListSectionViewModel { Title = $"{dirpathByName.Key} Files" };
                    foreach (string subFilepath in subFilepathArray.OrderBy(x => x))
                    {
                        string subFilename = System.IO.Path.GetFileName(subFilepath);
                        sectionViewModel.Add(UI.ListUI.ListItemViewModel.CreateLabel(subFilename));
                    }
                    listViewModel.SectionList.Add(sectionViewModel);
                }
            }
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GoToDirectoryAsync(View view, string path)
        {
            await view.Navigation.PushAsync(new DebugFilesystemDirectoryPage(path));
        }
    }
}
