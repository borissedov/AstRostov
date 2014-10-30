using System;
using System.Linq;
using System.Web.UI;
using AstCore.DataAccess;

namespace AstRostov
{
    public partial class DefaultPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void ClearCatalog()
        {
            //Clear All
            foreach (var product in CoreData.Context.Products.ToArray())
            {
                product.Attributes.Clear();

                foreach (var sku in product.SkuCollection)
                {
                    sku.AttributeValues.Clear();
                }

                CoreData.Context.Products.Remove(product);
            }
            var cids = CoreData.Context.Categories.Select(c => c.CategoryId).ToArray();
            foreach (var cid in cids)
            {
                var categoryToDelete = CoreData.Context.Categories.Single(c => c.CategoryId == cid);
                foreach (var childCategory in categoryToDelete.ChildCategories.ToArray())
                {
                    childCategory.ParentCategories.Remove(categoryToDelete);
                }

                foreach (var parentCategory in categoryToDelete.ParentCategories.ToArray())
                {
                    parentCategory.ChildCategories.Remove(categoryToDelete);
                }

                CoreData.Context.Categories.Remove(categoryToDelete);
                CoreData.Context.SaveChanges();

            }
            CoreData.Context.SaveChanges();
        }
    }
}