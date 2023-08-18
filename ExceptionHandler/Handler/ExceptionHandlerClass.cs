using System;
using System.Linq;
using System.Net;
using ExceptionHandler.StatusExceptions;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ExceptionHandler.Handler
{
    public class ExceptionHandlerClass : IExceptionHandlerClass
    {
        private readonly ILogger<ExceptionHandlerClass> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlerClass(ILogger<ExceptionHandlerClass> logger,
            IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public ErrorDetails HandleException(Exception exception)
        {
            var error = exception switch
            {
                NotFoundException resourceNotFoundException => HandleResourceNotFoundException(resourceNotFoundException),
                BadRequestException badRequestException => HandleBadRequestException(badRequestException),
                ConflictException conflictException => HandleConflictException(conflictException),
                NoContentException noContentException => HandleNoContentException(noContentException),
                UnprocessableEntityException unprocessableEntityException => HandleUnprocessableEntityException(unprocessableEntityException),
                HttpStatusCodeException httpStatusCodeException => HandleHttpStatusCodeException(httpStatusCodeException),
                ValidationException validationException => HandleValidationException(validationException),
                _ => HandleUnhandledExceptions(exception)
            };

            if (_environment.IsDevelopment())
            {
                error.Error = exception.StackTrace.ToString();
            }

            return error;
        }

        private ErrorDetails HandleValidationException(ValidationException validationException)
        {
            var message = validationException.Errors.Any() ? string.Join(System.Environment.NewLine, validationException.Errors) : validationException.Message;
            return new ErrorDetails
            {
                Status = (int)HttpStatusCode.PreconditionFailed,
                Message = message,
                StatusText = HttpStatusCode.PreconditionFailed.ToString(),
                HResult = validationException.HResult
            };
        }

        private ErrorDetails HandleHttpStatusCodeException(HttpStatusCodeException httpStatusCodeException)
        {
            _logger.LogInformation(httpStatusCodeException, httpStatusCodeException.Message);

            return new ErrorDetails
            {
                Status = (int)httpStatusCodeException.CustomStatus.HttpStatusCode,
                Message = httpStatusCodeException.Message,
                StatusText = httpStatusCodeException.CustomStatus.HttpStatusCode.ToString(),
                HResult = httpStatusCodeException.HResult
            };
        }

        private ErrorDetails HandleNoContentException(NoContentException noContentException)
        {
            _logger.LogInformation(noContentException, noContentException.Message);

            return new ErrorDetails
            {
                Status = (int)HttpStatusCode.NoContent,
                Message = string.Empty,
                StatusText = HttpStatusCode.NoContent.ToString(),
                HResult = noContentException.HResult
            };
        }

        private ErrorDetails HandleUnprocessableEntityException(UnprocessableEntityException unprocessableEntityException)
        {
            _logger.LogInformation(unprocessableEntityException, unprocessableEntityException.Message);

            return new ErrorDetails
            {
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Message = unprocessableEntityException.Message,
                StatusText = HttpStatusCode.UnprocessableEntity.ToString(),
                HResult = unprocessableEntityException.HResult
            };
        }

        private ErrorDetails HandleConflictException(ConflictException conflictException)
        {
            _logger.LogInformation(conflictException, conflictException.Message);

            return new ErrorDetails
            {
                Message = conflictException.Message,
                Status = (int)HttpStatusCode.Conflict,
                StatusText = HttpStatusCode.Conflict.ToString(),
                HResult = conflictException.HResult
            };
        }

        private ErrorDetails HandleResourceNotFoundException(NotFoundException notFoundException)
        {
            _logger.LogInformation(notFoundException, notFoundException.Message);

            return new ErrorDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Message = notFoundException.Message,
                StatusText = HttpStatusCode.NotFound.ToString(),
                HResult = notFoundException.HResult
            };
        }

        private ErrorDetails HandleBadRequestException(BadRequestException badRequestException)
        {
            _logger.LogInformation(badRequestException, badRequestException.Message);

            return new ErrorDetails
            {
                Message = badRequestException.Message,
                Status = (int)HttpStatusCode.BadRequest,
                StatusText = HttpStatusCode.BadRequest.ToString(),
                HResult = badRequestException.HResult
            };
        }

        private ErrorDetails HandleUnhandledExceptions(Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            return new ErrorDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = $"Something went wrong... Operation can't be done. Please check the message {exception.Message}",
                StatusText = HttpStatusCode.InternalServerError.ToString(),
                HResult = exception.HResult
            };
        }
    }
}
