using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculatorService.Server.ExceptionHandler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode ErrorStatus;
            string message;
            string ErrorCode = "Internal error";

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                ErrorStatus = HttpStatusCode.BadRequest;
            }
            else
            {
                ErrorStatus = HttpStatusCode.InternalServerError;
                message = exception.Message;
            }

            var result = JsonSerializer.Serialize(new { ErrorMessage = message, ErrorStatus, ErrorCode });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ErrorStatus;

            return context.Response.WriteAsync(result);
        }
    }
}
