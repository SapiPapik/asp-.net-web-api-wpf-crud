using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Workflow.ComponentModel.Compiler;

namespace ProductCatalog.Web.Filters.Validations {
    public class ApiException : Exception {
        public HttpStatusCode StatusCode { get; set; }

        public ValidationErrorCollection Errors { get; set; }

        public ApiException(string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            ValidationErrorCollection errors = null) :
            base(message) {
            StatusCode = statusCode;
            Errors = errors;
        }
        public ApiException(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(ex.Message) {
            StatusCode = statusCode;
        }
    }
}