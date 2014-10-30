using System.Web.Optimization;

namespace AstRostov.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                  "~/Scripts/jquery-2.0.3.min.js",
                  "~/Scripts/jquery-ui-1.10.3.min.js",
                  "~/Scripts/browser-fix.js",
                  "~/Scripts/jquery.responsivethumbnailgallery.min.js",
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/jquery.onebyone.min.js",
                  "~/Scripts/jquery.equal-heights.min.js",
                  "~/Scripts/cufon-yui.js",
                  "~/Scripts/Arial_400.font.js",
                  "~/Scripts/menu-active.js",
                  "~/Scripts/wysihtml/wysihtml5-0.3.0.js",
                  "~/Scripts/bootstrap-wysihtml5.js",
                  "~/Scripts/jquery.fancybox.pack.js",
                  "~/Scripts/jquery.mousewheel-3.0.6.pack.js",
                  "~/Scripts/config.js"));
            /*
             <asp:ScriptReference Path="~/Scripts/jquery-2.0.3.min.js" />
                    <%--<asp:ScriptReference Path="~/Scripts/jquery-ui-1.10.3.min.js" />--%>
                    <asp:ScriptReference Path="~/Scripts/browser-fix.js" />
                    <asp:ScriptReference Path="~/Scripts/jquery.responsivethumbnailgallery.min.js" />
                    <asp:ScriptReference Path="~/Scripts/bootstrap.js" />
                    <asp:ScriptReference Path="~/Scripts/jquery.onebyone.min.js" />
                    <asp:ScriptReference Path="~/Scripts/jquery.equal-heights.min.js" />
                    <asp:ScriptReference Path="~/Scripts/cufon-yui.js" />
                    <asp:ScriptReference Path="~/Scripts/Arial_400.font.js" />
                    <asp:ScriptReference Path="~/Scripts/menu-active.js" />
                    <asp:ScriptReference Path="~/Scripts/config.js" />
                    <asp:ScriptReference Path="~/Scripts/wysihtml/wysihtml5-0.3.0.js" />
                    <asp:ScriptReference Path="~/Scripts/bootstrap-wysihtml5.js" />
             */

            //bundles.Add(new ScriptBundle("~/bundles/MsAjaxScripts").Include(
            //    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.Scripts",
            //    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.Scripts",
            //    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.Scripts",
            //    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.Scripts"));

            //// Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //    "~/Scripts/modernizr-*"));
        }
    }
}