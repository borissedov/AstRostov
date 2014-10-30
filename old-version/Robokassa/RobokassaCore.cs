using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Robokassa
{
    public static class RobokassaCore
    {
        //registration data
        static readonly string PostUrl = ConfigurationManager.AppSettings["robokassaHost"];      //"https://auth.robokassa.ru/Merchant/Index.aspx";
        static readonly string MrchLogin = ConfigurationManager.AppSettings["robokassaUser"];     //"test";
        static readonly string Password1 = ConfigurationManager.AppSettings["robokassaPassword1"];//"securepass1";
        static readonly string Password2 = ConfigurationManager.AppSettings["robokassaPassword2"];//"securepass2";

        /// <summary>
        /// Format Url for Payment Request
        /// </summary>
        /// <param name="order">
        /// Must have int OrderId, decimal Total and DateTyme CreateDate
        /// </param>
        /// <returns></returns>
        public static string FormatPostPaymentUrl(dynamic order)
        {
            // order properties
            double outSum;
            int orderId;
            DateTime date;

            try
            {
                outSum = Math.Round((double)order.Total, 2);
                orderId = order.OrderId;
                date = order.CreateDate;
            }
            catch (Exception)
            {
                return String.Empty;
            }

            string desc = String.Format("Заказ №{0} от {1:dd.MM.yyyy}", orderId, date);

            string crcBase = string.Format("{0}:{1:R}:{2}:{3}",
                                             MrchLogin, outSum, orderId, Password1).Replace(',', '.');

            // build CRC value
            var md5 = new MD5CryptoServiceProvider();
            byte[] signature = md5.ComputeHash(Encoding.ASCII.GetBytes(crcBase));

            var sbSignature = new StringBuilder();
            foreach (byte b in signature)
            {
                sbSignature.AppendFormat("{0:x2}", b);
            }

            return String.Format("{0}?MrchLogin={1}&OutSum={2:R}&InvId={3}&Desc={4}&SignatureValue={5}",
                PostUrl, MrchLogin, outSum, orderId, desc, sbSignature).Replace(',', '.');
        }

        /// <summary>
        /// Parse feedback request from Robokassa
        /// </summary>
        /// <param name="requestContext">HttpContext of current request</param>
        /// <returns>Merchant OrderId request is valid, empty if not</returns>
        public static string ProcessFeedbackRequest(HttpContext requestContext)
        {
            // HTTP parameters
            string sOutSum = GetPrm("OutSum", requestContext);
            string merchantOrderId = GetPrm("InvId", requestContext);
            string sCrc = GetPrm("SignatureValue", requestContext);

            string sCrcBase = string.Format("{0}:{1}:{2}",
                                             sOutSum, merchantOrderId, Password2);

            // build own CRC
            var md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));

            var sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            string sMyCrc = sbSignature.ToString();

            if (!String.Equals(sMyCrc, sCrc, StringComparison.CurrentCultureIgnoreCase))
            {
                requestContext.Response.Write("bad sign");
                return String.Empty;
            }

            requestContext.Response.ContentType = "text/plain";
            requestContext.Response.Write(string.Format("OK{0}", merchantOrderId));

            return merchantOrderId;
        }

        /// <summary>
        /// Validate Success response from Robokassa
        /// </summary>
        /// <param name="currentContext">HttpContext</param>
        /// <returns>Merchant OrderId if valid, empty if not</returns>
        public static string ProcessSuccessResponse(HttpContext currentContext)
        {
            // HTTP parameters
            string sOutSum = GetPrm("OutSum", currentContext);
            string merchantOrderId = GetPrm("InvId", currentContext);
            string sCrc = GetPrm("SignatureValue", currentContext);

            string sCrcBase = string.Format("{0}:{1}:{2}",
                                             sOutSum, merchantOrderId, Password1);

            // build own CRC
            var md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));

            var sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            string sMyCrc = sbSignature.ToString();

            if (sMyCrc.ToUpper() != sCrc.ToUpper())
            {
                //currentContext.Response.Clear();
                //currentContext.Response.Write("bad sign");
                return string.Empty;
            }

            return merchantOrderId;
        }


        /// <summary>
        /// Get named parameter from current request
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        private static string GetPrm(string sName, HttpContext requestContext)
        {
            string sValue = requestContext.Request.Form[sName];

            if (string.IsNullOrEmpty(sValue))
                sValue = requestContext.Request.QueryString[sName];

            if (string.IsNullOrEmpty(sValue))
                sValue = String.Empty;

            return sValue;
        }
    }
}
