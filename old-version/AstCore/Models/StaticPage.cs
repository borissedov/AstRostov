using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class StaticPage
    {
        [Column(Order = 0), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(Order = 1), Key]
        public string Key { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
    }
}
