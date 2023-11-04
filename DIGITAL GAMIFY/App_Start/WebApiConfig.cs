using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace DIGITAL_GAMIFY
{
    public static class WebApiConfig
    {
        public class BrowserJsonFormatter : JsonMediaTypeFormatter
        {
            public BrowserJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
        //private static void EnableCrossSiteRequests(HttpConfiguration config)
        //{
        //    var cors = new EnableCorsAttribute(
        //        origins: "*",
        //        headers: "*",
        //        methods: "*");
        //    config.EnableCors(cors);
        //}

        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors();
            //// Web API configuration and services
            //EnableCrossSiteRequests(config);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Clear();
            config.Formatters.Add(new BrowserJsonFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        }
    }
}
