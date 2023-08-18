using System;
using System.Runtime.Serialization;

namespace ExceptionHandler.StatusExceptions
{
    [Serializable]
    // Used for HttpStatusCode 400 (BadRequest)
    //Missing data, domain validation, invalid formatting.
    //Can be used for ArgumentNullException
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
