using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;
using Attribute = AstCore.Models.Attribute;

namespace AstRostov.Admin
{
    public partial class EditProductConfiguration : System.Web.UI.Page
    {
        private Product _product;

        private int ProductId
        {
            get
            {
                int id;
                if (hdnProductId.Value != null && int.TryParse(hdnProductId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set { hdnProductId.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseProductId();
            }
        }

        private void ParseProductId()
        {
            int id;
            if (int.TryParse(Request.Params["pid"], out id))
            {
                ProductId = id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ProductId == 0)
            {
                Response.Redirect("~/");
                return;
            }

            _product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ProductId);
            if (_product == null)
            {
                lblError.Text = "Редактируемая сущность не найдена.";
                return;
            }

            if (!Page.IsPostBack)
            {
                BindAutocomplete();
                BindAttributes();

                hlBack.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProduct.aspx?id={0}", ProductId));
            }
        }

        private void BindAutocomplete()
        {
            //Here I want to put json serialized dectionary of attrs that not contains in current product
            var availableAttributes = CoreData.Context.Attributes.ToArray();
            hdnAttributeDictionary.Value = String.Format("{{{0}}}",
                String.Join(",", availableAttributes.Select(
                a => String.Format("\"{0}\": [\"{1}\"]",
                    a.Name, String.Join("\",\"", a.AttributeValues.Select(v => v.Value)))
                )));
        }

        private void BindAttributes()
        {
            List<Attribute> attrs = _product.Attributes.ToList();

            // Here is the stub of UI - max 5 attrs for product
            int addCount = 5 - attrs.Count;
            for (int i = 0; i < addCount; i++)
            {
                attrs.Add(new Attribute());
            }

            rptAttributes.DataSource = attrs;
            rptAttributes.DataBind();
        }


        protected void SaveConfiguration(object sender, EventArgs e)
        {
            var attrNames = new List<string>();
            var attrVals = new List<string>();

            foreach (RepeaterItem item in rptAttributes.Items)
            {
                var tbAttrName = item.FindControl("tbAttributeName") as TextBox;
                var tbAttrValue = item.FindControl("tbAttributeDefaultValue") as TextBox;
                if (tbAttrName != null && tbAttrValue != null)
                {
                    if (string.IsNullOrWhiteSpace(tbAttrName.Text) && !string.IsNullOrWhiteSpace(tbAttrValue.Text) ||
                        string.IsNullOrWhiteSpace(tbAttrValue.Text) && !string.IsNullOrWhiteSpace(tbAttrName.Text))
                    {
                        lblError.Text = "Форма заполнена не правильно.";
                        return;
                    }

                    if (!String.IsNullOrWhiteSpace(tbAttrName.Text))
                    {
                        attrNames.Add(tbAttrName.Text.Trim());
                        attrVals.Add(tbAttrValue.Text.Trim());
                    }
                }
            }

            if (attrNames.Count != attrVals.Count)
            {
                lblError.Text = "Ошибка при формировании атрибутов.";
                return;
            }

            if (attrNames.Count != attrNames.Distinct().Count())
            {
                lblError.Text = "У продукта не может быть атрибутов с одинаковыми названиями.";
                return;
            }

            //So validation passed. Let's save

            foreach (int skuId in _product.SkuCollection.Select(s=>s.SkuId).ToArray())
            {
                var sku = CoreData.Context.Skus.Single(s => s.SkuId == skuId);
                sku.AttributeValues.Clear();
                CoreData.Context.Skus.Remove(sku);
            }
            CoreData.Context.SaveChanges();

            _product.Attributes.Clear();
            CoreData.Context.SaveChanges();

            var attrList = new List<Attribute>();
            foreach (string attrName in attrNames)
            {
                Attribute attr = CoreData.Context.Attributes.SingleOrDefault(a => a.Name == attrName);
                if (attr == null)
                {
                    attr = new Attribute
                        {
                            Name = attrName
                        };
                    CoreData.Context.Attributes.Add(attr);
                    CoreData.Context.SaveChanges();
                }

                _product.Attributes.Add(attr);
                attrList.Add(attr);
            }
            CoreData.Context.SaveChanges();

            var attrValues = new List<AttributeValue>();
            for (int i = 0; i < attrList.Count; i++)
            {
                //var attrVal = CoreData.Context.Attributes.Attach(attrList[i]).AttributeValues.SingleOrDefault(v => v.Value == attrVals[i]);
                int attrId = attrList[i].AttributeId;
                string attrValStr = attrVals[i];
                var attrVal =
                    CoreData.Context.AttributeValues.SingleOrDefault(
                        v => v.Value == attrValStr && v.AttributeId == attrId);
                if (attrVal == null)
                {
                    attrVal = new AttributeValue
                        {
                            Value = attrVals[i],
                            Attribute = CoreData.Context.Attributes.Single(a => a.AttributeId == attrId)
                        };
                    //attrList[i].AttributeValues.Add(attrVal);
                }
                attrValues.Add(attrVal);
            }
            CoreData.Context.SaveChanges();

            var newSku = new Sku
                {
                    AttributeValues = attrValues,
                    Inventory = 0
                };
            _product.SkuCollection.Add(newSku);
            CoreData.Context.SaveChanges();

            Response.Redirect(hlBack.NavigateUrl);
        }
    }
}