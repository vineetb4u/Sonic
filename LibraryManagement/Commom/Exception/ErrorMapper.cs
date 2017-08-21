using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Common.Exception
{
    public static class ErrorMapper
    {
        //public static Error Map(Exception exception)
        //{
        //    Error error = new Error();
        //    error.StatusCode = HttpStatusCode.InternalServerError;

        //    /*if (exception is DataException)
        //    {
        //        error.StatusCode = HttpStatusCode.NotFound;
        //    }*/

        //    SubError subError = new SubError();

        //    subError.Exception = exception.GetType().Name;
        //    subError.Details = null; // hack to hide details from serializer when empty array

        //    subError.Message = exception.Message;
        //    if (exception is ArgumentException)
        //    {
        //        subError.Key = ((ArgumentException)exception).ParamName;
        //    }
        //    error.Errors.Add(subError);
        //    return error;
        //}

    }
}