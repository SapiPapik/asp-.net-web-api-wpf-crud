using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using ProductCatalog.BLL.Services.Exceptions;
using ProductCatalog.Web.Filters.Validations;

namespace ProductCatalog.Web.Filters {
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute {
        public override void OnException(HttpActionExecutedContext context) {
            ApiError apiError;
            HttpStatusCode statusCode;
            if (context.Exception is ApiException) {
                var ex = context.Exception as ApiException;
                apiError = new ApiError(ex.Message) { Errors = ex.Errors };
                statusCode = ex.StatusCode;
            }
            else if (context.Exception is UnauthorizedAccessException) {
                apiError = new ApiError("Unauthorized Access");
                statusCode = HttpStatusCode.Unauthorized;

                // handle logging here
            }
            else if (context.Exception is NotFoundException) {
                apiError = new ApiError(context.Exception.Message);
                statusCode = HttpStatusCode.NotFound;

            }
            else if (context.Exception is ArgumentNullException) {
                apiError = new ApiError(context.Exception.Message);
                statusCode = HttpStatusCode.NotFound;
            }
            else if (context.Exception is ObjectAlreadyExistException) {
                apiError = new ApiError(context.Exception.Message);
                statusCode = HttpStatusCode.Conflict;
            }
            else {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg) { Detail = stack };

                statusCode = HttpStatusCode.InternalServerError;
            }

            context.Exception = null;

            context.Response =
                context.Request.CreateErrorResponse(statusCode, JsonConvert.SerializeObject(apiError.Message));
            base.OnException(context);
        }
    }
}