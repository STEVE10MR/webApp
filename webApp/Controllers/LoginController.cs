using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webApp.Models;

namespace webApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly Usuario objUsuario = new Usuario();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Validar(string Usuario, string Password)
        {
            var rm = objUsuario.ValidarLogin(Usuario, Password);
            if (rm.response)
            {
                return RedirectToAction("Index", "Payment");
            }
            return Json(rm);
        }
    }
}