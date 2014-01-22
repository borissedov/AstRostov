using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    public class PaymentTariff
    {
        [Key, Column(Order = 0)]
        public PaymentMethod PaymentMethod { get; set; }

        [Required, Column(Order = 1)]
        public Decimal CommissionPercent { get; set; }
    }
}
