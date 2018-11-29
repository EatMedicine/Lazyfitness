using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Filter;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class articleManagementController : Controller
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
        public resourceInfo[] GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<resourceInfo, bool>> whereLambda, Expression<Func<resourceInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.resourceInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
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

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public resourceArea[] GetPagedListArea<TKey>(int pageIndex, int pageSize, Expression<Func<resourceArea, bool>> whereLambda, Expression<Func<resourceArea, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.resourceArea.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }

        public int GetSumPageArea(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.resourceArea.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }

        #endregion

        // GET: backStage/articleManagement
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
            try
            {
                int sumPage = GetSumPage(10);
                int nowPage = 1;
                resourceInfo[] allInfo = GetPagedList(1, 10, x => x == x, u => u.userId);


                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                if (allInfo == null || allInfo.Length == 0)
                {
                    return View();
                }
                ArrayList areaNameList = new ArrayList();
                ArrayList userNameList = new ArrayList();

                foreach (var item in allInfo)
                {
                    //依次获得分区名
                    int areaId = item.areaId;
                    resourceArea[] areaName = toolsHelpers.selectToolsController.selectResourceArea(u => u.areaId == areaId, u => u.areaId);
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
        [HttpPost]
        public ActionResult Index(string id)
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
            try
            {
                int sumPage = GetSumPage(10);
                int nowPage = Convert.ToInt32(id);
                resourceInfo[] allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.userId);

                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                if (allInfo == null || allInfo.Length == 0)
                {
                    return View();
                }
                ArrayList areaNameList = new ArrayList();
                ArrayList userNameList = new ArrayList();
                foreach (var item in allInfo)
                {
                    //依次获得分区名
                    int areaId = item.areaId;
                    resourceArea[] areaName = toolsHelpers.selectToolsController.selectResourceArea(u => u.areaId == areaId, u => u.areaId);
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

        #region 资源文章分区管理
        public ActionResult areaManagement(string id)
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
            try
            {
                if (id == null)
                {
                    id = 1.ToString();
                }
                int nowPage = Convert.ToInt32(id);
                int sumPage = GetSumPageArea(10);
                resourceArea[] allInfo = GetPagedListArea(nowPage, 10, x => x == x, u => u.areaId);

                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                return View();
            }
            catch
            {
                return Content("显示文章分区名出错！");
            }
        }

        #region 增加分区
        public ActionResult addArea()
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
        public string addArea(resourceArea area)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //先检查分区名存不存在                
                    resourceArea[] isExistArea = toolsHelpers.selectToolsController.selectResourceArea(u => u.areaName == area.areaName, u => u.areaId);
                    if (isExistArea.Length != 0)
                    {
                        return "存在相同的名字";
                    }
                    resourceArea obResourceArea = new resourceArea
                    {
                        areaName = area.areaName.Trim(),
                        areaBrief = area.areaBrief.Trim(),
                    };
                    if (toolsHelpers.insertToolsController.insertResourceArea(obResourceArea) == true)
                    {
                        Response.Redirect("/backStage/articleManagement/areaManagement");
                        return "success";
                    }
                    return "增加分区失败";
                }
            }
            catch
            {
                return "增加分区出错";
            }
        }
        #endregion

        #region 删除分区
        public ActionResult deleteArea()
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
            try
            {
                resourceArea[] allInfo = toolsHelpers.selectToolsController.selectResourceArea(x => x == x, u => u.areaId);
                if (allInfo.Length == 0 || allInfo == null)
                {
                    return Content("没有分区，请添加一个分区");
                }
                ViewBag.allInfo = allInfo;
                return View();
            }
            catch
            {
                return Content("加载文章分区失败");
            }
            
        }
        [HttpPost]
        public string deleteArea(int areaId)
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
                //先看有没有这个分区
                resourceArea[] list = toolsHelpers.selectToolsController.selectResourceArea(u => u.areaId == areaId, u => u.areaId);
                if (list == null)
                {
                    return "没有找到此分区";
                }
                //如果找到这个分区开始删除分区下所有的信息
                if( toolsHelpers.deleteToolsController.deleteResourceArea(areaId) == true)
                {
                    Response.Redirect("/backStage/articleManagement/areaManagement");
                    return "success";
                }
                return "false";
            }
            catch
            {
                return "false";
            }
        }
        #endregion

        #region 查询分区
        public ActionResult findArea()
        {

            resourceArea[] allInfo = toolsHelpers.selectToolsController.selectResourceArea(x => x == x, u => u.areaId);
            if (allInfo.Length == 0 || allInfo == null)
            {
                return Content("没有分区，请添加分区！");
            }
            ViewBag.allInfo = allInfo;       
            return View();
        }
        #endregion

        #region 修改分区
        [HttpPost]
        public ActionResult changeArea(int areaId)
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

            resourceArea[] list = toolsHelpers.selectToolsController.selectResourceArea(u => u.areaId == areaId, u=>u.areaId);
            if (list == null)
            {
                return Content("没有找到此分区");
            }
            ViewBag.info = list[0];         
            return View();
        }


        [HttpPost]
        public ActionResult updateArea(resourceArea area)
        {
            try
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
                //判断有无此分区
                if (toolsHelpers.selectToolsController.selectResourceArea(u => u.areaId == area.areaId, u=>u.areaId).Length == 0)
                {
                    return Content("没有此分区");
                }
                //有分区的时候修改为当前值
                if (toolsHelpers.updateToolsController.updateResourceArea(u => u.areaId == area.areaId, area) == true)
                {
                    Response.Redirect("/backStage/articleManagement/areaManagement");
                    return Content("success");
                }
                return Content("修改失败");
            }
            catch
            {
                return Content("修改出错");
            }
            

        }
        #endregion

        #endregion

        #region 增加资源文章
        public ActionResult addArticle()
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
            resourceArea[] areaList = toolsHelpers.selectToolsController.selectResourceArea(x => x == x, u => u.areaId);
            if (areaList.Length == 0 | areaList == null)
            {
                return Content("还没有所属分区不能增加文章，请先添加分区！");
            }
            ViewBag.areaList = areaList;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addArticle(resourceInfo resource)
        {
            try
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

                if (toolsHelpers.selectToolsController.selectUserInfo(u=>u.userId == resource.userId, u=>u.userId).Length == 0)
                {
                    return Content("没有此用户，不能增加文章");
                }
                resource.resourceTime = DateTime.Now;
                resource.pageView = 0;
                if (toolsHelpers.insertToolsController.insertResourceInfo(resource) == true)
                {
                    Response.Redirect("/backStage/articleManagement/Index");
                    return Content("success");
                }
                return Content("增加失败");
            }
            catch
            {
                return Content("增加出错");
            }
        }
        #endregion

        #region 查询文章
        public ActionResult findArticle()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("~/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public ActionResult articleList(int resourceId)
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
            try
            {
                resourceInfo[] allInfo = toolsHelpers.selectToolsController.selectResourceInfo(u => u.resourceId == resourceId, u => u.resourceId);
                if (allInfo.Length == 0 || allInfo == null)
                {
                    return Content("没有此文章");
                }
                ArrayList areaNameList = new ArrayList();
                ArrayList userNameList = new ArrayList();

                foreach (var item in allInfo)
                {
                    //依次获得分区名
                    int areaId = item.areaId;
                    resourceArea[] areaName = toolsHelpers.selectToolsController.selectResourceArea(u => u.areaId == areaId, u => u.areaId);
                    //依次获得用户名
                    int userId = item.userId.Value;
                    userInfo[] userName = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == userId, u => u.userId);
                    if (areaName != null && areaName.Length != 0 && userName != null && userName.Length != 0)
                    {
                        areaNameList.Add(areaName[0].areaName);
                        userNameList.Add(userName[0].userName);
                    }
                }
                ViewBag.allInfo = allInfo;
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

        #region 删除文章
        [HttpPost]
        public string deleteArticle(int resourceId)
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
                if (toolsHelpers.deleteToolsController.deleteResourceInfo(u => u.resourceId == resourceId) == true)
                {
                    return "success";
                }
                return "false";
            }
            catch
            {
                return "false";
            }
            
        }
        #endregion

        #region 修改文章
        [HttpPost]
        public ActionResult changeArticle(int resourceId)
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
                //获取对应文章信息
                resourceInfo[] info = toolsHelpers.selectToolsController.selectResourceInfo(u => u.resourceId == resourceId, u => u.resourceId);
                if (info == null || info.Length == 0)
                {
                    return Content("没有此文章！");
                }
                ViewBag.allInfo = info[0];
                //获取全部分区信息
                resourceArea[] area = toolsHelpers.selectToolsController.selectResourceArea(x => x == x, u => u.areaId);
                if (area == null || area.Length == 0)
                {
                    return Content("没有分区，请添加至少一个分区！");
                }
                ViewBag.areaList = area;
                return View();       
            }
            catch
            {
                return Content("获取文章信息出错！");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult changeArticleInfo(resourceInfo resource)
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
                if (toolsHelpers.updateToolsController.updateResourceInfo(u => u.resourceId == resource.resourceId, resource) == true)
                {
                    Response.Redirect("/backStage/articleManagement/Index");
                    return Content("success");
                }
                return Content("修改失败！");
            }
            catch
            {
                return Content("修改出错！");
            }
            
        }
        #endregion
    }
}