using CVU.CONDICA.ExceptionHandling.Exceptions;
using CVU.CONDICA.ExceptionHandling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IExceptionFilter = Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter;

namespace CVU.CONDICA.ExceptionHandling
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public virtual void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case BusinessException exception:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new JsonResult(new ErrorResponse(exception.FailureReason));
                    break;
                case RequestValidationFailedException exception:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new JsonResult(new ErrorResponse(FailureReason.ValidationErrors, exception.Failures));
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new JsonResult(new ErrorResponse { Detail = $"{context.Exception?.Message}, {context.Exception?.StackTrace}, {context.Exception?.InnerException.Message}" });
                    context.Result = new JsonResult(new ErrorResponse(FailureReason.InternalError));
                    break;
            }
        }
    }
}
