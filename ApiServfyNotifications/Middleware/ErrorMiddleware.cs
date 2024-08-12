using ApiServfyNotifications.Application.Exceptions;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace ApiServfyNotifications.Middleware
{
    public class ErrorMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            List<string> errors = new List<string>();
            HttpStatusCode statusCode;

            switch (exception)
            {
                case AppException appException:
                    errors.Add(exception.Message);
                    statusCode = appException.StatusCode ?? HttpStatusCode.InternalServerError;
                    break;

                case AppCrossCuttingException crossCuttingException:
                    errors.Add(exception.Message);
                    statusCode = crossCuttingException.StatusCode ?? HttpStatusCode.InternalServerError;
                    break;
                default:
                    errors.Add(exception.Message);
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new
            {
                errors = errors,
                status = statusCode
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}
