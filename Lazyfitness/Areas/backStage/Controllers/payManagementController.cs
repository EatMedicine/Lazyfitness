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
        #region 账户清零
        [HttpPost]
        public string resetAccount(int userId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var allInfo = db.userInfo.Where(u => u.userId == userId);
                userInfo obInfo = allInfo.FirstOrDefault();
                obInfo.userAccount = 0;
                db.SaveChanges();
                return "success";
            }
        }
        #endregion

        #endregion

        #region 充值卡管理
        public ActionResult refillCardManagement()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var allList = db.recharge.ToList();
                ViewBag.allList = allList;
                return View();
            }
        }
        #region 查看卡号
        public ActionResult findCardId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult findCardId(string rechargeId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var allInfo = db.recharge.Where(u => u.rechargeId == rechargeId.Trim());
                if (allInfo.ToList().Count == 0)
                {
                    return Content("没有此卡");
                }
                ViewBag.allInfo = allInfo.FirstOrDefault();
            }
            return View();
        }
        #endregion
        #region 更改充值卡信息
        public ActionResult changeCardInfo(recharge charge)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbCharge = db.recharge.Where(u => u.rechargeId == charge.rechargeId.Trim());
                recharge obCharge = dbCharge.FirstOrDefault();
                obCharge.rechargePwd = charge.rechargePwd;
                obCharge.amount = charge.amount;
                obCharge.isAvailable = charge.isAvailable;
                db.SaveChanges();
                return Content("修改成功");
            }
        }
        #endregion
        #region 清空不可用卡号
        public string resetUnAvailable()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var deleteList = db.recharge.Where(u => u.isAvailable == 0).ToList();
                db.recharge.RemoveRange(deleteList);
                db.SaveChanges();
                return "success";
            }
        }
        #endregion
        #endregion
    }
}