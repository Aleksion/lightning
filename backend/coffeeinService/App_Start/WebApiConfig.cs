using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using coffeeinService.DocumentDbWiring;
using Microsoft.Azure.Documents;
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

            var dbIniaitializer = new DocumentDbInitializer();
            dbIniaitializer.InitializeDatabase();
        }
    }

    public class DocumentDbInitializer
    {
        public DocumentDbInitializer()
        {
            dbContext = new DocumentDbContext("userRelatedCollection", "CoffeeInDocumentDb");
        }
        private DocumentDbContext dbContext { get; set; }

        public async void InitializeDatabase()
        {
            await dbContext.DeleteDatabase();
            var user = new User();

            for (int i = 0; i < 10; i++)
            {
                user.Id = i + "RandomId";

                await dbContext.Client.CreateUserAsync(dbContext.Collection.SelfLink, user);

                var profile = new Profile
                {
                    GivenName = "Aleksander " +i,
                    Picture = "https://media.licdn.com/mpr/mprx/0_Ow-uHBJRKD3_LlxoyDi1HqpMrfLG5lOopf61HqVLfWA-szdEtSG8QNmIOG5j6n060ertFPXI6Ydt",
                    HeadLine = "Founder at Kare Media",
                    UserId = user.Id
                    
                };

                await dbContext.Client.CreateDocumentAsync(dbContext.Collection.SelfLink, profile);

            }
        }
    }
}

