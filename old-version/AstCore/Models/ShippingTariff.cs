using System;
using System.ComponentModel.DataAnnotations;

namespace AstCore.Models
{
    public class ShippingTariff
    {
        [Key]
        public ShippingType ShippingType { get; set; }

        [Required]
        public Decimal ShippingCost { get; set; }
    }
}
