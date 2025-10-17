using Newtonsoft.Json;
using UMS.Application.Exceptions;
using UMS.Application.Wrappers;
//using System.ComponentModel.DataAnnotations;

namespace UMS.API.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)    
            {
                await HandleExceptionAsync(context, ex);
                /*var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ApiResponse<string>() { Successed = false, Message = ex.Message};
                switch (ex)
                {
                    case ApiException e:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);*/
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new ApiResponse<string>("Invalid request data.");

            if (exception is ValidationException validationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = "Validation failed.";
                //errorResponse.Errors = validationException.ValidationResult.ErrorMessage;
            }
            else if (exception is ArgumentException argException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = argException.Message;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = exception.Message;
            }
            var response = context.Response;
            response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(errorResponse);
            return response.WriteAsync(result);
        }
    }
}
