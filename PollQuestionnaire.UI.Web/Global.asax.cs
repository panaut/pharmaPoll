using System;
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
            DefaultSettings.CreateDefaultDbUser();
        }
        // Redirect http requests to the https URL
        protected void Application_BeginRequest()
        {
            if (!Context.Request.IsSecureConnection)
            {
                // This is an insecure connection, so redirect to the secure version
                UriBuilder uri = new UriBuilder(Context.Request.Url);
                uri.Scheme = "https";
                if (uri.Port > 30000 && uri.Host.Equals("localhost"))
                {
                    // Development box - set uri.Port to 44300 by default
                    // uncoment this to test localhost https
                    //uri.Port = 44311;                      
                }
                else
                {
                    uri.Port = 443;
                    Response.Redirect(uri.ToString());
                }

                
            }
        }
    }
}

