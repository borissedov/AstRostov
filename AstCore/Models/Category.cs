using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstCore.SearchEngine;

namespace AstCore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual CategoryImage Image { get; set; }

        public virtual ICollection<Category> ParentCategories { get; set; }

        [InverseProperty("ParentCategories")]
        public virtual ICollection<Category> ChildCategories { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// List of products in current category ordered by name
        /// </summary>
        [NotMapped]
        public IEnumerable<Product> ProductList
        {
            get
            {
                return ChildCategories.Aggregate<Category, IEnumerable<Product>>(
                    Products,
                    (currentProductList, childCategory) =>
                        currentProductList.Concat(childCategory.ProductList))
                    .Distinct(ProductComparer.GetInstance());
            }
        }

        [NotMapped]
        public bool IsRoot
        {
            get
            {
                return ParentCategories == null || !ParentCategories.Any();
            }
        }

        [NotMapped]
        public bool HasChildren
        {
            get
            {
                return ChildCategories != null && ChildCategories.Any();
            }
        }

        [NotMapped]
        public IEnumerable<int> AllParentsIds
        {
            get
            {
                var allParents = new List<int>();
                allParents.AddRange(ParentCategories.Select(p => p.CategoryId));
                foreach (var parent in ParentCategories)
                {
                    allParents.AddRange(parent.AllParentsIds);
                }
                return allParents.Distinct();
            }
        }

        [NotMapped]
        public IEnumerable<int> AllChildrenIds
        {
            get
            {
                var allChildren = new List<int>();
                allChildren.AddRange(ChildCategories.Select(p => p.CategoryId));
                foreach (var parent in ChildCategories)
                {
                    allChildren.AddRange(parent.AllChildrenIds);
                }
                return allChildren.Distinct();
            }
        }

        public Category()
        {
            ChildCategories = new List<Category>();
            ParentCategories = new List<Category>();
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
