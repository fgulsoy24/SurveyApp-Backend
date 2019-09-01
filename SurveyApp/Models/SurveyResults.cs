using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    public class SurveyResults
    {
        public virtual int Age { get; set; }
        public virtual string Gender { get; set; }
        public virtual string CarLicense { get; set; }
        public virtual string FirstCar { get; set; }
        public virtual string DriveTrain { get; set; }
        public virtual string Drifting { get; set; }
        public virtual int UsedBmwCount { get; set; }
        public virtual List<string> UsedBmwCarNameList { get; set; }
    }
}