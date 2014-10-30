using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Widgets.Countdown
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Payments.Comepay.ResultURL",
                 "Plugins/Comepay/Result",
                 new { controller = "Comepay", action = "Result" },
                 new[] { "Nop.Plugin.Payments.Comepay.Controllers" }
            );

            routes.MapRoute("Plugin.Payments.Comepay.FailURL",
                 "Plugins/Comepay/Fail",
                 new { controller = "Comepay", action = "Fail" },
                 new[] { "Nop.Plugin.Payments.Comepay.Controllers" }
            );
            routes.MapRoute("Plugin.Payments.Comepay.SuccessURL",
                 "Plugins/Comepay/Success",
                 new { controller = "Comepay", action = "Success" },
                 new[] { "Nop.Plugin.Payments.Comepay.Controllers" }
            );

        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
