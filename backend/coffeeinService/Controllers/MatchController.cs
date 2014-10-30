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
using MongoDB.Driver.Linq;
using User = Microsoft.Azure.Documents.User;

namespace coffeeinService.Controllers
{
    public class MatchController : ApiController
    {
        public ApiServices Services { get; set; }

        private BaseDocumentDomainManager<Match> MatchManager { get; set; }
        private BaseDocumentDomainManager<Profile> ProfileManager { get; set; }


        public MatchController()
        {

            MatchManager = new BaseDocumentDomainManager<Match>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
            ProfileManager = new BaseDocumentDomainManager<Profile>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
        }

        // GET api/user/{userId}/Match
        [Route("api/user/{userId}/match")]
        public IEnumerable<Profile> GetAllMatchesForUser(string userId)
        { 
            var matches =  MatchManager.Query("match").Where(o => o.UserOne == userId || o.UserTwo == userId).ToList();
            foreach (var match in matches)
            {
                
            }
            var profiles = ProfileManager.Query().Where(d=>d.UserId != userId).Where(d => d.Type == "profile").ToList();



            return profiles.Where(d => matches.Any(m => m.UserOne == d.UserId || m.UserTwo == d.UserId));
        }


    }
}
