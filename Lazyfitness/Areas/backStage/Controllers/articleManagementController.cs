using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class articleManagementController : Controller
    {
        // GET: backStage/articleManagement
        public ActionResult Index()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                ViewBag.Model = db.resourceArea.ToList();
                return View(ViewBag.Model);
            }
        }
    }
}