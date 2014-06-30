using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaMovieBeta.Models.Rovi.Models.SearchResponseJsonTypes
{
    public class People
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}