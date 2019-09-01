using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SurveyApp.Services;

namespace SurveyApp.App_Start
{
    public class DependencyResolverConfig
    {
        public static void RegisterDependencies()
        {
            var container = new Container();

            container.Register<ISurveyService, SurveyService>();


            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}