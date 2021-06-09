using System;
using System.Collections.ObjectModel;

namespace NG.ServiceWorker.ListViewModels
{
    public class ListViewModel : UI.ViewModel
    {
        /// <summary>The list of items in the section.</summary>
        public ObservableCollection<ListSectionViewModel> SectionList { get; private set; } = new ObservableCollection<ListSectionViewModel>();
    }
}
