using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using ODataServer.Models;

namespace ODataServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Attendee>("Attendees");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            ODataConventionModelBuilder builder1 = new ODataConventionModelBuilder();
            builder1.EntitySet<MovieTicket>("MovieTickets");
            config.Routes.MapODataServiceRoute("odata1", "odata", builder1.GetEdmModel());
        }
    }
}
