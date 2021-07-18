using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.ListUI
{
    public enum ListItemExtraType
    {
        None,
        Subtitle,
        Attribute
    }

    public enum ListItemSelection
    {
        None,        // The item doesn't show selection at all
        Selected,    // The item shows that it is selected in a way that stands out
        CheckedOff,  // The item shows that it is toggleable, and is off now
        CheckedOn    // The item shows that it is toggleable, and is on now
    }

    public enum ListItemDisclosure
    {
        None,    // There is no disclosure
        Segue,   // The item shows that there will be a segue when clicked
        Tick     // The item shows a tick for selection
    }

    public class ListItemViewModel : ViewModel
    {
        #region Static constructors

        public static ListItemViewModel CreateLabel(string text)
        {
            return new ListItemViewModel { MainText = text };
        }
        public static ListItemViewModel CreateSubtitled(string title, string subtitle)
        {
            return new ListItemViewModel { MainText = title, ExtraType = ListItemExtraType.Subtitle, ExtraText = subtitle };
        }
        public static ListItemViewModel CreateAttribute(string name, string value)
        {
            return new ListItemViewModel { MainText = name, ExtraType = ListItemExtraType.Attribute, ExtraText = value };
        }
        public static ListItemViewModel CreateCommand(string text, Func<View, Task> clickFuncAsync)
        {
            return new ListItemViewModel { MainText = text, Disclosure = ListItemDisclosure.Segue, ClickFuncAsync = clickFuncAsync };
        }
        public static ListItemViewModel CreateSubtitledCommand(string title, string subtitle, Func<View, Task> clickFuncAsync)
        {
            return new ListItemViewModel { MainText = title, ExtraType = ListItemExtraType.Subtitle, ExtraText = subtitle, Disclosure = ListItemDisclosure.Segue, ClickFuncAsync = clickFuncAsync };
        }
        public static ListItemViewModel CreateAttributeCommand(string name, string value, Func<View, Task> clickFuncAsync)
        {
            return new ListItemViewModel { MainText = name, ExtraType = ListItemExtraType.Attribute, ExtraText = value, Disclosure = ListItemDisclosure.Segue, ClickFuncAsync = clickFuncAsync };
        }

        public static ListItemViewModel CreateSelectableItem(string name, bool selected, Func<View, Task> clickFuncAsync)
        {
            return new ListItemViewModel { MainText = name, Selection = selected ? ListItemSelection.Selected : ListItemSelection.None, Disclosure = ListItemDisclosure.Segue, ClickFuncAsync = clickFuncAsync };
        }
        public static ListItemViewModel CreateToggleItem(string name, bool on, Func<View, Task> clickFuncAsync)
        {
            return new ListItemViewModel { MainText = name, Selection = on ? ListItemSelection.CheckedOn : ListItemSelection.CheckedOff, Disclosure = ListItemDisclosure.Tick, ClickFuncAsync = clickFuncAsync };
        }

        #endregion

        #region Set properties

        public string MainText { get; set; }

        public ListItemExtraType ExtraType { get; set; } = ListItemExtraType.None;

        public string ExtraText { get; set; }

        public ListItemSelection Selection { get; set; } = ListItemSelection.None;

        public ListItemDisclosure Disclosure { get; set; } = ListItemDisclosure.None;

        public Func<View, Task> ClickFuncAsync { get; set; }

        #endregion

        #region Calcluated properties

        public bool ShowDisclosure { get { return this.Disclosure == ListItemDisclosure.Segue && this.ClickFuncAsync != null; } }

        public bool ShowTick { get { return this.Disclosure == ListItemDisclosure.Tick; } }

        public Color TickColour { get { return (this.Selection == ListItemSelection.CheckedOff) ? Color.Gray : Color.Blue; } }

        public bool IsSubtitleVisible { get { return this.ExtraType == ListItemExtraType.Subtitle && string.IsNullOrEmpty(this.ExtraText) == false; } }

        public bool IsAttributeValueVisible { get { return this.ExtraType == ListItemExtraType.Attribute && string.IsNullOrEmpty(this.ExtraText) == false; } }

        public bool IsSelectedVisible { get { return this.Selection == ListItemSelection.Selected || this.Selection == ListItemSelection.CheckedOn; } }

        #endregion

        #region Functions

        /// <summary>Called to refresh the item.</summary>
        public void RefreshTick()
        {
            OnPropertyChanged("ShowTick");
            OnPropertyChanged("TickColour");
            OnPropertyChanged("IsSelectedVisible");
        }

        #endregion
    }
}
