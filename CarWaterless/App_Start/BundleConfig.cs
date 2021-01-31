using System.Web;
using System.Web.Optimization;

namespace CarWaterless
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/ArchitectThemes/css").Include(
            //          "~/ArchitectThemes/bootstrap.css",
            //          "~/ArchitectThemes/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/signup").Include(
               "~/ArchitectThemes/frontend/signup.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
               "~/ArchitectThemes/frontend/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/township").Include(
               "~/ArchitectThemes/frontend/township.js"));

            bundles.Add(new ScriptBundle("~/bundles/carcategory").Include(
              "~/ArchitectThemes/frontend/carcategory.js"));

            bundles.Add(new ScriptBundle("~/bundles/branch").Include(
               "~/ArchitectThemes/frontend/branch.js"));

            bundles.Add(new ScriptBundle("~/bundles/branch-new").Include(
               "~/ArchitectThemes/frontend/branch-new.js"));

            bundles.Add(new ScriptBundle("~/bundles/additionalservice").Include(
               "~/ArchitectThemes/frontend/additionalservice.js"));

            bundles.Add(new ScriptBundle("~/bundles/memberpackage").Include(
               "~/ArchitectThemes/frontend/memberpackage.js"));

            bundles.Add(new ScriptBundle("~/bundles/dailyhot").Include(
               "~/ArchitectThemes/frontend/dailyhot.js"));

            bundles.Add(new ScriptBundle("~/bundles/user").Include(
            "~/ArchitectThemes/frontend/user.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-new").Include(
              "~/ArchitectThemes/frontend/user-new.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-changepassword").Include(
             "~/ArchitectThemes/frontend/user-changepassword.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-editprofile").Include(
              "~/ArchitectThemes/frontend/user-editprofile.js"));
        }
    }
}
