using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class articleManagementController : Controller
    {
        // GET: backStage/articleManagement
        public ActionResult Index()
        {
            return View();
        }

        #region 资源文章分区管理
        public ActionResult areaManagement()
        {
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int Id = db.resourceArea.Max(u=>u.areaId);
                ViewBag.Id = Id + 1;
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
                        areaId = area.areaId,
                        areaName = area.areaName.Trim(),
                        areaBrief = area.areaBrief.Trim(),
                    };
                    db.resourceArea.Add(obResourceArea);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region 删除分区
        public ActionResult deleteArea()
        {
            return View();
        }
        [HttpPost]
        public string deleteArea(resourceArea area)
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
                //先看有没有这个分区
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {                    
                    var isExist = db.resourceArea.Where(u => u.areaName == area.areaName.Trim()). ToList();
                    if (isExist.Count == 0)
                    {
                        return "not find";
                    }               
                    //标记为删除状态
                    db.resourceArea.Remove(isExist.FirstOrDefault());
                    //执行删除的sql语句
                    db.SaveChanges();
                    return "succes";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region 查询分区
        public ActionResult findArea()
        {
            return View();
        }
        [HttpPost]
        public ActionResult findArea(string areaName)
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
                ViewBag.info = null;
                var info = db.resourceArea.Where(u => u.areaName == areaName.Trim()).ToList();
                if (info.Count == 0)
                {
                    return View("changeArea");     
                }
                resourceArea obArea = info.FirstOrDefault();
                ViewBag.info = obArea;
                return View("changeArea");
            }
        }
        #endregion

        #region 修改分区
        [HttpPost]
        public ActionResult changeArea(resourceArea area)
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
                var newInfo = db.resourceArea.Where(u => u.areaId == area.areaId).FirstOrDefault();
                newInfo.areaName = area.areaName;
                newInfo.areaBrief = area.areaBrief;
                db.SaveChanges();
                return Content("修改成功");
            }
            
        }
        #endregion

        #endregion

        #region 增加资源文章
        public ActionResult addArticle()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //获得所有分区列表
                var areaList = db.resourceArea.ToList();
                ViewBag.areaList = areaList;
            }
            return View();
        }
        [HttpPost]
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
                resource.resourceId = findId;
                resource.userId = Convert.ToInt32(cookieText);
                resource.resourceTime = DateTime.Now;
                resource.pageView = 0;
                //添加进数据库
                db.resourceInfo.Add(resource);
                db.SaveChanges();
                return Content("增加成功");
            }
        }
        #endregion
    }
}