using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Hello()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "小懒人健身网站";
            return View();
        }

        public ActionResult About()
        {

            ViewBag.Message = Request.Form["shuru"];

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}