using System;
using System.Collections.ObjectModel;

namespace NG.ServiceWorker.UI.ListUI
{
    public class ListViewModel : ViewModel
    {
        /// <summary>The list of items in the section.</summary>
        public ObservableCollection<ListSectionViewModel> SectionList { get; private set; } = new ObservableCollection<ListSectionViewModel>();
    }
}
