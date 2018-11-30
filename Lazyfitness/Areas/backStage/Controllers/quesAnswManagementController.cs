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

         
            int sumPage = GetSumPage(10);
            if (sumPage <= id)
            {
                id = sumPage;
            }
            if (id <= 0)
            {
                id = 1;
            }
            int nowPage = id;
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
               
                int sumPage = GetSumPagequesAnsw(10);
                if (sumPage <= id)
                {
                    id = sumPage;
                }
                if (id <= 0)
                {
                    id = 1;
                }
                int nowPage = id;
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
            try
            {
                //获取论坛有的分区
                quesArea[] areaList = toolsHelpers.selectToolsController.selectQuesArea(x => x == x, u => u.areaId);
                if (areaList.Length == 0 || areaList == null)
                {
                    return Content("增加问答帖子需要有分区，现在没有分区，请先添加一个分区再发帖！");
                }
                ViewBag.quesArea = areaList;
                return View();
            }
            catch
            {
                return Content("获取发问答帖需要的分区出错！");
            }
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
                if (toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == info.userId, u => u.userId).Length == 0)
                {
                    return "没有此用户，不能增加论坛帖子";
                }
                info.quesAnswTime = DateTime.Now;
                info.pageView = 0;
                if (toolsHelpers.insertToolsController.insertQuesAnswInfo(info) == true)
                {
                    Response.Redirect("/backStage/quesAnswManagement/quesAnswInvitationIndex");
                    return "success";
                }
                return "false";

            }
            catch
            {
                return ("论坛帖子增加出错！");
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
            return View();
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

                //判断postId是否存在
                if (toolsHelpers.selectToolsController.selectQuesAnswInfo(u => u.quesAnswId == info.quesAnswId, u => u.quesAnswId).Length == 0)
                {
                    return "此问答帖子不存在！";
                }
                if (toolsHelpers.deleteToolsController.deleteAllQuesAnswInfo(info.quesAnswId) == true)
                {
                    Response.Redirect("/backStage/quesAnswManagement/quesAnswInvitationIndex");
                    return "succes";
                }
                return "删除失败！";
            }
            catch
            {
                return "论坛帖子删除出错！";
            }
        }
        #endregion
        #region 修改
        [HttpPost]    
        public ActionResult quesAnswInvitationUpdate(quesAnswInfo info)
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
                //读取数据
                quesAnswInfo[] infoList = toolsHelpers.selectToolsController.selectQuesAnswInfo(u => u.quesAnswId == info.quesAnswId, u => u.quesAnswId);
                if (infoList == null || infoList.Length == 0)
                {
                    return Content("没有此问答帖子！");
                }
                //获取分区列表
                quesArea[] areaList = toolsHelpers.selectToolsController.selectQuesArea(x => x == x, u => u.areaId);
                if (areaList == null || areaList.Length == 0)
                {
                    return Content("没有分区，请至少添加一个分区！");
                }
                ViewBag.quesArea = areaList;
                ViewBag.allInfo = infoList[0];
                return View();
            }
            catch
            {
                return Content("查询问答帖子出错！");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult changeQuesAnswInfo(quesAnswInfo info)
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
                return Content("未登录");
            }


            try
            {
                if (toolsHelpers.updateToolsController.updateQuesAnswInfo(u => u.quesAnswId == info.quesAnswId, info) == true)
                {
                    Response.Redirect("/backStage/quesAnswManagement/quesAnswInvitationIndex");
                    return Content("success");
                }
                return Content("修改问答帖子失败！");
            }
            catch
            {
                return Content("修改问答帖子出错！");
            }
        }
        #endregion
        #endregion

    }
}