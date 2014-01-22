using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace AstCore.Models
{
    public class Product
    {
        public Product()
        {
            IsFeatured = false;
        }


        [Key]
        [Column("Id")]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string ProductNum { get; set; }

        [Required]
        public Decimal RetailPrice { get; set; }

        public Decimal? SalePrice { get; set; }

        [Required]
        public string Description { get; set; }

        public int Inventory { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

        [Required]
        public bool IsFeatured { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        [NotMapped]
        public ProductImage MainImage
        {
            get
            {
                return Images.SingleOrDefault(i => i.IsMain) ?? Images.OrderByDescending(pi => pi.Id).FirstOrDefault() ?? new ProductImage { FileName = "noimage.gif" };
            }
        }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category
        {
            get;
            set;
        }

        public int? BrandId { get; set; }

        public virtual Brand Brand
        {
            get;
            set;
        }

        [NotMapped]
        public Decimal FinalPrice
        {
            get
            {
                return SalePrice.HasValue ? SalePrice.Value : RetailPrice;
            }
        }

        [NotMapped]
        public string ShortDescription
        {
            get
            {
                if (String.IsNullOrEmpty(Description) || Description.Length < 65)
                {
                    return Description;
                }

                var subDescription = Description.Substring(0, 65);

                var indexOfNewLine = subDescription.IndexOf("<br><br>", StringComparison.Ordinal);
                if (indexOfNewLine == -1)
                {
                    indexOfNewLine = subDescription.LastIndexOf("<br>", StringComparison.Ordinal);
                }
                if (indexOfNewLine == -1)
                {
                    indexOfNewLine = subDescription.LastIndexOf('.');
                }

                if (indexOfNewLine != -1)
                {
                    subDescription = subDescription.Substring(0, indexOfNewLine);
                }


                if (subDescription != HttpUtility.HtmlEncode(subDescription))
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(subDescription);
                    while (htmlDoc.ParseErrors.Any())
                    {
                        subDescription = subDescription.Substring(0, subDescription.LastIndexOf('<'));
                        htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(subDescription);
                    }
                }
                return subDescription;
            }
        }

        [NotMapped]
        public string FormattedPrice
        {
            get
            {
                if (FinalPrice < RetailPrice)
                {
                    return String.Format(@"<span class=""price-old"">{0:c}</span><span class=""price-new"">{1:c}</span>", RetailPrice, SalePrice.Value);
                }
                else
                {
                    return String.Format(@"<span class=""price-new"">{0:c}</span>", RetailPrice);
                }
            }
        }


        
    }
}
