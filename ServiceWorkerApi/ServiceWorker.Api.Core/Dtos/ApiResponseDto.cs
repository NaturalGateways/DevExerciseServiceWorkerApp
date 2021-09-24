using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api
{
    /// <summary>The DTO of the response.</summary>
    public class ApiResponseDto
    {
        /// <summary>The actual response.</summary>
        public bool Success { get; set; }

        /// <summary>The actual response.</summary>
        public object Response { get; set; }

        /// <summary>The error.</summary>
        public string ErrorMessage { get; set; }
        /// <summary>The error.</summary>
        public List<ApiResponseExceptionDto> ErrorException { get; set; }
    }

    /// <summary>The DTO of an exception of the response.</summary>
    public class ApiResponseExceptionDto
    {
        /// <summary>The class type name.</summary>
        public string TypeName { get; set; }

        /// <summary>The message.</summary>
        public string Message { get; set; }

        /// <summary>The stack trace.</summary>
        public string StackTrace { get; set; }
    }
}
