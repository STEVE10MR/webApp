using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webApp.Models;


namespace webApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMongoDatabase _database;

        public OrderController() : this(DependencyResolver.Current.GetService<IMongoDatabase>()) { }

        public OrderController(IMongoDatabase database)
        {
            _database = database;
        }
        public ActionResult Index()
        {
            List<List<object>> lists = new List<List<object>>();

            var ordersCollection = _database.GetCollection<Order>("orders");

            var orders = ordersCollection.Find(FilterDefinition<Order>.Empty).ToList();

            
      
            return View(orders);
        }
        public ActionResult Detail(string order_id)
        {
            if (!string.IsNullOrEmpty(order_id))
            {
                var ordersCollection = _database.GetCollection<Order>("orders");
                var customersCollection = _database.GetCollection<Customer>("customers");
                var paymentCollection = _database.GetCollection<Payment>("payments");

                var order = ordersCollection.Find(c => c.order_id == order_id).FirstOrDefault();
                if (order != null)
                {

                    var customer = customersCollection.Find(c => c.customer_id == order.customer_id).FirstOrDefault();
                    var payment = paymentCollection.Find(c => c.payment_id == order.payment_id).FirstOrDefault();
                    ViewBag.dateFormat = order.timestamp.ToString("MM/dd/yyyy");
                    
                    if (customer != null)
                    {
                        order.customer = new Customer() { phone_number = customer.phone_number };
                    }
                    if (payment != null)
                    {
                        order.payments = new Payment() { payment_method = payment.payment_method };
                    }
                }
                return View(order);
            }
            return RedirectToAction("Index", "Order");
        }

    }
}