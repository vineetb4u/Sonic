using LibraryManagement.Common.Exception;
using LibraryManagement.Common.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LibraryManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //var cors = new EnableCorsAttribute("*", "*", "*", "*");
            //config.EnableCors(cors);

            config.Filters.Add(new ModelValidationAttribute());
            config.Filters.Add(new ApiExceptionAttribute());


            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;

            jsonSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;


            // Web API routes
            config.MapHttpAttributeRoutes();
          
        }
       
    }
}
