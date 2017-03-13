using Newtonsoft.Json;
using PollQuestionnaire.UI.Web.Serialization;
using Questionnaire.DataBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PollQuestionnaire.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            JsonConvert.DefaultSettings = () =>
            {
                KnownTypesBinder knownTypesBinder = new KnownTypesBinder
                {
                    KnownTypes = new List<Type> {
                        typeof(Survey),
                        typeof(Page),
                        typeof(NumericValidator),
                        typeof(TextValidator),
                        typeof(AnswerCountValidator),
                        typeof(EmailValidator),
                        typeof(RegexValidator),
                        typeof(QuestionCheckBox),
                        typeof(QuestionRadiogroup),
                        typeof(QuestionDropdown),
                        typeof(QuestionText),
                        typeof(QuestionComment),
                        typeof(QuestionRating),
                        typeof(QuestionHtml),
                        typeof(QuestionFile),
                        typeof(QuestionMatrix),
                        typeof(QuestionMatrixDropdown)
                    }
                };

                return new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Binder = knownTypesBinder,

                };
            };
        }
    }
}

