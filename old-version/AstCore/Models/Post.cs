using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace AstCore.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<PostComment> Comments { get; set; }
        //public ICollection<Tag> Tags { get; set; }

        //public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual User Author { get; set; }

        [NotMapped]
        public string ShortContent
        {
            get
            {
                if (String.IsNullOrEmpty(Content) || Content.Length < 1000)
                {
                    return Content;
                }

                var subcontent = Content.Substring(0, 1000);

                var indexOfNewLine = subcontent.IndexOf("<br><br>", StringComparison.Ordinal);
                if (indexOfNewLine == -1)
                {
                    indexOfNewLine = subcontent.LastIndexOf("<br>", StringComparison.Ordinal);
                }
                if (indexOfNewLine == -1)
                {
                    indexOfNewLine = subcontent.LastIndexOf('.');
                }

                if (indexOfNewLine != -1)
                {
                    subcontent = subcontent.Substring(0, indexOfNewLine);
                }


                if (subcontent != HttpUtility.HtmlEncode(subcontent))
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(subcontent);
                    while (htmlDoc.ParseErrors.Any())
                    {
                        subcontent = subcontent.Substring(0, subcontent.LastIndexOf('<'));
                        htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(subcontent);
                    }
                }
                return subcontent;
            }
        }
    }
}
