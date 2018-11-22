using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
using Lazyfitness.Areas.DbTable;
using System.Collections;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class forumManagementController : Controller
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public List<postArea> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<postArea, bool>> whereLambda, Expression<Func<postArea, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.postArea.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.postArea.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }

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

                var postArea = db.postArea.Count();
                ViewBag.postAreaNum = postArea;

                var postInfo = db.postInfo.Count();
                ViewBag.postInfoNum = postInfo;

            }
            return View();
        }
        // GET: backStage/forumManagement
        //论坛分区主页
        public ActionResult forumAreaIndex()
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
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(1, 10, x => x == x, u => u.areaId);

            return View();
        }
        [HttpPost]
        public ActionResult forumAreaIndex(int id)
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
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.areaId);
            return View();
        }
        //论坛帖子分区
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
        public ActionResult forumInvitationIndex()
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
            if (allInfo == null)
            {
                return View();
            }
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
        public ActionResult forumInvitationIndex(int id)
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

        #region 论坛分区管理
        #region 增加
        public ActionResult forumAreaAdd()
        {
            return View();
        }
        [HttpPost]
        public string forumAreaAdd(postArea area)
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
                    postArea _area = new postArea
                    {
                        areaName = area.areaName,
                        areaBrief = area.areaBrief
                    };
                    db.postArea.Add(_area);
                    db.SaveChanges();
                }
                return "论坛分区增加成功";
            }
            catch
            {
                return ("论坛分区增加失败");
            }
        }
        #endregion
        #region 查询
        public ActionResult forumAreaSearch()
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var areaInfo = db.postArea.ToList();
                if (areaInfo != null)
                {
                    ViewBag.areaInfo = areaInfo;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult forumAreaSearch(postArea area)
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
                //先查询
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postArea> dbAreasearch = db.postArea.Where(u => u.areaId == area.areaId) as DbQuery<postArea>;
                    postArea _postArea = dbAreasearch.FirstOrDefault();
                    if (_postArea != null)
                    {
                        ViewBag.postArea = _postArea;
                    }
                    else
                    {
                        return View("forumAreaUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("forumAreaUpdate");
            }
            catch
            {
                return View("forumAreaUpdate");
            }
        }
        #endregion
        #region 删除
        public ActionResult forumAreaDelete()
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var areaName = db.postArea.Select(u => u.areaName).ToList();
                if (areaName != null)
                {
                    ViewBag.areaName = areaName;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public string forumAreaDelete(postArea area)
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
                //根据不可重复的用户名找到postArea里面的areaName,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<postArea> dbArea = db.postArea.Where(u => u.areaName == area.areaName.Trim()) as DbQuery<postArea>;
                    postArea _postArea = dbArea.FirstOrDefault();
                    if (_postArea == null)
                    {
                        return "删除的论坛分区不存在";
                    }
                    var listInfo = db.postInfo.Where(u => u.areaId == _postArea.areaId).ToList();
                    if (listInfo != null)
                    {
                        db.postInfo.RemoveRange(listInfo);
                    }
                    db.Entry<postArea>(_postArea).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "论坛分区删除成功";
                }
            }
            catch
            {
                return "论坛分区删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult forumAreaUpdate()
        {
            return View();
        }
        [HttpPost]
        public string forumAreaUpdate(postArea area)
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
                    DbQuery<postArea> dbArea = db.postArea.Where(u => u.areaId == area.areaId) as DbQuery<postArea>;
                    postArea _postArea = dbArea.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _postArea.areaName = area.areaName;
                    _postArea.areaBrief = area.areaBrief;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "论坛分区修改成功";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
        #endregion

        #region 论坛帖子管理
        #region 增加

        public ActionResult forumInvitationAdd()
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var postArea = db.postArea.ToList();
                if (postArea != null)
                {
                    ViewBag.postArea = postArea;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public string forumInvitationAdd(postInfo info)
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
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //找到userId
                    var dbFindUser = db.userSecurity.Where(u => u.loginId == cookieText);
                    var obFindUser = dbFindUser.FirstOrDefault();
                    int rightUserId = obFindUser.userId;
                    postInfo _info = new postInfo
                    {
                        areaId = info.areaId,
                        postTitle = info.postTitle,
                        userId = rightUserId,
                        postTime = DateTime.Now,
                        pageView = 0,
                        isPost = info.isPost,                        
                        amount = info.amount,
                        postStatus = info.postStatus,
                        postContent = info.postContent
                    };
                    db.postInfo.Add(_info);
                    db.SaveChanges();
                }
                return "论坛帖子增加成功";
            }
            catch
            {
                return ("论坛帖子增加失败");
            }
        }
        #endregion
        #region 查询
        public ActionResult forumInvitationSearch()
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var postInfo = db.postInfo.ToList();
                if (postInfo != null)
                {
                    ViewBag.postInfo = postInfo;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult forumInvitationSearch(postInfo info)
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
                    postInfo _postinfo = db.postInfo.Where(u => u.postId == info.postId).FirstOrDefault();
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
        #endregion
        #region 删除
        public ActionResult forumInvitationDelete()
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var postInfo = db.postInfo.ToList();
                if (postInfo != null)
                {
                    ViewBag.postInfo = postInfo;
                }
                else
                {
                    return View();
                }
            }
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
        public ActionResult forumInvitationUpdate()
        {
            return View();
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
                    postInfo _postInfo = db.postInfo.Where(u => u.postId == info.postId).FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _postInfo.areaId = info.areaId;
                    _postInfo.postTitle = info.postTitle;
                    _postInfo.isPost = info.isPost;
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

        #region 论坛主页管理
        public ActionResult forumIndex()
        {
            return View();
        }

        #endregion
    }
}