using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class managerController : Controller
    {
        // GET: backStage/userManager
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public string login(backManager manager)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<backManager> dbManager = db.backManager.Where(u => u.managerId == manager.managerId) as DbQuery<backManager>;
                    backManager obManager = dbManager.FirstOrDefault();
                    string MD5Pwd = MD5Helper.MD5Helper.encrypt(manager.managerPwd.Trim());
                    if (obManager == null)
                    {
                        return "无效用户名";
                    }
                    DbQuery<backManager> dbManagerPwd = db.backManager.Where(u => u.managerId == manager.managerId).Where(u => u.managerPwd == MD5Pwd) as DbQuery<backManager>;
                    backManager obSurePwd = dbManagerPwd.FirstOrDefault();
                    if (obSurePwd != null)
                    {
                        HttpCookie cookieName = new HttpCookie("managerId");
                        cookieName.Value = manager.managerId.ToString();
                        cookieName.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(cookieName);
                        return "登录成功";
                    }
                    else
                    {
                        return "密码错误";
                    }
                }
        }
            catch
            {
                return "登录失败";
            }
}
    }
}