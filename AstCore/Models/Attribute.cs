using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class Attribute
    {
        [Key]
        public int AttributeId { get; set; }

        [Required]
        public string Name { get; set; } 
        
        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

        public virtual ICollection<Product> Products { get; set; } 
    }
}
