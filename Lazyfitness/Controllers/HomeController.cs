using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.MyClass;
using System.Data.Entity.Infrastructure;

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
                "首页",
                "食物",
                "器材",
                "技巧",
            };
            ViewBag.AreasUrl = new string[]
            {
                Url.Action("Article","Home"),
                Url.Action("ArticlePart","Home",new{partId=1}),
                Url.Action("ArticlePart","Home",new{partId=2}),
                Url.Action("ArticlePart","Home",new{partId=3}),
                Url.Action("ArticlePart","Home",new{partId=4}),
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
                Url.Action("Question","Home"),
                Url.Action("QuestionPart","Home"),
                Url.Action("QuestionPart","Home",new{ partId=2}),
                Url.Action("QuestionPart","Home",new{ partId=3}),
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
            };
            ViewBag.PartUrl = new string[]
            {
                Url.Action("forumPart","Home",new {partId=1}),
                Url.Action("forumPart","Home",new {partId=2}),
                Url.Action("forumPart","Home",new {partId=3}),
                Url.Action("forumPart","Home",new {partId=4}),
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
        /// <summary>
        /// 文章资源区分页
        /// </summary>
        /// <param name="partId">分区ID</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public ActionResult ArticlePart(int partId = 1, int pageNum = 1)
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
                "首页",
                "食物",
                "器材",
                "技巧",
            };
            ViewBag.AreasUrl = new string[]
            {
                Url.Action("Article","Home"),
                Url.Action("ArticlePart","Home",new{partId=1}),
                Url.Action("ArticlePart","Home",new{partId=2}),
                Url.Action("ArticlePart","Home",new{partId=3}),
                Url.Action("ArticlePart","Home",new{partId=4}),
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
            //此处应有个函数判断是否是管理员账户
            //
            ViewBag.IsAdmin = true;


            return View();
        }

        /// <summary>
        /// 问答区分页
        /// </summary>
        /// <param name="partId">分区ID</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public ActionResult QuestionPart(int partId = 1, int pageNum = 1)
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
            ViewBag.PartList = new string[]
            {
                "首页",
                "已解决",
                "未解决",
                "我提出的问题",
            };
            ViewBag.PartUrl = new string[]
            {
                Url.Action("Question","Home"),
                Url.Action("QuestionPart","Home"),
                Url.Action("QuestionPart","Home",new{ partId=2}),
                Url.Action("QuestionPart","Home",new{ partId=3}),
            };
            ViewBag.PartId = partId;
            ViewBag.PageNum = pageNum;
            ViewBag.ItemsName = Tools.GetQuestionPartName(partId, pageNum);
            ViewBag.ItemsTitle = Tools.GetQuestionPartTitle(partId, pageNum);
            ViewBag.ItemsUrl = Tools.GetQuestionPartUrl(partId, pageNum);
            ViewBag.ItemsHeadAdr = Tools.GetQuestionPartHeadAdr(partId, pageNum);
            ViewBag.ItemsIntroduction = Tools.GetQuestionPartIntroduction(partId, pageNum);
            ViewBag.ItemsMoney = Tools.GetQuestionPartMoney(partId, pageNum);
            return View();
        }

        /// <summary>
        /// 问答区分页
        /// </summary>
        /// <param name="partId">分区ID</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public ActionResult forumPart(int partId = 1, int pageNum = 1)
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
            ViewBag.PartName = new string[]
            {
                "分区1",
                "分区2",
                "分区3",
                "分区4",
            };
            ViewBag.PartUrl = new string[]
            {
                Url.Action("forumPart","Home",new {partId=1}),
                Url.Action("forumPart","Home",new {partId=2}),
                Url.Action("forumPart","Home",new {partId=3}),
                Url.Action("forumPart","Home",new {partId=4}),
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
            ViewBag.PartId = partId;
            ViewBag.PageNum = pageNum;
            ViewBag.ItemsName = Tools.GetforumPartName(partId, pageNum);
            ViewBag.ItemsTitle = Tools.GetforumPartTitle(partId, pageNum);
            ViewBag.ItemsUrl = Tools.GetforumPartUrl(partId, pageNum);
            ViewBag.ItemsHeadAdr = Tools.GetforumPartHeadAdr(partId, pageNum);
            ViewBag.ItemsIntroduction = Tools.GetforumPartIntroduction(partId, pageNum);
            return View();
        }

        /// <summary>
        /// 文章资源区帖子
        /// </summary>
        /// <param name="num">帖子序号</param>
        /// <returns></returns>
        public ActionResult ArticleDetail(int num = 0)
        {
            return View();
        }

        /// <summary>
        /// 管理员新增帖子
        /// </summary>
        /// <param name="partId">分区ID</param>
        /// <returns></returns>
        public ActionResult ArticleEditor(int partId)
        {
            //此处应有一个判断有无权限的函数
            //
            ViewBag.PartId = partId;
            return View();
        }

        public ActionResult QuestionDetail(int num = 0)
        {
            ViewBag.IsSolved = false;
            ViewBag.Money = 100;
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

        /// <summary>
        /// 上面是eason得 
        /// </summary>
        /// <returns></returns>
        
        public ActionResult PersonalData()
        {
            //if (Request.Cookies["userId"] != null)
            //{
                //获取Cookies的值
                //HttpCookie cookieName = Request.Cookies["userId"];
                //var cookieText = Server.HtmlEncode(cookieName.Value);
                //var userId = cookieText.ToString();
                //ViewBag.userId = userId;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == Convert.ToInt32(userId)) as DbQuery<userInfo>;
                    DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == 1) as DbQuery<userInfo>;
                    userInfo _userInfo = dbsearch.FirstOrDefault();
                    if (_userInfo != null)
                    {
                        ViewBag.userId = _userInfo.userId;
                        ViewBag.userName = _userInfo.userName;
                        ViewBag.userAge = _userInfo.userAge;
                        ViewBag.userSex = _userInfo.userSex;
                        ViewBag.userTel = _userInfo.userTel;
                        ViewBag.userStatus = _userInfo.userStatus;
                        ViewBag.userAccount = _userInfo.userAccount;
                    }
                }
            //}
            return View();
        }
        [HttpPost]
        public ActionResult PersonalData(userInfo info)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == info.userId) as DbQuery<userInfo>;
                userInfo _userInfo = dbsearch.FirstOrDefault();
                if (_userInfo != null)
                {
                    _userInfo.userName = info.userName;
                    _userInfo.userAge = info.userAge;
                    _userInfo.userSex = info.userSex;
                    db.SaveChanges();
                }
            }
            return Content("<script >window.window.history.back(-1)</script >", "text/html");
        }
    }
}