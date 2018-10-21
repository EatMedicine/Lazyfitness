using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class userManagementController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: backStage/manage
        public ActionResult add()
        {
            return View();
        }
        //[HttpPost]
        //public string add(userSecurity security)
        //{
        //    using (LazyfitnessEntities db = new LazyfitnessEntities())
        //    {
        //        //先判断登录Id是否可用
        //        var isLoginId = db.userSecurity.Where(u => u.loginId == security.loginId.Trim());
        //        if (isLoginId.ToList().Count != 0)
        //        {
        //            return "存在";
        //        }
        //        //当用户名可用时候，进行用户的增加

        //    }
        //}

        public ActionResult delete()
        {
            //显示用户
            ViewBag.allData = null;
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                List<userInfo> data = db.userInfo.ToList<userInfo>();
                ViewBag.allData = data;
            }
            return View();
        }
        [HttpPost]
        public string delete(string userName)
        {
            //根据不可重复的用户名找到userSecurity里面的userId,将其删除
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                try
                {
                    DbQuery<userInfo> dbInfo = db.userInfo.Where(u => u.userName == userName.Trim()) as DbQuery<userInfo>;
                    userInfo obInfo = dbInfo.FirstOrDefault();

                    //创建一个要删除的对象
                    userSecurity deleSecurity = new userSecurity
                    {
                        userId = obInfo.userId,
                    };
                    //附加到ef中
                    db.userSecurity.Attach(deleSecurity);
                    //标记为删除--标记当前对象为删除状态
                    db.userSecurity.Remove(deleSecurity);
                    db.SaveChanges();
                    return "删除成功";
                }
                catch
                {
                    return "删除失败";
                }
            }
        }
    }
}