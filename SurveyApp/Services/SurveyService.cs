using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SurveyApp.Models;

namespace SurveyApp.Services
{
    public class SurveyService : ISurveyService
    {
        private static readonly string resultsFile = $"{System.AppDomain.CurrentDomain.BaseDirectory}/Data/SurveyResults.json";
        public bool AddResultsToFile(SurveyResults surveyResults)
        {
            try
            {
                List<SurveyResults> allResults = JsonConvert.DeserializeObject<List<SurveyResults>>(File.ReadAllText(resultsFile));
                allResults.Add(surveyResults);
                string newJsonResult = JsonConvert.SerializeObject(allResults,
                    Formatting.Indented);
                File.WriteAllText(resultsFile, newJsonResult);
                return true;
            }
            catch (JsonException e)
            {

                throw new JsonException();
            }
        }

        public SurveyResults ConvertSurveyResultsDTOToSurveyResults(SurveyResultsDTO surveyResultsDto)
        {
            SurveyResults surveyResults = new SurveyResults()
            {
                Age = surveyResultsDto.Age,
                Gender = surveyResultsDto.Gender,
                CarLicense = surveyResultsDto.CarLicense,
                FirstCar = surveyResultsDto.FirstCar,
                DriveTrain = surveyResultsDto.DriveTrain,
                Drifting = surveyResultsDto.Drifting,
                UsedBmwCount = surveyResultsDto.UsedBmwCount,
                UsedBmwCarNameList = new List<string>()
            };
            if (surveyResultsDto.UsedBmw1Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw1Model.ToString());
            if (surveyResultsDto.UsedBmw2Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw2Model.ToString());
            if (surveyResultsDto.UsedBmw3Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw3Model.ToString());
            if (surveyResultsDto.UsedBmw4Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw4Model.ToString());
            if (surveyResultsDto.UsedBmw5Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw5Model.ToString());
            if (surveyResultsDto.UsedBmw6Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw6Model.ToString());
            if (surveyResultsDto.UsedBmw7Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw7Model.ToString());
            if (surveyResultsDto.UsedBmw8Model != null)  surveyResults.UsedBmwCarNameList.Add(surveyResultsDto.UsedBmw8Model.ToString());
            return surveyResults;
        }

        public List<dynamic> GetDetailedResults()
        {
           List<SurveyResults> allResults = JsonConvert.DeserializeObject<List<SurveyResults>>(File.ReadAllText(resultsFile));
           var underEighteen = allResults.Count(x => x.Age < 18);
           var biggerEighteen = allResults.Count(x => x.Age >= 18);
           var ageResults = new
            {
                series = new int[2]{underEighteen,biggerEighteen},
                labels = new string[2] {"Under 18","Bigger than 18"}
            };
            var haveCarLicense = allResults.Count(x => x.CarLicense == "Yes");
            var unlicensed = allResults.Count(x => x.CarLicense != "Yes");
            var carLicenseResults = new
            {
                series = new int[2] { haveCarLicense, unlicensed },
                labels = new string[2] { "Have a car license", "Unlicensed" }
            };
            var firstCarOwners = allResults.Count(x => x.FirstCar == "Yes" && x.Age > 18 && x.Age <= 25);
            var firstCarNoOwners = allResults.Count(x => x.FirstCar != "Yes" && x.Age > 18 && x.Age <= 25);

            var firstCarResults = new
            {
                series = new int[2] { firstCarOwners, firstCarNoOwners },
                labels = new string[2] { "First Car Owner", "Not First Car Owner" }
            }; 
            var nonTargetableClients = allResults.Count(x => x.FirstCar == "Yes" || x.Age < 18 || x.CarLicense != "Yes");
            var targetableClients = allResults.Count(x => x.FirstCar != "Yes" && x.Age > 18 && x.CarLicense == "Yes");

            var targetableResults = new
            {
                series = new int[2] { targetableClients, nonTargetableClients },
                labels = new string[2] { "Targetable Clients", "Non Targetable Clients" }
            };
            var allTargetableListCount = allResults.Count(x => x.FirstCar != "Yes" && x.Age > 18 && x.CarLicense == "Yes");
            int usedBmwListCount = 0;
            foreach (var result in allResults)
            {
                usedBmwListCount =  usedBmwListCount + result.UsedBmwCarNameList.Count();
            }
            
            var avgUsedBmw = Math.Round(Convert.ToDecimal((decimal)usedBmwListCount / (decimal)allTargetableListCount),1);


            var driveTrainAnswers = allResults.Count(x => x.FirstCar != "Yes" && x.Age > 18 && x.CarLicense == "Yes" && (x.DriveTrain == "FWD" || x.DriveTrain == "I don’t know"));
            var driftingAnswers = allResults.Count(x => x.FirstCar != "Yes" && x.Age > 18 && x.CarLicense == "Yes" && x.Drifting == "Yes");
            var percentageDriveTrain = Math.Round(Convert.ToDecimal((decimal)driveTrainAnswers / (decimal)allTargetableListCount) * 100,1);
            var percentageDrifting = Math.Round(Convert.ToDecimal((decimal)driftingAnswers / (decimal)allTargetableListCount) * 100,1);

            var modelDistributionList = CalculateModelDistribution(allResults);


            var modelDistribution = new
            {
                series = modelDistributionList.Select(x => x.Count),
                labels = modelDistributionList.Select(x => x.ModelName)
            };

            List<dynamic> resultsList = new List<dynamic>
            {
                ageResults,
                carLicenseResults,
                firstCarResults,
                targetableResults,
                avgUsedBmw,
                percentageDriveTrain,
                percentageDrifting,
                modelDistribution
            };

            return resultsList;
        }

        private List<DistrubitionModel> CalculateModelDistribution(List<SurveyResults> results)
        {
            List<DistrubitionModel> modelDist = new List<DistrubitionModel>();
            foreach (var result in results)
            {
                foreach (var name in result.UsedBmwCarNameList)
                {

                    if (modelDist.Count(x => x.ModelName == name) > 0)
                    {
                        foreach (var u in modelDist.Where(u => u.ModelName == name))
                        {
                            u.Count = u.Count + 1;
                        }
                    }
                    else
                    {
                        modelDist.Add(new DistrubitionModel(1, name));
                    }

                }
            }
            return modelDist;
        }

        public class DistrubitionModel
        {

            public DistrubitionModel(int item1, string item2)
            {
                Count = item1;
                ModelName = item2;
            }
            public int Count { get; set; }
            public string ModelName { get; set; }
        }
    }
}