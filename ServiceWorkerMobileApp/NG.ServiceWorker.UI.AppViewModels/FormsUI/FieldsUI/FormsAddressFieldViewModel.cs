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
        /// <summary>The last fetch.</summary>
        private IGeocoderSuggestion m_selectedSuggestion = null;

        /// <summary>Constructor.</summary>
        public FormsAddressFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            // Listen for changes
            formField.AnswerModel.AddListener(this);

            // Create commands
            this.SearchCommand = new Command(async () => { await OnSearchAsync(); });
            this.ClearCommand = new Command(OnClear);
            this.Suggestion1Command = new Command(() => { OnSelectSuggestion(0); });
            this.Suggestion2Command = new Command(() => { OnSelectSuggestion(1); });
            this.Suggestion3Command = new Command(() => { OnSelectSuggestion(2); });
            this.Suggestion4Command = new Command(() => { OnSelectSuggestion(3); });
            this.Suggestion5Command = new Command(() => { OnSelectSuggestion(4); });
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
            OnPropertyChanged("ValueText");
            OnPropertyChanged("ValueIsEnabled");
            OnPropertyChanged("GeocoderSuggestionsAreShowing");
            OnPropertyChanged("GeocoderSuggestion1IsShowing");
            OnPropertyChanged("GeocoderSuggestion2IsShowing");
            OnPropertyChanged("GeocoderSuggestion3IsShowing");
            OnPropertyChanged("GeocoderSuggestion4IsShowing");
            OnPropertyChanged("GeocoderSuggestion5IsShowing");
            OnPropertyChanged("GeocoderSuggestion1Text");
            OnPropertyChanged("GeocoderSuggestion2Text");
            OnPropertyChanged("GeocoderSuggestion3Text");
            OnPropertyChanged("GeocoderSuggestion4Text");
            OnPropertyChanged("GeocoderSuggestion5Text");
            OnPropertyChanged("GeocoderErrorIsShowing");
            OnPropertyChanged("GeocoderErrorText");
        }

        /// <summary>Called when the user wants to clear the field.</summary>
        private void OnClear()
        {
            this.ValueText = null;
            SwForms.FormsHelper.SetAnswer(this.FormField, SwForms.Answers.NullAnswer.Null);
            m_isBusy = false;
            m_suggestionFetch = null;
            m_selectedSuggestion = null;
            UpdateGeocoderProperties();
        }

        /// <summary>Called when the user selects a suggestion.</summary>
        private void OnSelectSuggestion(int suggestionIndex)
        {
            m_selectedSuggestion = m_suggestionFetch?.Suggestions?.Skip(suggestionIndex)?.FirstOrDefault();
            if (m_selectedSuggestion == null)
            {
                OnClear();
            }
            else
            {
                this.ValueText = m_selectedSuggestion.DisplayName;
                SwForms.FormsHelper.SetAnswer(this.FormField, new SwForms.Answers.StringAnswer(m_selectedSuggestion.DisplayName));
                m_suggestionFetch = null;
                UpdateGeocoderProperties();
            }
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
        public bool ValueIsEnabled { get { return m_isBusy == false && m_selectedSuggestion == null; } }
        /// <summary>The value text.</summary>
        public string ValueText { get;  set; }

        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestionsAreShowing { get { return m_suggestionFetch?.Suggestions?.Any() ?? false; } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion1IsShowing { get { return 1 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion2IsShowing { get { return 2 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion3IsShowing { get { return 3 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion4IsShowing { get { return 4 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public bool GeocoderSuggestion5IsShowing { get { return 5 <= (m_suggestionFetch?.Suggestions?.Count() ?? 0); } }
        /// <summary>Bound property.</summary>
        public string GeocoderSuggestion1Text { get { return m_suggestionFetch?.Suggestions?.FirstOrDefault()?.DisplayName ?? string.Empty; } }
        /// <summary>Bound property.</summary>
        public string GeocoderSuggestion2Text { get { return m_suggestionFetch?.Suggestions?.Skip(1)?.FirstOrDefault()?.DisplayName ?? string.Empty; } }
        /// <summary>Bound property.</summary>
        public string GeocoderSuggestion3Text { get { return m_suggestionFetch?.Suggestions?.Skip(2)?.FirstOrDefault()?.DisplayName ?? string.Empty; } }
        /// <summary>Bound property.</summary>
        public string GeocoderSuggestion4Text { get { return m_suggestionFetch?.Suggestions?.Skip(3)?.FirstOrDefault()?.DisplayName ?? string.Empty; } }
        /// <summary>Bound property.</summary>
        public string GeocoderSuggestion5Text { get { return m_suggestionFetch?.Suggestions?.Skip(4)?.FirstOrDefault()?.DisplayName ?? string.Empty; } }

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

        /// <summary>Bound command.</summary>
        public ICommand ClearCommand { get; private set; }

        /// <summary>Bound command.</summary>
        public ICommand Suggestion1Command { get; private set; }
        /// <summary>Bound command.</summary>
        public ICommand Suggestion2Command { get; private set; }
        /// <summary>Bound command.</summary>
        public ICommand Suggestion3Command { get; private set; }
        /// <summary>Bound command.</summary>
        public ICommand Suggestion4Command { get; private set; }
        /// <summary>Bound command.</summary>
        public ICommand Suggestion5Command { get; private set; }

        #endregion
    }
}
