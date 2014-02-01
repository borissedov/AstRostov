using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.CommonControls;
using AstCore.DataAccess;
using AstCore.Models;
using Attribute = AstCore.Models.Attribute;

namespace AstRostov.Admin
{
    public partial class AddSku : Page
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
            _product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ProductId);
            if (_product == null)
            {
                lblError.Text = "Указанный продукт не найден.";
                return;
            }

            if (!Page.IsPostBack)
            {
                BindAttributes();

                hlBack.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProduct.aspx?id={0}", _product.ProductId));
            }
        }

        private void BindAttributes()
        {
            rptAttributes.DataSource = _product.Attributes.ToArray();
            rptAttributes.DataBind();

            //Here I want to put json serialized dectionary of attrs that not contains in current product
            var availableAttributes = CoreData.Context.Attributes.Except(_product.Attributes).ToArray();
            hdnAttributeDictionary.Value = String.Format("{{{0}}}",
                String.Join(",", availableAttributes.Select(
                a => String.Format("{0}: [\"{1}\"]",
                    a.Name, String.Join("\",\"", a.AttributeValues))
                )));
        }

        protected void OnAttributeItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var attribute = e.Item.DataItem as Attribute;
            var tbAttributeValue = e.Item.FindControl("tbAttributeValue") as AutocompleteTextbox;
            if (attribute != null && tbAttributeValue != null)
            {
                tbAttributeValue.DataSource = attribute.AttributeValues.Select(v => v.Value).ToArray();
            }
        }

        protected void SaveSku(object sender, EventArgs e)
        {
            Dictionary<string, AttributeConfig> attrDictionary;
            var serializer = new JavaScriptSerializer();
            try
            {
                attrDictionary =
                    serializer.Deserialize<Dictionary<string, AttributeConfig>>(hdnAttributeDictionaryToSave.Value);
            }
            catch (Exception)
            {
                lblError.Text = "Ошибка десериализации атрибутов.";
                return;
            }

            var oldAttrs = attrDictionary.Where(kvp => _product.Attributes.Any(a => a.Name == kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var newAttrs = attrDictionary.Where(kvp => _product.Attributes.All(a => a.Name != kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            if (oldAttrs.Count() + newAttrs.Count() != attrDictionary.Count)
            {
                lblError.Text = "Ошибка конфигурации атрибутов.";
                return;
            }

            //Check for sku exist
            if (_product.SkuCollection.Any(s => s.AttributeValues.All(sv => oldAttrs[sv.Attribute.Name].Value == sv.Value)))
            {
                lblError.Text = "Такая конфигурация уже существует для данного продукта";
                return;
            }

            if (newAttrs.Count > 0)
            {
                if (newAttrs.Any(a => String.IsNullOrEmpty(a.Value.Default)))
                {
                    lblError.Text = "Для одного из новых атрибутов не указано значение по умолчанию";
                    return;
                }

                foreach (var attributeConfig in newAttrs)
                {
                    var attribute = new Attribute
                        {
                            Name = attributeConfig.Key
                        };
                    CoreData.Context.Attributes.Add(attribute);

                    var newAttrValue = new AttributeValue
                        {
                            Attribute = attribute,
                            Value = attributeConfig.Value.Value
                        };
                    CoreData.Context.AttributeValues.Add(newAttrValue);

                    var defaultAttributeValue = newAttrValue;
                    if (attributeConfig.Value.Default != attributeConfig.Value.Value)
                    {
                        defaultAttributeValue = new AttributeValue
                            {
                                Attribute = attribute,
                                Value = attributeConfig.Value.Default
                            };
                        CoreData.Context.AttributeValues.Add(defaultAttributeValue);
                    }

                    foreach (var sku in _product.SkuCollection)
                    {
                        sku.AttributeValues.Add(defaultAttributeValue);
                    }
                }
            }

            var newSku = new Sku
                {
                    Product = _product,
                    RetailPrice = null,
                    SalePrice = null
                };

            int inventory;
            if (int.TryParse(tbInventory.Text, out inventory))
            {
                newSku.Inventory = inventory;
            }
            else
            {
                lblError.Text = "Не указано количество на складе";
                return;
            }

            var salePriceString = tbSalePrice.Text.Trim();
            if (!String.IsNullOrEmpty(salePriceString))
            {
                decimal salePrice;
                if (decimal.TryParse(salePriceString, out salePrice))
                {
                    newSku.SalePrice = salePrice;
                }
                else
                {
                    lblError.Text = "Цена со скидкой указана не верно";
                    return;
                }
            }

            var retailPriceString = tbRetailPrice.Text.Trim();
            if (!String.IsNullOrEmpty(retailPriceString))
            {
                decimal retailPrice;
                if (decimal.TryParse(retailPriceString, out retailPrice))
                {
                    newSku.RetailPrice = retailPrice;
                }
                else
                {
                    lblError.Text = "Цена указана не верно";
                    return;
                }
            }

            foreach (var attributeConfig in attrDictionary)
            {
                var attrValue =
                    CoreData.Context.AttributeValues
                    .SingleOrDefault(av => av.Attribute.Name == attributeConfig.Key && av.Value == attributeConfig.Value.Value);
                if (attrValue == null)
                {
                    lblError.Text = "Ошибка формирования новой конфигурации продукта";
                    return;
                }

                newSku.AttributeValues.Add(attrValue);
            }

            CoreData.Context.Skus.Add(newSku);
            CoreData.Context.SaveChanges();
        }

        [Serializable]
        private class AttributeConfig
        {
            public string Value { get; set; }
            public string Default { get; set; }
        }
    }
}