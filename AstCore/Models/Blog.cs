using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class Blog
    {

        [Key]
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public string ManufacturedYear { get; set; }
        public string BuyYear { get; set; }
        public string Color { get; set; }
        public string ModelComment { get; set; }
        public bool Driving { get; set; }
        public bool Sell { get; set; }
        public string Power { get; set; }
        public string EngineVolume { get; set; }
        public string GosNumber { get; set; }
        public string Vin { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        //public int AuthorId { get; set; }
        [Required]
        public User Author { get; set; }
    }
}
