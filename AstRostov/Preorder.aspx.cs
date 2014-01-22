using System;
using System.Globalization;
using System.IO;
using System.Linq;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;
using NLog;

namespace AstRostov
{
    public partial class PreorderPage : System.Web.UI.Page
    {
        private Product _product;

        private int ProductId
        {
            get
            {
                int id;
                if (hdnProductId.Value != null && int.TryParse(hdnProductId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set { hdnProductId.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseRequest();
            }
        }

        private void ParseRequest()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ProductId = id;
            }

            int count;
            if (!String.IsNullOrEmpty(Request.Params["count"]) && int.TryParse(Request.Params["count"], out count))
            {
                tbCount.Text = count.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ProductId);
            if (_product == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                tbProductName.Text = _product.Name;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
        }

        protected void CreatePreorder(object sender, EventArgs e)
        {
            int count;
            if (int.TryParse(tbCount.Text, out count))
            {
                var preorder = new Preorder
                {
                    ProductId = _product.ProductId,
                    ProductName = _product.Name,
                    EstimatedPrice = _product.FinalPrice,
                    Count = count,
                    CustomerName = tbCustomer.Text,
                    Phone = tbPhone.Text,
                    CustomerEmail = tbEmail.Text,
                    Comment = tbComment.Text,
                    Date = DateTime.Now,
                    State = PreorderState.Pending
                };
                CoreData.Context.Preorders.Add(preorder);
                CoreData.Context.SaveChanges();

                var message = String.Format(MessageTemplate,
                    preorder.CustomerName,
                    preorder.ProductName,
                    preorder.Count,
                    preorder.PreorderId,
                    preorder.EstimatedPrice * preorder.Count);

                try
                {
                    AstMail.SendEmail(tbEmail.Text, message, false, "АСТ-Ростов: Предзаказ");
                    lblSuccess.Visible = true;
                    lblError.Text = "";
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Fatal(ex);
                    logger.ErrorException("Ошибка при отправке почты", ex);
                    lblError.Text = "Произошла ошибка при оформлении заказа. Проверьте правильность заполнения полей.";
                }
            }
        }

        /// <summary>
        /// {0} Customer
        /// {1} ProductName
        /// {2} Count
        /// {3} PreorderId
        /// {4} Amount
        /// </summary>
        private string MessageTemplate
        {
            get
            {
                return File.ReadAllText(Server.MapPath("~/MessageTemplates/Preorder.txt"));
            }
        }
    }
}