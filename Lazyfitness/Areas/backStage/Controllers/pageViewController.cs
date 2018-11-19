using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
using System.Collections;
using Lazyfitness.Areas.DbTable;

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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var article = db.resourceInfo.Count();
                ViewBag.articleNum = article;

                var post = db.postInfo.Count();
                ViewBag.postNum = post;

                var quesAnsw = db.quesAnswInfo.Count();
                ViewBag.quesAnswNum = quesAnsw;

            }
            return View();
        }
        
        #region 资源文章人气主页
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public List<resourceInfo> GetPagedListresource<TKey>(int pageIndex, int pageSize, Expression<Func<resourceInfo, bool>> whereLambda, Expression<Func<resourceInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.resourceInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int GetSumPageresource(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.resourceInfo.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }

        // GET: backStage/articleManagement

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
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            ViewBag.nowPage = 1;
            ViewBag.resourcesumPage = GetSumPageresource(10);
            ViewBag.allInfo = GetPagedListresource(1, 10, x => x == x, u => u.userId);
            var allInfo = GetPagedListresource(1, 10, x => x == x, u => u.userId);
            ArrayList areaNameList = new ArrayList();
            ArrayList userNameList = new ArrayList();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < allInfo.Count; i++)
                {
                    int userId = allInfo[i].userId.Value;
                    var obUser = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                    string userName = obUser.userName;
                    int areaId = allInfo[i].areaId;
                    var obArea = db.resourceArea.Where(u => u.areaId == areaId).FirstOrDefault();
                    string areaName = obArea.areaName;
                    areaNameList.Add(areaName);
                    userNameList.Add(userName);
                }
            }
            ViewBag.areaNameList = areaNameList;
            ViewBag.userNameList = userNameList;
            return View();
        }
        [HttpPost]
        public ActionResult articlePageViewIndex(string id)
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
            ViewBag.nowPage = id;
            ViewBag.resourcesumPage = GetSumPageresource(10);
            var allInfo = GetPagedListresource(Convert.ToInt32(id), 10, x => x == x, u => u.userId);
            ViewBag.allInfo = allInfo;
            ArrayList areaNameList = new ArrayList();
            ArrayList userNameList = new ArrayList();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < allInfo.Count; i++)
                {
                    int userId = allInfo[i].userId.Value;
                    var obUser = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                    string userName = obUser.userName;
                    int areaId = allInfo[i].areaId;
                    var obArea = db.resourceArea.Where(u => u.areaId == areaId).FirstOrDefault();
                    string areaName = obArea.areaName;
                    areaNameList.Add(areaName);
                    userNameList.Add(userName);
                }
            }
            ViewBag.areaNameList = areaNameList;
            ViewBag.userNameList = userNameList;
            return View();
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
                    resourceInfo obResource = db.resourceInfo.Where(u => u.resourceId == resourceId).FirstOrDefault();
                    ViewBag.allInfo = obResource;

                    var areaList = db.resourceArea.ToList();
                    ViewBag.areaList = areaList;

                    var senderName = db.userInfo.Where(u => u.userId == obResource.userId).FirstOrDefault().userName;
                    ViewBag.senderName = senderName;
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
        public string articleUpdate(resourceInfo resource)
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
                    var obResource = db.resourceInfo.Where(u => u.resourceId == resource.resourceId).FirstOrDefault();
                    obResource.areaId = resource.areaId;
                    obResource.resourceName = resource.resourceName;
                    obResource.resourceTime = DateTime.Now;
                    obResource.pageView = resource.pageView;
                    obResource.isTop = resource.isTop;
                    obResource.resourceContent = resource.resourceContent;
                    db.SaveChanges();
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
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public List<postInfo> GetPagedListpost<TKey>(int pageIndex, int pageSize, Expression<Func<postInfo, bool>> whereLambda, Expression<Func<postInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.postInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetSumPagepost(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.postInfo.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }
        public ActionResult forumPageViewIndex()
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
            ViewBag.nowPage = 1;
            ViewBag.postsumPage = GetSumPagepost(10);
            ViewBag.allInfo = GetPagedListpost(1, 10, x => x == x, u => u.userId);
            var allInfo = GetPagedListpost(1, 10, x => x == x, u => u.userId);
            ArrayList areaNameList = new ArrayList();
            ArrayList userNameList = new ArrayList();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < allInfo.Count; i++)
                {
                    int userId = allInfo[i].userId.Value;
                    var obUser = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                    string userName = obUser.userName;
                    int areaId = allInfo[i].areaId;
                    var obArea = db.postArea.Where(u => u.areaId == areaId).FirstOrDefault();
                    string areaName = obArea.areaName;
                    areaNameList.Add(areaName);
                    userNameList.Add(userName);
                }
            }
            ViewBag.areaNameList = areaNameList;
            ViewBag.userNameList = userNameList;
            return View();
        }
        // GET: backStage/forumManagement
        [HttpPost]
        public ActionResult forumPageViewIndex(int id)
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
            ViewBag.nowPage = id;
            ViewBag.postsumPage = GetSumPagepost(10);
            var allInfo = GetPagedListpost(Convert.ToInt32(id), 10, x => x == x, u => u.userId);
            ViewBag.allInfo = allInfo;
            ArrayList areaNameList = new ArrayList();
            ArrayList userNameList = new ArrayList();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < allInfo.Count; i++)
                {
                    int userId = allInfo[i].userId.Value;
                    var obUser = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                    string userName = obUser.userName;
                    int areaId = allInfo[i].areaId;
                    var obArea = db.postArea.Where(u => u.areaId == areaId).FirstOrDefault();
                    string areaName = obArea.areaName;
                    areaNameList.Add(areaName);
                    userNameList.Add(userName);
                }
            }
            ViewBag.areaNameList = areaNameList;
            ViewBag.userNameList = userNameList;
            return View();
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
                    postInfo _postinfo = db.postInfo.Where(u => u.postId == postId).FirstOrDefault();
                    if (_postinfo != null)
                    {
                        ViewBag.postInfo = _postinfo;

                        var postArea = db.postArea.ToList();
                        if (postArea != null)
                        {
                            ViewBag.postArea = postArea;
                        }
                        else
                        {
                            return Content("<script >alert('帖子分区已被注销！无法查看！！！');</script >", "text/html");
                        }
                        var userInfo = db.userInfo.Where(u => u.userId == _postinfo.userId).FirstOrDefault();
                        if (userInfo != null)
                        {
                            ViewBag.userInfo = userInfo;
                        }
                        else
                        {
                            return Content("<script >alert('帖子拥有者已被注销！无法查看！！！');</script >", "text/html");
                        }
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
                    _postInfo.postTitle = info.postTitle;
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
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public List<quesAnswInfo> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<quesAnswInfo, bool>> whereLambda, Expression<Func<quesAnswInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.quesAnswInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int GetSumPagequesAnsw(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.quesAnswInfo.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }
        public ActionResult quesAnswPageViewIndex()
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
            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPagequesAnsw(10);
            ViewBag.allInfo = GetPagedList(1, 10, x => x == x, u => u.userId);
            var allInfo = GetPagedList(1, 10, x => x == x, u => u.userId);
            ArrayList areaNameList = new ArrayList();
            ArrayList userNameList = new ArrayList();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < allInfo.Count; i++)
                {
                    int userId = allInfo[i].userId.Value;
                    var obUser = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                    string userName = obUser.userName;
                    int areaId = allInfo[i].areaId;
                    var obArea = db.quesArea.Where(u => u.areaId == areaId).FirstOrDefault();
                    string areaName = obArea.areaName;
                    areaNameList.Add(areaName);
                    userNameList.Add(userName);
                }
            }
            ViewBag.areaNameList = areaNameList;
            ViewBag.userNameList = userNameList;

            return View();
        }
        // GET: backStage/forumManagement
        [HttpPost]
        public ActionResult quesAnswPageViewIndex(int id)
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
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPagequesAnsw(10);
            ViewBag.allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.userId);
            var allInfo = GetPagedList(1, 10, x => x == x, u => u.userId);
            ArrayList areaNameList = new ArrayList();
            ArrayList userNameList = new ArrayList();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < allInfo.Count; i++)
                {
                    int userId = allInfo[i].userId.Value;
                    var obUser = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                    string userName = obUser.userName;
                    int areaId = allInfo[i].areaId;
                    var obArea = db.quesArea.Where(u => u.areaId == areaId).FirstOrDefault();
                    string areaName = obArea.areaName;
                    areaNameList.Add(areaName);
                    userNameList.Add(userName);
                }
            }
            ViewBag.areaNameList = areaNameList;
            ViewBag.userNameList = userNameList;
            return View();
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
                    var quesAnswInfo = db.quesAnswInfo.Where(u => u.quesAnswId == quesAnswId).FirstOrDefault();
                    if (quesAnswInfo != null)
                    {
                        ViewBag.quesAnswInfo = quesAnswInfo;

                        var quesArea = db.quesArea.ToList();
                        if (quesArea != null)
                        {
                            ViewBag.quesArea = quesArea;
                        }
                        else
                        {
                            return Content("帖子分区已被注销！无法查看！！！");
                        }
                        var userInfo = db.userInfo.Where(u => u.userId == quesAnswInfo.userId).FirstOrDefault();
                        if (userInfo != null)
                        {
                            ViewBag.userInfo = userInfo;
                        }
                        else
                        {
                            return Content("帖子拥有者已被注销！无法查看！！！");
                        }
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
                    _quesAnswInfo.quesAnswTitle = info.quesAnswTitle;
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