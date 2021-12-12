using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI.SelectionUI
{
    public class ToggleButtonViewModel : ViewModel
    {
        /// <summary>The field.</summary>
        private SwForms.IFormField m_field = null;

        /// <summary>The answer model.</summary>
        private SwForms.AnswerModel m_answerModel = null;

        /// <summary>The answer.</summary>
        public SwForms.IAnswer Answer { get; private set; }

        /// <summary>constructor.</summary>
        public ToggleButtonViewModel(SwForms.IFormField field, SwForms.AnswerModel answerModel, SwForms.IAnswer answer)
        {
            m_field = field;
            m_answerModel = answerModel;
            this.Answer = answer;
        }

        public void RefreshProperties()
        {
            OnPropertyChanged("ButtonBgColor");
            OnPropertyChanged("ButtonTextColor");
        }

        private void OnClicked()
        {
            SwForms.FormsHelper.SetAnswer(m_field, this.Answer);
        }

        public string ButtonText { get { return this.Answer.DisplayValue; } }

        public bool IsSelected { get { return m_answerModel.Answer.CodeValue == this.Answer.CodeValue; } }

        public Color ButtonBgColor { get { return this.IsSelected ? (Color)Application.Current.Resources["Primary"] : Color.White; } }

        public Color ButtonTextColor { get { return this.IsSelected ? Color.White : (Color)Application.Current.Resources["Primary"]; } }

        public ICommand ButtonCommand => new Command(OnClicked);
    }

    public partial class ToggleButton : ContentView
    {
        public ToggleButton(ToggleButtonViewModel viewModel)
        {
            InitializeComponent();

            this.BindingContext = viewModel;
        }
    }
}
