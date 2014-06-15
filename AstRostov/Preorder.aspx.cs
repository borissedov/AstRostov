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
        private Sku _sku;

        private int SkuId
        {
            get
            {
                int id;
                if (hdnSkuId.Value != null && int.TryParse(hdnSkuId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set { hdnSkuId.Value = value.ToString(CultureInfo.InvariantCulture); }
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
                SkuId = id;
            }

            int count;
            if (!String.IsNullOrEmpty(Request.Params["count"]) && int.TryParse(Request.Params["count"], out count))
            {
                tbCount.Text = count.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                tbCount.Text = "1";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _sku = CoreData.Context.Skus.SingleOrDefault(p => p.SkuId == SkuId);
            if (_sku == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                tbProductName.Text = _sku.Product.Name;
                tbAttributesConfig.Text = _sku.AttributeConfig;
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
                    SkuId = _sku.ProductId,
                    ProductName = _sku.Product.Name,
                    EstimatedPrice = _sku.FinalPrice,
                    AttributeConfig = _sku.AttributeConfig,
                    Count = count,
                    CustomerName = tbCustomer.Text,
                    Phone = tbPhone.Text,
                    CustomerEmail = tbEmail.Text,
                    Comment = tbComment.Text,
                    SkuNumber = _sku.SkuNumber,
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

                    AstMail.SendEmail("sasha2507@aaanet.ru", String.Format("Выставлен новый предзаказ №{0} от {1:G}", preorder.PreorderId, DateTime.Now), true, String.Format("АСТ-Ростов: Предзаказ №{0}", preorder.PreorderId));

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