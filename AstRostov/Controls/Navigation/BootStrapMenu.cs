using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace AstRostov.Controls.Navigation
{
    public class BootStrapMenu : Menu
    {
        protected override Menu GetSubmenu()
        {
            return new BootStrapMenu();
        }

        protected override void OnElementCreated(System.Web.UI.Control control, int depth, SiteMapNode node)
        {
            base.OnElementCreated(control, depth, node);
            if (control is Menu)
            {
                var menu = control as Menu;
                if (Depth == 0)
                {
                    menu.Attributes["class"] = "nav navbar-nav";
                }
                else
                {
                    menu.Attributes["class"] = "dropdown-menu";
                }
            }
            else if (control is HtmlAnchor)
            {
                var element = control as HtmlAnchor;
                if (depth == 0 && node.HasChildNodes)
                {
                    element.Attributes["class"] = "dropdown-toggle";
                    element.Attributes["data-toggle"] = "dropdown";
                    element.InnerText += " ";
                    var b = new HtmlGenericControl("b");
                    b.Attributes["class"] = "caret";
                    element.Controls.Add(b);
                }


            }
            else if (control is HtmlGenericControl)
            {
                var element = control as HtmlGenericControl;
                var tag = element.TagName;

                switch (tag)
                {
                    case "li":
                        {
                            if (node.HasChildNodes)
                            {
                                if (depth == 0)
                                {
                                    element.Attributes["class"] += " dropdown";
                                }
                                else
                                {
                                    element.Attributes["class"] += " dropdown-submenu";
                                }
                            }
                            break;
                        }
                }
            }
        }
    }
}