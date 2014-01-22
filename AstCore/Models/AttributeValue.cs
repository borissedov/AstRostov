using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class AttributeValue
    {
        
        //public int AttributeValueId
        //{
        //    get; set;
        //}

        [Key]
        [Required]
        public virtual Attribute Attribute
        {
            get; set;
        }

        [Key]
        [Required]
        public string Value { get; set; }
    
        public virtual ICollection<Sku> Skus { get; set; } 
    }
}
