using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Plugin.Payments.Robokassa.Models;
using Nop.Services.Payments;
using System.Collections;
using Nop.Services.Orders;
using Nop.Services.Logging;
using System.Web;
using Nop.Services.Configuration;

namespace Nop.Plugin.Payments.Robokassa.Controllers
{
    public class RobokassaController : BasePaymentController
    {
        IOrderService _orders;
        ILogger _log;
        HttpContextBase _httpcontext;
        ISettingService _settings;

        public RobokassaController(IOrderService orders,ILogger log, HttpContextBase httpcontext,ISettingService SettingService)
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
            var settings = _settings.LoadSetting<RobokassaSettings>();
            model.login = settings.login;
            model.password1 = settings.password1;
            model.password2 = settings.password2;
            model.paymentdescription = settings.paymentdescription;
            return View("Nop.Plugin.Payments.Robokassa.Views.Robokassa.Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            model.httpcontext = _httpcontext;
            if (!ModelState.IsValid)
                return Configure();
            _settings.SaveSetting<RobokassaSettings>(new RobokassaSettings() { login = model.login, password1 = model.password1, password2 = model.password2, paymentdescription = model.paymentdescription });
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
            return View("Nop.Plugin.Payments.Robokassa.Views.Robokassa.PaymentInfo");
        }

        [HttpGet]
        public ActionResult Result(string OutSum, string InvId, string SignatureValue)
        {
            if (!String.IsNullOrEmpty(OutSum)&&!String.IsNullOrEmpty(InvId)&&!String.IsNullOrEmpty(SignatureValue)){

                string merchantpass2 = _settings.LoadSetting<RobokassaSettings>().password2;

                //check md5:
                string computedmd5 = MD5Helper.GetMD5Hash(String.Format("{0}:{1}:{2}",OutSum,InvId,merchantpass2));
                if (computedmd5.ToUpper() != SignatureValue.ToUpper()) return Content("Error");

                int orderid = Convert.ToInt32(InvId);
                var order = _orders.GetOrderById(orderid);

                if (order.OrderGuid != Guid.Empty){
                    decimal sum = Convert.ToDecimal(OutSum.Replace(".",","));
                    if (order.OrderTotal <= sum){ //если оплачен полностью (или больше), то отметить как оплаченный
                        order.PaymentStatus = Core.Domain.Payments.PaymentStatus.Paid;
                        _orders.UpdateOrder(order);
                    }
                }
                return Content(string.Format("OK{0}",InvId));
            }
            return Content("Error");
        }

        [HttpGet]
        public ActionResult Fail(string OutSum, string InvId)
        {
            var model = new Fail();
            if (!String.IsNullOrEmpty(OutSum) && !String.IsNullOrEmpty(InvId))
            {
                model.orderid = InvId;
                model.ordersum = OutSum;
            }
            else
            {
                model.orderid = "0";
                model.ordersum = "0";
            }
            return View("Nop.Plugin.Payments.Robokassa.Views.Robokassa.Fail",model);
        }

        [HttpGet]
        public ActionResult Success(string OutSum, string InvId, string SignatureValue, string Culture)
        {
            if (!String.IsNullOrEmpty(OutSum) && !String.IsNullOrEmpty(InvId) && !String.IsNullOrEmpty(SignatureValue))
            {

                string merchantpass = _settings.LoadSetting<RobokassaSettings>().password1;

                //check md5:
                string computedmd5 = MD5Helper.GetMD5Hash(String.Format("{0}:{1}:{2}", OutSum, InvId, merchantpass));
                if (computedmd5.ToUpper() != SignatureValue.ToUpper())
                {
                    _log.InsertLog(logLevel: Core.Domain.Logging.LogLevel.Error, shortMessage: "Неправильный переход на success", fullMessage: String.Format("OutSum:{0}, InvId:{1}, SignatureValue:{2}", OutSum, InvId, SignatureValue));
                    return Content("Ошибка: От Робокассы получены неверные параметры.\nОшибка сохранена в лог сервера.");
                }

                return RedirectToRoute("CheckoutCompleted", new { orderId = InvId });
            }
            return Content("Ошибка: не указаны параметры при переходе.");
        }

    }
}
