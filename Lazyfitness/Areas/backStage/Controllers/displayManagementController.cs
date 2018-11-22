using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class displayManagementController : Controller
    {
        // GET: backStage/displayManagement
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

        #region 增加
        public ActionResult Add()
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
        public ActionResult displayAdd(int flag)
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
            ViewBag.flag = flag;
            return View();
        }
        [HttpPost]
        public string displayAddGo(serverShowInfo info)
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
                    var isareaId = db.serverShowInfo.Where(u => u.areaId == info.areaId).Where(u => u.flag == info.flag).Where(u=>u.title == info.title);
                    if (isareaId.ToList().Count != 0)
                    {
                        return "同类型资源标题已存在";
                    }
                    //轮播图
                    if(info.flag == 0)
                    {
                        serverShowInfo _info = new serverShowInfo
                        {
                            areaId = info.areaId,
                            title = info.title,
                            //图片
                            pictureAdr = info.pictureAdr,
                            url = info.url,
                            flag = 0,
                            Infostatus = info.Infostatus
                        };
                        db.serverShowInfo.Add(_info);
                    }
                    //公告
                    if (info.flag == 1)
                    {
                        serverShowInfo _info = new serverShowInfo
                        {
                            areaId = info.areaId,
                            title = info.title,
                            pictureAdr = null,
                            url = info.url,
                            flag = 1,
                            Infostatus = info.Infostatus
                        };
                        db.serverShowInfo.Add(_info);
                    }
                    db.SaveChanges();
                }
                return "内容增加成功";
            }
            catch
            {
                return ("内容增加失败（ERROR）");
            }
        }
        #endregion


        #region 查询
        public ActionResult displaySearch()
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
        public ActionResult showResult()
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
        public ActionResult displaySearch(serverShowInfo info)
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
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    if (info.title != null)
                    {
                        var _serverShowInfo = db.serverShowInfo.Where(u => u.areaId == info.areaId).Where(u => u.flag == info.flag).Where(u => u.title == info.title).ToList();
                        if (_serverShowInfo.Count != 0)
                        {
                            ViewBag.serverShowInfo = _serverShowInfo;
                        }
                    }
                    else
                    {
                        var _serverShowInfo = db.serverShowInfo.Where(u => u.areaId == info.areaId).Where(u => u.flag == info.flag).ToList();
                        if (_serverShowInfo.Count != 0)
                        {
                            ViewBag.serverShowInfo = _serverShowInfo;
                        }
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("showResult");
            }
            catch
            {
                return Content("查询失败！（ERROR）");
            }
        }
        #endregion


        #region 删除
        public ActionResult displayDelete()
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
        public string displayDelete(serverShowInfo info)
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

                    DbQuery<serverShowInfo> dbInfo = db.serverShowInfo.Where(u => u.id == info.id) as DbQuery<serverShowInfo>;
                    serverShowInfo _serverShowInfo = dbInfo.FirstOrDefault();
                    if (_serverShowInfo == null)
                    {
                        return "删除的内容不存在！";
                    }
                    db.Entry<serverShowInfo>(_serverShowInfo).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "删除成功";
                }
            }
            catch
            {
                return "删除失败！（ERROR）";
            }
        }
        #endregion


        #region 修改
        public ActionResult displayUpdate()
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
        public ActionResult displayUpdate(int id)
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
                serverShowInfo _serverShowInfo = db.serverShowInfo.Where(u => u.id == id).FirstOrDefault();
                if (_serverShowInfo != null)
                {
                    ViewBag.serverShowInfo = _serverShowInfo;
                }
            }
            return View();
        }
        [HttpPost]
        public string displayUpdateGo(serverShowInfo info)
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
                    serverShowInfo _serverShowInfo = db.serverShowInfo.Where(u => u.id == info.id).FirstOrDefault();
                    if (_serverShowInfo != null)
                    {
                        //将要修改的值，放到数据上下文中
                        //轮播图
                        if (info.flag == 0)
                        {
                            _serverShowInfo.title = info.title;
                            _serverShowInfo.pictureAdr = info.pictureAdr;
                            _serverShowInfo.url = info.url;
                            _serverShowInfo.Infostatus = info.Infostatus;
                        }
                        //公告
                        if (info.flag == 1)
                        {
                            _serverShowInfo.title = info.title;
                            _serverShowInfo.url = info.url;
                            _serverShowInfo.Infostatus = info.Infostatus;
                        }
                        db.SaveChanges(); //将修改之后的值保存到数据库中
                        return "修改成功";
                    }
                    return "无修改对象！";
                }
            }
            catch
            {
                return "修改失败(ERROR)";
            }
        }
        #endregion

        #region 上传图片

        /// <summary>
        /// 把图片上传到服务器并保存路径到数据库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveImage(serverShowInfo info)
        {
            string result = "";
            HttpPostedFileBase imageName = Request.Files["image"];// 从前台获取文件
            if(imageName ==null)
            {
                return Content("(error)未获取到文件");
            }
            string file = imageName.FileName;
            string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
            Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
            if (string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
            {
                result = "(error)文件格式支持(bmp)|(png)|(gif)|(jpg)|(jpeg)";
            }
            else
            {
                string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                string imageStr = "/upload/"; // 获取保存图片的项目文件夹
                string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
                string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                string saveFile = uploadPath + fileName;//文件路径
                imageName.SaveAs(saveFile);// 保存文件
                // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                string image = imageStr + fileName;// 设置数据库保存的路径

                ViewBag.pictureAdr = image;
                ViewBag.serverShowInfo = info;
                
                if (info.id == 0)
                {
                    ViewBag.flag = 0;
                    return View("displayAdd");
                }
                else
                {
                    return View("displayUpdate");
                }
            }
            return View("Index");
        }
        #endregion
    }
}