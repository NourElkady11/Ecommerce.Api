
using Domain.Exeption;
using Shared.ErrorModels;
using System.Net;

namespace Ecommerce.Api.Midlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
                // hena yab2a ana keda mfee4 exeptions w ana rage3 men el request w ha handle b2a n el endpoint nafsha tkon m4 mawgodaa
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundEndPoint(httpContext);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Something Went Wrong {ex}");

                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message,
            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundEx => (int)HttpStatusCode.NotFound,
                UnAuthorizedException=> (int)HttpStatusCode.Unauthorized,
                ValidationExeption validationExeption => HandleValidationException(validationExeption,response),
                _ => (int)HttpStatusCode.InternalServerError
            };
            response.StatusCode=httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response);
        }
        private int HandleValidationException(ValidationExeption validationExeption, ErrorDetails response)
        {
            response.Errors = validationExeption.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
        private async Task HandleNotFoundEndPoint(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            //The type error details is for handling the responses of the Controllers Exeptions 
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End Point {httpContext.Request.Path} Not Found"
            };
            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
