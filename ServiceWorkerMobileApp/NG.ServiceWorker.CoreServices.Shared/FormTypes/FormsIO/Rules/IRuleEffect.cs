using System;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.Rules
{
    public interface IRuleEffect
    {
        /// <summary>Proprty for whether the rule target is visible.</summary>
        bool RuleEffectIsVisible { get; set; }

        /// <summary>Proprty for the effect model.</summary>
        UI.Model EffectModel { get; }
    }
}
