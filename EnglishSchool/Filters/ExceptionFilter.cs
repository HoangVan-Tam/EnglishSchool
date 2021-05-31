using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace EnglishSchool.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errorMessage = string.Empty;
            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                errorMessage = actionExecutedContext.Exception.Message;
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                errorMessage = actionExecutedContext.Exception.Message;
                statusCode = HttpStatusCode.NotFound;
            }
            else
            {
                errorMessage = "Something Went Wrong :)))";
                statusCode = HttpStatusCode.InternalServerError;
            }
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(errorMessage),
                ReasonPhrase = "From Exception Filter"
            };

            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}