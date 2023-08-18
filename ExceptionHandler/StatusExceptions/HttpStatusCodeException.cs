using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ExceptionHandler.StatusExceptions
{
    [Serializable]
    public class HttpStatusCodeException : Exception
    {
        public CustomStatus CustomStatus { get; set; }

        public HttpStatusCodeException(CustomStatus customStatus, string message = "")
        : base(message)
        {
            CustomStatus = customStatus; 
        }

        public HttpStatusCodeException(CustomStatus customStatus, Exception inner)
            : this(customStatus, inner.ToString()) { }

        public HttpStatusCodeException(CustomStatus customStatus, JObject errorObject)
            : this(customStatus, errorObject.ToString()) { }

    }

    public class CustomStatus
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string DefaultStatusMessage { get; set; }
    }
}
