using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using coffeeinService.DataObjects;
using coffeeinService.DocumentDbWiring;
using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Mobile.Service;
using User = Microsoft.Azure.Documents.User;

namespace coffeeinService.Controllers
{
    public class DislikeController : ApiController
    {
        public ApiServices Services { get; set; }

        private BaseDocumentDomainManager<Dislike> DislikeManager { get; set; }

        public DislikeController()
        {

            DislikeManager = new BaseDocumentDomainManager<Dislike>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
        }

        // GET api/user/{userId}/Match
        [Route("api/user/{userId}/dislike")]
        public IQueryable<Dislike> GetAllDislikesForUser(string userId)
        {
            return DislikeManager.Query("dislike").Where(o => o.UserId == userId);
        }

        // GET api/user/{userId}/Match
        [Route("api/user/{userId}/dislike")]
        public async Task<IHttpActionResult> PostDislike(string userId, Dislike dislike)
        {
            dislike.UserId = userId;
            var doc = await DislikeManager.InsertAsync(dislike);

            return CreatedAtRoute("DefaultApis", new { controller = "like", id = doc.Id }, doc);
        }


    }
}
