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
        public List<postInfo> GetPagedList<TKey>(int pageIndex, int pageSize/*, Expression<Func<userInfo, bool>> whereLambda*/, Expression<Func<postInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.postInfo/*.Where(whereLambda)*/.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.userInfo.ToList().Count;
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
            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(1, 10, u => u.userId);

            return View();
        }
        // GET: backStage/forumManagement
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
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(Convert.ToInt32(id), 10, u => u.userId);
            return View();
        }
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
            ViewBag.allInfo = GetPagedList(1, 10, u => u.userId);

            return View();
        }

        //论坛帖子分区
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
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(1, 10, u => u.userId);

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
                    //先判断登录Id是否可用
                    var isareaId = db.postArea.Where(u => u.areaId == area.areaId);
                    if (isareaId.ToList().Count != 0)
                    {
                        return "论坛分区已存在";
                    }
                    postArea _area = new postArea
                    {
                        areaId = area.areaId,
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
            return View();
        }
        [HttpGet]
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
                    DbQuery<postArea> dbAreasearch = db.postArea.Where(u => u.areaName == area.areaName) as DbQuery<postArea>;
                    postArea _postArea = dbAreasearch.FirstOrDefault();
                    if (_postArea != null)
                    {
                        ViewBag.areaId = _postArea.areaId;
                        ViewBag.areaName = _postArea.areaName;
                        ViewBag.areaBrief = _postArea.areaBrief;
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
                    _postArea.areaId = area.areaId;
                    _postArea.areaName = area.areaName;
                    _postArea.areaBrief = area.areaBrief;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "论坛分区修改成功";
            }
            catch
            {
                return "论坛分区修改失败";
            }
        }
        #endregion
        #endregion

        #region 论坛帖子管理
        #region 增加

        public ActionResult forumInvitationAdd()
        {
            return View();
        }
        [HttpPost]
        public string forumInvitationAdd(postInfo info)
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
                    //先判断登录Id是否可用
                    var isareaId = db.postArea.Where(u => u.areaId == info.areaId);
                    var ispostId = db.postInfo.Where(u => u.postId == info.postId);
                    if (isareaId.ToList().Count == 0)
                    {
                        return "论坛分区不存在";
                    }
                    if (ispostId.ToList().Count != 0)
                    {
                        return "论坛帖子ID已存在";
                    }
                    postInfo _info = new postInfo
                    {
                        areaId = info.areaId,
                        postId = info.postId,
                        postTitle = info.postTitle,
                        userId = info.userId,
                        postTime = DateTime.Now,
                        pageView = 0,
                        isPost = info.isPost,
                        isPay = info.isPay,
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
                    DbQuery<postInfo> dbInvitationsearch = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postinfo = dbInvitationsearch.FirstOrDefault();
                    if (_postinfo != null)
                    {
                        ViewBag.areaId = _postinfo.areaId;
                        ViewBag.postId = _postinfo.postId;
                        ViewBag.postTitle = _postinfo.postTitle;
                        ViewBag.userId = _postinfo.userId;
                        ViewBag.postTime = _postinfo.postTime;
                        ViewBag.pageView = _postinfo.pageView;
                        ViewBag.isPost = _postinfo.isPost;
                        ViewBag.isPay = _postinfo.isPay;
                        ViewBag.amount = _postinfo.amount;
                        ViewBag.postStatus = _postinfo.postStatus;
                        ViewBag.postContent = _postinfo.postContent;
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
                    DbQuery<postInfo> dbInvitation = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postInfo = dbInvitation.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _postInfo.areaId = info.areaId;
                    _postInfo.postId = info.postId;
                    _postInfo.postTitle = info.postTitle;
                    _postInfo.userId = info.userId;
                    _postInfo.isPost = info.isPost;
                    _postInfo.isPay = info.isPay;
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