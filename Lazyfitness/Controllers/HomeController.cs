using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.MyClass;

namespace Lazyfitness.Controllers
{
    public class HomeController : Controller
    {
        //欢迎页
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
        //首页
        public ActionResult Index()
        {
            #region 数据
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
            #endregion
            return View();
        }
        //文章资源
        public ActionResult Article()
        {

            #region 数据
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
            ViewBag.AreasName = new string[]
            {
                "食物",
                "器材",
                "技巧",
            };
            ViewBag.AreasUrl = new string[]
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
            #endregion
            return View();
        }
        //问答
        public ActionResult Question()
        {
            ViewBag.PartList = new string[]
            {
                "首页",
                "已解决",
                "未解决",
                "我提出的问题",
            };
            ViewBag.PartUrl = new string[]
            {
                "#",
                "#",
                "#",
                "#",
            };
            return View();
        }
        //论坛
        public ActionResult forum()
        {
            ViewBag.PartName = new string[]
            {
                "分区1",
                "分区2",
                "分区3",
                "分区4",
                "分区5",
                "分区6",
                "分区7",
                "分区8",
                "分区9",
                "分区10",
            };
            ViewBag.PartUrl = new string[]
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
                "#",
                "#",
            };
            ViewBag.PartPicUrl = new string[]
            {
                Url.Content("~/Resource/picture/list-style-dot-red.png"),
                Url.Content("~/Resource/picture/list-style-dot-orange.png"),
                Url.Content("~/Resource/picture/list-style-dot-yellow.png"),
                Url.Content("~/Resource/picture/list-style-dot-green.png"),
                Url.Content("~/Resource/picture/list-style-dot-cyan.png"),
                Url.Content("~/Resource/picture/list-style-dot-blue.png"),
                Url.Content("~/Resource/picture/list-style-dot-purple.png"),
            };
            ViewBag.PartSelect = -1;
            return View();
        }
        //文章资源区内容
        public JsonResult ArticleItemContent()
        {
            ArticleItem[] items = new ArticleItem[5];
            for (int count = 0; count < 5; count++)
            {
                items[count] = new ArticleItem();
                items[count].Name = count.ToString() + "名字";
                items[count].Title = count.ToString() + "标题";
                items[count].HeadAdr = "/Resource/picture/DefaultHeadPic.jpg";
                items[count].Introduction = count.ToString() + "简介";
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        //问答区内容
        public JsonResult QuesItemContent()
        {
            ArticleItem[] items = new ArticleItem[5];
            for (int count = 0; count < 5; count++)
            {
                items[count] = new ArticleItem();
                items[count].Name = count.ToString() + "名字";
                items[count].Title = count.ToString() + "标题";
                items[count].HeadAdr = "/Resource/picture/DefaultHeadPic.jpg";
                items[count].Introduction = count.ToString() + "简介";
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        //论坛区内容
        public JsonResult forumItemContent()
        {
            ArticleItem[] items = new ArticleItem[5];
            for (int count = 0; count < 5; count++)
            {
                items[count] = new ArticleItem();
                items[count].Name = count.ToString() + "名字";
                items[count].Title = count.ToString() + "标题";
                items[count].HeadAdr = "/Resource/picture/DefaultHeadPic.jpg";
                items[count].Introduction = count.ToString() + "简介";
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        //文章资源区分页
        public ActionResult ArticlePart(int partId = 0, int pageNum = 1)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }
            if (Request.QueryString["GoPageNum"] != null)
            {
                int num;
                if (Int32.TryParse(Request.QueryString["GoPageNum"] as string, out num) == true) 
                {
                    pageNum = num;
                }
                else
                {
                    pageNum = 1;
                }

            }
            ViewBag.AreasName = new string[]
{
                "食物",
                "器材",
                "技巧",
};
            ViewBag.AreasUrl = new string[]
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
            //下面4个ViewBag是用于传入帖子信息的
            ViewBag.ItemsName = Tools.GetArticlePartName(partId,pageNum);
            ViewBag.ItemsTitle = Tools.GetArticlePartTitle(partId, pageNum);
            ViewBag.ItemsUrl = Tools.GetArticlePartUrl(partId, pageNum);
            ViewBag.ItemsHeadAdr = Tools.GetArticlePartHeadAdr(partId, pageNum);
            ViewBag.ItemsIntroduction = Tools.GetArticlePartIntroduction(partId, pageNum);
            //这是传入PartId的;
            ViewBag.PartId = partId;
            ViewBag.PageNum = pageNum;
            ViewBag.PartName = Tools.GetArticleName(partId);


            return View();
        }

        #region Test
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
        #endregion
    }
}