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

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ShoppingCartEntity")]
        public Guid ShoppingCartEntityId;

        public virtual ShoppingCartEntity ShoppingCartEntity { get; set; }
        
        public bool CheckInventory()
        {
            return CheckInventory(Count);
        }

        public bool CheckInventory(int count)
        {
            return Product.Inventory >= count && count <= 10;
        }

        [NotMapped]
        public Decimal Subtotal
        {
            get
            {
                return Product.FinalPrice * Count;
            }
        }
    }
}
