using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NG.ServiceWorker.DebugUI.FilesystemDebugUI
{
    public class DebugFilesystemDirectoryPage : ContentPage
    {
        public DebugFilesystemDirectoryPage(string dirpath)
        {
            // Set title
            string dirname = System.IO.Path.GetFileName(dirpath);
            this.Title = dirname;

            // Get dirpaths
            ListViewModels.ListViewModel listViewModel = new ListViewModels.ListViewModel();
            string[] subDirpathArray = System.IO.Directory.GetDirectories(dirpath);
            string[] subFilepathArray = System.IO.Directory.GetFiles(dirpath);
            if (subDirpathArray.Any())
            {
                ListViewModels.ListSectionViewModel sectionViewModel = new ListViewModels.ListSectionViewModel { Title = "Directories" };
                foreach (string subDirpath in subDirpathArray.OrderBy(x => x))
                {
                    string subDirname = System.IO.Path.GetFileName(subDirpath);
                    sectionViewModel.Add(ListViewModels.ListItemViewModel.CreateCommand(subDirname, async (view) => { await GoToDirectoryAsync(view, subDirpath); }));
                }
                listViewModel.SectionList.Add(sectionViewModel);
            }
            if (subFilepathArray.Any())
            {
                ListViewModels.ListSectionViewModel sectionViewModel = new ListViewModels.ListSectionViewModel { Title = "Files" };
                foreach (string subFilepath in subFilepathArray.OrderBy(x => x))
                {
                    sectionViewModel.Add(CreateItemForFilepath(subFilepath));
                }
                listViewModel.SectionList.Add(sectionViewModel);
            }
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        private ListViewModels.ListItemViewModel CreateItemForFilepath(string filepath)
        {
            bool isBackedUp = Services.FileSystemService.GetFileBackedUp(filepath);
            string subtitleText = isBackedUp ? "Backed up" : "Local only";
            string filename = System.IO.Path.GetFileName(filepath);
            switch (System.IO.Path.GetExtension(filepath).ToLowerInvariant())
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                    return ListViewModels.ListItemViewModel.CreateSubtitledCommand(filename, subtitleText, async (view) => { await GoToImageFileAsync(view, filepath); });
                default:
                    return ListViewModels.ListItemViewModel.CreateSubtitled(filename, subtitleText);
            }
        }

        public async Task GoToDirectoryAsync(View view, string path)
        {
            await view.Navigation.PushAsync(new DebugFilesystemDirectoryPage(path));
        }

        public async Task GoToImageFileAsync(View view, string path)
        {
            await view.Navigation.PushAsync(new DebugfilesystemImagePage(path));
        }
    }
}
