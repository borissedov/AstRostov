using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;
using NLog;

namespace AstRostov.Admin
{
    public partial class PreorderConvert : System.Web.UI.Page
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

        private Preorder _preorder;

        private Order _order;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindPreorder();
            if (!Page.IsPostBack)
            {
                BindUsernamesAutocomplete();
                BindProduct();
                BindMethods();
            }
            Calculate();
        }

        private void BindUsernamesAutocomplete()
        {
            txtUserName.DataSource = CoreData.Context.Users.Select(u => u.UserName).ToArray();

            var user =
                CoreData.Context.Users.FirstOrDefault(u => u.Membership.Email == _preorder.CustomerEmail);
            if (user != null)
            {
                txtUserName.Text = user.UserName;
            }
        }

        private void BindPreorder()
        {
            _preorder = CoreData.Context.Preorders.SingleOrDefault(p => p.PreorderId == ItemId);
            if (_preorder == null)
            {
                throw new Exception("Preorder not found");
                return;
            }
            if (_preorder.State != PreorderState.Pending)
            {
                throw new Exception("Preorder with invalid state");
                return;
            }
        }


        private void BindProduct()
        {
            var sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == _preorder.SkuId);
            if (sku == null)
            {
                throw new Exception("Sku for preorder not found");
            }

            lblProductName.Text = sku.Product.Name;
            lblSkuAttrs.Text = sku.AttributeConfig;
            lblSkuNumber.Text = sku.SkuNumber;
            tbProductPrice.Text = sku.FinalPrice.ToString("F");

            tbCount.Text = _preorder.Count.ToString(CultureInfo.InvariantCulture);
            tbEmail.Text = _preorder.CustomerEmail;
            tbPhone.Text = _preorder.Phone;
        }

        private void BindMethods()
        {
            rblShippingType.DataSource =
                CoreData.Context.ShippingTariffs.ToArray().Select(
                    t => new ListItem(t.ShippingType.GetDescription(), ((int)t.ShippingType).ToString(CultureInfo.InvariantCulture)));
            rblShippingType.DataTextField = "Text";
            rblShippingType.DataValueField = "Value";
            rblShippingType.DataBind();
            if(!_preorder.ShippingType.HasValue)
            {
                rblShippingType.SelectedValue =
                    ((int)CoreData.Context.ShippingTariffs.OrderBy(t => t.ShippingCost).ToArray().Last().ShippingType).ToString(
                        CultureInfo.InvariantCulture);
            }
            else
            {
                rblShippingType.SelectedValue = ((int)_preorder.ShippingType.Value).ToString(CultureInfo.InvariantCulture);
            }
            
            rblPaymentMethod.DataSource =
                CoreData.Context.PaymentTariffs.ToArray().Select(
                    t => new ListItem(t.PaymentMethod.GetDescription(), ((int)t.PaymentMethod).ToString(CultureInfo.InvariantCulture)));
            rblPaymentMethod.DataTextField = "Text";
            rblPaymentMethod.DataValueField = "Value";
            rblPaymentMethod.DataBind();
            rblPaymentMethod.SelectedValue =
                ((int)CoreData.Context.PaymentTariffs.OrderBy(t => t.CommissionPercent).ToArray().Last().PaymentMethod).ToString(
                    CultureInfo.InvariantCulture);
        }

        private void Calculate()
        {
            int count;
            if (!int.TryParse(tbCount.Text, out count) || count <= 0)
            {
                throw new Exception("Invalid count");
                return;
            }

            var sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == _preorder.SkuId);
            if (sku == null)
            {
                throw new Exception("Sku not found");
            }

            decimal finalPrice;
            if (!Decimal.TryParse(tbProductPrice.Text, out finalPrice) || finalPrice <= 0)
            {
                finalPrice = sku.FinalPrice;
                tbProductPrice.Text = finalPrice.ToString("F");
            }

            decimal retailPrice = sku.RetailPrice ?? sku.Product.RetailPrice;
            if (retailPrice < finalPrice)
            {
                retailPrice = finalPrice;
            }

            _order = new Order
            {
                Preorder = this._preorder
            };

            _order.FullName = tbFullName.Text;
            _order.Email = tbEmail.Text;
            _order.Country = ddlCountry.SelectedValue;
            _order.Region = tbRegion.Text;
            _order.City = tbCity.Text;
            _order.Address1 = tbAddress1.Text;
            _order.Address2 = tbAddress2.Text;
            _order.ZipCode = tbZipCode.Text;
            _order.Phone = tbPhone.Text;
            _order.DocumentType = ddlDocumentType.SelectedValue;
            _order.DocumentNumber = tbDocumentNumber.Text;

            _order.OrderLineItems.Add(new OrderLineItem
            {
                ProductId = sku.ProductId,
                SkuId = sku.SkuId,
                RetailPrice = retailPrice,
                SalePrice = finalPrice,
                ProductName = sku.Product.Name,
                Count = count,
                AttributeConfig = sku.AttributeConfig,
                SkuNumber = sku.SkuNumber,
            });

            _order.ItemsSubtotal = finalPrice * count;

            _order.DiscountTotal = count * (retailPrice - finalPrice);

            _order.ShippingType = SelectedShippingType;

            _order.PaymentMethod = SelectedPaymentMethod;

            ShippingTariff shippingTariff;
            switch (_order.ShippingType)
            {
                case ShippingType.PickUp:
                    shippingTariff =
                        CoreData.Context.ShippingTariffs.Single(t => t.ShippingType == ShippingType.PickUp);
                    break;
                case ShippingType.ShippingCompany:
                    shippingTariff =
                        CoreData.Context.ShippingTariffs.Single(t => t.ShippingType == ShippingType.ShippingCompany);
                    break;
                default:
                    throw new Exception("Невозможно рассчитать стоимость доставки. Не указан способ доставки.");
            }

            _order.ShippingCost = shippingTariff.ShippingCost;

            PaymentTariff paymentTariff;
            switch (_order.PaymentMethod)
            {

                case PaymentMethod.BankTransfer:
                    paymentTariff =
                        CoreData.Context.PaymentTariffs.Single(t => t.PaymentMethod == PaymentMethod.BankTransfer);
                    break;
                case PaymentMethod.Robokassa:
                    paymentTariff =
                        CoreData.Context.PaymentTariffs.Single(t => t.PaymentMethod == PaymentMethod.Robokassa);
                    break;
                default:
                    throw new Exception("Невозможно рассчитать комиссию. Не указан способ оплаты.");
            }
            _order.CommissionTotal = (_order.ShippingCost + _order.ItemsSubtotal) * paymentTariff.CommissionPercent / 100;

            _order.Total = _order.ItemsSubtotal + _order.ShippingCost + _order.CommissionTotal;

            gridLineItems.Visible = true;
            gridLineItems.DataSource = _order.OrderLineItems;
            gridLineItems.DataBind();

        }

        private ShippingType SelectedShippingType
        {
            get
            {
                ShippingType selectedShippingType;
                if (Enum.TryParse(rblShippingType.SelectedValue, out selectedShippingType))
                {
                    return selectedShippingType;
                }
                return default(ShippingType);
            }
        }


        private PaymentMethod SelectedPaymentMethod
        {
            get
            {
                PaymentMethod selectedPaymentMethod;
                if (Enum.TryParse(rblPaymentMethod.SelectedValue, out selectedPaymentMethod))
                {
                    return selectedPaymentMethod;
                }

                return default(PaymentMethod);
            }
        }

        protected void ValidateUserName(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;

            var username = args.Value;

            if (String.IsNullOrEmpty(username))
            {
                return;
            }

            var user = CoreData.Context.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                args.IsValid = true;
            }
        }

        protected void GridItemDataBount(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer && _order != null)
            {
                var litSubtotal = e.Item.FindControl("litSubtotal") as Literal;
                var litShippingPrice = e.Item.FindControl("litShippingPrice") as Literal;
                var litShippingName = e.Item.FindControl("litShippingName") as Literal;
                var litCommission = e.Item.FindControl("litCommission") as Literal;
                var litTotal = e.Item.FindControl("litTotal") as Literal;

                if (litSubtotal != null && litShippingPrice != null && litShippingName != null && litCommission != null && litTotal != null)
                {
                    litSubtotal.Text = _order.ItemsSubtotal.ToString("c");
                    litShippingPrice.Text = _order.ShippingCost.ToString("c");
                    litShippingName.Text = _order.ShippingType.GetDescription();
                    litCommission.Text = _order.CommissionTotal.ToString("c");
                    litTotal.Text = _order.Total.ToString("c");
                }
            }
        }

        protected void PreorderChanged(object sender, EventArgs e)
        {
        }

        protected void CreateOrder(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var user = CoreData.Context.Users.FirstOrDefault(u => u.UserName == txtUserName.Text);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                _order.Account = user;
                _order.CreateDate = DateTime.Now;
                _order.OrderState = OrderState.Pending;
                CoreData.Context.Orders.Add(_order);

                _preorder.State = PreorderState.Accepted;
                CoreData.Context.SaveChanges();

                try
                {
                    var message = String.Format(File.ReadAllText(Server.MapPath("~/MessageTemplates/OrderBankTransfer.txt")),
                       _order.Account,
                       _order.OrderId);

                    AstMail.SendEmail(_order.Email, message, true, String.Format("АСТ-Ростов: Заказ №{0}", _order.OrderId));

                    AstMail.SendEmail("sasha2507@aaanet.ru", String.Format("Выставлен новый заказ №{0} от {1:G}", _order.OrderId, DateTime.Now), false, String.Format("АСТ-Ростов: Новый заказ №{0}", _order.OrderId));
                    AstMail.SendEmail( "Marketing@ast-rostov.ru", String.Format("Выставлен новый заказ №{0} от {1:G}", _order.OrderId, DateTime.Now), false, String.Format("АСТ-Ростов: Новый заказ №{0}", _order.OrderId));
                    AstMail.SendEmail("sasha2507alexin@yandex.ru", String.Format("Выставлен новый заказ №{0} от {1:G}", _order.OrderId, DateTime.Now), false, String.Format("АСТ-Ростов: Новый заказ №{0}", _order.OrderId));
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Fatal(ex);
                    logger.ErrorException("Ошибка при отправке почты", ex);
                }

                Response.Redirect("~/Admin/PreorderList.aspx");
            }
        }
    }
}