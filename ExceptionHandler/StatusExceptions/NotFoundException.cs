using System;
using System.Runtime.Serialization;

namespace ExceptionHandler.StatusExceptions
{
    [Serializable]
    //Used for HttpStatusCode 404 (NotFound) 
    //The server has not found anything matching the Request-URI./The Requested resource does not exists.
    //The key you are looking for is not found.
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
