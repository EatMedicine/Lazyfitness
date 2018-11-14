using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                return Content("未登录");
            }
            return View();
        }

        #region 资源文章人气主页
        public ActionResult articlePageViewIndex()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                ///浏览量查询
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //查询资源文章人气
                    var resource = db.resourceInfo.OrderByDescending(u => u.pageView).Take(10).ToList();
                    if (resource == null)
                    {
                        ViewBag.resourceconfirm = "false";
                    }
                    else
                    {
                        ViewBag.resourceconfirm = "true";
                        ViewBag.resource = resource;
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        #region 删除
        public ActionResult articleDelete()
        {
            return View();
        }
        [HttpPost]
        public string articleDelete(resourceInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                //根据不可重复的用户名找到postInfo里面的postId,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<resourceInfo> dbInvitation = db.resourceInfo.Where(u => u.resourceId == info.resourceId) as DbQuery<resourceInfo>;
                    resourceInfo _resourceInfo = dbInvitation.FirstOrDefault();
                    if (_resourceInfo == null)
                    {
                        return "删除的资源文章不存在";
                    }
                    db.Entry<resourceInfo>(_resourceInfo).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "资源文章删除成功";
                }
            }
            catch
            {
                return "资源文章删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult articleUpdate(int resourceId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return View("Index");
            }
            try
            {
                //先查询,后修改
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceInfo> dbInvitationsearch = db.resourceInfo.Where(u => u.resourceId == resourceId) as DbQuery<resourceInfo>;
                    resourceInfo _resourceInfo = dbInvitationsearch.FirstOrDefault();
                    if (_resourceInfo != null)
                    {
                        ViewBag.resourceInfo = _resourceInfo;
                    }
                    else
                    {
                        return View("articleUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("articleUpdate");
            }
            catch
            {
                return View("articleUpdate");
            }
        }
        [HttpPost]
        public string articleUpdate(resourceInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceInfo> dbInvitation = db.resourceInfo.Where(u => u.resourceId == info.resourceId) as DbQuery<resourceInfo>;
                    resourceInfo _resourceInfo = dbInvitation.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _resourceInfo.areaId = info.areaId;
                    _resourceInfo.resourceId = info.resourceId;
                    _resourceInfo.resourceName = info.resourceName;
                    _resourceInfo.userId = info.userId;
                    _resourceInfo.isTop = info.isTop;
                    _resourceInfo.resourceContent = info.resourceContent;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "资源文章修改成功";
            }
            catch
            {
                return "资源文章修改失败";
            }
        }
        #endregion
        #endregion

        #region 论坛帖子人气主页
        public ActionResult forumPageViewIndex()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                ///浏览量查询
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //查询论坛帖子人气
                    var post = db.postInfo.OrderByDescending(u => u.pageView).Take(10).ToList();
                    if (post == null)
                    {
                        ViewBag.postconfirm = "false";
                    }
                    else
                    {
                        ViewBag.postconfirm = "true";
                        ViewBag.post = post;
                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        #region 删除
        public ActionResult forumInvitationDelete()
        {
            return View();
        }
        [HttpPost]
        public string forumInvitationDelete(postInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                //根据不可重复的用户名找到postInfo里面的postId,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<postInfo> dbInvitation = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postInfo = dbInvitation.FirstOrDefault();
                    if (_postInfo == null)
                    {
                        return "删除的论坛帖子不存在";
                    }
                    db.Entry<postInfo>(_postInfo).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "论坛帖子删除成功";
                }
            }
            catch
            {
                return "论坛帖子删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult forumInvitationUpdate(int postId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return View("Index");
            }
            try
            {
                //先查询,后修改
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dbInvitationsearch = db.postInfo.Where(u => u.postId == postId) as DbQuery<postInfo>;
                    postInfo _postinfo = dbInvitationsearch.FirstOrDefault();
                    if (_postinfo != null)
                    {
                        ViewBag.postInfo = _postinfo;
                    }
                    else
                    {
                        return View("forumInvitationUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("forumInvitationUpdate");
            }
            catch
            {
                return View("forumInvitationUpdate");
            }
        }
        [HttpPost]
        public string forumInvitationUpdate(postInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dbInvitation = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postInfo = dbInvitation.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _postInfo.areaId = info.areaId;
                    _postInfo.postId = info.postId;
                    _postInfo.postTitle = info.postTitle;
                    _postInfo.userId = info.userId;
                    _postInfo.isPost = info.isPost;
                    _postInfo.isPay = info.isPay;
                    _postInfo.amount = info.amount;
                    _postInfo.postStatus = info.postStatus;
                    _postInfo.postContent = info.postContent;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "论坛帖子修改成功";
            }
            catch
            {
                return "论坛帖子修改失败";
            }
        }
        #endregion
        #endregion

        #region 问答帖子人气主页
        public ActionResult quesAnswPageViewIndex()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                ///浏览量查询
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //查询问答帖子人气
                    var quesAnsw = db.quesAnswInfo.OrderByDescending(u => u.pageView).Take(10).ToList();
                    if (quesAnsw == null)
                    {
                        ViewBag.quesAnswconfirm = "false";
                    }
                    else
                    {
                        ViewBag.quesAnswconfirm = "true";
                        ViewBag.quesAnsw = quesAnsw;
                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        #region 删除
        public ActionResult quesAnswInvitationDelete()
        {
            return View();
        }
        [HttpPost]
        public string quesAnswInvitationDelete(quesAnswInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                //根据不可重复的用户名找到quesAnswInfo里面的quesAnswId,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<quesAnswInfo> dbInvitation = db.quesAnswInfo.Where(u => u.quesAnswId == info.quesAnswId) as DbQuery<quesAnswInfo>;
                    quesAnswInfo _quesAnswInfo = dbInvitation.FirstOrDefault();

                    if (_quesAnswInfo == null)
                    {
                        return "删除的问答帖子不存在";
                    }
                    db.Entry<quesAnswInfo>(_quesAnswInfo).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "问答帖子删除成功";
                }
            }
            catch
            {
                return "问答帖子删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult quesAnswInvitationUpdate(int quesAnswId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                //先查询,后修改
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswInfo> dbInvitationsearch = db.quesAnswInfo.Where(u => u.quesAnswId == quesAnswId) as DbQuery<quesAnswInfo>;
                    quesAnswInfo quesAnswInfo = dbInvitationsearch.FirstOrDefault();
                    if (quesAnswInfo != null)
                    {
                        ViewBag.quesAnswInfo = quesAnswInfo;
                    }
                    else
                    {
                        return View("quesAnswInvitationUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("quesAnswInvitationUpdate");
            }
            catch
            {
                return View("quesAnswInvitationUpdate");
            }
        }
        [HttpPost]
        public string quesAnswInvitationUpdate(quesAnswInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswInfo> dbInvitation = db.quesAnswInfo.Where(u => u.quesAnswId == info.quesAnswId) as DbQuery<quesAnswInfo>;
                    quesAnswInfo _quesAnswInfo = dbInvitation.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _quesAnswInfo.areaId = info.areaId;
                    _quesAnswInfo.quesAnswId = info.quesAnswId;
                    _quesAnswInfo.quesAnswTitle = info.quesAnswTitle;
                    _quesAnswInfo.userId = info.userId;
                    _quesAnswInfo.amount = info.amount;
                    _quesAnswInfo.isPost = info.isPost;
                    _quesAnswInfo.quesAnswStatus = info.quesAnswStatus;
                    _quesAnswInfo.quesAnswContent = info.quesAnswContent;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "问答帖子修改成功";
            }
            catch
            {
                return "问答帖子修改失败";
            }
        }
        #endregion
        #endregion
    }
}