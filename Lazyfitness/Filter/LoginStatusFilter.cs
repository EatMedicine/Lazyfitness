using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Filter
{
    /// <summary>
    /// 登录过滤
    /// </summary>
    public class LoginStatusFilter:ActionFilterAttribute
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
            if (certificateTools.IsCookieEmpty(loginIdCookie)==false||
                certificateTools.IsCookieEmpty(userIdCookie)==false||
                certificateTools.IsCookieEmpty(certificationCookie)==false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
            string userId = userIdCookie.Value;
            string certifcation = certificationCookie.Value;
            if (certificateTools.verifyCertification(userId, certifcation) == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
            filterContext.Controller.ViewBag.UserId = userId;

        }

        /// <summary>
        /// OnActionExecuted:Action执行时但还未返回结果时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        /// <summary>
        /// OnResultExecuting:OnResultExecuting也和OnActionExecuted一样，但前者是在后者执行完后才执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        /// <summary>
        /// OnResultExecuted:是Action执行完后将要返回ActionResult的时候执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }
    }
}