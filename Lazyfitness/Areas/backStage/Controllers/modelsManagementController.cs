using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Filter;
using System.Linq.Expressions;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class modelsManagementController : Controller
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
        public serverShowInfo[] GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<serverShowInfo, bool>> whereLambda, Expression<Func<serverShowInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.serverShowInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }

        public int GetSumPage(int pageSize, Expression<Func<serverShowInfo, bool>> whereLambda)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.serverShowInfo.Where(whereLambda).ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }
        #endregion
        [BackStageFilter]
        // GET: backStage/modelsManagement
        public ActionResult Index()
        {
            return View();
        }

        #region 增加
        [BackStageFilter]
        public ActionResult modelsPicAdd_Hello()
        {
            return View();
        }
        [BackStageFilter]
        public ActionResult modelsPicAdd_Index()
        {
            return View();
        }
        [BackStageFilter]
        public ActionResult modelsSlgAdd()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public ActionResult modelsAddInfo(serverShowInfo info)
        {
            try
            {
                if (toolsHelpers.insertToolsController.insertServerShowInfo(info) == true)
                {
                    Response.Redirect("/backStage/modelsManagement/Index");
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
        [BackStageFilter]
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
                return View("Index");
            }
            try
            {
                    int sumPage = GetSumPage(10, u =>  u.flag == 2);
                    serverShowInfo[] info = GetPagedList(Convert.ToInt32(1), 10, x => x == x, u => u.flag == 2);
                    if (info == null || info.Length == 0)
                    {
                        return Content("没有此展示！");
                    }
                    ViewBag.nowPage = 1;
                    ViewBag.sumPage = sumPage;
                    ViewBag.allInfo = info;
                    return View();
            }
            catch
            {
                return Content("查询失败！（ERROR）");
            }
        }
        #endregion


        #region 删除
        [HttpPost]
        [BackStageFilter]
        public string modelsDelete(serverShowInfo info)
        {
            try
            {
                if (toolsHelpers.deleteToolsController.deleteServerShowInfo(u => u.id == info.id) == true)
                {
                    Response.Redirect("/backStage/modelsManagement/showResult");
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
                    string imageStr = "/Resource/picture/"; // 获取保存图片的项目文件夹
                    string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                    string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
                    string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                    string saveFile = uploadPath + fileName;//文件路径
                    imageName.SaveAs(saveFile);// 保存文件
                    string image = imageStr + fileName;// 设置数据库保存的路径

                    ViewBag.pictureAdr = image;
                    ViewBag.serverShowInfo = info;
                    if (info.areaId == 1)
                    {
                        return View("modelsPicAdd_Index");
                    }
                    else
                    {
                        return View("modelsPicAdd_Hello"); ;
                    }
                }
            }
            catch
            {
                return Content("上传出错！");
            }
        }
        #endregion
    }
}