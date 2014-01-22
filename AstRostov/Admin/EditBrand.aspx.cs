using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditBrand : System.Web.UI.Page
    {
        private int ItemId
        {
            get
            {
                int id;
                if (hdnItemId.Value != null && int.TryParse(hdnItemId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set { hdnItemId.Value = value.ToString(); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseItemId();
            }
        }

        private void ParseItemId()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBrandForm();
            }
        }

        private void BindBrandForm()
        {
            Brand brand;
            if (ItemId == 0)
            {
                brand = new Brand();
            }
            else
            {
                brand = CoreData.Context.Brands.SingleOrDefault(i => i.BrandId == ItemId);
            }
            if (brand == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            tbBrandName.Text = brand.Name;

            litEditTitle.Text =
                ItemId == 0
                ? @"Создание нового производителя"
                : String.Format(@"Редактирование производителя #{0}", ItemId);

        }

        protected void SaveBrand(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Brand brand = ItemId == 0
                    ? new Brand()
                    : CoreData.Context.Brands.SingleOrDefault(i => i.BrandId == ItemId);
                if (brand == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                brand.Name = tbBrandName.Text.Trim();

                if (ItemId == 0)
                {
                    CoreData.Context.Brands.Add(brand);
                }

                CoreData.Context.SaveChanges();
                Response.Redirect("~/Admin/BrandList.aspx");
                //Response.Redirect(String.Format("~/Admin/EditBrand.aspx?id={0}", brand.BrandId));
            }
        }

        protected void ValidateUniqueName(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(tbBrandName.Text))
            {
                args.IsValid = false;
                return;
            }

            if (CoreData.Context.Brands.Count(b => b.Name.ToLower() == tbBrandName.Text.Trim().ToLower()) > 1)
            {
                args.IsValid = false;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }
    }
}