using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.ServiceWorker
{
    public class GeocoderSuggestionFetchResults
    {
        /// <summary>If not null, the error for a fetch.</summary>
        public string ErrorText { get; set; }

        /// <summary>The fetched suggestions.</summary>
        public IEnumerable<IGeocoderSuggestion> Suggestions { get; set; }
    }

    public interface IGeocoderSuggestion
    {
        /// <summary>The display name.</summary>
        string DisplayName { get; }
    }

    public interface IGeocoderService
    {
        /// <summary>Gets a set of suggested addresses from a Geocoder source for a piece of text.</summary>
        Task<GeocoderSuggestionFetchResults> GetSuggestionsForTextAsync(string addressText, int maxNum);
    }
}
