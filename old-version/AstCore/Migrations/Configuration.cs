using AstCore.Models;
using System.Data.Entity.Migrations;

namespace AstCore.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.AstEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccess.AstEntities context)
        {
            context.ShippingTariffs.AddOrUpdate(
                 new ShippingTariff { ShippingType = ShippingType.PickUp, ShippingCost = 0M },
                 new ShippingTariff { ShippingType = ShippingType.ShippingCompany, ShippingCost = 500M }
                );

            context.PaymentTariffs.AddOrUpdate(
                 new PaymentTariff { PaymentMethod = PaymentMethod.BankTransfer, CommissionPercent = 0M },
                 new PaymentTariff { PaymentMethod = PaymentMethod.Robokassa, CommissionPercent = 5M }
                );
        }
    }
}
