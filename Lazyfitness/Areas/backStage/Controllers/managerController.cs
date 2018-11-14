using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{    
    public class managerController : Controller
    {
        #region 后台用户登录
        // GET: backStage/userManager
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public string login(int managerId, string managerPwd)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<backManager> dbManager = db.backManager.Where(u => u.managerId == managerId) as DbQuery<backManager>;
                    backManager obManager = dbManager.FirstOrDefault();
                    string MD5Pwd = MD5Helper.MD5Helper.encrypt(managerPwd.Trim());
                    if (obManager == null)
                    {
                        //没有用户名
                        return "没有此用户名";
                    }
                    DbQuery<backManager> dbManagerPwd = db.backManager.Where(u => u.managerId == managerId).Where(u => u.managerPwd == MD5Pwd) as DbQuery<backManager>;
                    backManager obSurePwd = dbManagerPwd.FirstOrDefault();
                    if (obSurePwd != null)
                    {
                        HttpCookie cookieName = new HttpCookie("managerId");
                        cookieName.Value = managerId.ToString().Trim();
                        cookieName.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(cookieName);
                        //登录成功
                        Response.Redirect("/backStage/manager/Index");
                        return "success";
                    }
                    else
                    {
                        //密码错误
                        return "密码错误";
                    }
                }
            }
            catch
            {
                return "登录失败";
            }
        }
        #endregion

        #region 登录后默认的界面
        public ActionResult Index()
        {
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText;
            }
            else
            {
                return Content("未登录");
            }
           
            return View();
        }
        
        #endregion

        #region 判断用户是否登录
        public string backStageIsLogin()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                return cookieText.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 退出登录
        public string loginOut()
        {
            if (backStageIsLogin() != null)
            {
                //把cookie的过期时间设置为马上过期               
                Response.Cookies.Add(CookiesHelper.CookiesHelper.clearCookie("managerId"));
                Response.Redirect("/backStage/manager/login");
                return "success";
            }
            else
            {
                return "F";
            }
        }
        #endregion
    }

}