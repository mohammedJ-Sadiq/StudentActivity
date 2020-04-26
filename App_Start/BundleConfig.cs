using System.Web;
using System.Web.Optimization;

namespace StudentActivity
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", 
                      "~/Scripts/bootbox.js",
                      "~/Scripts/Alert.js",
                      "~/Scripts/jquery-ui-1.12.1.min.js",
                      "~/Scripts/jquery-ui-sliderAccess.js",
                      "~/Scripts/jquery-ui-timepicker-addon.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-united.css",
                      "~/Content/fontawesome.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-ui-timepicker-addon.css",
                      "~/Content/site.css"));
        }
    }
}
