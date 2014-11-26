using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Plugin.Payments.Comepay.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public string prv_id { get; set; }
        public string prv_name { get; set; }
        public string prv_purse { get; set; }
        public string password { get; set; }
        public string password2 { get; set; }
        public string paymentdescription { get; set; }
        public bool testMode { get; set; }
        public decimal AdditionalFee { get; set; }
        public bool AdditionalFeePercentage { get; set; }
        public HttpContextBase httpcontext { get; set; }
    }
}
