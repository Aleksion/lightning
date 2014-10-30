using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace coffeeinService.DataObjects
{
    public class Dislike : BaseDocument
    {
        public Dislike()
        {
            this.Type = "dislike";
        }
              [JsonProperty(PropertyName = "userId")]
              public string UserId { get; set; }

              [JsonProperty(PropertyName = "dislikedUser")]
              public string DislikedUSer { get; set; }

    }
}