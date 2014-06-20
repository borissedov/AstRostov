using System;
using System.Linq;
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
                case "ConvertToOrder":
                    Response.Redirect(String.Format("~/Admin/PreorderConvert.aspx?id={0}", e.CommandArgument));
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

        protected void OnGridRowDataBound(object sender, GridViewRowEventArgs e)
        {
            var lbtnConvertToOrder = e.Row.FindControl("lbtnConvertToOrder") as LinkButton;
            var dataItem = e.Row.DataItem as Preorder;

            if (lbtnConvertToOrder != null && dataItem != null)
            {
                var skuId = dataItem.SkuId;
                lbtnConvertToOrder.Visible = CoreData.Context.Skus.Any(s => s.SkuId == skuId) && dataItem.State == PreorderState.Pending;
            }
        }
    }
}