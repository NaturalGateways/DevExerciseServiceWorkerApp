using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NG.ServiceWorker.UIServices.CoreUIServices
{
    public class DefaultUserInterfaceViewFactoryService : IUserInterfaceViewFactoryService
    {
        #region Base

        /// <summary>Mapping of view model types to view models.</summary>
        private Dictionary<Type, Type> m_viewTypesByViewModelType = new Dictionary<Type, Type>();

        #endregion

        #region IUserInterfaceViewFactoryService implementation

        /// <summary>Creates a mapping between the given view model and view types.</summary>
        public void RegisterViewModelViewMapping<ViewModelType, ViewType>()
        {
            Type viewModelType = typeof(ViewModelType);
            Type viewType = typeof(ViewType);
            if (m_viewTypesByViewModelType.ContainsKey(viewModelType))
            {
                throw new Exception($"View model type has been registered twice: RegisterViewModelViewMapping<{viewModelType.FullName}, {viewType.FullName}>()");
            }
            m_viewTypesByViewModelType.Add(viewModelType, viewType);
        }

        /// <summary>Creates a view for the given view model.</summary>
        public Page CreatePageFromViewModel<ViewModelType>(ViewModelType viewModel) where ViewModelType : UI.ViewModel
        {
            Type viewModelType = typeof(ViewModelType);
            if (m_viewTypesByViewModelType.ContainsKey(viewModelType) == false)
            {
                throw new Exception($"Unrecognised view model: CreateViewFromViewModel<{viewModelType.FullName}>()");
            }
            Type viewType = m_viewTypesByViewModelType[viewModelType];
            Page page = (Page)Activator.CreateInstance(viewType, viewModel);
            page.BindingContext = viewModel;
            return page;
        }

        /// <summary>Creates a view for the given view model.</summary>
        public View CreateViewFromViewModel<ViewModelType>(ViewModelType viewModel) where ViewModelType : UI.ViewModel
        {
            Type viewModelType = typeof(ViewModelType);
            if (m_viewTypesByViewModelType.ContainsKey(viewModelType) == false)
            {
                throw new Exception($"Unrecognised view model: CreateViewFromViewModel<{viewModelType.FullName}>()");
            }
            Type viewType = m_viewTypesByViewModelType[viewModelType];
            View view = (View)Activator.CreateInstance(viewType, viewModel);
            view.BindingContext = viewModel;
            return view;
        }

        #endregion
    }
}
