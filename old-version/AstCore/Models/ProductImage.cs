﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        public string FileName { get; set; }

        public bool IsMain { get; set; }

        //[NotMapped] 
        //public const string CatalogPath = "~/img/Catalog/";

        ////[NotMapped] 
        ////public const string DefaultImage = "NoImage.png";

        //[NotMapped]
        //public string ImageUrl
        //{
        //    get
        //    {
        //        var page = HttpContext.Current.Handler as Page;
        //        if (page != null)
        //        {
        //            return page.ResolveUrl(String.Format("{0}{1}", CatalogPath, FileName));
        //        }
        //        return String.Empty;
        //    }
        //}

        public override string ToString()
        {
            return FileName;
        }
    }
}
