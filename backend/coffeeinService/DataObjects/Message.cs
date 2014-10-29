using System.ComponentModel;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace coffeeinService.DataObjects
{
    public class Message : BaseDocument
    {
        public Message()
        {
            this.Type = "message";
        }
        [JsonProperty(PropertyName = "matchId")]
        public string MatchId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

    }
}