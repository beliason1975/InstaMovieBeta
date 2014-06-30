using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using InstaMovieBeta.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using InstaMovieBeta.Models.Rovi;

namespace InstaMovieBeta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Search(FormCollection collection)
        {
            string searchTerm = collection["searchTerm"];
            string cosmoId = collection["cosmoid"];
            object autocompleteResults = collection["autocompleteResults"];
            //build the api key
            string key = "edar754hh646dayx3zkr2fey";
            string sharedSecret = "zukWGATaSz";
            string unixTime = Utility.GetCurrentUnixTimestampSeconds().ToString();
            byte[] combBytes = System.Text.Encoding.UTF8.GetBytes(key + sharedSecret + unixTime);
            var sig = Utility.ToHex(MD5.Create("System.Security.Cryptography.MD5").ComputeHash(combBytes), false);

            using (var client = new HttpClient())
            {
                string uri = "http://api.rovicorp.com/data/v1.1/movie/info?apikey=" + key + "&sig=" + sig + "&cosmoid=" + cosmoId;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    string content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {

                        if (!string.IsNullOrEmpty(content))
                        {
                            MovieInfo movieInfo = (MovieInfo)JsonConvert.DeserializeObject(content, typeof(MovieInfo));
                            return View(movieInfo);
                        }
                        return Json("NO CONTENT");
                    }
                    return Json(content);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<ActionResult> Autocomplete(string input)
        {
            //build the api key
            string key = "edar754hh646dayx3zkr2fey";
            string sharedSecret = "zukWGATaSz";
            //string key = "hbed3xw3zt43gmefw6f4vd6z";
            //string sharedSecret = "GsHMtgdxd9";
            string unixTime = Utility.GetCurrentUnixTimestampSeconds().ToString();
            byte[] combBytes = System.Text.Encoding.UTF8.GetBytes(key + sharedSecret + unixTime);
            var sig = Utility.ToHex(MD5.Create("System.Security.Cryptography.MD5").ComputeHash(combBytes), false);

            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            using (var client = new HttpClient(handler))
            {
                string uri = "http://api.rovicorp.com/search/v2.1/amgvideo/singlestagesearch?apikey=" + key + "&sig=" + sig + "&size=5&entitytype=movie&query=" + Url.Encode(input);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(content))
                        {
                            SearchResponse searchResponse = (SearchResponse)JsonConvert.DeserializeObject(content, typeof(SearchResponse));
                            if (searchResponse.Details.Results.Length > 0)
                            {
                                List<dynamic> listings = new List<dynamic>();
                                foreach (var result in searchResponse.Details.Results)
                                {
                                    dynamic item = new JObject();
                                    item.DisplayTitle = result.Movie.Title + " (" + result.Movie.ReleaseYear.ToString() + ")";
                                    item.CosmoId = result.Movie.Ids.CosmoId;
                                    listings.Add(item);
                                }

                                JsonNetResult jsonResult = new JsonNetResult();
                                jsonResult.Formatting = Newtonsoft.Json.Formatting.Indented;
                                jsonResult.Data = listings;
                                return jsonResult;
                            }
                            return null;
                        }
                        return null;
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