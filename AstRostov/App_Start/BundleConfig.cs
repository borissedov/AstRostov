using System.Web.Optimization;

namespace AstRostov.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsScripts").Include(
                  "~/Scripts/WebForms/WebForms.Scripts",
                  "~/Scripts/WebForms/WebUIValidation.Scripts",
                  "~/Scripts/WebForms/MenuStandards.Scripts",
                  "~/Scripts/WebForms/Focus.Scripts",
                  "~/Scripts/WebForms/GridView.Scripts",
                  "~/Scripts/WebForms/DetailsView.Scripts",
                  "~/Scripts/WebForms/TreeView.Scripts",
                  "~/Scripts/WebForms/WebParts.Scripts"));

            bundles.Add(new ScriptBundle("~/bundles/MsAjaxScripts").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.Scripts",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.Scripts",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.Scripts",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.Scripts"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));
        }
    }
}