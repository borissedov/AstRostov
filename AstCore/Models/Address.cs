using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public virtual User Account { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public string DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        [NotMapped]
        public string ShortAddress
        {
            get
            {
                return String.Format("{0}, {1}, {2}, {3} {4}, {5}", FullName, Country, City, Address1, Address2, ZipCode);
            }
        }

    }
}
