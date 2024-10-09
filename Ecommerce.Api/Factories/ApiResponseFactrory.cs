using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace Ecommerce.Api.Factories
{
    public class ApiResponseFactrory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Where(error => error.Value.Errors.Any()).Select(error =>new ValidationError{Field=error.Key,Errors=error.Value.Errors.Select(e=>e.ErrorMessage) });
            //To capture errors from modelstate Dictionary and project it as a ValidationError

            var response = new ValidationErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validatin Falied",
                Errors = errors

            };
            return new BadRequestObjectResult(response);
        }
    }
}
