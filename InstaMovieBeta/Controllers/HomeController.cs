using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InstaMovieBeta.Utils;

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

        public async Task<ActionResult> GetResults(string input)
        {
            //build the api key
            string key = "hbed3xw3zt43gmefw6f4vd6z";
            string sharedSecret = "GsHMtgdxd9";
            string unixTime = Utility.GetCurrentUnixTimestampSeconds().ToString();
            byte[] combBytes = System.Text.Encoding.UTF8.GetBytes(key + sharedSecret + unixTime);
            var sig = Utility.ToHex(MD5.Create("System.Security.Cryptography.MD5").ComputeHash(combBytes), false);
            
            using (var client = new HttpClient())
            {
                string uri = "http://api.rovicorp.com/search/v2.1/music/autocomplete?apikey=" + key + "&sig=" + sig + "&entitytype=artist&size=10&query=e";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(await response.Content.ReadAsStringAsync(), JsonRequestBehavior.AllowGet);

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
                // New code:
            }
        }  
    }
}