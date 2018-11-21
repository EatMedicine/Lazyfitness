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
            HttpCookie cookie = filterContext.HttpContext.Response.Cookies["loginId"];
            if (cookie.Values.Count == 0)
            {           
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
            //判断是不是管理员的函数

        }
    }
}