using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Plugins;
using Nop.Services.Payments;
using Nop.Services.Configuration;
using System.Web;
using System.Web.Routing;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.Robokassa.Controllers;
using Nop.Core.Domain.Payments;
using System.Security.Cryptography;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.Robokassa
{

    public class Robokassa : BasePlugin, IPaymentMethod
    {
        private readonly RobokassaSettings _robokassaSettingsSettings;

        private readonly ISettingService _settingService;

        private readonly HttpContextBase _httpContext;

        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        
        public Robokassa(RobokassaSettings robokassaSettingsSettings, ISettingService SettingService, IOrderTotalCalculationService orderTotalCalculationService,  HttpContextBase httpContext)
        {
            this._settingService = SettingService;
            this._httpContext = httpContext;
            this._robokassaSettingsSettings = robokassaSettingsSettings;
            this._orderTotalCalculationService = orderTotalCalculationService;
        }


        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Robokassa";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Payments.Robokassa.Controllers" }, { "area", null } };
        }
        public override void Install()
        {
            var settings = new RobokassaSettings() { };
            _settingService.SaveSetting(settings);

            base.Install();
        }
        public override void Uninstall()
        {
            _settingService.DeleteSetting<RobokassaSettings>();
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
                _robokassaSettingsSettings.AdditionalFee, _robokassaSettingsSettings.AdditionalFeePercentage);
            return result;
        }
        public Type GetControllerType()
        {
            return typeof(RobokassaController);
        }
        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "Robokassa";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Payments.Robokassa.Controllers" }, { "area", null } };
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
            var builder = new StringBuilder();
            var settings = _settingService.LoadSetting<RobokassaSettings>();
            string merchant = settings.login;
            string merchantpass = settings.password1;
            string desc = settings.paymentdescription;
            string signature = String.Format("{0}:{1}:{2}:{3}", merchant, postProcessPaymentRequest.Order.OrderTotal.ToString().Replace(",","."), postProcessPaymentRequest.Order.Id, merchantpass);
            string currency = "rub";
            string culture = "ru";

            builder.Append("https://auth.robokassa.ru/Merchant/Index.aspx");
            builder.AppendFormat("?MrchLogin={0}", merchant);
            builder.AppendFormat("&OutSum={0}", postProcessPaymentRequest.Order.OrderTotal.ToString().Replace(",", "."));
            builder.AppendFormat("&InvId={0}", postProcessPaymentRequest.Order.Id);
            builder.AppendFormat("&Desc={0}", HttpUtility.UrlEncode(desc));
            builder.AppendFormat("&SignatureValue={0}", MD5Helper.GetMD5Hash(signature));
            builder.AppendFormat("&sIncCurrLabel={0}", currency);
            builder.AppendFormat("&Email={0}", postProcessPaymentRequest.Order.BillingAddress.Email);
            builder.AppendFormat("&sCulture={0}", culture);

            _httpContext.Response.Redirect(builder.ToString());
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

    public class MD5Helper
    {
        public static string GetMD5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }

}
