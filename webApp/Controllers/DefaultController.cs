using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using webApp.Models;

namespace webApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        private readonly IMongoDatabase _database;

        public DefaultController() : this(DependencyResolver.Current.GetService<IMongoDatabase>()) { }

        public DefaultController(IMongoDatabase database)
        {
            _database = database;
        }
        public ActionResult Index()
        {

            var ordersCollection = _database.GetCollection<Order>("orders");
            var orders = ordersCollection.Find(FilterDefinition<Order>.Empty).ToList();

            var filteredOrders = orders.Where(order => order.status == "P").ToList();

            var monthlySalesList = orders
            .GroupBy(order => new { order.timestamp.Year, order.timestamp.Month })
            .Select(group => new {
                Year = group.Key.Year,
                Month = group.Key.Month,
                TotalSales = group.Sum(order => order.total_amount)
            })
            .ToList();

            int year = DateTime.Now.Year;
            int mont = DateTime.Now.Month;
            var monthlySalesArray = new decimal[12];

            for (int month = 1; month <= 12; month++)
            {
                var salesForMonth = monthlySalesList.FirstOrDefault(x => x.Year == year && x.Month == month);
                var totalSales = salesForMonth != null ? salesForMonth.TotalSales : 0;
                monthlySalesArray[month - 1] = totalSales;
            }


            ViewBag.monthlyTotals = monthlySalesArray[mont - 1];
            ViewBag.annualTotal = monthlySalesArray.Sum();
            ViewBag.filteredOrders = filteredOrders.Count();
            ViewBag.MonthlySalesArray = JsonConvert.SerializeObject(monthlySalesArray);
            return View();
        }
       
    }
}