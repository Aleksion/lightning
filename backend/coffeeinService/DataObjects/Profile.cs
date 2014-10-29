using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.Azure.Documents;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;

namespace coffeeinService.DataObjects
{
    public class Profile : BaseDocument
    {
        public Profile()
        {
            this.Type = "profile";
        }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "lastKnownLatitude")]
        public double LastKnownLatitude { get; set; }

        [JsonProperty(PropertyName = "lastKnownLongitude")]
        public double LastKnownLongitude { get; set; }

        [JsonProperty(PropertyName = "linkedInId")]
        public string LinkedInId { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "headLine")]
        public string HeadLine { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public string Industry { get; set; }

        [JsonProperty(PropertyName = "profilePictureUrl")]
        public string ProfilePictureUrl { get; set; }



    }
}