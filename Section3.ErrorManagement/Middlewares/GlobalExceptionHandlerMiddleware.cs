using System.Net;
using Section3.ErrorManagement.Results;

namespace Section3.ErrorManagement.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;
        public GlobalExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                logger.LogError($"Something went wrong: {exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorResult()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            };
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorDetails.Message = "Resource not found.";
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorDetails.Message = "Access denied.";
            }

            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }

    public class NotFoundException : Exception { }
}