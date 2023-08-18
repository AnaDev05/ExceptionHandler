using System;

namespace ExceptionHandler.Handler
{
    public interface IExceptionHandlerClass
    {
        public ErrorDetails HandleException(Exception exception);
    }
}
