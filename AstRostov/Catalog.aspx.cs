using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class Catalog1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindChildren();
                BindPaging();
            }
        }
        
        private void BindChildren()
        {
            var rootCategories = CoreData.Context.Categories.ToArray()
                .Where(c => c.IsRoot).ToArray();

            Category[] paginatedSubCategoryList = rootCategories
                .Skip((CurrentPageNo - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToArray();
            int count = rootCategories.Count();

            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }

            var subcategoryIndexRows =
                paginatedSubCategoryList.Select((p, i) => i).GroupBy(i => i / 3).Cast<IEnumerable<int>>().ToArray();//groups of indexes in array by 3
            var productRows = subcategoryIndexRows.Select(pir => pir.Select(i => paginatedSubCategoryList[i]).ToArray()).ToArray();//groups of products by 3
            rptChildCategoriesRows.Visible = true;
            rptChildCategoriesRows.DataSource = productRows;
            rptChildCategoriesRows.DataBind();
        }

       
        protected void ChildCategoryRowDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rptChildCategoriesItems = e.Item.FindControl("rptChildCategoriesItems") as Repeater;
            var categoryRowDataSource = e.Item.DataItem as Category[];
            if (rptChildCategoriesItems != null && categoryRowDataSource != null)
            {
                rptChildCategoriesItems.DataSource = categoryRowDataSource;
                rptChildCategoriesItems.DataBind();
            }
        }


        #region Pagination

        private const int ItemsPerPage = 12;

        protected int CurrentPageNo
        {
            get
            {
                int pageNo;
                if (int.TryParse(Request.Params["page"], out pageNo))
                {
                    return pageNo;
                }
                return 1;
            }
        }

        protected int PageCount
        {
            get
            {
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }

        private void BindPaging()
        {
            var list = new List<int>();
            for (int i = 1; i <= PageCount; i++)
            {
                list.Add(i);
            }

            if (list.Count > 1)
            {
                rptPaging.DataSource = list.Select(i => new
                {
                    PageNo = i
                });
                rptPaging.DataBind();
            }
            else
            {
                divPagination.Visible = false;
            }
        }

        #endregion
    }
}