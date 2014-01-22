using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace AstCore.Helpers
{
    public class AstMail
    {
        public static void SendEmail(string toAddress, string body, bool isBodyHtml = false, string subject = "АСТ-Ростов: Оповещение")
        {
            var fromMailAddress = new MailAddress("admin@ast-rostov.ru", "Администрация сайта АСТ-Ростов");
            var toMailAddress = new MailAddress(toAddress, toAddress);

            var smtp = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPassword"]),
                Host = ConfigurationManager.AppSettings["smtpHost"],
                Port = 25
            };
            using (var message = new MailMessage(fromMailAddress, toMailAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            })
            {
                smtp.Send(message);
            }
        }
    }
}
