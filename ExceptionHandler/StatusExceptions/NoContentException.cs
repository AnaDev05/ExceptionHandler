using System;
using System.Runtime.Serialization;

namespace ExceptionHandler.StatusExceptions
{
    [Serializable]
    // Used for HttpStatusCode 204 (NoContent);
    // Request sucessfully, but with no returned content.
    public class NoContentException: Exception
    {
        public NoContentException(string message) : base(message)
        {
        }

        public NoContentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
