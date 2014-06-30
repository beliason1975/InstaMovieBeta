﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InstaMovieBeta;

namespace InstaMovieBeta.Models.Rovi
{

    public class PrecededBy
    {

        [JsonProperty("ids")]
        public Ids Ids { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("directors")]
        public Director[] Directors { get; set; }

        [JsonProperty("releaseYear")]
        public int? ReleaseYear { get; set; }

        [JsonProperty("rating")]
        public int? Rating { get; set; }
    }

}
