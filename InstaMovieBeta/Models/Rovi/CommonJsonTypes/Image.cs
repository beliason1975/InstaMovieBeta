﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace InstaMovieBeta.Models.Rovi.CommonJsonTypes
{

    public class Image
    {

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("formatid")]
        public int Formatid { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("copyrightOwner")]
        public string CopyrightOwner { get; set; }

        [JsonProperty("imageTypeId")]
        public int ImageTypeId { get; set; }

        [JsonProperty("zoomLevel")]
        public int ZoomLevel { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
