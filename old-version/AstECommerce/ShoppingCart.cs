using System.Collections.Generic;
using System.Linq;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstECommerce
{
    public class ShoppingCart : ShoppingCartRepository
    {
        public static ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return ShoppingCartInstance.ShoppingCartItems; }
        }

        public static bool AddToCart(int skuId, int count = 1)
        {
            if (CoreData.Context != null)
            {
                Sku sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == skuId);
                return AddToCart(sku, count);
            }
            else
            {
                using (var context = new AstEntities())
                {
                    Sku sku = context.Skus.SingleOrDefault(s => s.SkuId == skuId);
                    bool res = AddToCart(sku, count);
                    context.SaveChanges();
                    return res;
                }
            }
        }

        public static bool AddToCart(Sku sku, int count = 1)
        {
            var cartItem = ShoppingCartItems.SingleOrDefault(ci => ci.SkuId == sku.SkuId);
            //Product exists in ShoppingCart
            if (cartItem != null)
            {
                if (cartItem.CheckInventory(cartItem.Count + count))
                {
                    cartItem.Count += count;
                    SaveState();
                    return true;
                }
                return false;
            }

            //Creating new ShoppingCartItem
            cartItem = new ShoppingCartItem
                {
                    Sku = sku,
                    Count = count
                };

            if (cartItem.CheckInventory())
            {
                ShoppingCartItems.Add(cartItem);
                SaveState();
                return true;
            }
            return false;
        }

        public static decimal Total
        {
            get
            {
                if (!ShoppingCartInstance.Total.HasValue)
                {
                    Calculate();
                }

                return ShoppingCartInstance.Total.Value;
            }
            private set
            {
                ShoppingCartInstance.Total = value;
            }
        }

        public static decimal TotalWithoutDiscount
        {
            get
            {
                if (!ShoppingCartInstance.TotalWithoutDiscount.HasValue)
                {
                    Calculate();
                }

                return ShoppingCartInstance.TotalWithoutDiscount.Value;
            }
            private set
            {
                ShoppingCartInstance.TotalWithoutDiscount = value;
            }
        }

        public static decimal Discount
        {
            get
            {
                if (!ShoppingCartInstance.Discount.HasValue)
                {
                    Calculate();
                }

                return ShoppingCartInstance.Discount.Value;
            }
            private set
            {
                ShoppingCartInstance.Discount = value;
            }
        }

        public static bool AvailabilityCheck
        {
            get
            {
                if (!ShoppingCartInstance.AvailabilityCheck.HasValue)
                {
                    Calculate();
                }

                return ShoppingCartInstance.AvailabilityCheck.Value;
            }
            private set
            {
                ShoppingCartInstance.AvailabilityCheck = value;
            }
        }

        public static void Calculate()
        {
            bool res = true;
            foreach (var shoppingCartItem in ShoppingCartItems)
            {
                res &= shoppingCartItem.CheckInventory();
                if (!res)
                {
                    break;
                }
            }
            AvailabilityCheck = res;

            Total = ShoppingCartItems.Sum(i => i.Count * i.Sku.FinalPrice);
            TotalWithoutDiscount = ShoppingCartItems.Sum(i => i.Count * (i.Sku.RetailPrice ?? i.Sku.Product.RetailPrice));
            Discount = ShoppingCartItems.Sum(i => i.Count * ((i.Sku.RetailPrice ?? i.Sku.Product.RetailPrice) - i.Sku.FinalPrice));
            SaveState();
        }

        public static void UpdateQuantity(int skuId, int count)
        {
            var item = ShoppingCartItems.SingleOrDefault(i => i.SkuId == skuId);
            if (item != null)
            {
                item.Count = count;
            }
            SaveState();
        }

        public static void RemoveItem(int skuId)
        {
            var item = ShoppingCartItems.SingleOrDefault(i => i.SkuId == skuId);
            if (item != null)
            {
                ShoppingCartItems.Remove(item);
            }
            SaveState();
        }

        public static void ClearCart()
        {
            ClearRepository();
        }

    }
}
