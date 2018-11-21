using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Filter
{
    /// <summary>
    /// 管理员过滤
    /// </summary>
    public class AdminFilter:ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting:正要准备执行Action的时候但还未执行时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie loginIdCookie = HttpContext.Current.Request.Cookies.Get("loginId");
            HttpCookie userIdCookie = HttpContext.Current.Request.Cookies.Get("userId");
            HttpCookie certificationCookie = HttpContext.Current.Request.Cookies.Get("certification");
            if (certificateTools.IsCookieEmpty(loginIdCookie) == false ||
                certificateTools.IsCookieEmpty(userIdCookie) == false ||
                certificateTools.IsCookieEmpty(certificationCookie) == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
            string userId = userIdCookie.Value;
            string certifcation = certificationCookie.Value;
            if (certificateTools.verifyCertification(userId, certifcation) == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
            //判断是不是管理员的函数
            if (certificateTools.IsAdmin(userId) == false)
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            filterContext.Controller.ViewBag.UserId = userId;
        }
    }
}