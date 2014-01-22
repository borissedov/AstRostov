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

        public static bool AddToCart(int productId, int count = 1)
        {
            if (CoreData.Context != null)
            {
                Product product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == productId);
                return AddToCart(product, count);
            }
            else
            {
                using (var context = new AstEntities())
                {
                    Product product = context.Products.SingleOrDefault(p => p.ProductId == productId);
                    bool res = AddToCart(product, count);
                    context.SaveChanges();
                    return res;
                }
            }
        }

        public static bool AddToCart(Product product, int count = 1)
        {
            var cartItem = ShoppingCartItems.SingleOrDefault(ci => ci.ProductId == product.ProductId);
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
                    Product = product,
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

            Total = ShoppingCartItems.Sum(i => i.Count * i.Product.FinalPrice);
            TotalWithoutDiscount = ShoppingCartItems.Sum(i => i.Count * i.Product.RetailPrice);
            Discount = ShoppingCartItems.Sum(i => i.Count * (i.Product.RetailPrice - i.Product.FinalPrice));
            SaveState();
        }

        public static void UpdateQuantity(int productId, int count)
        {
            var item = ShoppingCartItems.SingleOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Count = count;
            }
            SaveState();
        }

        public static void RemoveItem(int productId)
        {
            var item = ShoppingCartItems.SingleOrDefault(i => i.ProductId == productId);
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
