using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstECommerce
{
    public class ShoppingCartRepository
    {
        #region Repository

        protected static ShoppingCartEntity ShoppingCartInstance
        {
            get
            {
                AstEntities context = CoreData.Context;
                var shoppingCart =
                    context.ShoppingCartEntities.SingleOrDefault(e => e.SessionId == SessionId);
                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCartEntity
                        {
                            SessionId = SessionId,
                            ShoppingCartItems = new List<ShoppingCartItem>()
                        };
                    context.ShoppingCartEntities.Add(shoppingCart);
                    context.SaveChanges();
                }

                return shoppingCart;
            }
        }

        private static Guid SessionId
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    var guid = HttpContext.Current.Session["ShoppingCartGuid"] as Guid?;
                    if (guid == null)
                    {
                        guid = Guid.NewGuid();
                        HttpContext.Current.Session["ShoppingCartGuid"] = guid.Value;
                    }
                    return guid.Value;
                }

                return default(Guid);
            }
        }

        protected static void SaveState()
        {
            CoreData.Context.SaveChanges();
        }

        public static void DisposeState()
        {
            AstEntities context = CoreData.Context;
            var instance = context.ShoppingCartEntities.SingleOrDefault(s => s.SessionId == SessionId);
            if (instance != null)
            {
                context.ShoppingCartEntities.Remove(instance);
                context.SaveChanges();
            }
        }

        public static void ClearRepository()
        {
            AstEntities context = CoreData.Context;
            foreach (ShoppingCartEntity stateToDelete in context.ShoppingCartEntities)
            {
                context.ShoppingCartEntities.Remove(stateToDelete);
            }
            context.SaveChanges();
        }

        #endregion

    }
}
