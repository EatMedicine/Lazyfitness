using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Filter
{
    public class FirstRedirectFilter : ActionFilterAttribute
    {

        /// <summary>
        /// OnActionExecuting:正要准备执行Action的时候但还未执行时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string isFirst = WebConfigHelper.GetAppSetting("FirstOpen");
            if (isFirst == "true")
            {
                string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
                filterContext.HttpContext.Response.Write(string.Format(js, "系统未初始化，请完成初始化设置", "/Home/Welcome"));
                filterContext.HttpContext.Response.End();
            }



        }
    }
}