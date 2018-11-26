using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Filter
{
    public class BackStageFilter : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting:正要准备执行Action的时候但还未执行时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取cookie
            HttpCookie managerIdCookie = HttpContext.Current.Request.Cookies.Get("managerId");
            HttpCookie managerCertificationCookie = HttpContext.Current.Request.Cookies.Get("managerCertification");
            //验证是否为空
            if (certificateTools.IsCookieEmpty(managerIdCookie) == false ||
                certificateTools.IsCookieEmpty(managerCertificationCookie) == false)
            {
                filterContext.HttpContext.Response.Redirect("/BackStage/Manager/Login");
            }
            //获取值
            string managerId = managerIdCookie.Value;
            string certifcation = managerCertificationCookie.Value;
            //验证凭证是否对应
            if (certificateTools.verifyCertification(managerId, certifcation) == false)
            {
                filterContext.HttpContext.Response.Redirect("/BackStage/Manager/Login");
            }
            //判断是不是管理员的函数
            if (certificateTools.IsBackStageManager(managerId) == false)
                filterContext.HttpContext.Response.Redirect("/BackStage/Manager/Login");
            filterContext.Controller.ViewBag.managerId = managerId;
        }

    }
}