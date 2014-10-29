using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace coffeeinService.DataObjects
{
    public class Match : BaseDocument
    {
        public Match()
        {
            this.Type = "match";
        }
        [JsonProperty(PropertyName = "userOne")]
        public string UserOne { get; set; }

        [JsonProperty(PropertyName = "userTwo")]
        public string UserTwo { get; set; }

    }
}