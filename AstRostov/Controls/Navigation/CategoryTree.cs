using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Controls.Navigation
{
    public class CategoryTree : WebControl
    {
        protected Category RootNode { get; set; }
        protected int Depth { get; set; }

        public CategoryTree()
            : base(HtmlTextWriterTag.Ul)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            if (RootNode == null)
            {
                var rootNodes = CoreData.Context.Categories.ToArray().Where(c => c.IsRoot).ToArray();
                if (rootNodes.Any())
                {
                    RootNode = new Category();
                    foreach (var categoryNode in rootNodes)
                    {
                        RootNode.ChildCategories.Add(categoryNode);
                    }
                }
                else
                {
                    this.Visible = false;
                    return;
                }
            }

            BindTree();
        }

        private void BindTree()
        {
            OnElementCreated(this, Depth, RootNode);
            foreach (Category node in RootNode.ChildCategories)
            {
                var li = new HtmlGenericControl("li");
                OnElementCreated(li, Depth, node);
                var a = new HtmlAnchor()
                {
                    InnerText = node.Name,
                    HRef = ResolveUrl(String.Format("~/Category.aspx?id={0}", node.CategoryId))
                };
                OnElementCreated(a, Depth, node);
                li.Controls.Add(a);

                if (node.ChildCategories.Count > 0)
                {
                    var child = GetSubtree();
                    if (child != null)
                    {
                        child.RootNode = node;
                        child.Depth = Depth + 1;

                        li.Controls.Add(child);
                    }
                }

                this.Controls.Add(li);
            }
        }

        protected virtual void OnElementCreated(Control control, int depth, Category node)
        {

        }

        protected virtual CategoryTree GetSubtree()
        {
            return new CategoryTree();
        }
    }
}