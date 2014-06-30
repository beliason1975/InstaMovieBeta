﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InstaMovieBeta;

namespace InstaMovieBeta.Models.Rovi
{

    public class Result
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("relevance")]
        public Relevance[] Relevance { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("messages")]
        public object Messages { get; set; }

        [JsonProperty("movie")]
        public Movie Movie { get; set; }
    }

}
