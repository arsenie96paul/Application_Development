using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalendarManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // For Event page
        public ActionResult Event()
        {
            ViewBag.Message = "Your event page.";

            return View();
        }

        // For event by day page
        public ActionResult EventsByDay()
        {
            ViewBag.Message = "Your event by day page.";

            return View();
        }

        //For Login Page
        public ActionResult Login()
        {
            return View();
        }

        //For register Page
        public ActionResult Register()
        {

            return View();
        }

    }
}