using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace coffeeinService.DataObjects
{
    public class Like : BaseDocument
    {
        public Like()
        {
            this.Type = "like";
        }
              [JsonProperty(PropertyName = "userId")]
              public string UserId { get; set; }

              [JsonProperty(PropertyName = "likedUser")]
              public string LikedUser { get; set; }

    }
}