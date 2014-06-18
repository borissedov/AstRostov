using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    public class CategoryImage
    {
        [Key]
        public int Id { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string FileName { get; set; }
        
       
        public override string ToString()
        {
            return FileName;
        }
    }
}
