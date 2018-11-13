using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
using Lazyfitness.Areas.DbTable;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class quesAnswManagementController : Controller
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
        public List<quesAnswInfo> GetPagedList<TKey>(int pageIndex, int pageSize/*, Expression<Func<userInfo, bool>> whereLambda*/, Expression<Func<quesAnswInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.quesAnswInfo/*.Where(whereLambda)*/.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        //问答分区主页
        public ActionResult quesAnswAreaIndex()
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

        //问答帖子分区
        public ActionResult quesAnswInvitationIndex()
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

        #region 问答分区管理
        #region 增加
        public ActionResult quesAnswAreaAdd()
        {
            return View();
        }
        [HttpPost]
        public string quesAnswAreaAdd(quesArea area)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //先判断登录Id是否可用
                    var isareaId = db.quesArea.Where(u => u.areaId == area.areaId);
                    if (isareaId.ToList().Count != 0)
                    {
                        return "问答分区已存在";
                    }
                    quesArea _area = new quesArea
                    {
                        areaId = area.areaId,
                        areaName = area.areaName,
                        areaBrief = area.areaBrief
                    };
                    db.quesArea.Add(_area);
                    db.SaveChanges();
                }
                return "问答分区增加成功";
            }
            catch
            {
                return ("问答分区增加失败");
            }
        }
        #endregion
        #region 查询
        public ActionResult quesAnswAreaSearch()
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
                var areaName = db.quesArea.Select(u => u.areaName).ToList();
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
        public ActionResult quesAnswAreaSearch(quesArea area)
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
                    DbQuery<quesArea> dbAreasearch = db.quesArea.Where(u => u.areaName == area.areaName) as DbQuery<quesArea>;
                    quesArea _quesArea = dbAreasearch.FirstOrDefault();
                    if (_quesArea != null)
                    {
                        ViewBag.quesArea = _quesArea;
                    }
                    else
                    {
                        return View("quesAnswAreaUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("quesAnswAreaUpdate");
            }
            catch
            {
                return View("quesAnswAreaUpdate");
            }
        }
        #endregion
        #region 删除
        public ActionResult quesAnswAreaDelete()
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
                var areaName = db.quesArea.Select(u => u.areaName).ToList();
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
        public string quesAnswAreaDelete(quesArea area)
        {
            try
            {
                //根据不可重复的用户名找到quesArea里面的areaName,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<quesArea> dbArea = db.quesArea.Where(u => u.areaName == area.areaName.Trim()) as DbQuery<quesArea>;
                    quesArea _quesArea = dbArea.FirstOrDefault();

                    if (_quesArea == null)
                    {
                        return "删除的问答分区不存在";
                    }
                    db.Entry<quesArea>(_quesArea).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "问答分区删除成功";
                }
            }
            catch
            {
                return "问答分区删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult quesAnswAreaUpdate()
        {
            return View();
        }
        [HttpPost]
        public string quesAnswAreaUpdate(quesArea area)
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
                    DbQuery<quesArea> dbArea = db.quesArea.Where(u => u.areaId == area.areaId) as DbQuery<quesArea>;
                    quesArea _quesArea = dbArea.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _quesArea.areaId = area.areaId;
                    _quesArea.areaName = area.areaName;
                    _quesArea.areaBrief = area.areaBrief;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "问答分区修改成功";
            }
            catch
            {
                return "问答分区修改失败";
            }
        }
        #endregion
        #endregion

        #region 问答帖子管理
        #region 增加

        public ActionResult quesAnswInvitationAdd()
        {
            return View();
        }
        [HttpPost]
        public string quesAnswInvitationAdd(quesAnswInfo info)
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
                    var ispostId = db.quesAnswInfo.Where(u => u.quesAnswId == info.quesAnswId);
                    if (ispostId.ToList().Count != 0)
                    {
                        return "问答帖子ID已存在";
                    }
                    quesAnswInfo _info = new quesAnswInfo
                    {
                        areaId = info.areaId,
                        quesAnswId = info.quesAnswId,
                        quesAnswTitle = info.quesAnswTitle,
                        userId = info.userId,
                        quesAnswTime = DateTime.Now,
                        pageView = 0,
                        isPost = info.isPost,
                        quesAnswStatus = info.quesAnswStatus,
                        amount = info.amount,
                        quesAnswContent = info.quesAnswContent
                    };
                    db.quesAnswInfo.Add(_info);
                    db.SaveChanges();
                }
                return "问答帖子增加成功";
            }
            catch
            {
                return ("问答帖子增加失败");
            }
        }
        #endregion
        #region 查询
        public ActionResult quesAnswInvitationSearch()
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
                var quesAnswInfo = db.quesAnswInfo.ToList();
                if (quesAnswInfo != null)
                {
                    ViewBag.quesAnswInfo = quesAnswInfo;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult quesAnswInvitationSearch(quesAnswInfo info)
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
                    DbQuery<quesAnswInfo> dbInvitationsearch = db.quesAnswInfo.Where(u => u.quesAnswId == info.quesAnswId) as DbQuery<quesAnswInfo>;
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
        #endregion
        #region 删除
        public ActionResult quesAnswInvitationDelete()
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
                var quesAnswInfo = db.quesAnswInfo.ToList();
                if (quesAnswInfo != null)
                {
                    ViewBag.quesAnswInfo = quesAnswInfo;
                }
                else
                {
                    return View();
                }
            }
            return View();
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
        public ActionResult quesAnswInvitationUpdate()
        {
            return View();
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

        #region 问答专区主页管理
        public ActionResult forumIndex()
        {
            return View();
        }

        #endregion
    }
}