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
using Lazyfitness.Filter;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class forumManagementController : Controller
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
        public postArea[] GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<postArea, bool>> whereLambda, Expression<Func<postArea, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.postArea.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.postArea.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
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
        public postInfo[] GetPagedListpost<TKey>(int pageIndex, int pageSize, Expression<Func<postInfo, bool>> whereLambda, Expression<Func<postInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.postInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }

        public int GetSumPagepost(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.postInfo.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }
        #endregion
        [BackStageFilter]
        public ActionResult Index()
        {
            return View();
        }
        #region 论坛分区主页
        // GET: backStage/forumManagement
        //论坛分区主页
        [BackStageFilter]
        public ActionResult forumAreaIndex()
        {

            int nowPage = 1;
            int sumPage = GetSumPage(10);
            postArea[] allInfo = GetPagedList(1, 10, x => x == x, u => u.areaId);
            ViewBag.nowPage = nowPage;
            ViewBag.sumPage = sumPage;
            ViewBag.allInfo = allInfo;

            return View();
        }
        [HttpPost]
        [BackStageFilter]
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
            postArea[] allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.areaId);
            ViewBag.nowPage = nowPage;
            ViewBag.sumPage = sumPage;
            ViewBag.allInfo = allInfo;
            return View();
        }
        #endregion

        #region 论坛主页
        [BackStageFilter]
        public ActionResult forumInvitationIndex()
        {

            try
            {
               
                int postsumPage = GetSumPagepost(10);
                int nowPage = 1;
                postInfo[] allInfo = GetPagedListpost(1, 10, x => x == x, u => u.userId);

                ViewBag.nowPage = nowPage;
                ViewBag.postsumPage = postsumPage;
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
                    postArea[] areaName = toolsHelpers.selectToolsController.selectPostArea(u => u.areaId == areaId, u => u.areaId);
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
        [BackStageFilter]
        public ActionResult forumInvitationIndex(int id)
        {
            try
            {
           
                int postsumPage = GetSumPagepost(10);
                if (postsumPage <= id)
                {
                    id = postsumPage;
                }
                if (id <= 0)
                {
                    id = 1;
                }
                int nowPage = id;
                postInfo[] allInfo = GetPagedListpost(id, 10, x => x == x, u => u.userId);

                ViewBag.nowPage = nowPage;
                ViewBag.postsumPage = GetSumPagepost(10);
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
                    postArea[] areaName = toolsHelpers.selectToolsController.selectPostArea(u => u.areaId == areaId, u => u.areaId);
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

        #region 论坛分区管理
        #region 增加
        [BackStageFilter]
        public ActionResult forumAreaAdd()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public string forumAreaAdd(postArea area)
        {
            try
            {
                //检查是否存在相同的分区名
                if (toolsHelpers.selectToolsController.selectPostArea(u=>u.areaName == area.areaName, u=>u.areaId).Length != 0)
                {
                    return "存在相同的论坛分区名";
                }
                if (toolsHelpers.insertToolsController.insertPostArea(area) == true)
                {
                    Response.Redirect("/backStage/forumManagement/forumAreaIndex");
                    return "success";
                }
                return "增加论坛分区失败";
            }
            catch
            {
                return ("增加论坛分区出错");
            }
        }
        #endregion
        #region 查询
        [BackStageFilter]
        public ActionResult forumAreaSearch()
        {
            try
            {
                postArea[] areaInfo = toolsHelpers.selectToolsController.selectPostArea(x => x == x, u => u.areaId);
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
        [BackStageFilter]
        public ActionResult forumAreaDelete()
        {
            try
            {
                postArea[] forumAreaList = toolsHelpers.selectToolsController.selectPostArea(x => x == x, u => u.areaId);
                if (forumAreaList.Length == 0 || forumAreaList == null)
                {
                    return Content("没有分区，请先增加一个分区！");
                }
                ViewBag.forumAreaList = forumAreaList;
                return View();
            }
            catch
            {
                return Content("查询论坛分区出错！");
            }
        }
        [HttpPost]
        [BackStageFilter]
        public string forumAreaDelete(postArea area)
        {
            try
            {               
                //删除论坛回复表中的信息，论坛表中的信息和论坛分区表中的信息
                if (toolsHelpers.deleteToolsController.deleteAllPostAreaInfo(area.areaId) == true)
                {
                    Response.Redirect("/backStage/forumManagement/forumAreaIndex");
                    return "true";
                }
                return "false";
            }
            catch
            {
                return "论坛分区删除出错！";
            }
        }
        #endregion
        #region 修改
        [BackStageFilter]
        public ActionResult changeforumArea(int areaId)
        {
            try
            {
                postArea[] areaInfoList = toolsHelpers.selectToolsController.selectPostArea(x => x == x, u => u.areaId);
                if (areaInfoList.Length == 0 || areaInfoList == null)
                {
                    return Content("没有这个分区！");
                }
                postArea areaInfo = areaInfoList[0];
                ViewBag.areaInfo = areaInfo;
                return View();
            }
            catch
            {
                return Content("加载出错！");
            }
        }
        [HttpPost]
        [BackStageFilter]
        public string forumAreaUpdate(postArea area)
        {
            try
            {

                if (toolsHelpers.updateToolsController.updatePostArea(u=>u.areaId == area.areaId, area) == true)
                {
                    Response.Redirect("/backStage/forumManagement/forumAreaIndex");
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

        #region 论坛帖子管理
        #region 增加
        [BackStageFilter]
        public ActionResult forumInvitationAdd()
        {
            try
            {
                //获取论坛有的分区
                postArea[] areaList = toolsHelpers.selectToolsController.selectPostArea(x => x == x, u => u.areaId);
                if(areaList.Length == 0 || areaList == null)
                {
                    return Content("增加论坛帖子需要有分区，现在没有分区，请先添加一个分区再发帖！");
                }
                ViewBag.postArea = areaList;
                return View();
            }
            catch
            {
                return Content("获取发帖需要的分区出错！");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        [BackStageFilter]
        public string forumInvitationAdd(postInfo info)
        {
            try
            {
                if (toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == info.userId, u => u.userId).Length == 0)
                {
                    return "没有此用户，不能增加论坛帖子";
                }
                info.postTime = DateTime.Now;
                info.pageView = 0;
                if (toolsHelpers.insertToolsController.insertPostInfo(info) == true)
                {
                    Response.Redirect("/backStage/forumManagement/forumInvitationIndex");
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
        [BackStageFilter]
        public ActionResult forumInvitationSearch()
        {        
            return View();
        }

        #endregion
        #region 删除
        [BackStageFilter]
        public ActionResult forumInvitationDelete()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public string forumInvitationDelete(int postId)
        {
            try
            {

                //判断postId是否存在
                if (toolsHelpers.selectToolsController.selectPostInfo(u => u.postId == postId, u => u.postId).Length == 0)
                {
                    return "此论坛帖子不存在！";
                }
                if (toolsHelpers.deleteToolsController.deleteAllPostInfo(postId) == true)
                {
                    Response.Redirect("/backStage/forumManagement/forumInvitationIndex");
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
        [BackStageFilter]
        public ActionResult forumInvitationUpdate(int postId)
        {
            try
            {
                //读取数据
                postInfo[] infoList = toolsHelpers.selectToolsController.selectPostInfo(u => u.postId == postId, u => u.postId);
                if (infoList == null || infoList.Length == 0)
                {
                    return Content("没有此论坛帖子！");
                }
                //获取分区列表
                postArea[] areaList = toolsHelpers.selectToolsController.selectPostArea(x => x == x, u => u.areaId);
                if (areaList == null || areaList.Length == 0)
                {
                    return Content("没有分区，请至少添加一个分区！");
                }
                ViewBag.postArea = areaList;
                ViewBag.allInfo = infoList[0];
                return View();
            }
            catch
            {
                return Content("查询论坛帖子出错！");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        [BackStageFilter]
        public ActionResult changeForum(postInfo info)
        {
            try
            {
                if (toolsHelpers.updateToolsController.updatePostInfo(u => u.postId == info.postId, info) == true)
                {
                    Response.Redirect("/backStage/forumManagement/forumInvitationIndex");
                    return Content("success");
                }
                return Content("修改论坛帖子失败！");
            }
            catch
            {
                return Content("修改论坛帖子出错！");
            }
            
        }
        #endregion
        #endregion
    }
}