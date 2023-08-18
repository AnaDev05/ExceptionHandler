using System;
using System.Runtime.Serialization;

namespace ExceptionHandler.StatusExceptions
{
    [Serializable]
    //StatusCode 422 Unprocessable Entity
    //the server understands the content type of the request entity, and the syntax of the request entity is correct,
    //but it was unable to process the contained instructions
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string message) : base(message)
        {
        }

        public UnprocessableEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnprocessableEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
