using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public ICollection<NewsItem> News { get; set; }
        public ICollection<NewsComment> NewsComments { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
    }
}
