using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using coffeeinService.DataObjects;
using coffeeinService.DocumentDbWiring;
using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Mobile.Service;
using User = Microsoft.Azure.Documents.User;

namespace coffeeinService.Controllers
{
    public class MatchController : ApiController
    {
        public ApiServices Services { get; set; }

        private BaseDocumentDomainManager<Match> MatchManager { get; set; }

        public MatchController()
        {

            MatchManager = new BaseDocumentDomainManager<Match>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
        }

        // GET api/user/{userId}/Match
        [Route("api/user/{userId}/match")]
        public IQueryable<Match> GetAllMatchesForUser(string userId)
        {
            return MatchManager.Query("match").Where(o => o.UserOne == userId || o.UserTwo == userId);
        }


    }
}
