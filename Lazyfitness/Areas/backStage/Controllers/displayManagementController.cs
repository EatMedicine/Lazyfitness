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
        public ActionResult displayAdd(serverShowInfo serverShowInfo)
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
            ViewBag.serverShowInfo = serverShowInfo;
            return View();
        }
        [HttpPost]
        public ActionResult displayAddInfo(serverShowInfo info)
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
                if (toolsHelpers.insertToolsController.insertServerShowInfo(info) == true)
                {
                    Response.Redirect("/backStage/displayManagement/Index");
                    return Content("success");
                }
                return Content("增加失败!");
            }
            catch
            {
                return Content("内容增加失败（ERROR）");
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
        public ActionResult displaySearch(serverShowInfo serverShowInfo)
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
                if(serverShowInfo.title == null)
                {
                    serverShowInfo[] info = toolsHelpers.selectToolsController.selectServerShowInfo(u => u.areaId == serverShowInfo.areaId && u.flag == serverShowInfo.flag);
                    if (info == null || info.Length == 0)
                    {
                        return Content("没有此展示！");
                    }
                    ViewBag.allInfo = info;
                    //Response.Redirect("/backStage/displayManagement/showResult");
                    return View("showResult");
                }
                else
                {
                    serverShowInfo[] info = toolsHelpers.selectToolsController.selectServerShowInfo(u => u.areaId == serverShowInfo.areaId && u.flag == serverShowInfo.flag && u.title == serverShowInfo.title);
                    if (info == null || info.Length == 0)
                    {
                        return Content("没有此展示！");
                    }
                    ViewBag.allInfo = info;
                    //Response.Redirect("/backStage/displayManagement/showResult");
                    return View("showResult");
                }
                return Content("查询成功");
            }
            catch
            {
                return Content("查询失败！（ERROR）");
            }
        }
        #endregion


        #region 删除
        //public ActionResult displayDelete()
        //{
        //    if (Request.Cookies["managerId"] != null)
        //    {
        //        //获取Cookies的值
        //        HttpCookie cookieName = Request.Cookies["managerId"];
        //        var cookieText = Server.HtmlEncode(cookieName.Value);
        //    }
        //    else
        //    {
        //        return Content("未登录");
        //    }
        //    return View();
        //}
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
                if(toolsHelpers.deleteToolsController.deleteServerShowInfo(u=>u.id == info.id) == true)
                {
                    Response.Redirect("/backStage/displayManagement/displaySearch");
                    return "success";
                }
                else
                {
                    return "不存在删除内容！";
                }
            }
            catch
            {
                return "删除失败！（ERROR）";
            }
        }
        #endregion


        #region 修改
        //public ActionResult displayUpdate()
        //{
        //    if (Request.Cookies["managerId"] != null)
        //    {
        //        //获取Cookies的值
        //        HttpCookie cookieName = Request.Cookies["managerId"];
        //        var cookieText = Server.HtmlEncode(cookieName.Value);
        //    }
        //    else
        //    {
        //        return Content("未登录");
        //    }
        //    return View();
        //}
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
            try
            {
                //获取对应充值卡信息
                serverShowInfo[] info = toolsHelpers.selectToolsController.selectServerShowInfo(u => u.id == id);
                if (info == null || info.Length == 0)
                {
                    return Content("没有此展示内容！");
                }
                ViewBag.serverShowInfo = info[0];
                return View();
            }
            catch
            {
                return Content("进入修改信息界面出错！");
            }
        }
        [HttpPost]
        public string displayUpdateInfo(serverShowInfo serverShowInfo)
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
                if (toolsHelpers.updateToolsController.updateServerShowInfo(u => u.id == serverShowInfo.id, serverShowInfo) == true)
                {
                    Response.Redirect("/backStage/displayManagement/displaySearch");
                    return "success";
                }
                return "修改失败!";
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
            try
            {
                HttpPostedFileBase imageName = Request.Files["image"];// 从前台获取文件
                if (imageName == null)
                {
                    return Content("(error)未获取到文件");
                }
                string file = imageName.FileName;
                string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
                Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
                if (string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
                {
                    return Content("(error)文件格式支持(bmp)|(png)|(gif)|(jpg)|(jpeg)");
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
                    string image = imageStr + fileName;// 设置数据库保存的路径

                    ViewBag.pictureAdr = image;
                    ViewBag.serverShowInfo = info;
                    if(info.id != 0)
                    {
                        /*Response.Redirect("/backStage/displayManagement/displayUpdate")*/
                        return View("displayUpdate");
                    }
                    else
                    {
                        /*Response.Redirect("/backStage/displayManagement/displayAdd")*/
                        return View("displayAdd");;
                    }
                }
                return Content("上传失败！");
            }
            catch
            {
                return Content("上传出错！");
            }
        }
        #endregion
    }
}