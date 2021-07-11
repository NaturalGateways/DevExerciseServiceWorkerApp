using System;

namespace NG.ServiceWorker.UI
{
    public class AppViewModel : ViewModel
    {
        /// <summary>This is the type used when the view model is mapped to a view or other UI element.</summary>
        public virtual Type ViewModelType { get { return GetType(); } }

        /// <summary>This is a reference to the view created.</summary>
        public object View { get; set; }
    }
}
