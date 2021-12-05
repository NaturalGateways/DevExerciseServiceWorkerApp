using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.InputPageUI
{
    public class SegueMultiSelectionInputItemViewModel : ListUI.ListItemViewModel
    {
        /// <summary>The field being modified.</summary>
        private SwForms.IFormField m_formField = null;
        /// <summary>The model for the current answer.</summary>
        private SwForms.AnswerModel m_answerModel = null;

        /// <summary>The selectable answer for this item.</summary>
        private SwForms.IAnswer m_selectableAnswer = null;

        /// <summary>Getter for whether the item is selected.</summary>
        private bool IsSelected
        {
            get
            {
                SwForms.Answers.MultiAnswer multiAnswer = m_answerModel.Answer as SwForms.Answers.MultiAnswer;
                return multiAnswer?.HasCodeValue(m_selectableAnswer.CodeValue) ?? false;
            }
        }

        /// <summary>Constructor.</summary>
        public SegueMultiSelectionInputItemViewModel(SwForms.IFormField formField, SwForms.IAnswer selectableAnswer)
        {
            m_formField = formField;
            m_answerModel = formField.AnswerModel;
            m_selectableAnswer = selectableAnswer;

            this.MainText = selectableAnswer.DisplayValue;
            this.Selection = this.IsSelected ? ListUI.ListItemSelection.CheckedOn : ListUI.ListItemSelection.CheckedOff;
            this.Disclosure = ListUI.ListItemDisclosure.Tick;
            this.ClickFuncAsync = OnClicked;
        }

        /// <summary>Called when the item is clicked.</summary>
        private Task OnClicked(View view)
        {
            // Set answer
            SwForms.IAnswer originalAnswer = m_answerModel.Answer;
            SwForms.Answers.MultiAnswer originalMultiAnswer = originalAnswer as SwForms.Answers.MultiAnswer;
            if (originalMultiAnswer?.HasCodeValue(m_selectableAnswer.CodeValue) ?? false)
            {
                SwForms.FormsHelper.SetAnswer(m_formField, originalMultiAnswer.MultiAnswerWithAnswerRemoved(m_selectableAnswer.CodeValue));
            }
            else
            {
                SwForms.FormsHelper.SetAnswer(m_formField, new SwForms.Answers.MultiAnswer(originalAnswer, m_selectableAnswer));
            }

            // Refresh UI
            this.Selection = this.IsSelected ? ListUI.ListItemSelection.CheckedOn : ListUI.ListItemSelection.CheckedOff;
            RefreshTick();

            // Return fake async
            return Task.CompletedTask;
        }
    }
}
