using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    [Table("Profile")]
    public partial class Profile
    {
        public Guid UserId { get; set; }
        public string PropertyNames { get; set; }
        public string PropertyValueStrings { get; set; }
        public byte[] PropertyValueBinary { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public virtual User User { get; set; }
    }
}
