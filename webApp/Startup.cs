using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using MongoDB.Driver;
using webApp.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(webApp.Startup))]
namespace webApp
{
    public class Startup
    {
        public static void RegisterServices()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var databaseName = "restaurant";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            DependencyResolver.SetResolver(new MongoDependencyResolver(mongoDatabase));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
