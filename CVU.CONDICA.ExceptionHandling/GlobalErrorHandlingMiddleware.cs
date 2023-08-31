using CVU.CONDICA.ExceptionHandling.Exceptions;
using CVU.CONDICA.ExceptionHandling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CVU.CONDICA.ExceptionHandling
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var stackTrace = string.Empty;

            object responseBody;

            //var exception = exception.GetType();

            switch (ex)
            {
                case BusinessException exception:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    responseBody = new JsonResult(new ErrorResponse(exception.FailureReason));
                    break;
                case RequestValidationFailedException exception:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    responseBody = new JsonResult(new ErrorResponse(FailureReason.ValidationErrors, exception.Failures));
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    responseBody = new JsonResult(new ErrorResponse(FailureReason.InternalError));
                    break;
            }

            var exceptionResult = JsonSerializer.Serialize(responseBody);
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
