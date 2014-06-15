using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
