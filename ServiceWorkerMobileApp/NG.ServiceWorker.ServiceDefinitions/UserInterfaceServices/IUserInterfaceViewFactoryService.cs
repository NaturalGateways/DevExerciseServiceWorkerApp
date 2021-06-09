using System;

using Xamarin.Forms;

namespace NG.ServiceWorker.UIServices
{
    public interface IUserInterfaceViewFactoryService
    {
        /// <summary>Creates a mapping between the given view model and view types.</summary>
        void RegisterViewModelViewMapping<ViewModelType, ViewType>();

        /// <summary>Creates a view for the given view model.</summary>
        View CreateViewFromViewModel<ViewModelType>(ViewModelType viewModel) where ViewModelType : UI.ViewModel;
    }
}
