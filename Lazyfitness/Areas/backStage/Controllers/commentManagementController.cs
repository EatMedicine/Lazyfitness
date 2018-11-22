using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class commentManagementController : Controller
    {
        public ActionResult Index()
        {

            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        #region 论坛评论管理
        //论坛评论管理
        public ActionResult forumComment()
        {

            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        //增加论坛评论
        public ActionResult addForumComment()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public ActionResult sureForum(int postId)
        {

            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbForum = db.postInfo.Where(u => u.postId == postId);
                if (dbForum == null)
                {
                    return Content("没有这篇帖子");
                }
                var obForum = dbForum.FirstOrDefault();
                string areaName = db.postArea.Where(u => u.areaId == obForum.areaId).FirstOrDefault().areaName;
                string ownerName = db.userInfo.Where(u => u.userId == obForum.userId).FirstOrDefault().userName;
                ViewBag.areaName = areaName;
                ViewBag.ownerName = ownerName;
                ViewBag.forumInfo = obForum;
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult add(int postId, string replyContent)
        {
            string cookieText = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                cookieText = Server.HtmlEncode(cookieName.Value).ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {

                var getUserId = db.userInfo.Where(u => u.userName == cookieText).FirstOrDefault().userId;

                postReply obReply = new postReply
                {
                    postId = postId,
                    userId = getUserId,
                    replyTime = DateTime.Now,
                    replyContent = replyContent
                };
                db.postReply.Add(obReply);
                db.SaveChanges();
                return Content("T");
            }
        }

        //删除评论
        public ActionResult deleteForumComment()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }

        [HttpPost]
        public ActionResult sureReply(int postId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var replyList = db.postReply.Where(u => u.postId == postId).ToList();
                ViewBag.replyList = replyList;
                return View();
            }
        }
        [HttpPost]
        public string delete(int id)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    var dbReply = db.postReply.Where(u => u.id == id);
                    var obReply = dbReply.FirstOrDefault();
                    db.postReply.Remove(obReply);
                    db.SaveChanges();
                    return "T";
                }
            }
            catch
            {
                return "F";
            }
            
        }
        #endregion
        //问答评论管理
        public ActionResult quesAnswComment()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }

        //增加回答
        public ActionResult addQueAnswComment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sureAnswer(int quesAnswId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbQues = db.quesAnswInfo.Where(u => u.quesAnswId == quesAnswId);
                if (dbQues == null)
                {
                    return Content("没有这个问题");
                }
                var obQuesInfo = dbQues.FirstOrDefault();
                string areaName = db.quesArea.Where(u => u.areaId == obQuesInfo.areaId).FirstOrDefault().areaName;
                string ownerName = db.userInfo.Where(u => u.userId == obQuesInfo.userId).FirstOrDefault().userName;
                ViewBag.areaName = areaName;
                ViewBag.ownerName = ownerName;
                ViewBag.obQuesInfo = obQuesInfo;
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addAnswer(int quesAnswId, string replyContent)
        {
            string cookieText = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                cookieText = Server.HtmlEncode(cookieName.Value).ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {

                var getUserId = db.userInfo.Where(u => u.userName == cookieText).FirstOrDefault().userId;

                quesAnswReply obReply = new quesAnswReply
                {
                    quesAnswId = quesAnswId,
                    userId = getUserId,
                    replyTime = DateTime.Now,
                    replyContent = replyContent,
                    isAgree = 0
                };
                db.quesAnswReply.Add(obReply);
                db.SaveChanges();
                return Content("T");
            }
        }

        //删除回答
        public ActionResult deleteQueAnswComment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sureReplyAnswer(int quesAnswId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var replyList = db.quesAnswReply.Where(u => u.quesAnswId == quesAnswId).ToList();
                ViewBag.replyList = replyList;
                return View();
            }
        }
        [HttpPost]
        public string deleteAnswer(int id)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    var dbReply = db.quesAnswReply.Where(u => u.id == id);
                    var obReply = dbReply.FirstOrDefault();
                    db.quesAnswReply.Remove(obReply);
                    db.SaveChanges();
                    return "T";
                }
            }
            catch
            {
                return "F";
            }
        }
    }
}