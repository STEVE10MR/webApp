using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webApp.Models;

namespace webApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMongoDatabase _database;

        public PaymentController() : this(DependencyResolver.Current.GetService<IMongoDatabase>()) { }

        public PaymentController(IMongoDatabase database)
        {
            _database = database;
        }
        public ActionResult Index()
        {
            var paymentCollection = _database.GetCollection<Payment>("payments");
            return View(paymentCollection.Find(FilterDefinition<Payment>.Empty).ToList());
        }
        
        public ActionResult ChangeStatus(string payment_id)
        {
            var paymentCollection = _database.GetCollection<Payment>("payments");

            var filter = Builders<Payment>.Filter.Eq("payment_id", payment_id);

            var payment = paymentCollection.Find(filter).FirstOrDefault();

            if (payment != null)
            {
                var currentStatus = payment.status;

                if (currentStatus == "I")
                {
                    var update = Builders<Payment>.Update.Set("status", "A");
                    var result = paymentCollection.UpdateOne(filter, update);
                }
                else
                {
                    var update = Builders<Payment>.Update.Set("status", "I");
                    var result = paymentCollection.UpdateOne(filter, update);
                }
                TempData["Message"] = "Se cambio el estado correctamente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error al cambiar el estado."+ payment_id;
                return RedirectToAction("Index");
            }
        }
       
        public ActionResult Add(string paymentMethod)
        {
            var paymentCollection = _database.GetCollection<Payment>("payments");
            var paymentId = Guid.NewGuid().ToString();

            var newPayment = new Payment
            {
                payment_id = paymentId,
                payment_method = paymentMethod,
                status = "A",
                timestamp = DateTime.Now,
                __v = 0
            };

            try
            {
                paymentCollection.InsertOne(newPayment);
                TempData["Message"] = "Registro exitoso";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Message"] = "Hubo un error";
                return RedirectToAction("Index");
            }

        }
        public ActionResult RegisterEdit()
        {
            return View();
        }

    }
}