using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class pageViewController : Controller
    {
        // GET: backStage/pageView
        public ActionResult Index()
        {
            try
            {
                ///浏览量查询
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //查询资源文章人气
                    var resource = db.resourceInfo.OrderByDescending(u => u.pageView).Take(10).ToList();
                    if(resource == null)
                    {
                        ViewBag.resourceconfirm = "false";
                    }
                    else
                    {
                        ViewBag.resourceconfirm = "true";
                    }
                    ViewBag.resource = resource;
                    //查询论坛帖子人气
                    var post = db.postInfo.OrderByDescending(u => u.pageView).Take(10).ToList();
                    if (post == null)
                    {
                        ViewBag.postconfirm = "false";
                    }
                    else
                    {
                        ViewBag.postconfirm = "true";
                    }
                    ViewBag.post = post;

                    //查询论坛帖子人气
                    var quesAnsw = db.quesAnswInfo.OrderByDescending(u => u.pageView).Take(10).ToList();
                    if (quesAnsw == null)
                    {
                        ViewBag.quesAnswconfirm = "false";
                    }
                    else
                    {
                        ViewBag.quesAnswconfirm = "true";
                    }
                    ViewBag.quesAnsw = quesAnsw;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}