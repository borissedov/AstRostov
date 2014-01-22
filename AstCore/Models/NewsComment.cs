using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class NewsComment
    {
        [Key]
        public int NewsCommentId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public int NewsItemId { get; set; }
        public virtual NewsItem NewsItem { get; set; }

        public virtual User Author { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; }
    }
}
