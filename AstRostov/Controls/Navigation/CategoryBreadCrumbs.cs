using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Controls.Navigation
{
    public class CategoryBreadCrumbs : WebControl
    {
        public CategoryBreadCrumbs()
            : base(HtmlTextWriterTag.Ul)
        {
        }

        private int ItemId
        {
            get
            {
                int id;
                if (ViewState["CategoryBreadCrumbsItemId"] != null && int.TryParse(ViewState["CategoryBreadCrumbsItemId"].ToString(), out id))
                {
                    return id;
                }
                return 0;
            }
            set
            {
                ViewState["CategoryBreadCrumbsItemId"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void ParseRequest()
        {
            int id;
            if (int.TryParse(HttpContext.Current.Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            ParseRequest();

            this.Attributes["class"] = "breadcrumb";

            var category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == ItemId);
            if (category != null)
            {
                //If category has any parents
                if (category.IsRoot)
                {
                    this.Visible = false;
                    return;
                }
                else
                {
                    Category parentCategory = category.ParentCategories.First();
                    while (parentCategory != null) 
                    {
                        //Binding parent link and divider
                        var rootLi = new HtmlGenericControl
                        {
                            TagName = "li"
                        };
                        rootLi.Controls.Add(new HtmlAnchor
                        {
                            InnerHtml = parentCategory.Name,
                            HRef = ResolveUrl(String.Format("~/Category.aspx?id={0}", parentCategory.CategoryId))
                        });

                        var dividerSpan = new HtmlGenericControl
                        {
                            TagName = "span",
                            InnerText = "/"
                        };
                        dividerSpan.Attributes["class"] = "divider";
                        rootLi.Controls.Add(dividerSpan);

                        this.Controls.AddAt(0, rootLi);
                        parentCategory = parentCategory.ParentCategories.FirstOrDefault();
                    } 
                }

                //Current category title
                var activeLi = new HtmlGenericControl
                {
                    TagName = "li",
                    InnerText = category.Name
                };
                activeLi.Attributes["class"] = "active";
                this.Controls.Add(activeLi);
            }
            else
            {
                this.Visible = false;
            }
        }

    }
}