using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using coffeeinService.DataObjects;
using coffeeinService.Models;

namespace coffeeinService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
            options.CorsPolicy =
            new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new coffeeinInitializer());
        }
    }

    public class coffeeinInitializer : ClearDatabaseSchemaIfModelChanges<coffeeinContext>
    {
        protected override void Seed(coffeeinContext context)
        {
            

            base.Seed(context);
        }
    }
}

