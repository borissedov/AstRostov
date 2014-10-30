using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AstRostov.Controls.Navigation
{
    public class Menu : WebControl
    {
        private SiteMapProvider _provider;

        protected SiteMapNode RootNode { get; set; }
        protected int Depth { get; set; }

        public  Menu()
            :base(HtmlTextWriterTag.Ul)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            if (RootNode == null)
            {
                if (Provider != null && Provider.RootNode != null)
                {
                    RootNode = Provider.RootNode;
                }
                else
                {
                    throw new Exception("No root node is set.");
                }
            }

            BindMenu();
        }

        public SiteMapProvider Provider
        {
            get
            {
                return _provider ?? new SiteMapDataSource().Provider;
            }
            set { _provider = value; }
        }

        private void BindMenu()
        {
            OnElementCreated(this, Depth, RootNode);
            foreach (SiteMapNode node in RootNode.ChildNodes)
            {
                var li = new HtmlGenericControl("li");
                OnElementCreated(li, Depth, node);
                var a = new HtmlAnchor
                {
                    InnerText = node.Title,
                    HRef = node.Url
                };
                OnElementCreated(a, Depth, node);
                li.Controls.Add(a);

                if (node.HasChildNodes)
                {
                    var child = GetSubmenu();
                    if (child != null)
                    {
                        child.RootNode = node;
                        child.Depth = Depth + 1;

                        li.Controls.Add(child);
                    }
                }

                Controls.Add(li);
            }
        }

        protected virtual void OnElementCreated(Control control, int depth, SiteMapNode node)
        {
           
        }

        protected virtual Menu GetSubmenu()
        {
            return new Menu();
        }
    }
}