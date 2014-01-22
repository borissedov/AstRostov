using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditCategory : System.Web.UI.Page
    {
        private int ItemId
        {
            get
            {
                int id;
                if (hdnItemId.Value != null && int.TryParse(hdnItemId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set
            {
                hdnItemId.Value = value.ToString();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseItemId();
            }
        }

        private void ParseItemId()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategoryForm();
            }
        }

        private void BindCategoryForm()
        {
            Category category;
            if (ItemId == 0)
            {
                category = new Category();
            }
            else
            {
                category = CoreData.Context.Categories.SingleOrDefault(i => i.CategoryId == ItemId);
            }
            if (category == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            tbCategoryName.Text = category.Name;

            if (ItemId == 0)
            {
                litEditTitle.Text = @"Создание новой категории";
                phEditBindings.Visible = false;
            }
            else
            {
                litEditTitle.Text = String.Format(@"Редактирование категории #{0}", ItemId);
                phEditBindings.Visible = true;
                BindBindings(category);
            }

        }

        private void BindBindings(Category category)
        {
            gridChildCategories.DataSource = category.ChildCategories;
            gridChildCategories.DataBind();

            gridParentCategories.DataSource = category.ParentCategories;
            gridParentCategories.DataBind();

            BindAddChildForm(category);
            BindAddParentForm(category);
        }

        private void BindAddParentForm(Category category)
        {
            ddlSelectParent.Items.Add(new ListItem
                {
                    Text = "Выберите категорию",
                    Value = "0",
                    Selected = true
                });

            ddlSelectParent.Items.AddRange(
                CoreData.Context.Categories.ToArray().Where(
                    c => 
                    c.CategoryId != category.CategoryId && //Not current
                    category.ParentCategories.All(cc => cc.CategoryId != c.CategoryId) && //Not in existing parents
                    category.AllChildrenIds.All(cid => cid != c.CategoryId)) //Not in children
                        .Select(c => new ListItem(c.Name, c.CategoryId.ToString(CultureInfo.InvariantCulture))).ToArray()//Select list of ListItems
            );
        }

        private void BindAddChildForm(Category category)
        {
            ddlSelectChild.Items.Add(new ListItem
            {
                Text = "Выберите категорию",
                Value = "0",
                Selected = true
            });

            ddlSelectChild.Items.AddRange(
                CoreData.Context.Categories.ToArray().Where(
                    c =>
                    c.CategoryId != category.CategoryId && //Not current
                    category.ChildCategories.All(cc => cc.CategoryId != c.CategoryId) && //Not in existing children
                    category.AllParentsIds.All(pid => pid != c.CategoryId)) //Not in parents
                    .Select(c => new ListItem(c.Name, c.CategoryId.ToString(CultureInfo.InvariantCulture))).ToArray()//Select list of ListItems
            );
        }


        protected void SaveCategory(object sender, EventArgs e)
        {

            Category category = ItemId == 0 ? new Category() : CoreData.Context.Categories.SingleOrDefault(i => i.CategoryId == ItemId);
            if (category == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            category.Name = tbCategoryName.Text.Trim();

            if (ItemId == 0)
            {
                CoreData.Context.Categories.Add(category);
            }

            CoreData.Context.SaveChanges();

            Response.Redirect(String.Format("~/Admin/EditCategory.aspx?id={0}", category.CategoryId));
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditCategory.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    UnbindCategory(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        private void UnbindCategory(int categoryId)
        {
            Category category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == ItemId);
            if (category == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            Category categoryToDelete;
            if (category.HasChildren)
            {
                categoryToDelete = category.ChildCategories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (categoryToDelete != null)
                {
                    category.ChildCategories.Remove(categoryToDelete);
                }
                else
                {
                    ErrorLabel.Text = "Удаляемая дочерняя категория не найдена.";
                    return;
                }
            }
            else if (category.ParentCategories.Any())
            {
                categoryToDelete = category.ParentCategories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (categoryToDelete != null)
                {
                    category.ParentCategories.Remove(categoryToDelete);
                }
                else
                {
                    ErrorLabel.Text = "Удаляемая родительская категория не найдена.";
                    return;
                }
            }
            else
            {
                ErrorLabel.Text = "У данной категории нет ни дочерних ни родительских категорий.";
                return;
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditCategory.aspx?id={0}", ItemId));
        }

        protected void BindParentCategory(object sender, EventArgs e)
        {
            Category category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == ItemId);
            if (category == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            int categoryId;
            if (int.TryParse(ddlSelectParent.SelectedValue, out categoryId) && categoryId != 0 && category.ParentCategories.All(c => c.CategoryId != categoryId))
            {
                Category categoryToBind = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (categoryToBind != null)
                {
                    category.ParentCategories.Add(categoryToBind);
                    CoreData.Context.SaveChanges();
                    Response.Redirect("~/Admin/CategoryList.aspx");
                    //Response.Redirect(String.Format("~/Admin/EditCategory.aspx?id={0}", ItemId));
                }
                else
                {
                    ErrorLabel.Text = "Привязываемая категория не найдена.";
                    return;
                }
            }
            else
            {
                ErrorLabel.Text = "Категория не указана или уже привязана.";
                return;
            }
        }

        protected void BindChildCategory(object sender, EventArgs e)
        {
            Category category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == ItemId);
            if (category == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            int categoryId;
            if (int.TryParse(ddlSelectChild.SelectedValue, out categoryId) && categoryId != 0 && category.ChildCategories.All(c => c.CategoryId != categoryId))
            {
                Category categoryToBind = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (categoryToBind != null)
                {
                    category.ChildCategories.Add(categoryToBind);
                    CoreData.Context.SaveChanges();
                    Response.Redirect("~/Admin/CategoryList.aspx");
                    //Response.Redirect(String.Format("~/Admin/EditCategory.aspx?id={0}", ItemId));
                }
                else
                {
                    ErrorLabel.Text = "Привязываемая категория не найдена.";
                    return;
                }
            }
            else
            {
                ErrorLabel.Text = "Категория не указана или уже привязана.";
                return;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }
    }
}