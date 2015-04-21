using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Configuration;
using System.Web;
using System.Web.Routing;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.Comepay.Controllers;
using Nop.Core.Domain.Payments;

namespace Nop.Plugin.Payments.Comepay
{

    public class Comepay : BasePlugin, IPaymentMethod
    {
        private readonly ComepaySettings _comepaySettings;

        private readonly ISettingService _settingService;

        private readonly HttpContextBase _httpContext;

        private readonly IOrderTotalCalculationService _orderTotalCalculationService;

        public Comepay(ComepaySettings comepaySettings, ISettingService SettingService, IOrderTotalCalculationService orderTotalCalculationService, HttpContextBase httpContext)
        {
            this._settingService = SettingService;
            this._httpContext = httpContext;
            this._orderTotalCalculationService = orderTotalCalculationService;
            this._comepaySettings = comepaySettings;
        }


        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Comepay";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Payments.Comepay.Controllers" }, { "area", null } };
        }
        public override void Install()
        {
            var settings = new ComepaySettings() { };
            _settingService.SaveSetting(settings);

            base.Install();
        }
        public override void Uninstall()
        {
            _settingService.DeleteSetting<ComepaySettings>();
            base.Uninstall();
        }

        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            var result = new CancelRecurringPaymentResult();
            result.AddError("Ежемесячная оплата не поддерживается.");
            return result;
        }
        public bool CanRePostProcessPayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            //удостоверимся, что прошло не меньше минуты после перехода к оплате
            if ((DateTime.UtcNow - order.CreatedOnUtc).TotalMinutes < 1)
                return false;

            return true;
        }
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var result = new CapturePaymentResult();
            result.AddError("Разрешение оплаты не поддерживается.");
            return result;
        }

        /// <summary>
        /// Returns a value indicating whether payment method should be hidden during checkout
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>true - hide; false - display.</returns>
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            //you can put any logic here
            //for example, hide this payment method if all products in the cart are downloadable
            //or hide this payment method if current customer is from certain country
            
            return false;
        }

        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            var result = this.CalculateAdditionalFee(_orderTotalCalculationService, cart,
               _comepaySettings.AdditionalFee, _comepaySettings.AdditionalFeePercentage);
            return result;
        }
        public Type GetControllerType()
        {
            return typeof(ComepayController);
        }
        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "Comepay";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Payments.Comepay.Controllers" }, { "area", null } };
        }

        public PaymentMethodType PaymentMethodType
        {
            get
            {
                return PaymentMethodType.Redirection;
            }
        }

        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            const string putUrlLive = "https://shop.comepay.ru/api/prv/{0}/bills/{1}";
            const string putUrlTest = "https://moneytest.comepay.ru:439/api/prv/{0}/bills/{1}";

            const string redirectUrlLive = "https://shop.comepay.ru/order/external/main.action?shop={0}&transaction={1}&successUrl={2}&failUrl={3}";
            const string redirectUrlTest = "https://moneytest.comepay.ru:439/order/external/main.action?shop={0}&transaction={1}&successUrl={2}&failUrl={3}";

            var settings = _settingService.LoadSetting<ComepaySettings>();

            string customerPhoneNumber =
                postProcessPaymentRequest.Order.BillingAddress.PhoneNumber.Replace("+7", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("-", "")
                    .Replace(" ", "");

            if (customerPhoneNumber.Length != 10)
            {
                throw new NopException("Телефон плательщика задан неправильно.");
            }

            string putBilltData = String.Format("=user%3Dtel%253A%252B7{0}%26amount%3D{1}%26ccy%3DRUB%26comment%3D{2}%26lifetime%3D{3}%26prv_name%3D{4}",
                HttpUtility.UrlEncode(customerPhoneNumber),
                HttpUtility.UrlEncode(postProcessPaymentRequest.Order.OrderTotal.ToString("N2").Replace(",", ".")),
                HttpUtility.UrlEncode(String.Format("Оплата заказа №{0} от магазина \"{1}\"", postProcessPaymentRequest.Order.Id, settings.prv_name)/*settings.paymentdescription*/),
                HttpUtility.UrlEncode(DateTime.Now.AddMonths(1).ToString("s")),
                HttpUtility.UrlEncode(settings.prv_name)
                );

            string basicAuthHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", settings.prv_purse, settings.password)));
            string authHeaderValue = String.Format("Basic {0}", basicAuthHash);

            var putUri = String.Format(settings.testMode ? putUrlTest : putUrlLive, settings.prv_id, postProcessPaymentRequest.Order.Id);

            var webRequest = (HttpWebRequest)WebRequest.Create(putUri);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            webRequest.Method = "PUT";
            webRequest.Headers.Add(HttpRequestHeader.Authorization, authHeaderValue);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] byteData = Encoding.UTF8.GetBytes(putBilltData);
            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
            }
            var response = (HttpWebResponse)webRequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string responseText = streamReader.ReadToEnd();

                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Information("Request: " + putBilltData + Environment.NewLine + "Response: " + responseText);
            }

            string rootpath = "http://" + HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port != 80 && HttpContext.Current.Request.Url.Port != 443)
            {
                rootpath += ":" + HttpContext.Current.Request.Url.Port;
            }
            string success = rootpath + "/Plugins/Comepay/Success";
            string fail = rootpath + "/Plugins/Comepay/Fail";

            string redirectUrl = String.Format(
                    settings.testMode ? redirectUrlTest : redirectUrlLive,
                    settings.prv_id,
                    postProcessPaymentRequest.Order.Id,
                    success,
                    fail
                    );
            _httpContext.Response.Redirect(redirectUrl);
        }

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.NewPaymentStatus = PaymentStatus.Pending;
            return result;
        }

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.AddError("Ежемесячная оплата не поддерживается.");
            return result;
        }

        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                return RecurringPaymentType.NotSupported;
            }
        }

        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var result = new RefundPaymentResult();
            result.AddError("Возврат денег не поддерживается.");
            return result;
        }

        public bool SupportCapture
        {
            get
            {
                return false;
            }
        }

        public bool SupportPartiallyRefund
        {
            get
            {
                return false;
            }
        }

        public bool SupportRefund
        {
            get
            {
                return false;
            }
        }

        public bool SupportVoid
        {
            get
            {
                return false;
            }
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var result = new VoidPaymentResult();
            result.AddError("Метод недействительности не поддерживается.");
            return result;
        }

        public bool SkipPaymentInfo
        {
            get
            {
                return false;
            }
        }

    }


}
