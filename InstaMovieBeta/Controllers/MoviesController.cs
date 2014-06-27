using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InstaMovieBeta.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public  ActionResult Index()
        {
            //HttpRequestMessage request = new HttpRequestMessage(
            //    HttpMethod.Get,
            //    "http://data.tmsapi.com/v1/movies/showings?startDate=2014-06-17&zip=72756&api_key=wfhu3b6a7crqyxvh8yvtsy4g");
            //HttpClient client = new HttpClient();
            //var response = await client.SendAsync(request);
            //var content = await response.Content.ReadAsStringAsync();
            var content = "";
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movies/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movies/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
