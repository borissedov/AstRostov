using Nop.Core.Configuration;

namespace Nop.Plugin.Payments.Robokassa
{
    public class RobokassaSettings: ISettings
    {
        public string login { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }
        public string paymentdescription { get; set; }
        public decimal AdditionalFee { get; set; }
        public bool AdditionalFeePercentage { get; set; }
    }
}
