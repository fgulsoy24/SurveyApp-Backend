using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SurveyApp.Services;
using Newtonsoft.Json;

namespace SurveyApp.Controllers
{
   
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        [HttpPost]
        public void GetResults(SurveyResultsDTO surveyResultsDto)
        {
            var surveyResults =_surveyService.ConvertSurveyResultsDTOToSurveyResults(surveyResultsDto);
            var executionResult = _surveyService.AddResultsToFile(surveyResults);


        }
        [HttpGet]
        public JsonNetResult GetDetailedResults(SurveyResultsDTO surveyResultsDto)
        {
            var results = _surveyService.GetDetailedResults();
            var response = new
            {
                results
            };
            var jsonResult = (JsonNetResult) Json(response, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }
    }
}
