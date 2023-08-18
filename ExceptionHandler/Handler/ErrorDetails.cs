using System;

namespace ExceptionHandler.Handler
{
    [Serializable]
    public class ErrorDetails
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string StatusText { get; set; }
        public string Error { get; set; }
        public int HResult { get; set; }

    }
}
