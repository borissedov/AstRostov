using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public long ShoppingCartItemId { get; set; }

        public int Count { get; set; }

        public int SkuId { get; set; }

        [ForeignKey("SkuId")]
        public virtual Sku Sku { get; set; }

        [ForeignKey("ShoppingCartEntity")]
        public Guid ShoppingCartEntityId;

        public virtual ShoppingCartEntity ShoppingCartEntity { get; set; }
        
        public bool CheckInventory()
        {
            return CheckInventory(Count);
        }

        public bool CheckInventory(int count)
        {
            return Sku.Inventory >= count && count <= 10;
        }

        [NotMapped]
        public Decimal Subtotal
        {
            get
            {
                return Sku.FinalPrice * Count;
            }
        }
    }
}
