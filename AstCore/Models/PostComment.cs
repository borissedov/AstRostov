using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class PostComment
    {
        [Key]
        public int PostCommentId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        //public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public virtual User Author { get; set; }

        //public ICollection<Tag> Tags { get; set; }
    }
}
