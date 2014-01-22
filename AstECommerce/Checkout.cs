using System;
using System.Linq;
using System.Web;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstECommerce
{
    /// <summary>
    /// Class for checkout scenario
    /// 1) LoadOrderFromCart
    /// 2) SetAddress 
    /// 3) External set Shipping and Payment methods
    /// 4) CalculateTotals
    /// 5) ProccessCheckout
    /// 6) External action for selected Payment method
    /// </summary>
    public static class Checkout
    {
        public static Order Order
        {
            get
            {
                var order = HttpContext.Current.Session["CheckoutOrder"] as Order;
                if (order == null)
                {
                    order = new Order
                    {
                        CreateDate = DateTime.Now
                    };
                    HttpContext.Current.Session["CheckoutOrder"] = order;
                }
                return order;
            }
            set
            {
                HttpContext.Current.Session["CheckoutOrder"] = value;
            }
        }

        public static void SetAddress(Address address)
        {
            Order.FullName = address.FullName;
            Order.Email = address.Email;
            Order.Country = address.Country;
            Order.Region = address.Region;
            Order.City = address.City;
            Order.Address1 = address.Address1;
            Order.Address2 = address.Address2;
            Order.ZipCode = address.ZipCode;
            Order.Phone = address.Phone;
            Order.DocumentType = address.DocumentType;
            Order.DocumentNumber = address.DocumentNumber;
        }

        public static void LoadOrderFromCart()
        {
            ShoppingCart.Calculate();
            if (ShoppingCart.ShoppingCartItems.Any() && ShoppingCart.AvailabilityCheck)
            {
                Order = null;
                foreach (var shoppingCartItem in ShoppingCart.ShoppingCartItems)
                {
                    Order.OrderLineItems.Add(new OrderLineItem
                    {
                        ProductId = shoppingCartItem.Sku.ProductId,
                        SkuId = shoppingCartItem.Sku.SkuId,
                        RetailPrice = shoppingCartItem.Sku.RetailPrice ?? shoppingCartItem.Sku.Product.RetailPrice,
                        SalePrice = shoppingCartItem.Sku.FinalPrice,
                        ProductName = shoppingCartItem.Sku.Product.Name,
                        Count = shoppingCartItem.Count,
                        AttributeConfig = shoppingCartItem.Sku.AttributeConfig,
                        ProductNum = shoppingCartItem.Sku.Product.ProductNum
                    });
                }
                Order.Account = AstMembership.CurrentUser;
                Order.ItemsSubtotal = ShoppingCart.Total;
                Order.DiscountTotal = ShoppingCart.Discount;
            }
            else
            {
                throw new Exception("Корзина пуста или количество заказываемого продукта на складе уменьшилось.");
            }
        }

        /// <summary>
        /// Calculate totals after selecting Shipping and Payment Methods
        /// </summary>
        public static void CalculateTotals()
        {
            ShippingTariff shippingTariff;
            switch (Order.ShippingType)
            {
                case ShippingType.PickUp:
                    shippingTariff =
                        CoreData.Context.ShippingTariffs.Single(t => t.ShippingType == ShippingType.PickUp);
                    break;
                case ShippingType.ShippingCompany:
                    shippingTariff =
                        CoreData.Context.ShippingTariffs.Single(t => t.ShippingType == ShippingType.ShippingCompany);
                    break;
                default:
                    throw new Exception("Невозможно рассчитать стоимость доставки. Не указан способ доставки.");
            }
            Order.ShippingCost = shippingTariff.ShippingCost;


            PaymentTariff paymentTariff;
            switch (Order.PaymentMethod)
            {

                case PaymentMethod.BankTransfer:
                    paymentTariff =
                        CoreData.Context.PaymentTariffs.Single(t => t.PaymentMethod == PaymentMethod.BankTransfer);
                    break;
                case PaymentMethod.Robokassa:
                    paymentTariff =
                        CoreData.Context.PaymentTariffs.Single(t => t.PaymentMethod == PaymentMethod.Robokassa);
                    break;
                default:
                    throw new Exception("Невозможно рассчитать комиссию. Не указан способ оплаты.");
            }
            Order.CommissionTotal = (Order.ShippingCost + Order.ItemsSubtotal) * paymentTariff.CommissionPercent / 100;


            Order.Total = Order.ItemsSubtotal + Order.ShippingCost + Order.CommissionTotal;
        }

        /// <summary>
        /// Final valiations
        /// Updates inventory of products
        /// Saving order as pending
        /// </summary>
        /// <returns></returns>
        public static bool ProccessCheckout(out int orderId)
        {
            bool checkInventory = true;
            foreach (var item in Order.OrderLineItems)
            {
                var sku = CoreData.Context.Skus.Single(p => p.SkuId == item.SkuId);
                checkInventory &= sku.Inventory >= item.Count;
            }

            if (!checkInventory)
            {
                orderId = 0;
                return false;
            }


            foreach (var item in Order.OrderLineItems)
            {
                var sku = CoreData.Context.Skus.Single(p => p.SkuId == item.SkuId);
                sku.Inventory -= item.Count;
            }

            Order.OrderState = OrderState.Pending;

            CoreData.Context.Users.Attach(Order.Account);
            CoreData.Context.Orders.Add(Order);

            CoreData.Context.SaveChanges();
            orderId = Order.OrderId;

            ShoppingCart.ClearCart();
            Order = null;
            return true;
        }
    }
}
