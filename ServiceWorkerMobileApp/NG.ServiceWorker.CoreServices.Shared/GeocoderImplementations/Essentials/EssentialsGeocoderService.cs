using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace NG.ServiceWorker.CoreServices.Essentials
{
    public class EssentialsGeocoderService : IGeocoderService
    {
        #region Suggestion class

        /// <summary>Warpper for a suggested address.</summary>
        private class Suggestion : IGeocoderSuggestion
        {
            #region Base

            /// <summary>The location.</summary>
            private Location m_location = null;
            /// <summary>The location.</summary>
            private Placemark m_placemark = null;

            /// <summary>Constructor.</summary>
            public Suggestion(Location location, Placemark placemark)
            {
                m_location = location;
                m_placemark = placemark;
                this.DisplayName = $"{placemark.FeatureName}, {placemark.Locality} {placemark.AdminArea} {placemark.PostalCode}";
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
        public async Task<GeocoderSuggestionFetchResults> GetSuggestionsForTextAsync(string addressText, int maxNum)
        {
            // Get the locations
            IEnumerable<Location> locations = await Geocoding.GetLocationsAsync(addressText);
            if (locations == null)
            {
                return new GeocoderSuggestionFetchResults { ErrorText = "Cannot find any addresses." };
            }

            // Look up placemarks to create results
            List<IGeocoderSuggestion> suggestionList = new List<IGeocoderSuggestion>();
            foreach (Location location in locations)
            {
                IEnumerable<Placemark> placemarks = await Geocoding.GetPlacemarksAsync(location);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    suggestionList.Add(new Suggestion(location, placemark));
                }
            }

            // Return
            return new GeocoderSuggestionFetchResults { Suggestions = suggestionList.Take(maxNum).ToArray() };
        }

        #endregion
    }
}
