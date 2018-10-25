using Lazyfitness.Models;
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

        public ActionResult Error()
        {
            ViewBag.IsRefresh = true;
            ViewBag.OtherHtml = Url.Action("Index", "Home", new { area = "" });
            ViewBag.ErrorMsg = "没错这就是错误页面";
            return View("~/Views/Shared/err.cshtml");
        }

        public ActionResult Index()
        {
            ViewBag.Title = "小懒人健身网站";
            ViewBag.IsLogin = true;
            ViewBag.headPicadr = Url.Content("~/Resource/picture/DefaultHeadPic.jpg");
            ViewBag.Scrollpic = new string[]
            {
                Url.Content("~/Resource/picture/pic1.jpg"),
                Url.Content("~/Resource/picture/pic2.jpg"),
                Url.Content("~/Resource/picture/pic3.png"),
            };
            ViewBag.NoticeName = new string[]
            {
                "震惊！XX居然。。。",
                "吓人！XX居然。。。",
                "美女荷官在线发牌",
                "来贪玩蓝月，你从未见过的船新版本",
                "女朋友失误，竟炼出史前尸鲲",
                "想不出要说啥了但这里估计也看不到（",
                "但现在 你看到了",
                "因为 增加到第10个了",
                "这里是第九个",
                "终于编完了嘤"
            };
            ViewBag.NoticeUrl = new string[]
            {
                "#",
                "#",
                "#",
                "#",
                "#",
                "#",
                "#",
                "#",
                "#",
                "#",
            };
            ViewBag.NoticeTitle = new string[]
            {
                "震惊！XX居然。。。",
                "吓人！QQ居然。。。",
                "美女荷官在线发牌",
                "来贪玩蓝月，你从未见过的船新版本",
                "女朋友失误，竟炼出史前尸鲲",
                "想不出要说啥了但这里估计也看不到（",
            };

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

        public JsonResult AjaxTest()
        {
            userSecurity obj = new userSecurity
            {
                userId = 10086,
                loginId = "CY",
                userPwd = "123"
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}