using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    public class OrderLineItem
    {
        [Key]
        public long OrderLineItemId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int SkuId { get; set; }
        
        public string ProductNum { get; set; }

        [Required]
        public Decimal RetailPrice { get; set; }

        [Required]
        public Decimal SalePrice { get; set; }

        [Required]
        public string AttributeConfig { get; set; }

        public virtual Order Order { get; set; }

        [NotMapped]
        public Decimal Subtotal
        {
            get
            {
                return SalePrice * Count;
            }
        }

        [NotMapped]
        public Decimal DiscountSubtotal
        {
            get
            {
                return (RetailPrice - SalePrice) * Count;
            }
        }
    }
}
