using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AstCore.Models
{
    public class Sku
    {
        [Key]
        public int SkuId { get; set; }

        public int ProductId { get; set; }

        public bool IsDefault { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

        public Decimal? RetailPrice { get; set; }

        public Decimal? SalePrice { get; set; }

        public int Inventory { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        [NotMapped]
        public string AttributeConfig
        {
            get
            {
                if(AttributeValues.Any())
                {
                    return String.Join(", ",
                                   AttributeValues.Select(v => String.Format("{0}: {1}", v.Attribute.Name, v.Value)));
                }
                return String.Empty;
            }
        }

        [NotMapped]
        public Decimal FinalPrice
        {
            get
            {
                return SalePrice ?? RetailPrice ?? Product.SalePrice ?? Product.RetailPrice;
            }
        }
    }
}
