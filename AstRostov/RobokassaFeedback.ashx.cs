using System;
using System.IO;
using System.Linq;
using System.Web;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;
using NLog;

namespace AstRostov
{
    public class RobokassaFeedback : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string merchantOrderId = Robokassa.RobokassaCore.ProcessFeedbackRequest(context);
            int orderId;
            if (int.TryParse(merchantOrderId, out orderId) && orderId > 0)
            {
                var order = CoreData.Context.Orders.SingleOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.OrderState = OrderState.PaymentConfirmed;
                    CoreData.Context.SaveChanges();

                    try
                    {
                        var message = String.Format(File.ReadAllText(context.Server.MapPath("~/MessageTemplates/OrderRobokassa.txt")),
                           order.Account,
                           order.OrderId);

                        AstMail.SendEmail(order.Email, message, true, String.Format("АСТ-Ростов: Заказ №{0}", order.OrderId));

                        AstMail.SendEmail("sasha2507@aaanet.ru", String.Format("Заказ №{0} оплачен в {1:G}", order.OrderId, DateTime.Now), false, String.Format("АСТ-Ростов: Заказ №{0} оплачен", order.OrderId));
                    }
                    catch (Exception ex)
                    {
                        Logger logger = LogManager.GetCurrentClassLogger();
                        logger.Fatal(ex);
                        logger.ErrorException("Ошибка при отправке почты", ex);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}