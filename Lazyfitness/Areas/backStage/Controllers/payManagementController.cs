using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public List<userInfo> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<userInfo, bool>> whereLambda, Expression<Func<userInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.userInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.userInfo.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }


        public ActionResult userAccountManagement()
        {
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(1, 10, x => x == x, u => u.userId);
            return View();
        }

        [HttpPost]
        public ActionResult userAccountManagement(int id)
        {
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                return Content("未登录");
            }
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.userId);
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


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public List<recharge> GetPagedListCard<TKey>(int pageIndex, int pageSize, Expression<Func<recharge, bool>> whereLambda, Expression<Func<recharge, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.recharge.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }


        public int GetSumPageCard(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.recharge.ToList().Count;
                return ((listSum / pageSize) + 1);
            }
        }

        public ActionResult refillCardManagement()
        {
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPageCard(10);
            ViewBag.allInfo = GetPagedListCard(1, 10, x => x == x, u => u.rechargeId);
            return View();
        }
        [HttpPost]
        public ActionResult refillCardManagement(int id)
        {
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                return Content("未登录");
            }
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPageCard(10);
            ViewBag.allInfo = GetPagedListCard(Convert.ToInt32(id), 10, x => x == x, u => u.rechargeId);
            return View();
        }
        #region 查看卡号
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