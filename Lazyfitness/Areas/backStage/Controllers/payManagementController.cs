using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class payManagementController : Controller
    {
        // GET: backStage/payManagement
        public ActionResult Index()
        {
            return View();
        }

        #region 用户账户管理
        public ActionResult userAccountManagement()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var allList = db.userInfo.ToList();
                ViewBag.allList = allList;
                return View();
            }
        }
        #endregion

        #region 充值卡管理
        public ActionResult refillCardManagement()
        {
            return View();
        }
        #region 查询用户
        [HttpPost]
        public ActionResult findUser(string userName)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //先查询有无此人
                var isExist = db.userInfo.Where(u => u.userName == userName.Trim()).ToList();
                if (isExist.Count == 0)
                {
                    return Content("无此用户");
                }
                ViewBag.allInfo = isExist.FirstOrDefault();
                return View();
            }
        }
        #endregion
        #region 修改信息
        [HttpPost]
        public string changeAccount(int userId, int userAccount)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var allInfo = db.userInfo.Where(u => u.userId == userId);
                userInfo Info = allInfo.FirstOrDefault();
                Info.userAccount = userAccount;
                db.SaveChanges();
                return "修改成功";
            }
        }
        #endregion
        #endregion
    }
}