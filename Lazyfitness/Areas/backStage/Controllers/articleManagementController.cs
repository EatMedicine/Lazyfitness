using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class articleManagementController : Controller
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
        public List<resourceInfo> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<resourceInfo, bool>> whereLambda, Expression<Func<resourceInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.resourceInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.resourceInfo.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }


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
            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPage(10);            
            ViewBag.allInfo = GetPagedList(1, 10,x => x == x, u => u.userId);
            var allInfo = GetPagedList(1, 10, x => x == x, u => u.userId);
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
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPage(10);
            var allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.userId);
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

        #region 资源文章分区管理
        public ActionResult areaManagement()
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
                var allInfo = db.resourceArea.ToList();
                ViewBag.allInfo = allInfo;
            }
            return View();
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
                    var isExist = db.resourceArea.Where(u => u.areaName == area.areaName.Trim()).ToList();
                    if (isExist.Count != 0)
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var allInfo = db.resourceArea.ToList();
                ViewBag.allInfo = allInfo;
            }
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
                if (toolsHelpers.selectToolsController.isExistResourceArea(u => u.areaId == area.areaId) == false)
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //获得所有分区列表
                var areaList = db.resourceArea.ToList();
                ViewBag.areaList = areaList;
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addArticle(resourceInfo resource)
        {
            string cookieText = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //先找到resourceId
                int findId;
                if (db.resourceInfo.ToList().Count == 0)
                {
                    findId = 1;
                }
                else
                {
                    findId = db.resourceInfo.Max(u => u.resourceId) + 1;
                }
                //通过登录的name找到userId
                var userId = Int32.Parse(cookieText);
                resource.resourceId = findId;
                resource.userId = userId;
                resource.resourceTime = DateTime.Now;
                resource.pageView = 0;
                //添加进数据库
                db.resourceInfo.Add(resource);
                db.SaveChanges();
                return Content("增加成功");
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
        public ActionResult findArticleList(string id, string resourceName)
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
            ViewBag.sumPage = GetSumPage(10);
            var allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x.resourceName == resourceName.Trim(), u => u.userId);
            ViewBag.allInfo = allInfo;
            ViewBag.resourceName = resourceName;

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

        /*1111111111111111111111111111111111111111111111*/
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var obDelete = db.resourceInfo.Where(u => u.resourceId == resourceId).FirstOrDefault();
                //标记为删除对象
                db.resourceInfo.Remove(obDelete);
                //执行删除sql语句
                db.SaveChanges();
                return "success";
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                resourceInfo obResource = db.resourceInfo.Where(u => u.resourceId == resourceId).FirstOrDefault();
                ViewBag.allInfo = obResource;

                var areaList = db.resourceArea.ToList();
                ViewBag.areaList = areaList;

                var senderName = db.userInfo.Where(u => u.userId == obResource.userId).FirstOrDefault().userName;
                ViewBag.senderName = senderName;
                return View();
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var obResource = db.resourceInfo.Where(u => u.resourceId == resource.resourceId).FirstOrDefault();
                obResource.areaId = resource.areaId;
                obResource.resourceName = resource.resourceName;
                obResource.pageView = resource.pageView;
                obResource.isTop = resource.isTop;
                obResource.resourceContent = resource.resourceContent;
                db.SaveChanges();
                return Content("修改成功!");
            }
        }
        #endregion
    }
}