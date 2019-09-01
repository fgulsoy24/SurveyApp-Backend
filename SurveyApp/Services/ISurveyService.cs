using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApp.Models;

namespace SurveyApp.Services
{
    public interface ISurveyService
    {
        bool AddResultsToFile(SurveyResults surveyResults);
        SurveyResults ConvertSurveyResultsDTOToSurveyResults(SurveyResultsDTO surveyResultsDto);
        List<dynamic> GetDetailedResults();
    }
}
