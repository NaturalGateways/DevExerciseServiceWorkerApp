using System;

namespace NG.ServiceWorker
{
    public interface ISqliteRow
    {
        /// <summary>Getter for a not-nullable string.</summary>
        string GetString();

        /// <summary>Getter for a not-nullable datetime.</summary>
        DateTime GetDateTime();

        /// <summary>Getter for a not-nullable integer.</summary>
        int GetInteger();
    }
}
