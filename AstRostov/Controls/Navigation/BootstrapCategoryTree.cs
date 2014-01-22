using System.Web.UI.HtmlControls;
using AstCore.Models;

namespace AstRostov.Controls.Navigation
{
    public class BootstrapCategoryTree : CategoryTree
    {
        protected override CategoryTree GetSubtree()
        {
            return new BootstrapCategoryTree();
        }

        protected override void OnElementCreated(System.Web.UI.Control control, int depth, Category node)
        {
            base.OnElementCreated(control, depth, node);
            if (control is CategoryTree)
            {
                var tree = control as CategoryTree;
                tree.Attributes["class"] = "dropdown-menu category-menu";

                if (Depth == 0)
                {
                    var header = new HtmlGenericControl
                        {
                            TagName = "li"
                        };
                    header.Controls.Add(new HtmlGenericControl
                        {
                            InnerText = "Каталог",
                            TagName = "h4"
                        });

                    var divider = new HtmlGenericControl
                    {
                        TagName = "li"
                    };
                    divider.Attributes["class"] = "divider";

                    tree.Controls.Add(header);
                    tree.Controls.Add(divider);
                }
                else
                {
                    tree.Attributes["class"] = "dropdown-menu";
                }
            }
            else if (control is HtmlAnchor)
            {
                //var element = control as HtmlAnchor;
                //if (depth == 0 && node.Children.Count > 0)
                //{
                //    element.Attributes["data-target"] = "#";
                //    element.Attributes["class"] = "dropdown-toggle";
                //    element.Attributes["data-toggle"] = "dropdown";
                //    element.InnerText += " ";
                //    var b = new HtmlGenericControl("b");
                //    b.Attributes["class"] = "caret";
                //    element.Controls.Add(b);
                //}


            }
            else if (control is HtmlGenericControl)
            {
                var element = control as HtmlGenericControl;
                var tag = element.TagName;

                switch (tag)
                {
                    case "li":
                        {
                            if (node.ChildCategories.Count > 0)
                            {
                                element.Attributes["class"] = "dropdown-submenu category-submenu";
                                //if (depth == 0)
                                //{
                                //    //element.Attributes["class"] = "dropdown";
                                //}
                                //else
                                //{
                                //    //element.Attributes["class"] = "dropdown-submenu";
                                //}
                            }
                            break;
                        }


                    default:
                        break;
                }
            }
        }
    }
}