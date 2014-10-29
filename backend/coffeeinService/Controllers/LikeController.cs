using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using coffeeinService.DataObjects;
using coffeeinService.DocumentDbWiring;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Mobile.Service;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using User = Microsoft.Azure.Documents.User;

namespace coffeeinService.Controllers
{
    public class LikeController : ApiController
    {
        public ApiServices Services { get; set; }

        private BaseDocumentDomainManager<Like> LikeManager { get; set; }
        private BaseDocumentDomainManager<Match> MatchManager { get; set; }

        public LikeController()
        {
            LikeManager = new BaseDocumentDomainManager<Like>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
            MatchManager = new BaseDocumentDomainManager<Match>("userRelatedCollection", "CoffeeInDocumentDb", Request, Services);
        }

        // GET api/user/{userId}/Like
        [Route("api/user/{userId}/Like")]
        public IQueryable<Like> GetAllLikesForUser(string userId)
        {
            return LikeManager.Query("like").Where(o => o.UserId == userId);
        }

        public Match GetMatch(string id)
        {
            return MatchManager.Lookup(id);
        }
        // POST api/user/{userId}/Like
        [Route("api/user/{userId}/Like")]
        public async Task<IHttpActionResult> PostLikeForUser(Like like, string userId)
        {
            like.UserId = userId;

            var matchTrigger = new Trigger()
            {
                Id = "AddMatchToResponse",
                Body = @"function() {

var context = getContext();
        var collection = context.getCollection();
        var response = context.getResponse();

        // document that was created
        var createdDocument = response.getBody();
        
var filterQuery =  'SELECT * FROM root WHERE ((root.likedUser =""' + createdDocument.userId+'"") AND (root.userId = ""'+createdDocument.likedUser+'""))';

var accept = collection.queryDocuments(collection.getSelfLink(), filterQuery,
            addMatchDetails);
        if(!accept) throw 'Unable to find like while creating Match, abort';

function addMatchDetails(err, documents, responseOptions) {
            if(err) throw new Error('Error' + err.message);
                     if(documents.length != 1) return;

                     createdDocument.userIdOfMatchingLike = documents[0].userId;                   
                    
                     return;                    
        }          
    }",
                TriggerOperation = TriggerOperation.Create,
                TriggerType = TriggerType.Post
            };
                var doc = await LikeManager.InsertAsync(like, matchTrigger);
            if (doc.GetPropertyValue<string>("userIdOfMatchingLike") != null)
            {
                await MatchManager.InsertAsync(new Match {UserOne = like.UserId, UserTwo = like.LikedUser});
            }
                return CreatedAtRoute("DefaultApis", new {controller="like", id = doc.Id }, doc);
        }



    }
}
