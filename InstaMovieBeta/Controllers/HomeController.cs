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
        private readonly RoviHelper mRoviHelper = new RoviHelper();
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

            if (string.IsNullOrEmpty(searchTerm))
            {
                //TODO: No input view
                return View();
            }

            if (string.IsNullOrEmpty(cosmoId))
            {
                //Perform search
                SearchResponse searchResponse = await mRoviHelper.Search(
                    RoviHelper.RoviSearchType.Search,
                    RoviHelper.RoviSearchEndpoint.Video,
                    "&size=20&entitytype=movie&query=" + Url.Encode(searchTerm));
                //Populate View
                return View("Search", searchResponse.Response.Results);
            }
            else
            {
                //Perform Video lookup
                VideoInfo videoInfo = (VideoInfo)await mRoviHelper.Lookup(RoviHelper.RoviDataEndpoint.Video, "&cosmoid=" + cosmoId);
                return View("Video", videoInfo);
            }

            ////build the api key
            //string key = "edar754hh646dayx3zkr2fey";
            //string sharedSecret = "zukWGATaSz";
            //string unixTime = Utility.GetCurrentUnixTimestampSeconds().ToString();
            //byte[] combBytes = System.Text.Encoding.UTF8.GetBytes(key + sharedSecret + unixTime);
            //var sig = Utility.ToHex(MD5.Create("System.Security.Cryptography.MD5").ComputeHash(combBytes), false);

            //using (var client = new HttpClient())
            //{
            //    string uri = "http://api.rovicorp.com/data/v1.1/video/info?apikey=" + key + "&sig=" + sig + "&cosmoid=" + cosmoId;
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    try
            //    {
            //        HttpResponseMessage response = await client.GetAsync(uri);
            //        string content = await response.Content.ReadAsStringAsync();
            //        if (response.IsSuccessStatusCode)
            //        {

            //            if (!string.IsNullOrEmpty(content))
            //            {
            //                VideoInfo movieInfo = (VideoInfo)JsonConvert.DeserializeObject(content, typeof(VideoInfo));
            //                return View(movieInfo);
            //            }
            //            return Json("NO CONTENT");
            //        }
            //        return Json(content);
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}
        }

        public async Task<ActionResult> Autocomplete(string input)
        {
            SearchResponse searchResponse = await mRoviHelper.Search(
                RoviHelper.RoviSearchType.SingleStageSearch,
                RoviHelper.RoviSearchEndpoint.Video,
                "&size=5&entitytype=movie&query=" + Url.Encode(input));

            if (searchResponse.Response.Results.Length > 0)
            {
                List<dynamic> listings = new List<dynamic>();
                foreach (var result in searchResponse.Response.Results)
                {
                    dynamic item = new JObject();
                    item.DisplayTitle = result.Video.MasterTitle + " (" + result.Video.ReleaseYear.ToString() + ")";
                    item.CosmoId = result.Video.Ids.CosmoId;
                    listings.Add(item);
                }

                JsonNetResult jsonResult = new JsonNetResult();
                jsonResult.Formatting = Newtonsoft.Json.Formatting.Indented;
                jsonResult.Data = listings;
                return jsonResult;
            }

            return null;
        }  
    }
}