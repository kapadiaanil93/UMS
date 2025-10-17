using UMS.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UMS.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors?.Select(e => e.ErrorMessage).ToArray()
                    );

                var response = new ApiResponse<string>("Invalid request data")
                {
                    Errors = errors.SelectMany(e => e.Value).ToList()
                };
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
