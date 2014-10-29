using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace coffeeinService.DataObjects
{
    public class BaseDocument : Resource
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } 
    }
}
