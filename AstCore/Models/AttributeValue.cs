using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    public class AttributeValue
    {
        
        //public int AttributeValueId
        //{
        //    get; set;
        //}

        [Key, Column(Order = 1)]
        [ForeignKey("Attribute")]
        public virtual int AttributeId
        {
            get;
            set;
        }

        public virtual Attribute Attribute
        {
            get; set;
        }

        [Key, Column(Order = 0)]
        [Required]
        public string Value { get; set; }
    
        public virtual ICollection<Sku> Skus { get; set; } 
    }
}
