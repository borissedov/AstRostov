using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
