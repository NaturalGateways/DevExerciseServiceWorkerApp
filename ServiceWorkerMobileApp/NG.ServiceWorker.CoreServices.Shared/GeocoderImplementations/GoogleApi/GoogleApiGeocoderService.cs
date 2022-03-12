using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.ServiceWorker.CoreServices.GoogleApi
{
    public class GoogleApiGeocoderService : IGeocoderService
    {
        #region Prediction class

        /// <summary>The suggestion class.</summary>
        private class Prediction : IGeocoderSuggestion
        {
            #region Base

            /// <summary>The prediction.</summary>
            private PlacesResponsePrediction m_prediction = null;

            /// <summary>Constructor.</summary>
            public Prediction(PlacesResponsePrediction prediction)
            {
                m_prediction = prediction;
            }

            #endregion

            #region IGeocoderSuggestion implementation

            /// <summary>The display name.</summary>
            public string DisplayName { get { return m_prediction.description; } }

            #endregion
        }

        #endregion

        #region JSON DTO classes

        /// <summary>Response from a Places API call.</summary>
        private class PlacesResponse
        {
            public string error_message { get; set; }

            public PlacesResponsePrediction[] predictions { get; set; }
        }

        /// <summary>Response from a Places API call.</summary>
        private class PlacesResponsePrediction
        {
            public string description { get; set; }
        }

        #endregion

        #region IGeocoderService implementation

        /// <summary>Gets a set of suggested addresses from a Geocoder source for a piece of text.</summary>
        private GeocoderSuggestionFetchResults GetSuggestionsForText(string addressText, int maxNum)
        {
            // Construct URL
            string urlDomain = "https://maps.googleapis.com";
            string encodedAddress = Services.HttpService.EncodeUrl(addressText);
            string apiKey = Services.ConfigService.GetString("GoogleApiKey");
            if (string.IsNullOrEmpty(apiKey))
            {
                return new GeocoderSuggestionFetchResults { ErrorText = "Missing API Key" };
            }
            string url = $"{urlDomain}/maps/api/place/autocomplete/json?input={encodedAddress}&components=country:au&key={apiKey}";

            // Get predictions
#if DEBUG
            string responseText = Services.HttpService.GetConnection(urlDomain).GetString(url);
#endif
            PlacesResponse response = Services.HttpService.GetConnection(urlDomain).GetJson<PlacesResponse>(url);
            if (response == null)
            {
                return new GeocoderSuggestionFetchResults { ErrorText = "Cannot contact server" };
            }
            if (string.IsNullOrEmpty(response.error_message) == false)
            {
                return new GeocoderSuggestionFetchResults { ErrorText = response.error_message };
            }
            if ((response.predictions?.Any() ?? false) == false)
            {
                return new GeocoderSuggestionFetchResults { ErrorText = "No matched addresses" };
            }

            // Return result
            return new GeocoderSuggestionFetchResults
            {
                Suggestions = response.predictions.Take(maxNum).Select(x => new Prediction(x)).ToArray()
            };
        }

        /// <summary>Gets a set of suggested addresses from a Geocoder source for a piece of text.</summary>
        public Task<GeocoderSuggestionFetchResults> GetSuggestionsForTextAsync(string addressText, int maxNum)
        {
            return Task.FromResult(GetSuggestionsForText(addressText, maxNum));
        }

        #endregion
    }
}
