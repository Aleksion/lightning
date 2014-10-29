using System;
using System.Collections;
using System.Collections.Generic;
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
    public class ProfileController : DocumentController<Profile>
    {

        private BaseDocumentDomainManager<Like> LikeManager { get; set; }
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new BaseDocumentDomainManager<Profile>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
            LikeManager = new BaseDocumentDomainManager<Like>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
        }
        
        public IQueryable<Profile> GetAllProfiles()
        {
            return Query("profile");
        }


        [Route("api/user/{userId}/profiles")]
        public IEnumerable<Profile> GetPotentialMatches(string userId)
        {
            var userLikes = LikeManager.Query().Where(d => d.UserId == userId).Where(d => d.Type == "like").ToList();
            var potentialMatches = Query().Where(d => d.UserId != userId).Where(d => d.Type == "profile").ToList();

            return potentialMatches.Except(potentialMatches.Join(userLikes, u => u.UserId, p => p.LikedUser, (u,p)=> u));

        }

        public Profile GetProfile(string id)
        {
            var user = new User();
            return Lookup(id);
        }


        [HttpPut]
        public Task<Profile> ReplaceProfile(string id, Profile profile)
        {
            return ReplaceAsync(id, profile);
        }

        [Route("api/user/{userId}/profile")]
        public async Task<IHttpActionResult> PostProfile(Profile profile, string userId)
        {
            profile.UserId = userId;
            var doc = await InsertAsync(profile);

            return CreatedAtRoute("DefaultApis", new { controller = "profile", id = doc.Id }, doc);
        }

        public Task DeleteProfile(string id)
        {
            return DeleteAsync(id);
        }
    }
}