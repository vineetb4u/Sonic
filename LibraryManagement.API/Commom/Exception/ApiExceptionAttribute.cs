using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace LibraryManagement.Common.Exception
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            actionExecutedContext.ActionContext.Response = actionExecutedContext.ActionContext.Request.CreateResponse(exception.Message);

            base.OnException(actionExecutedContext);
        }
    }
}