using System.ComponentModel.DataAnnotations;

namespace AstCore.Models
{
    public class MainSliderItem
    {
        [Key]
        public int MainSliderItemId { get; set; }
        [Required]
        public string ImageFile { get; set; }
        //[Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Url { get; set; }
    }
}
