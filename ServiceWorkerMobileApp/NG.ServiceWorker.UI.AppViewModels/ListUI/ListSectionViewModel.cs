using System;
using System.Collections.ObjectModel;

namespace NG.ServiceWorker.UI.ListUI
{
    public class ListSectionViewModel : ObservableCollection<ListItemViewModel>
    {
        /// <summary>The section title.</summary>
        public string Title { get; set; }
    }
}
