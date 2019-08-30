using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;


namespace SurveyApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Home/GetResults")]
        public void GetResults([FromBody]string value)
        {
            ViewBag.Title = "Home Page";

           
        }
        public class Student
        {
            public int Age { get; set; }
        }
    }
}
