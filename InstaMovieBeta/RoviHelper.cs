using InstaMovieBeta.Models.Rovi;
using InstaMovieBeta.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace InstaMovieBeta
{
    public class RoviHelper
    {
        private class KeyInfo
        {
            public string Key { get; set; }
            public string SharedSecret { get; set; }
            public string BaseUrl { get; set; }
        }

        private Dictionary<string, KeyInfo> Keys = new Dictionary<string, KeyInfo>()
        {
            {"Search", new KeyInfo{Key = "edar754hh646dayx3zkr2fey", SharedSecret = "zukWGATaSz", BaseUrl = "http://api.rovicorp.com/search/v2.1/"}},
            {"Data", new KeyInfo{Key = "edar754hh646dayx3zkr2fey", SharedSecret = "zukWGATaSz", BaseUrl = "http://api.rovicorp.com/data/v1.1/"}},
            {"TV", new KeyInfo{Key = "zj4jdpk8qc6dfs7w84nuvs4a", SharedSecret = string.Empty, BaseUrl = string.Empty}}
        };

        public enum RoviCallType
        {
            Lookup = 1,
            Search = 2
        }

        public enum RoviSearchType
        {
            Search = 1,
            Autocomplete = 2,
            SingleStageSearch = 3,
            FilterBrowse = 4,
            Sort = 5
        }

        public enum RoviSearchEndpoint
        {
            Music = 1,
            AmgVideo = 2,
            Video = 3
        }

        public enum RoviDataEndpoint
        {
            Music = 1,
            Movie = 2,
            Video = 3
        }

        public async Task<SearchResponse> Search(RoviSearchType searchType, RoviSearchEndpoint endpointType, string queryParams)
        {
            string key = Keys["Search"].Key;
            string sharedSecret = Keys["Search"].SharedSecret;
            string url = Keys["Search"].BaseUrl;
            string unixTime = Utility.GetCurrentUnixTimestampSeconds().ToString();
            byte[] combBytes = System.Text.Encoding.UTF8.GetBytes(key + sharedSecret + unixTime);
            string sig = Utility.ToHex(MD5.Create("System.Security.Cryptography.MD5").ComputeHash(combBytes), false);
            string endpoint = null;

            switch (endpointType)
	        {
		        case RoviSearchEndpoint.Music:
                    endpoint = "music/";
                    break;
                case RoviSearchEndpoint.AmgVideo:
                    endpoint = "amgvideo/";
                    break;
                case RoviSearchEndpoint.Video:
                    endpoint = "video/";
                    break;
                default:
                    break;
	        }
            switch (searchType)
            {
                case RoviSearchType.Search:
                    url += endpoint + "search";
                    break;
                case RoviSearchType.Autocomplete:
                    url += endpoint + "autocomplete";
                    break;
                case RoviSearchType.SingleStageSearch:
                    url += endpoint + "singlestagesearch";
                    break;
                case RoviSearchType.FilterBrowse:
                    url += endpoint + "filterbrowse";
                    break;
                case RoviSearchType.Sort:
                    url += endpoint + "sort";
                    break;
                default:
                    break;
            }

            url += "?apikey=" + key + "&sig=" + sig + queryParams;
            string content = await MakeCall(url);
            if (!string.IsNullOrEmpty(content))
            {
                return (SearchResponse)JsonConvert.DeserializeObject(content, typeof(SearchResponse));
            }

            return null;
        }

        public async Task<Object> Lookup(RoviDataEndpoint endpointType, string queryParams)
        {
            string key = Keys["Data"].Key;
            string sharedSecret = Keys["Data"].SharedSecret;
            string url = Keys["Data"].BaseUrl;
            string unixTime = Utility.GetCurrentUnixTimestampSeconds().ToString();
            byte[] combBytes = System.Text.Encoding.UTF8.GetBytes(key + sharedSecret + unixTime);
            string sig = Utility.ToHex(MD5.Create("System.Security.Cryptography.MD5").ComputeHash(combBytes), false);
            string endpoint = null;

            switch (endpointType)
            {
                case RoviDataEndpoint.Music:
                    endpoint = "music/info";
                    break;
                case RoviDataEndpoint.Movie:
                    endpoint = "movie/info";
                    break;
                case RoviDataEndpoint.Video:
                    endpoint = "video/info";
                    break;
                default:
                    break;
            }

            url += endpoint + "?apikey=" + key + "&sig=" + sig + queryParams;
            string content = await MakeCall(url);
            object returnValue = null;
            if (!string.IsNullOrEmpty(content))
            {
                switch (endpointType)
                {
                    case RoviDataEndpoint.Music:
                        break;
                    case RoviDataEndpoint.Movie:
                        returnValue = (MovieInfo)JsonConvert.DeserializeObject(content, typeof(MovieInfo));
                        break;
                    case RoviDataEndpoint.Video:
                        returnValue = (VideoInfo)JsonConvert.DeserializeObject(content, typeof(VideoInfo));
                        break;
                    default:
                        break;
                }
            }

            return returnValue;
        }

        private async Task<string> MakeCall(string uri)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}