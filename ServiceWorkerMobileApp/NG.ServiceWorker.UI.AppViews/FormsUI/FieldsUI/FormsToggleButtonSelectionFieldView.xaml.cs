using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public partial class FormsToggleButtonSelectionFieldView : ContentView, IModelListener
    {
        private List<SelectionUI.ToggleButtonViewModel> m_itemViewModelList = new List<SelectionUI.ToggleButtonViewModel>();

        public FormsToggleButtonSelectionFieldView(FormsToggleButtonSelectionFieldViewModel viewModel)
        {
            InitializeComponent();

            foreach (SwForms.IAnswer selectableAnswer in viewModel.FormField.SelectList.FlatLevel.SelectableAnswers)
            {
                SelectionUI.ToggleButtonViewModel itemViewModel = new SelectionUI.ToggleButtonViewModel(viewModel.FormField, viewModel.FormField.AnswerModel, selectableAnswer);
                m_itemViewModelList.Add(itemViewModel);
                this.ToggleButtonStack.Children.Add(new SelectionUI.ToggleButton(itemViewModel));
            }

            viewModel.FormField.AnswerModel.AddListener(this);
        }

        /// <summary>Called when the data has cchanged.</summary>
        public void OnDataChanged(Model model)
        {
            foreach (SelectionUI.ToggleButtonViewModel itemViewModel in m_itemViewModelList)
            {
                itemViewModel.RefreshProperties();
            }
        }
    }
}
