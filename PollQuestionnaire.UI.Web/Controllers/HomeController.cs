using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "*******";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "*******";

            return View();
        }
    }
}