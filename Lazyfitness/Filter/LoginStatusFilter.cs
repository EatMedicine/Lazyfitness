using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Filter
{
    public class LoginStatusFilter:ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting:正要准备执行Action的时候但还未执行时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = filterContext.HttpContext.Response.Cookies["loginId"];
            if (cookie == null)
            {
                filterContext.Controller.ViewBag.IsLogin = false;
            }
            else
            {
                filterContext.Controller.ViewBag.IsLogin = true;
                int userId = Int32.Parse(filterContext.HttpContext.Response.Cookies["loginId"].Value);
                filterContext.Controller.ViewBag.UserName = Tools.GetUserName(userId);
                filterContext.Controller.ViewBag.UserId = userId;
            }
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