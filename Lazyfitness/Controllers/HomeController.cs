﻿using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.MyClass;
using System.Data.Entity.Infrastructure;
using Lazyfitness.Filter;

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
            //获取首页轮播图数据
            serverShowInfo[] ScrollInfo = Tools.GetIndexScroll();
            if (ScrollInfo.Length >= 3)
            {
                ViewBag.Scrollpic = new string[]
                {
                ScrollInfo[0].pictureAdr,
                ScrollInfo[1].pictureAdr,
                ScrollInfo[2].pictureAdr,
                };
                ViewBag.ScrollTitle = new string[]
                {
                ScrollInfo[0].title,
                ScrollInfo[1].title,
                ScrollInfo[2].title,
                };
                ViewBag.ScrollUrl = new string[]
                {
                ScrollInfo[0].url,
                ScrollInfo[1].url,
                ScrollInfo[2].url,
                };
            }
            
            //获取首页公告数据
            serverShowInfo[] NoticeInfo = Tools.GetIndexNotice();
            string[] noticName = new string[NoticeInfo.Length];
            for(int count = 0; count < NoticeInfo.Length; count++)
                noticName[count] = NoticeInfo[count].title;
            string[] noticUrl = new string[NoticeInfo.Length];
            for (int count = 0; count < NoticeInfo.Length; count++)
                noticUrl[count] = NoticeInfo[count].url;
            ViewBag.NoticeName = noticName;
            ViewBag.NoticeUrl = noticUrl;
            //获取首页文章区信息
            //热门
            resourceInfo[] ArticleHotInfo = Tools.GetArticleDetailInfo(2, true, 9);
            ViewBag.ArticleHotInfo = ArticleHotInfo;
            //最新
            resourceInfo[] ArticleLastestInfo = Tools.GetArticleDetailInfo(1, true, 9);
            ViewBag.ArticleLastestInfo = ArticleLastestInfo;

            //获取首页问答区信息
            //热门
            quesAnswInfo[] QuestionHotInfo = Tools.GetQuestionDetailInfo(2, true, 9);
            ViewBag.QuestionHotInfo = QuestionHotInfo;
            //最新
            quesAnswInfo[] QuestionLastestInfo = Tools.GetQuestionDetailInfo(1, true, 9);
            ViewBag.QuestionLastestInfo = QuestionLastestInfo;

            //获取首页论坛信息
            //热门
            postInfo[] forumHotInfo = Tools.GetforumDetailInfo(2, true, 9);
            ViewBag.forumHotInfo = forumHotInfo;
            //最新
            postInfo[] forumLastestInfo = Tools.GetforumDetailInfo(1, true, 9);
            ViewBag.forumLastestInfo = forumLastestInfo;

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
            //获取首页轮播图数据
            serverShowInfo[] ScrollInfo = Tools.GetArticleScroll();
            if (ScrollInfo.Length >= 3)
            {
                ViewBag.Scrollpic = new string[]
                {
                ScrollInfo[0].pictureAdr,
                ScrollInfo[1].pictureAdr,
                ScrollInfo[2].pictureAdr,
                };
                ViewBag.ScrollTitle = new string[]
                {
                ScrollInfo[0].title,
                ScrollInfo[1].title,
                ScrollInfo[2].title,
                };
                ViewBag.ScrollUrl = new string[]
                {
                ScrollInfo[0].url,
                ScrollInfo[1].url,
                ScrollInfo[2].url,
                };
            }
            
            //获取大区信息
            resourceArea[] areasInfo = Tools.GetArticleAreaInfo();
            string[] areasName = new string[areasInfo.Length];
            for(int count = 0; count < areasInfo.Length; count++)
            {
                areasName[count] = areasInfo[count].areaName;
            }
            int[] areasId = new int[areasInfo.Length];
            for(int count = 0; count < areasInfo.Length; count++)
            {
                areasId[count] = areasInfo[count].areaId;
            }
            ViewBag.AreasName = areasName;
            string[] url = new string[areasInfo.Length];
            for (int count = 0; count < areasInfo.Length; count++) 
            {
                url[count] = Url.Action("ArticlePart", "Home", new { partId = areasId[count] });
            }
            ViewBag.AreasUrl = url;
            #endregion
            return View();
        }
        //问答
        public ActionResult Question()
        {
            //获取大区信息
            quesArea[] quesInfo = Tools.GetQuestionAreaInfo();
            string[] quesName = new string[quesInfo.Length];
            for (int count = 0; count < quesInfo.Length; count++)
            {
                quesName[count] = quesInfo[count].areaName;
            }
            int[] quesId = new int[quesInfo.Length];
            for (int count = 0; count < quesInfo.Length; count++)
            {
                quesId[count] = quesInfo[count].areaId;
            }          
            ViewBag.PartList = quesName;
            string[] url = new string[quesInfo.Length];
            for(int count = 0; count < quesInfo.Length; count++)
            {
                url[count] = Url.Action("QuestionPart", "Home", new { partId = quesId[count] });
            }
            ViewBag.PartUrl = url;
            return View();
        }
        //论坛
        public ActionResult forum()
        {
            //获取大区信息
            postArea[] postInfo = Tools.GetforumAreaInfo();
            string[] postName = new string[postInfo.Length];
            for (int count = 0; count < postInfo.Length; count++)
            {
                postName[count] = postInfo[count].areaName;
            }
            int[] postId = new int[postInfo.Length];
            for (int count = 0; count < postInfo.Length; count++)
            {
                postId[count] = postInfo[count].areaId;
            }
            ViewBag.PartName = postName;
            string[] urls = new string[postInfo.Length];
            for (int i = 0; i < postInfo.Length; i++)
            {
                urls[i] = Url.Action("forumPart", "Home", new { partId = postId[i] });
            }
            ViewBag.PartUrl = urls;
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
            resourceArea[] areasInfo = Tools.GetArticleAreaInfo();
            string[] areasName = new string[areasInfo.Length];
            for (int count = 0; count < areasInfo.Length; count++)
            {
                areasName[count] = areasInfo[count].areaName;
            }
            int[] areasId = new int[areasInfo.Length];
            for (int count = 0; count < areasInfo.Length; count++)
            {
                areasId[count] = areasInfo[count].areaId;
            }
            ViewBag.AreasName = areasName;           
            string[] AreasUrlList = new string[areasInfo.Length];
            for (int i = 0; i < areasInfo.Length; i++)
            {
                AreasUrlList[i] = Url.Action("ArticlePart", "Home", new { partId = areasId[i] });
            }
            ViewBag.AreasUrl = AreasUrlList;

            //下面4个ViewBag是用于传入帖子信息的

            resourceInfo[] articleInfo = Tools.GetArticle(partId, pageNum);
            ViewBag.ArticleInfo = articleInfo;

            ViewBag.ItemsName = Tools.GetArticlePartName(partId,pageNum);
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
            quesArea[] quesInfo = Tools.GetQuestionAreaInfo();
            string[] quesName = new string[quesInfo.Length];
            for (int count = 0; count < quesInfo.Length; count++)
            {
                quesName[count] = quesInfo[count].areaName;
            }
            int[] quesId = new int[quesInfo.Length];
            for (int count = 0; count < quesInfo.Length; count++)
            {
                quesId[count] = quesInfo[count].areaId;
            }
            ViewBag.PartList = quesName;
            string[] url = new string[quesInfo.Length];
            for (int count = 0; count < quesInfo.Length; count++)
            {
                url[count] = Url.Action("QuestionPart", "Home", new { partId = quesId[count] });
            }
            ViewBag.PartUrl = url;

            //传入帖子信息
            quesAnswInfo[] ItemInfo = Tools.GetQuestion(partId, pageNum);
            ViewBag.ItemInfo = ItemInfo;

            ViewBag.PartId = partId;
            ViewBag.PageNum = pageNum;
            ViewBag.ItemsName = Tools.GetQuestionPartName(partId, pageNum);
            ViewBag.ItemsHeadAdr = Tools.GetQuestionPartHeadAdr(partId, pageNum);
            ViewBag.ItemsIntroduction = Tools.GetQuestionPartIntroduction(partId, pageNum);
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
            postArea[] postInfo = Tools.GetforumAreaInfo();
            string[] postName = new string[postInfo.Length];
            for (int count = 0; count < postInfo.Length; count++)
            {
                postName[count] = postInfo[count].areaName;
            }
            int[] postId = new int[postInfo.Length];
            for (int count = 0; count < postInfo.Length; count++)
            {
                postId[count] = postInfo[count].areaId;
            }
            ViewBag.PartName = postName;
            string[] urls = new string[postInfo.Length];
            for (int i = 0; i < postInfo.Length; i++)
            {
                Url.Action("forumPart", "Home", new { partId = postId[i] });
            }
            ViewBag.PartUrl = urls;
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

            //输入帖子信息
            postInfo[] ItemInfo = Tools.GetForum(partId, pageNum);
            ViewBag.ItemInfo = ItemInfo;

            ViewBag.PartId = partId;
            ViewBag.PageNum = pageNum;
            ViewBag.ItemsName = Tools.GetforumPartName(partId, pageNum);
            ViewBag.ItemsHeadAdr = Tools.GetforumPartHeadAdr(partId, pageNum);
            ViewBag.ItemsIntroduction = Tools.GetforumPartIntroduction(partId, pageNum);
            return View();
        }

        /// <summary>
        /// 文章资源区帖子
        /// </summary>
        /// <param name="num">帖子序号</param>
        /// <returns></returns>
        public ActionResult ArticleDetail(int num = 1)
        {
            resourceInfo info = Tools.GetArticleInfo(num);
            ViewBag.ArticleInfo = info;
            userInfo userInfo = Tools.GetUserInfo((int)info.userId);
            ViewBag.UserInfo = userInfo;
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
            //此处应先验证，然后从cookie里获取userId
            ViewBag.UserId = 111;
            return View();
        }

        /// <summary>
        /// 问答详细页
        /// </summary>
        /// <param name="num">问答贴id</param>
        /// <returns></returns>
        public ActionResult QuestionDetail(int num = 1)
        {
            ViewBag.QuestionId = num;
            ViewBag.IsSolved = false;
            quesAnswInfo info = Tools.GetQuestionInfo(num);
            ViewBag.QuesInfo = info;
            ViewBag.QuesUserInfo = Tools.GetUserInfo((int)info.userId);
            quesAnswReply[] replys = Tools.GetQuestionReply(num);
            ViewBag.QuestionReplys = replys;
            userInfo[] replyerInfo = new userInfo[replys.Length];
            for (int count = 0; count < replys.Length; count++)
            {
                replyerInfo[count] = Tools.GetUserInfo((int)replys[count].userId);
            }
            ViewBag.ReplyerInfo = replyerInfo;
            return View();
        }

        public ActionResult QuestionEditor(int partId = 0)
        {
            ViewBag.PartId = partId;
            //此处应先验证，然后从cookie里获取userId
            ViewBag.UserId = 111;
            return View();
        }

        /// <summary>
        /// 论坛详情页
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public ActionResult forumDetail(int num = 1)
        {
            ViewBag.ForumId = num;
            ViewBag.ForumInfo = Tools.GetforumInfo(num);
            postReply[] reply = Tools.GetforumReply(num);
            ViewBag.ForumReply = reply;
            ViewBag.ForumReplyNum = reply.Length;
            ViewBag.ForumUserInfo = Tools.GetUserInfo(ViewBag.ForumInfo.userId);
            string[] name = new string[reply.Length];
            for (int count = 0; count < reply.Length; count++)
            {
                name[count] = Tools.GetUserName((int)reply[count].userId);
            }
            ViewBag.ForumReplyName = name;
            return View();
        }
        
        public ActionResult forumEditor(int partId = 0)
        {
            ViewBag.PartId = partId;
            //此处应先验证，然后从cookie里获取userId
            ViewBag.UserId = 111;
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


        #region 获取个人信息
        public ActionResult PersonalData()
        {
            if (Request.Cookies["loginId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["loginId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                var loginId = cookieText.ToString();

                HttpCookie cookieNameUserId = Request.Cookies["userId"];
                var cookieTextUserId = Server.HtmlEncode(cookieNameUserId.Value);
                var userId = Convert.ToInt32(cookieTextUserId.ToString());
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == userId) as DbQuery<userInfo>;
                    userInfo _userInfo = dbsearch.FirstOrDefault();
                    if (_userInfo != null)
                    {
                        ViewBag.userId = _userInfo.userId;
                        ViewBag.userName = _userInfo.userName;
                        ViewBag.userAge = _userInfo.userAge;
                        ViewBag.userSex = _userInfo.userSex;
                        ViewBag.userEmail = _userInfo.userEmail;
                        ViewBag.userStatus = _userInfo.userStatus;
                        ViewBag.userAccount = _userInfo.userAccount;
                        ViewBag.userIntroduce = _userInfo.userIntroduce;
                        ViewBag.userHeaderPic = _userInfo.userHeaderPic;
                    }
                }
            }
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
                    _userInfo.userEmail = info.userEmail;
                    _userInfo.userStatus = info.userStatus;
                    _userInfo.userIntroduce = info.userIntroduce;
                    db.SaveChanges();
                }
            }
            return Content("<script >window.window.history.back(-1)</script >", "text/html");
        }
        #endregion

        #region 采纳回答
        [HttpPost]
        public string adoptAnswer(int quesAnswId, int userId)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    var dbQuesAnswReply = db.quesAnswReply.Where(u => u.quesAnswId == quesAnswId && u.userId == userId);
                    var obQuesAnswReply = dbQuesAnswReply.FirstOrDefault();
                    //将此对象的isAgree 改为1 表示采纳
                    obQuesAnswReply.isAgree = 1;

                    //修改此问答帖子的状态
                    var dbQuesAnsw = db.quesAnswInfo.Where(u => u.quesAnswId == quesAnswId);
                    var obQuesAnsw = dbQuesAnsw.FirstOrDefault();
                    int getPayId = obQuesAnsw.userId.Value;
                    var dbInfo = db.userInfo.Where(u => u.userId == getPayId);
                    var obInfo = dbInfo.FirstOrDefault();
                    obInfo.userAccount += obQuesAnsw.amount; 
                    obQuesAnsw.quesAnswStatus = 1;

                    db.SaveChanges();
                    return "true";
                }
            }
            catch
            {
                return "false";
            }
        }
        #endregion
    }
}