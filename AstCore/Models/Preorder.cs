using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AstCore.Models
{
    public class Preorder
    {
        [Key]
        public long PreorderId { get; set; }
        
        [Required]
        public string ProductName { get; set; }

        [Required]
        public int SkuId { get; set; }

        public string SkuNumber { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        public string AttributeConfig { get; set; }
        
        [Required]
        public Decimal EstimatedPrice { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        public PreorderState State { get; set; }

        public DateTime Date { get; set; }
    }

    public enum PreorderState
    {
        [Description("В обработке")]
        Pending = 1,
        [Description("Принят")]
        Accepted = 2,
        [Description("Отклонен")]
        Declined = 3
    }
}
