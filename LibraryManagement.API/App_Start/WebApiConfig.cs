using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using LibraryManagement.Common.Validation;
using LibraryManagement.Common.Exception;
using Newtonsoft.Json;

namespace LibraryManagement.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
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
