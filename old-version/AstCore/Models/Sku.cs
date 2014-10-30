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

        public string SkuNumber { get; set; }

        public string AdditionalDescription { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

        public Decimal? RetailPrice { get; set; }

        public Decimal? SalePrice { get; set; }

        public int Inventory { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual ICollection<SkuImage> Images { get; set; }

        [NotMapped]
        public string AttributeConfig
        {
            get
            {
                if (AttributeValues.Any())
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


        public string FormattedPrice(int count = 1)
        {

            if (RetailPrice.HasValue && FinalPrice < RetailPrice)
            {
                return String.Format(@"<span class=""price-old"">{0:c}</span><span class=""price-new"">{1:c}</span>", RetailPrice * count, FinalPrice * count);
            }
            else
            {
                return String.Format(@"<span class=""price-new"">{0:c}</span>", FinalPrice * count);
            }
        }

        [NotMapped]
        public SkuImage MainImage
        {
            get
            {
                return Images.SingleOrDefault(i => i.IsMain) ?? Images.OrderByDescending(pi => pi.Id).FirstOrDefault() ?? new SkuImage { FileName = "noimage.gif" };
            }
        }
    }
}
