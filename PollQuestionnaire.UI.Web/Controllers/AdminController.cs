using Questionnaire.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class AdminController : Controller
    {
        private Lazy<ISurveyService> surveyService = new Lazy<ISurveyService>(() => new SurveyService());

        [Authorize]
        // GET: Admin
        public ActionResult Index()
        {
            var model = surveyService.Value.GetAllSurveys(onlyActive: false).OperationResult;

            return View(model);
        }
        [HttpPost()]
        public bool ActivateSurvey(int surveyId)
        {
            var result = surveyService.Value.SetSurveyStatus(surveyId, true);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.OperationResult.Value;
        }

        [HttpPost()]
        public bool DeactivateSurvey(int surveyId)
        {
            var result = surveyService.Value.SetSurveyStatus(surveyId, false);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.OperationResult.Value;
        }
        [HttpGet()]
        public string GetSurvey(int surveyId)
        {
            var result = surveyService.Value.GetSurvey(surveyId, onlyActive: false);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.OperationResult;
        }
        [HttpPost()]
        public string DeleteSurvey(int surveyId)
        {
            var result = surveyService.Value.DeleteSurvey(surveyId);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("Failed to update survey status");
            }

            return result.ToString();
        }
        [HttpPost()]
        public string SaveSurvey(string surveyJson)
        {
            var result = surveyService.Value.CreateOrSaveSurvey(surveyJson);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.ToString();
        }
        [HttpGet()]
        public string GetLocalizationList(string surveyCode)
        {
            var result = surveyService.Value.GetLocalizationList(surveyCode, onlyActive: false);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.OperationResult;
        }
        [HttpPost]
        public ActionResult GetFolderStructure()
        {
            DirectoryInfo uploadFolder = null;
            FileInfo[] files = null;

            try
            {
                string uploadPath = Server.MapPath("~/uploads");
                uploadFolder = new DirectoryInfo(uploadPath);
                files = uploadFolder.GetFiles();
            }
            catch (DirectoryNotFoundException exp)
            {
                //throw new FTPSalesFileProcessingException("Could not open the ftp directory", exp);
            }
            catch (IOException exp)
            {
                //throw new FTPSalesFileProcessingException("Failed to access directory", exp);
            }

            files = files.OrderBy(f => f.Name).ToArray();

            // var localizzationFiles = files.Where(f => f.Extension == ".pdf" || f.Extension == ".doc");

            files = files.ToArray();
            return View(files);
            //    try
            //    {
            //        var files = Directory.EnumerateFiles(Server.MapPath("~/uploads"));
            //    }
            //    catch (Exception ex)
            //    {
            //        return Json("Error occurred. Error details: " + ex.Message);
            //    }        
            ////else
            ////{
            ////    return Json("No files selected.");
            ////}
            //return files;
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    string pathForSaving = Server.MapPath("~/Uploads");
                    bool isUploaded = false;
                    string message = "File upload failed";

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        if (this.CreateFolderIfNeeded(pathForSaving))
                        {
                            try
                            {
                                file.SaveAs(Path.Combine(pathForSaving, "en-us_" + Path.GetFileName(fname)));
                                isUploaded = true;
                                message = "File uploaded successfully!";
                            }
                            catch (Exception ex)
                            {
                                message = string.Format("File upload failed: {0}", ex.Message);
                            }
                        }

                        // Get the complete folder path and store the file inside it.  
                        //fname = Path.Combine(pathForSaving, fname);
                        //file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
    }
}