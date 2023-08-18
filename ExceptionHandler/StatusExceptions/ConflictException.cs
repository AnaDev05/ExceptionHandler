using System;
using System.Runtime.Serialization;

namespace ExceptionHandler.StatusExceptions
{
    [Serializable]
    // Used for HttpStatusCode 409 (NoContent);
    // indicates a request conflict with the current state of the target resource. eg:File Already Exists.
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
