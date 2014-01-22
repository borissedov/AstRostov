using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace AstCore.Models
{
    public class FeaturedItem
    {
        [Key]
        public int FeaturedItemId { get; set; }
        
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [NotMapped]
        public string Url
        {
            get
            {
                var virtualUrl = String.Format("~/Product.aspx?id={0}", Product.ProductId);
                if (HttpContext.Current != null && HttpContext.Current.Handler is Page)
                {
                    return ((Page) HttpContext.Current.Handler).ResolveUrl(virtualUrl);
                }
                return virtualUrl;
            }
        }

        [NotMapped]
        public string FormattedPrice
        {
            get
            {
                return Product.FormattedPrice;
            }
        }
    }
}
