using System.Xml.Linq;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Nop.Plugin.Payments.Comepay.Models;
using Nop.Services.Payments;
using Nop.Services.Orders;
using Nop.Services.Logging;
using System.Web;
using Nop.Services.Configuration;

namespace Nop.Plugin.Payments.Comepay.Controllers
{
    public class ComepayController : BasePaymentController
    {
        IOrderService _orders;
        ILogger _log;
        HttpContextBase _httpcontext;
        ISettingService _settings;

        public ComepayController(IOrderService orders, ILogger log, HttpContextBase httpcontext, ISettingService SettingService)
        {
            this._orders = orders;
            this._log = log;
            this._httpcontext = httpcontext;
            this._settings = SettingService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();
            model.httpcontext = _httpcontext;
            var settings = _settings.LoadSetting<ComepaySettings>();
            model.prv_id = settings.prv_id;
            model.password = settings.password;
            model.prv_name = settings.prv_name;
            model.prv_purse = settings.prv_purse;
            model.paymentdescription = settings.paymentdescription;
            model.testMode = settings.testMode;
            return View("Nop.Plugin.Payments.Comepay.Views.Comepay.Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            model.httpcontext = _httpcontext;
            if (!ModelState.IsValid)
                return Configure();
            _settings.SaveSetting<ComepaySettings>(new ComepaySettings()
            {
                prv_id = model.prv_id,
                prv_name = model.prv_name,
                prv_purse = model.prv_purse,
                password = model.password,
                password2 = model.password2,
                paymentdescription = model.paymentdescription,
                testMode = model.testMode
            });
            return Configure();
        }

        [NonAction]
        public override ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest();
            return paymentInfo;
        }
        [NonAction]
        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            var warnings = new List<string>();
            return warnings;
        }
        [ChildActionOnly]
        public ActionResult PaymentInfo()
        {
            return View("Nop.Plugin.Payments.Comepay.Views.Comepay.PaymentInfo");
        }

        [HttpPost]
        public ActionResult Result(string bill_id, string status, string error, string amount, string user, string prv_name, string ccy, string comment)
        {
            string authHeader = Request.Headers["Authorization"];
            if (!String.IsNullOrEmpty(authHeader) && !String.IsNullOrEmpty(bill_id))
            {
                var authParts = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Contains("Basic ") ? authHeader.Replace("Basic ", "") : authHeader)).Split(new[] { ':' });
                if (authParts.Length != 2)
                    return Content("Error");

                string purseNum = authParts[0];
                string password2 = authParts[1];

                var settings = _settings.LoadSetting<ComepaySettings>();

                if (purseNum != settings.prv_purse || password2 != settings.password2)
                    return Content("Error");


                int orderid = Convert.ToInt32(bill_id);
                var order = _orders.GetOrderById(orderid);

                if (order.OrderGuid != Guid.Empty)
                {
                    order.PaymentStatus = Core.Domain.Payments.PaymentStatus.Paid;
                    _orders.UpdateOrder(order);
                }
                return Content(XDocument.Parse("<result><result_code>0</result_code></result>").ToString());
            }
            return Content("Error");
        }

        [HttpGet]
        public ActionResult Fail(string order)
        {
            var model = new Fail();
            model.orderid = !String.IsNullOrEmpty(order) ? order : "0";
            return View("Nop.Plugin.Payments.Comepay.Views.Comepay.Fail", model);
        }

        [HttpGet]
        public ActionResult Success(string order)
        {
            if (!String.IsNullOrEmpty(order))
            {
                return RedirectToRoute("CheckoutCompleted", new { orderId = order });
            }
            return Content("Ошибка: не указаны параметры при переходе.");
        }

    }
}
