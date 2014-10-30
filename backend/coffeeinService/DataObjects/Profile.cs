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

        [JsonProperty(PropertyName = "headline")]
        public string HeadLine { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public string Industry { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }

        [JsonProperty(PropertyName = "educations")]
        public Education[] Educations { get; set; }



    }

    public class Education
    {

        [JsonProperty(PropertyName = "degree")]
        public string Degree { get; set; }

        [JsonProperty(PropertyName = "fieldOfStudy")]
        public string FieldOfStudy { get; set; }

        [JsonProperty(PropertyName = "schoolName")]
        public string SchoolName { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateObject EndDate { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateObject StartDate { get; set; }

    }

    public class DateObject
    {

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }
    }
}