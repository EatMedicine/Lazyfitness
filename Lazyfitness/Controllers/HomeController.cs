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
            ViewBag.AreasUrl = new string[]
            {
                Url.Action("Article","Home"),
                Url.Action("ArticlePart","Home",new{partId=areasId[0]}),
                Url.Action("ArticlePart","Home",new{partId=areasId[1]}),
                Url.Action("ArticlePart","Home",new{partId=areasId[2]}),
                Url.Action("ArticlePart","Home",new{partId=areasId[3]}),
            };
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
            ViewBag.PartUrl = new string[]
            {
                Url.Action("Question","Home"),
                Url.Action("QuestionPart","Home",new{ partId=quesId[0]}),
                Url.Action("QuestionPart","Home",new{ partId=quesId[1]}),
                Url.Action("QuestionPart","Home",new{ partId=quesId[2]}),
            };
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
            ViewBag.PartUrl = new string[]
            {
                Url.Action("forumPart","Home",new {partId=postId[0]}),
                Url.Action("forumPart","Home",new {partId=postId[1]}),
                Url.Action("forumPart","Home",new {partId=postId[2]}),
                Url.Action("forumPart","Home",new {partId=postId[3]}),
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
            ViewBag.AreasUrl = new string[]
            {
                Url.Action("Article","Home"),
                Url.Action("ArticlePart","Home",new{partId=areasId[0]}),
                Url.Action("ArticlePart","Home",new{partId=areasId[1]}),
                Url.Action("ArticlePart","Home",new{partId=areasId[2]}),
                Url.Action("ArticlePart","Home",new{partId=areasId[3]}),
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
            ViewBag.PartUrl = new string[]
            {
                Url.Action("Question","Home"),
                Url.Action("QuestionPart","Home",new{ partId=quesId[0]}),
                Url.Action("QuestionPart","Home",new{ partId=quesId[1]}),
                Url.Action("QuestionPart","Home",new{ partId=quesId[2]}),
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
            ViewBag.PartUrl = new string[]
            {
                Url.Action("forumPart","Home",new {partId=postId[0]}),
                Url.Action("forumPart","Home",new {partId=postId[1]}),
                Url.Action("forumPart","Home",new {partId=postId[2]}),
                Url.Action("forumPart","Home",new {partId=postId[3]}),
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
            //此处应先验证，然后从cookie里获取userId
            ViewBag.UserId = 111;
            return View();
        }

        /// <summary>
        /// 问答详细页
        /// </summary>
        /// <param name="num">问答贴id</param>
        /// <returns></returns>
        public ActionResult QuestionDetail(int num = 0)
        {
            ViewBag.QuestionId = num;
            ViewBag.IsSolved = false;
            ViewBag.Money = 100;
            quesAnswInfo info = Tools.GetQuestionInfo(num);
            ViewBag.QuesInfo = info;
            ViewBag.QuesUserInfo = Tools.GetUserInfo((int)info.userId);
            quesAnswReply[] replys = Tools.GetQuestionReply(num);
            int[] id = new int[replys.Length];
            string[] name = new string[replys.Length];
            string[] picAdr = new string[replys.Length];
            string[] time = new string[replys.Length];
            string[] content = new string[replys.Length];
            int[] isAgree = new int[replys.Length];
            for (int count = 0; count < replys.Length; count++)
            {
                id[count] = (int)replys[count].userId;
                name[count] = Tools.GetUserName(id[count]);
                picAdr[count] = Tools.GetUserPicAdr(id[count]);
                time[count] = DateTime.Now.ToString();
                content[count] = replys[count].replyContent;
                isAgree[count] = (int)replys[count].isAgree;

            }
            ViewBag.ReplyId = id;
            ViewBag.ReplyName = name;
            ViewBag.ReplyUserPic = picAdr;
            ViewBag.ReplyTime = time;
            ViewBag.ReplyContent = content;
            ViewBag.ReplyIsAgree = isAgree;
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
        public ActionResult forumDetail(int num = 0)
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
    }
}