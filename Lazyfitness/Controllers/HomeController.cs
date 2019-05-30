using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.MyClass;
using System.Data.Entity.Infrastructure;
using Lazyfitness.Filter;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Lazyfitness.json;
using Lazyfitness.json.model;
using Lazyfitness.Areas.toolsHelpers;

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
        [FirstRedirectFilter]
        public ActionResult Index()
        {
            #region 数据
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
        [FirstRedirectFilter]
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
        [FirstRedirectFilter]
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
            //获取个人信息
            HttpCookie loginIdCookie = Request.Cookies.Get("loginId");
            HttpCookie userIdCookie = Request.Cookies.Get("userId");
            HttpCookie certificationCookie = Request.Cookies.Get("certification");
            //如果登录了
            if (certificateTools.IsUserCookieCorrect(userIdCookie,loginIdCookie,certificationCookie) == true)
            {
                ViewBag.IsLogin = true;
                ViewBag.UserInfo = Tools.GetUserInfo(Int32.Parse(userIdCookie.Value));

            }
            else
            {
                ViewBag.IsLogin = false;
            }


            return View();
        }
        //论坛
        [FirstRedirectFilter]
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
        public JsonResult ArticleItemContent(int pageNum)
        {
            if (pageNum<1)
            {
                pageNum = 1;
            }
            resourceInfo[] info = Tools.GetArticleAll(pageNum);
            ArticleItem[] items = new ArticleItem[info.Length];
            for (int count = 0; count < info.Length; count++)
            {
                userInfo user = Tools.GetUserInfo((int)info[count].userId);
                items[count] = new ArticleItem();
                items[count].Name = user.userName;
                items[count].Title = info[count].resourceName;
                items[count].HeadAdr = user.userHeaderPic;
                items[count].Url = Url.Action("ArticleDetail", "Home", new { num = info[count].resourceId });
                int length = 20;
                string intro = Tools.GetNoHTMLString(info[count].resourceContent);
                if (intro.Length < 20)
                {
                    length = intro.Length;
                }
                items[count].Introduction = intro.Substring(0,length);
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        //问答区内容
        public JsonResult QuesItemContent(int pageNum)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }
            quesAnswInfo[] info = Tools.GetQuestionAll(pageNum);
            ArticleItem[] items = new ArticleItem[info.Length];
            for (int count = 0; count < info.Length; count++)
            {
                userInfo user = Tools.GetUserInfo((int)info[count].userId);
                items[count] = new ArticleItem();
                items[count].Name = user.userName;
                items[count].Title = info[count].quesAnswTitle;
                items[count].HeadAdr = user.userHeaderPic;
                items[count].Url = Url.Action("QuestionDetail", "Home", new { num = info[count].quesAnswId });
                int length = 20;
                string intro = Tools.GetNoHTMLString(info[count].quesAnswContent);
                if (intro.Length < 20)
                {
                    length = intro.Length;
                }
                items[count].Introduction = intro.Substring(0, length);
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        //论坛区内容
        public JsonResult forumItemContent(int pageNum)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }
            postInfo[] info = Tools.GetForumAll(pageNum);
            ArticleItem[] items = new ArticleItem[info.Length];
            for (int count = 0; count < info.Length; count++)
            {
                userInfo user = Tools.GetUserInfo((int)info[count].userId);
                items[count] = new ArticleItem();
                items[count].Name = user.userName;
                items[count].Title = info[count].postTitle;
                items[count].HeadAdr = user.userHeaderPic;
                items[count].Url = Url.Action("forumDetail", "Home", new { num = info[count].postId });
                int length = 20;
                string intro = Tools.GetNoHTMLString(info[count].postContent);
                if (intro.Length < 20)
                {
                    length = intro.Length;
                }
                items[count].Introduction = intro.Substring(0, length);
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

            //获取个人信息
            HttpCookie loginIdCookie = Request.Cookies.Get("loginId");
            HttpCookie userIdCookie = Request.Cookies.Get("userId");
            HttpCookie certificationCookie = Request.Cookies.Get("certification");
            //如果登录了
            if (certificateTools.IsUserCookieCorrect(userIdCookie, loginIdCookie, certificationCookie) == true)
            {
                ViewBag.IsLogin = true;
                ViewBag.UserInfo = Tools.GetUserInfo(Int32.Parse(userIdCookie.Value));

            }
            else
            {
                ViewBag.IsLogin = false;
            }
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
                urls[i] = Url.Action("forumPart", "Home", new { partId = postId[i] });
            }
            ViewBag.PartUrl = urls;
            //获取高亮ID
            for(int count = 0; count < postId.Length; count++)
            {
                if(postId[count] == partId)
                {
                    ViewBag.PartSelect = count;
                }
            }
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
            //增加浏览量
            if(HttpContext.Application["PageViewHelper"] is PageViewHelper helper)
            {
                helper.PageViewAdd(0, num, 1);
            }
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
        [AdminFilter]
        public ActionResult ArticleEditor(int partId)
        {
            ViewBag.PartId = partId;
            return View();
        }

        /// <summary>
        /// 问答详细页
        /// </summary>
        /// <param name="num">问答贴id</param>
        /// <returns></returns>
        public ActionResult QuestionDetail(int num = 1)
        {
            //增加浏览量
            if (HttpContext.Application["PageViewHelper"] is PageViewHelper helper)
            {
                helper.PageViewAdd(1, num, 1);
            }
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
        [LoginStatusFilter]
        public ActionResult QuestionEditor(int partId = 0)
        {
            ViewBag.PartId = partId;
            return View();
        }

        /// <summary>
        /// 论坛详情页
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public ActionResult forumDetail(int num = 1)
        {
            //增加浏览量
            if (HttpContext.Application["PageViewHelper"] is PageViewHelper helper)
            {
                helper.PageViewAdd(2, num, 1);
            }
            ViewBag.ForumId = num;
            ViewBag.ForumInfo = Tools.GetforumInfo(num);
            postReply[] reply = Tools.GetforumReply(num);
            ViewBag.ForumReply = reply;
            ViewBag.ForumReplyNum = reply.Length;
            ViewBag.ForumUserInfo = Tools.GetUserInfo(ViewBag.ForumInfo.userId);
            string[] name = new string[reply.Length];
            string[] headPic = new string[reply.Length];
            for (int count = 0; count < reply.Length; count++)
            {
                userInfo info = Tools.GetUserInfo((int)reply[count].userId);
                name[count] = info.userName;
                headPic[count] = info.userHeaderPic;
            }
            ViewBag.ForumReplyName = name;
            ViewBag.ForumReplyHeadPic = headPic;
            return View();
        }
        [LoginStatusFilter]
        public ActionResult forumEditor(int partId = 0)
        {
            ViewBag.PartId = partId;
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

        public JsonResult test()
        {
            /*
            if (HttpContext.Application["PageViewHelper"] is PageViewHelper helper)
            {
                helper.SaveToDB();
            }*/
            return Json(true,JsonRequestBehavior.AllowGet);
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
        public ActionResult PersonalDataUpdate(userInfo info)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == info.userId) as DbQuery<userInfo>;
                userInfo _userInfo = dbsearch.FirstOrDefault();
                if (_userInfo != null)
                {
                    if(info.userHeaderPic == null)
                    {
                        _userInfo.userHeaderPic = "/Resource/picture/DefaultHeadPic1.png";
                    }
                    else
                    {
                        _userInfo.userHeaderPic = info.userHeaderPic;
                    }
                    if (info.userName == null)
                    {
                        _userInfo.userName = "默认用户名";
                    }
                    else
                    {
                        _userInfo.userName = info.userName;
                    }
                    if (info.userAge == null)
                    {
                        _userInfo.userAge = 0;
                    }
                    else
                    {
                        _userInfo.userAge = info.userAge;
                    }
                    _userInfo.userSex = info.userSex;
                    _userInfo.userEmail = info.userEmail;
                    if(info.userIntroduce == null)
                    {
                        _userInfo.userIntroduce = "这个人很懒，没有写简介";
                    }
                    else
                    {
                        _userInfo.userIntroduce = info.userIntroduce;
                    }
                    db.SaveChanges();
                }
            }
            Response.Redirect("/Home/PersonalData");
            return Content("sueccess");
        }
        #region 上传图片
        [HttpPost]
        public ActionResult SaveImage(int userId)
        {
            HttpPostedFileBase imageName = Request.Files["image"];// 从前台获取文件
            if (imageName == null)
            {
                return Content("(error)未获取到文件");
            }
            string file = imageName.FileName;
            string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
            Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
            if (string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
            {
                return Content("(error)文件格式支持(bmp)|(png)|(gif)|(jpg)|(jpeg)");
            }
            else
            {
                string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                string imageStr = "/upload/"; // 获取保存图片的项目文件夹
                string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
                string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                string saveFile = uploadPath + fileName;//文件路径
                imageName.SaveAs(saveFile);// 保存文件
                // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                string image = imageStr + fileName;// 设置数据库保存的路径

                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == userId) as DbQuery<userInfo>;
                    userInfo _userInfo = dbsearch.FirstOrDefault();
                    _userInfo.userHeaderPic = image;
                    db.SaveChanges();
                }
            }
            Response.Redirect("/Home/PersonalData");
            return Content("sueccess");
        }
        #endregion
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
                    //增加回答者的钱
                    //提问者的钱在提问时就扣除了
                    int getPayId = userId;
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

        #region 充值
        [LoginStatusFilter]
        public ActionResult Recharge()
        {
            int userId = Int32.Parse(ViewBag.UserId);
            userInfo user = Tools.GetUserInfo(userId);
            //因为是从Login过滤器里面过来的 所以肯定存在
            ViewBag.UserInfo = user;
            return View();
        }
        [LoginStatusFilter]
        [HttpPost]
        public ActionResult Recharge(recharge card)
        {
            //充值代码
            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                recharge searchCard = db.recharge.Where(c => c.rechargeId == card.rechargeId).FirstOrDefault();
                if(searchCard == null)
                {
                    Tools.AlertAndRedirect("卡号或卡密错误", Url.Action("Recharge", "Home"));
                    return View();
                }
                if (searchCard.rechargePwd != card.rechargePwd)
                {
                    Tools.AlertAndRedirect("卡号或卡密错误", Url.Action("Recharge", "Home"));
                    return View();
                }
                if (searchCard.isAvailable == 0)
                {
                    Tools.AlertAndRedirect("该卡已被使用", Url.Action("Recharge", "Home"));
                    return View();
                }
                int userId = Int32.Parse(ViewBag.UserId);
                userInfo user = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                user.userAccount += searchCard.amount;
                searchCard.isAvailable = 0;
                ViewBag.UserInfo = user;
                db.SaveChanges();
            }
            Tools.AlertAndRedirect("充值成功", Url.Action("Recharge", "Home"));
            return View();
        }
        #endregion

        #region 初次使用设置
        public JsonResult NotFirst()
        {
            string isFirst = WebConfigHelper.GetAppSetting("FirstOpen");
            if (isFirst == "true")
                WebConfigHelper.SetAppSetting("FirstOpen", "false");
            else
                WebConfigHelper.SetAppSetting("FirstOpen", "true");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [FirstFilter]
        public ActionResult Welcome()
        {
            return View();
        }
        [HttpPost]
        [FirstFilter]
        public ActionResult Welcome(string jsonData)
        {
            SubmitData data = JsonHelper.JsonDeserialize<SubmitData>(jsonData);
            #region 检测是否有网站名字
            string webName = Tools.GetWebsiteName();
            serverShowInfo nameInfo = new serverShowInfo()
            {
                flag = 2,
                areaId = 2,
                title = data.webname,
                pictureAdr = null,
                url = null,
                Infostatus = 1
            };
            if (webName == Tools.DefaultWebsiteName)
            {
                //直接插入数据
                insertToolsController.insertServerShowInfo(nameInfo);
            }
            else
            {
                //更新数据
                updateToolsController.updateServerShowInfo(i => i.flag == 2 && i.areaId == 2, nameInfo);
            }
            #endregion
            #region 设置网站管理员
            int managerId = Tools.InsertBackManagerInfo(data.password);
            #endregion
            #region 设置分区
            //文章区
            foreach(SubmitData.ArticlePartData item in data.articlePartData)
            {
                if (item.pname == "")
                    continue;
                resourceArea area = new resourceArea()
                {
                    areaName = item.pname,
                    areaBrief = Tools.DefaultAreaBrief
                };
                insertToolsController.insertResourceArea(area);
            }
            //问答区
            foreach (SubmitData.QuesPartData item in data.quesPartData)
            {
                if (item.pname == "")
                    continue;
                quesArea area = new quesArea()
                {
                    areaName = item.pname,
                    areaBrief = Tools.DefaultAreaBrief
                };
                insertToolsController.insertQuesArea(area);
            }
            //论坛区
            foreach (SubmitData.ForumPartData item in data.forumPartData)
            {
                if (item.pname == "")
                    continue;
                postArea area = new postArea()
                {
                    areaName = item.pname,
                    areaBrief = Tools.DefaultAreaBrief
                };
                insertToolsController.insertPostArea(area);
            }
            #endregion
            //设置已经搞定
            WebConfigHelper.SetAppSetting("FirstOpen", "false");
            WebConfigHelper.Save();
            Tools.AlertAndRedirect(string.Format("网站已初始化成功，管理员id为：{0}", managerId), Url.Action("login", "manager", new { area = "backStage" }));
            return View();
        }
        #endregion
    }
}