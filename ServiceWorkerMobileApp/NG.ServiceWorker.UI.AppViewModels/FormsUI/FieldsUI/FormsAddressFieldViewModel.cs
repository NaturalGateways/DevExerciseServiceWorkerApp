using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsAddressFieldViewModel : FormsFieldViewModel, IModelListener
    {
        #region Base

        /// <summary>Boolean for whether a search is underway.</summary>
        private bool m_isBusy = false;

        /// <summary>The last fetch.</summary>
        private GeocoderSuggestionFetchResults m_suggestionFetch =  null;

        /// <summary>Constructor.</summary>
        public FormsAddressFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            // Listen for changes
            formField.AnswerModel.AddListener(this);

            // Create commands
            this.SearchCommand = new Command(async () => { await OnSearchAsync(); });
        }

        /// <summary>Called when a search is started.</summary>
        private async Task OnSearchAsync()
        {
            // Set is busy
            m_isBusy = true;
            m_suggestionFetch = null;
            UpdateGeocoderProperties();

            // Try-finally to ensure is busy is set to false
            try
            {
                // Do fetch
                GeocoderSuggestionFetchResults fetch = await Services.GeocoderService.GetSuggestionsForTextAsync(this.ValueText, 5);
                m_suggestionFetch = fetch;
            }
            catch (Exception ex)
            {
                Services.LogService.CreateLogger("Geocoder UI").Error($"Error seraching for text '{this.ValueText}'.", ex);
                m_suggestionFetch = new GeocoderSuggestionFetchResults { ErrorText = "Unknown error fetching suggestions." };
            }
            finally
            {
                m_isBusy = false;
                UpdateGeocoderProperties();
            }
        }

        /// <summary>Update the geocoder properties.</summary>
        private void UpdateGeocoderProperties()
        {
            OnPropertyChanged("ValueIsEnabled");
            OnPropertyChanged("GeocoderSuggestionsAreShowing");
            OnPropertyChanged("GeocoderSuggestion1IsShowing");
            OnPropertyChanged("GeocoderSuggestion2IsShowing");
            OnPropertyChanged("GeocoderSuggestion3IsShowing");
            OnPropertyChanged("GeocoderSuggestion4IsShowing");
            OnPropertyChanged("GeocoderSuggestion5IsShowing");
            OnPropertyChanged("GeocoderErrorIsShowing");
            OnPropertyChanged("GeocoderErrorText");
        }

        #endregion

        #region IModelListener implementation

        /// <summary>Called when the data has changed.</summary>
        public void OnDataChanged(Model model)
        {
            OnPropertyChanged("ValueText");
            OnPropertyChanged("ValidationIsShowing");
            OnPropertyChanged("ValidationText");
        }

        #endregion

        #region Internal XAML properties

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>Bound property.</summary>
        public bool MandatoryIsShowing { get { if (this.FormField.Validation == null) { return false; } return this.FormField.Validation.ValidationFlags.HasFlag(SwForms.ValidationFlags.Mandatory); } }

        /// <summary>The value text.</summary>
        public bool ValueIsEnabled { get { return m_isBusy == false; } }
        /// <summary>The value text.</summary>
        public string ValueText
        {
            get { return this.FormField.AnswerModel.Answer.DisplayValue; }
            set
            {
                SwForms.FormsHelper.SetAnswer(this.FormField, new SwForms.Answers.StringAnswer(value));
            }
        }

        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestionsAreShowing { get { return m_suggestionFetch?.Suggestions?.Any() ?? false; } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion1IsShowing { get { return 1 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion2IsShowing { get { return 2 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion3IsShowing { get { return 3 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion4IsShowing { get {
                int countcount = (m_suggestionFetch?.Suggestions?.Count() ?? 0);
                return 4 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0);
            } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion5IsShowing { get { return 5 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }

        /// <summary>Bound property.</summary>
        public bool GeocoderErrorIsShowing { get { return string.IsNullOrEmpty(m_suggestionFetch?.ErrorText) == false; } }
        /// <summary>Bound property.</summary>
        public string GeocoderErrorText { get { return m_suggestionFetch?.ErrorText ?? string.Empty; } }

        /// <summary>Bound property.</summary>
        public bool ValidationIsShowing { get { return this.FormField.AnswerModel.Validation.IsFailed; } }
        /// <summary>Bound property.</summary>
        public string ValidationText { get { return this.FormField.AnswerModel.Validation.FieldMessage ?? string.Empty; } }

        #endregion

        #region Internal XAML commands

        /// <summary>Bound command.</summary>
        public ICommand SearchCommand { get; private set; }

        #endregion
    }
}
