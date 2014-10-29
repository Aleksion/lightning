using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using coffeeinService.DocumentDbWiring;
using Microsoft.Azure.Documents;
using Microsoft.WindowsAzure.Mobile.Service;
using coffeeinService.DataObjects;
using coffeeinService.Models;

namespace coffeeinService.Controllers
{
    public class UserController : UserDocumentController<User>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new UserDocumentDomainManager("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
        }

        public IQueryable<User> GetAllUsers()
        {
            return Query();
        }

        public User GetUser(string id)
        {
            return Lookup(id);
        }


        public async Task<IHttpActionResult> PostUser(User profile)
        {
            var doc = await InsertAsync(profile);

            return CreatedAtRoute("DefaultApis", new { id = doc.Id }, doc);
        }

        public Task DeleteUser(string id)
        {
            return DeleteAsync(id);
        }
    }
}