using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        [HttpPost]
        public void GetResults(int? Age)
        {
            ViewBag.Title = "Home Page";
        }
    }
}
