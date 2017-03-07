using System.Web.Optimization;

namespace PollQuestionnaire.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/surveyEditor").Include(
                        "~/Scripts/knockout/knockout-{version}.js",
                        "~/Scripts/surveyjs/editor/survey.ko.min.js",
                        "~/Scripts/surveyjs/editor/surveyeditor.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/survey").Include(
                        "~/Scripts/surveyjs/survey/survey.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/surveyEditor/css").Include(
                      "~/Content/surveyjs/surveyeditor.css"));

            bundles.Add(new StyleBundle("~/Content/survey/css").Include(
                      "~/Content/surveyjs/survey.css"));
        }
    }
}
