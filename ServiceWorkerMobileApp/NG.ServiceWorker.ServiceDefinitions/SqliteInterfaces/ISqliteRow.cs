using System;

namespace NG.ServiceWorker
{
    public interface ISqliteRow
    {
        /// <summary>Skips a column.</summary>
        void SkipColumn();

        /// <summary>Getter for a not-nullable string.</summary>
        string GetString();

        /// <summary>Getter for a not-nullable datetime.</summary>
        DateTime GetDateTime();

        /// <summary>Getter for a not-nullable integer.</summary>
        int GetInteger();

        /// <summary>Getter for a not-nullable string of JSON of a specific type.</summary>
        JsonType GetJson<JsonType>();
    }
}
