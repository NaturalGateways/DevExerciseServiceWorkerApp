using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api
{
    public class ApiException : Exception
    {
        /// <summary>A message that is guaranteed to be user friendly.</summary>
        public string UserMessage { get; private set; }

        /// <summary>Constructor with the same message for the dev message and user message.</summary>
        public ApiException(string message)
            : base(message)
        {
            this.UserMessage = message;
        }

        /// <summary>Constructor with the same message for the dev message and user message.</summary>
        public ApiException(string devNessage, string userMessage)
            : base(devNessage)
        {
            this.UserMessage = userMessage;
        }

        /// <summary>Constructor with the same message for the dev message and user message.</summary>
        public ApiException(string message, Exception ex)
            : base(message, ex)
        {
            this.UserMessage = message;
        }

        /// <summary>Constructor with the same message for the dev message and user message.</summary>
        public ApiException(string devNessage, string userMessage, Exception ex)
            : base(devNessage, ex)
        {
            this.UserMessage = userMessage;
        }
    }
}
