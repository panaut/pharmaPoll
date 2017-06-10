using Newtonsoft.Json;
using Questionnaire.Service;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class SurveyController : Controller
    {
        private Lazy<ISurveyService> surveyService = new Lazy<ISurveyService>(() => new SurveyService());
        private Lazy<IVotingService> votingService = new Lazy<IVotingService>(() => new VotingService());

        [Authorize]
        // GET: SurveyDemo
        public ActionResult Index()
        {
            var model = surveyService.Value.GetAllSurveyInfo().OperationResult;
            return View(model.ToList());
        }

        //[RequireHttps]
        [AllowAnonymous]
        public ActionResult PatQueSurvey(
                        string surveyCode,
                        string sessionCode = default(string),
                        string lang = default(string))
        {
            // Survey Code is mandatory parameter
            if (string.IsNullOrEmpty(surveyCode))
            {
                // Redirect to Index Page
                var model = surveyService.Value.GetAllSurveyInfo().OperationResult;
                return View("Index", model.ToList());
            }

            // Set Culture of the current thread
            this.SetCulture(lang);

            var surveyInfoResult = this.surveyService.Value.GetSurveyInfo(surveyCode);

            if (surveyInfoResult.Status != OperationStatus.Success)
            {
                throw new Exception($"Survey with code: {surveyCode} wasn't found");
            }

            if (surveyInfoResult.OperationResult != null)
            {
                ViewBag.language = lang;
                ViewBag.codeSurveyId = HttpUtility.HtmlEncode(surveyInfoResult.OperationResult.Code);
                ViewBag.surveyName = HttpUtility.HtmlEncode(surveyInfoResult.OperationResult.Title);
                ViewBag.sessionCode = HttpUtility.HtmlEncode(sessionCode);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string SaveResults(
            int surveyId,
            string jsonResults,
            string sessionCode = default(string),
            int? lastVisitedPageIndex = default(int?),
            bool isFinal = true)
        {
            var clientIp = Request.UserHostAddress;

            var result = votingService.Value
                .SaveSession(
                surveyId, clientIp, jsonResults, lastVisitedPageIndex, sessionCode, isFinal);

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception("Failed to store results");
            }

            return result.OperationResult;
        }

        [HttpGet()]
        public string GetActiveSurvey(string surveyCode, string lang = default(string))
        {
            // Now retrieve the survey
            var surveyResult = this.surveyService.Value.GetSurvey(surveyCode, lang);

            if (surveyResult.Status != OperationStatus.Success)
            {
                throw new Exception($"Survey with code: {surveyCode} wasn't found");
            }

            return surveyResult.OperationResult;
        }

        [HttpGet()]
        public string GetActiveSession(string surveyCode, string sessionCode)
        {
            // Retrieve the session
            var sessionResult = this.votingService.Value.GetSession(surveyCode, sessionCode);

            if (sessionResult.Status != OperationStatus.Success)
            {
                throw new Exception("Failed to retrieve voting session");
            }

            if (sessionResult.OperationResult == null)
            {
                throw new Exception("Session doesn't exist or it is inactive.");
            }

            return JsonConvert.SerializeObject(sessionResult.OperationResult);
        }

        [HttpPost]
        public void SendEmail(string resumeLink, string email)
        {
            resumeLink = HttpUtility.HtmlDecode(resumeLink);
            //var smtpClient = new System.Net.Mail.SmtpClient("smtp.patque.net");

            //smtpClient.Credentials = new System.Net.NetworkCredential("info@patque.net", "mp$Z%vJ3!");
            //smtpClient.UseDefaultCredentials = true;
            //smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = false;
            //var mail = new System.Net.Mail.MailMessage();

            ////Setting From , To and CC
            //mail.From = new System.Net.Mail.MailAddress("info@patque.net", "PatQue.net");
            //mail.To.Add(new System.Net.Mail.MailAddress("ivan.cojbasic@gmail.com"));
            ////mail.CC.Add(new System.Net.Mail.MailAddress("MyEmailID@gmail.com"));

            //smtpClient.Send(mail);
        }

        [HttpGet]
        public bool VerifySurveyPdf(string surveyCode, string lang = default(string))
        {
            string pdfDownloadPath = Server.MapPath("~/SurveyPdf");

            if (string.IsNullOrEmpty(lang))
            {
                lang = "DEFAULT";
            }

            var targetFileName = System.IO.Path.Combine(pdfDownloadPath, $"{surveyCode}.{lang}.pdf");

            return System.IO.File.Exists(targetFileName);
        }

        [HttpGet]
        public FileResult DownloadPdf(string surveyCode, string lang = default(string))
        {
            string pdfDownloadPath = Server.MapPath("~/SurveyPdf");

            if (string.IsNullOrEmpty(lang))
            {
                lang = "DEFAULT";
            }

            var targetFileName = System.IO.Path.Combine(pdfDownloadPath, $"{surveyCode}.{lang}.pdf");

            if (System.IO.File.Exists(targetFileName))
            {
                return File(targetFileName, "application/pdf", $"{surveyCode}.{lang}.pdf");
            }
            else
            {
                throw new System.IO.FileNotFoundException($"File was requested, but doesn't exist ({targetFileName})");
            }
        }

        private void SetCulture(string language)
        {
            CultureInfo ci;
            switch (language?.ToLower())
            {
                case "en":
                    ci = new CultureInfo("en");
                    break;
                case "sr":
                    ci = new CultureInfo("sr");
                    break;
                case "at":
                    ci = new CultureInfo("de-AT");
                    break;
                case "de":
                    ci = new CultureInfo("de-DE");
                    break;
                case "es":
                    ci = new CultureInfo("es");
                    break;
                case "gr":
                    ci = new CultureInfo("el");
                    break;
                case "nl":
                    ci = new CultureInfo("nl");
                    break;
                case "pt":
                    ci = new CultureInfo("pt");
                    break;
                case "pl":
                    ci = new CultureInfo("pl");
                    break;
                case "hu":
                    ci = new CultureInfo("hu");
                    break;
                case "tr":
                    ci = new CultureInfo("tr");
                    break;
                case "it":
                    ci = new CultureInfo("it");
                    break;
                default:
                    ci = CultureInfo.CurrentCulture;
                    break;
            }

            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}