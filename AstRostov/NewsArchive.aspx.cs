using System;
using System.Collections.Generic;
using System.Linq;
using AstCore.DataAccess;

namespace AstRostov
{
    public partial class NewsArchive : System.Web.UI.Page
    {
        private const int ItemsPerPage = 10;

        protected int Year
        {
            get
            {
                int year;
                return int.TryParse(Request.Params["year"], out year) ? year : 0;
            }
        }

        protected int Month
        {
            get
            {
                int year;
                return int.TryParse(Request.Params["month"], out year) ? year : 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNews();
                BindPaging();
            }
        }

        private void BindNews()
        {
            var allNews = CoreData.Context.News.OrderByDescending(n => n.Created).ToArray();
            if (Year != 0)
            {
                allNews = allNews.Where(n => n.Created.Year == Year).ToArray();
            }
            if (Month != 0)
            {
                allNews = allNews.Where(n => n.Created.Month == Month).ToArray();
            }

            int count = allNews.Count();
            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }
            var pageNews = allNews.Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage);
            rptNews.DataSource = pageNews.ToArray();
            rptNews.DataBind();
        }

        private void BindPaging()
        {
            var list = new List<int>();
            for (int i = 1; i <= PageCount; i++)
            {
                list.Add(i);
            }

            rptPaging.DataSource = list.Select(i => new
                {
                    PageNo = i
                });
            rptPaging.DataBind();
        }

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

    }
}