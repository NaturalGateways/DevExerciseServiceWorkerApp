using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.ServiceWorker.CoreServices.Geocoder.Essentials
{
    public class EssentialsGeocoderService : IGeocoderService
    {
        #region IGeocoderService implementation

        /// <summary>Warpper for a suggested address.</summary>
        private class Suggestion : IGeocoderSuggestion
        {
            #region Base

            /// <summary>Constructor.</summary>
            public Suggestion(string addressText)
            {
                this.DisplayName = addressText;
            }

            #endregion

            #region IGeocoderSuggestion implementation

            /// <summary>The display name.</summary>
            public string DisplayName { get; private set; }

            #endregion
        }

        #endregion

        #region IGeocoderService implementation

        /// <summary>Gets a set of suggested addresses from a Geocoder source for a piece of text.</summary>
        public Task<GeocoderSuggestionFetchResults> GetSuggestionsForTextAsync(string addressText, int maxNum)
        {
            System.Threading.Thread.Sleep(500);

            //return Task.FromResult<GeocoderSuggestionFetchResults>(new GeocoderSuggestionFetchResults { ErrorText = "Not implemented" });

            return Task.FromResult<GeocoderSuggestionFetchResults>(new GeocoderSuggestionFetchResults
            {
                Suggestions = new IGeocoderSuggestion[]
                {
                    new Suggestion("Alpha"),
                    new Suggestion("Beta"),
                    new Suggestion("Gamma")
                }
            });
        }

        #endregion
    }
}
