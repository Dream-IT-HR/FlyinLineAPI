using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Northwind.Application.Exceptions;

namespace Flyinline.WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;

            IActionResult result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });

            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (context.Exception is NotAuthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else if (context.Exception is ValidationException)
            {

                code = HttpStatusCode.BadRequest;
                result = new JsonResult(
                    ((ValidationException)context.Exception).Failures);

            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = result;
        }
    }
}
