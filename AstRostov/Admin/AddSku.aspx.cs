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
            var attrDictionary = new Dictionary<string, string>();

            foreach (RepeaterItem item in rptAttributes.Items)
            {
                var tbAttrName = item.FindControl("lblAttributeTitle") as Label;
                var tbAttrValue = item.FindControl("tbAttributeValue") as AutocompleteTextbox;
                if (tbAttrName != null && tbAttrValue != null)
                {
                    if (string.IsNullOrWhiteSpace(tbAttrValue.Text))
                    {
                        lblError.Text = "Форма заполнена не правильно.";
                        return;
                    }

                    attrDictionary[tbAttrName.Text.Trim()] = tbAttrValue.Text.Trim();
                }
            }

            if (attrDictionary.Count == 0)
            {
                lblError.Text = "Нельзя вручную добавить пустую конфигурацию продукта.";
                return;
            }

            //Check for sku exist
            if (_product.SkuCollection.Count > 0)
            {
                if (
                    _product.SkuCollection.Any(
                        s => s.AttributeValues.All(sv => attrDictionary[sv.Attribute.Name] == sv.Value)))
                {
                    lblError.Text = "Такая конфигурация уже существует для данного продукта";
                    return;
                }
            }
            else
            {
                lblError.Text = "Фатальная ошибка конфигураций продукта. У продукта всегда должна быть хотя бы одна конфигурация!";
                return;
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

            var attrVals = new List<AttributeValue>();
            foreach (var attributeConfig in attrDictionary)
            {
                AttributeValue attrValue =
                    CoreData.Context.AttributeValues
                    .SingleOrDefault(av => av.Attribute.Name == attributeConfig.Key && av.Value == attributeConfig.Value);
                if (attrValue == null)
                {
                    attrValue = new AttributeValue
                        {
                            Value = attributeConfig.Value
                        };
                    Attribute attribute = _product.Attributes.SingleOrDefault(a => a.Name == attributeConfig.Key);
                    if (attribute == null)
                    {
                        lblError.Text = "Не найден атрибут по ключу";
                        return;
                    }

                    attribute.AttributeValues.Add(attrValue);
                    CoreData.Context.SaveChanges();
                }
                attrVals.Add(attrValue);
                //newSku.AttributeValues.Add(attrValue);
            }
            newSku.AttributeValues = attrVals;


            CoreData.Context.Skus.Add(newSku);
            CoreData.Context.SaveChanges();

            Response.Redirect(hlBack.NavigateUrl);
        }

    }
}