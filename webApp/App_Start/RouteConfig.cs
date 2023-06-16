using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PaymentChangeStatus",
                url: "Payment/ChangeStatus/{payment_id}",
                defaults: new { controller = "Payment", action = "ChangeStatus" }
             );
            routes.MapRoute(
                name: "OrderDetail",
                url: "Order/Detail/{order_id}",
                defaults: new { controller = "Order", action = "Detail" }
             );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
