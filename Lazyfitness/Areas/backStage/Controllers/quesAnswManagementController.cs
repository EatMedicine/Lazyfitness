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
    public class quesAnswManagementController : Controller
    {
        #region 分页类
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public quesArea[] GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<quesArea, bool>> whereLambda, Expression<Func<quesArea, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.quesArea.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.resourceInfo.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }

        //问答帖子分区
        public quesAnswInfo[] GetPagedListquesAnsw<TKey>(int pageIndex, int pageSize, Expression<Func<quesAnswInfo, bool>> whereLambda, Expression<Func<quesAnswInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.quesAnswInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }
        public int GetSumPagequesAnsw(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.resourceInfo.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }
        #endregion
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
        #region 问答分区主页
        // GET: backStage/forumManagement
        //问答分区主页
        public ActionResult quesAnswAreaIndex()
        {
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

            int nowPage = 1;
            int sumPage = GetSumPage(10);
            quesArea[] allInfo = GetPagedList(1, 10, x => x == x, u => u.areaId);
            ViewBag.nowPage = nowPage;
            ViewBag.sumPage = sumPage;
            ViewBag.allInfo = allInfo;


            return View();
        }
        [HttpPost]
        public ActionResult quesAnswAreaIndex(int id)
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

            int nowPage = id;
            int sumPage = GetSumPage(10);
            quesArea[] allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.areaId);
            ViewBag.nowPage = nowPage;
            ViewBag.sumPage = sumPage;
            ViewBag.allInfo = allInfo;

            return View();
        }
        #endregion

        #region 问答区主页
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

            try
            {
                int nowPage = 1;
                int sumPage = GetSumPagequesAnsw(10);
                quesAnswInfo[] allInfo =  GetPagedListquesAnsw(1, 10, x => x == x, u => u.userId);

                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                if (allInfo.Length == 0 || allInfo == null)
                {
                    return View();
                }
                ArrayList areaNameList = new ArrayList();
                ArrayList userNameList = new ArrayList();

                foreach (var item in allInfo)
                {
                    //依次获得分区名
                    int areaId = item.areaId;
                    quesArea[] areaName = toolsHelpers.selectToolsController.selectQuesArea(u => u.areaId == areaId, u => u.areaId);
                    //依次获得用户名
                    int userId = item.userId.Value;
                    userInfo[] userName = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == userId, u => u.userId);
                    if (areaName != null && areaName.Length != 0 && userName != null && userName.Length != 0)
                    {
                        areaNameList.Add(areaName[0].areaName);
                        userNameList.Add(userName[0].userName);
                    }
                    else
                    {
                        areaNameList.Add("【出错数据】");
                        userNameList.Add("【出错数据】");
                    }
                }
                ViewBag.areaNameList = areaNameList;
                ViewBag.userNameList = userNameList;
                return View();
            }
            catch
            {
                return Content("加载出错！");
            }
        }
        // GET: backStage/forumManagement
        [HttpPost]
        public ActionResult quesAnswInvitationIndex(int id)
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

            try
            {
                int nowPage = id;
                int sumPage = GetSumPagequesAnsw(10);
                quesAnswInfo[] allInfo = GetPagedListquesAnsw(id, 10, x => x == x, u => u.userId);

                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                if (allInfo.Length == 0 || allInfo == null)
                {
                    return View();
                }
                ArrayList areaNameList = new ArrayList();
                ArrayList userNameList = new ArrayList();

                foreach (var item in allInfo)
                {
                    //依次获得分区名
                    int areaId = item.areaId;
                    quesArea[] areaName = toolsHelpers.selectToolsController.selectQuesArea(u => u.areaId == areaId, u => u.areaId);
                    //依次获得用户名
                    int userId = item.userId.Value;
                    userInfo[] userName = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == userId, u => u.userId);
                    if (areaName != null && areaName.Length != 0 && userName != null && userName.Length != 0)
                    {
                        areaNameList.Add(areaName[0].areaName);
                        userNameList.Add(userName[0].userName);
                    }
                    else
                    {
                        areaNameList.Add("【出错数据】");
                        userNameList.Add("【出错数据】");
                    }
                }
                ViewBag.areaNameList = areaNameList;
                ViewBag.userNameList = userNameList;
                return View();
            }
            catch
            {
                return Content("加载出错！");
            }
        }
        #endregion


        #region 问答分区管理
        #region 增加
        public ActionResult quesAnswAreaAdd()
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
            return View();
        }
        [HttpPost]
        public string quesAnswAreaAdd(quesArea area)
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
                //检查是否存在相同的分区名
                if (toolsHelpers.selectToolsController.selectQuesArea(u => u.areaName == area.areaName, u => u.areaId).Length != 0)
                {
                    return "存在相同的问答分区名";
                }
                if (toolsHelpers.insertToolsController.insertQuesArea(area) == true)
                {
                    Response.Redirect("/backStage/quesAnswManagement/quesAnswAreaIndex");
                    return "success";
                }
                return "增加问答分区失败";
            }
            catch
            {
                return ("增加问答分区出错");
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
            try
            {
                quesArea[] areaInfo = toolsHelpers.selectToolsController.selectQuesArea(x => x == x, u => u.areaId);
                if (areaInfo.Length == 0 || areaInfo == null)
                {
                    return Content("没有分区请先增加一个分区！");
                }
                ViewBag.areaInfo = areaInfo;
                return View();
            }
            catch
            {
                return Content("查询分区出错！");
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
            try
            {
                quesArea[] quesAreaList = toolsHelpers.selectToolsController.selectQuesArea(x => x == x, u => u.areaId);
                if (quesAreaList.Length == 0 || quesAreaList == null)
                {
                    return Content("没有分区，请先增加一个分区！");
                }
                ViewBag.quesAreaList = quesAreaList;
                return View();
            }
            catch
            {
                return Content("查询论坛分区出错！");
            }
        }
        [HttpPost]
        public string quesAnswAreaDelete(quesArea area)
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
                //删除问答回复表中的信息，问答表中的信息和问答分区表中的信息
                if (toolsHelpers.deleteToolsController.deleteAllQuesAreaInfo(area.areaId) == true)
                {
                    Response.Redirect("/backStage/quesAnswManagement/quesAnswAreaIndex");
                    return "true";
                }
                return "false";
            }
            catch
            {
                return "问答分区删除出错！";
            }
        }
        #endregion
        #region 修改
        public ActionResult changeQuesAnswArea()
        {
            try
            {
                quesArea[] areaInfoList = toolsHelpers.selectToolsController.selectQuesArea(x => x == x, u => u.areaId);
                if (areaInfoList.Length == 0 || areaInfoList == null)
                {
                    return Content("没有这个分区！");
                }
                quesArea areaInfo = areaInfoList[0];
                ViewBag.areaInfo = areaInfo;
                return View();
            }
            catch
            {
                return Content("加载出错！");
            }
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

                if (toolsHelpers.updateToolsController.updateQuesArea(u => u.areaId == area.areaId, area) == true)
                {
                    Response.Redirect("/backStage/quesAnswManagement/quesAnswAreaIndex");
                    return "success";
                }
                return "修改失败";
            }
            catch
            {
                return "修改出错！";
            }
        }
        #endregion
        #endregion

        #region 问答帖子管理
        #region 增加

        public ActionResult quesAnswInvitationAdd()
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
                var quesArea = db.quesArea.ToList();
                if (quesArea != null)
                {
                    ViewBag.quesArea = quesArea;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
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
                    var quesAnswInfo = db.quesAnswInfo.Where(u => u.quesAnswId == info.quesAnswId).FirstOrDefault();
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
        [ValidateInput(false)]
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