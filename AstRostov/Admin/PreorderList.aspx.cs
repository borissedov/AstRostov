using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class PreorderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindPreorders();
            }
        }

        private void BindPreorders()
        {
            gridPreorders.DataSource = CoreData.Context.Preorders.ToArray();
            gridPreorders.DataBind();
        }

        protected void DeclinePreorder(int preorderId)
        {
            var preorderToDecline = CoreData.Context.Preorders.SingleOrDefault(n => n.PreorderId == preorderId);
            if (preorderToDecline != null)
            {
                preorderToDecline.State = PreorderState.Declined;
                CoreData.Context.SaveChanges();
            }
            Response.Redirect(String.Format("~/Admin/PreorderList.aspx"));
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                //case "Edit":
                //    Response.Redirect(String.Format("~/Admin/EditNewsComment.aspx?id={0}", e.CommandArgument));
                //    break;
                case "Decline":
                    DeclinePreorder(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        protected int GetProductIdBySkuId(int skuId)
        {
            var sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == skuId);
            return sku == null ? 0 : sku.ProductId;
        }

        protected string GetProductNameBySkuId(int skuId)
        {
            var sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == skuId);
            return sku == null ? "Продукт был удален" : sku.Product.Name;
        }

        protected string GetAttributeConfigBySkuId(int skuId)
        {
            var sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == skuId);
            return sku == null ? "Продукт был удален" : sku.AttributeConfig;
        }
    }
}