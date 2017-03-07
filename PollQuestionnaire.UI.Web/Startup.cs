using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PollQuestionnaire.UI.Web.Startup))]
namespace PollQuestionnaire.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
