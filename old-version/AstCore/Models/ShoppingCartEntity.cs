using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AstCore.Models
{
    public class ShoppingCartEntity
    {
        [Key]
        public Guid SessionId { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public decimal? Total { get; set; }
        public decimal? TotalWithoutDiscount { get; set; }
        public decimal? Discount { get; set; }
        public bool? AvailabilityCheck { get; set; }
    }
}
