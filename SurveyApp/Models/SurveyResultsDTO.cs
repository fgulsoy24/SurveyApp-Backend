using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    [Serializable]
    public class SurveyResultsDTO
    {
        public virtual int Age { get; set; }
        public virtual string Gender { get; set; }
        public virtual string CarLicense { get; set; }
        public virtual string FirstCar { get; set; }
        public virtual string DriveTrain { get; set; }
        public virtual string Drifting { get; set; }
        public virtual int UsedBmwCount { get; set; }
        public virtual string UsedBmw1Model { get; set; }
        public virtual string UsedBmw2Model { get; set; }
        public virtual string UsedBmw3Model { get; set; }
        public virtual string UsedBmw4Model { get; set; }
        public virtual string UsedBmw5Model { get; set; }
        public virtual string UsedBmw6Model { get; set; }
        public virtual string UsedBmw7Model { get; set; }
        public virtual string UsedBmw8Model { get; set; }
    }
}