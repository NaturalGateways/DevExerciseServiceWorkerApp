using System;

namespace NG.ServiceWorker.SwForms
{
    public interface IRule
    {
        /// <summary>Called when a rule cause changes so that the rule needs to retrigger,</summary>
        void OnCauseChanged();
    }
}
