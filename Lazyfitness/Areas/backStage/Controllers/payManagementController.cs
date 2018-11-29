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
        public ActionResult Index(int id)
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
            
            int sumPage = GetSumPageCard(10);
            if (sumPage <= id)
            {
                id = sumPage;
            }
            if (id <= 0)
            {
                id = 1;
            }
            int nowPage = id;
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPageCard(10);
            ViewBag.allInfo = GetPagedListCard(Convert.ToInt32(id), 10, x => x == x, u => u.rechargeId);
            return View();
        }

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
        #region 增加充值卡
        public ActionResult addCard()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public ActionResult addCard(recharge recharge)
        {
            try
            {
                if (Request.Cookies["managerId"] != null)
                {
                    //获取Cookies的值
                    HttpCookie cookieName = Request.Cookies["managerId"];
                    var cookieText = Server.HtmlEncode(cookieName.Value);
                }
                else
                {
                    return Content("未登录");
                }
                if (toolsHelpers.selectToolsController.selectRecharge(u => u.rechargeId == recharge.rechargeId).Length != 0)
                {
                    return Content("已存在此序列号充值卡！");
                }
                if (toolsHelpers.insertToolsController.insertRecharge(recharge) == true)
                {
                    Response.Redirect("/backStage/payManagement/Index");
                    return Content("success");
                }
                return Content("增加失败");
            }
            catch
            {
                return Content("增加出错");
            }
        }
        #endregion
        #region 修改充值卡信息
        public ActionResult searchCard()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            return View();
        }
        //详细充值卡信息界面
        public ActionResult changeCard()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            return View();
        }
        //查询对应序列号的充值卡
        [HttpPost]
        public ActionResult changeCard(string rechargeId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                //获取对应充值卡信息
                recharge[] info = toolsHelpers.selectToolsController.selectRecharge(u => u.rechargeId == rechargeId);
                if (info == null || info.Length == 0)
                {
                    return Content("没有此充值卡！");
                }
                ViewBag.allInfo = info[0];
                return View();
            }
            catch
            {
                return Content("获取充值卡信息出错！");
            }
        }
        //修改提交信息
        [HttpPost]
        public ActionResult changeCardInfo(recharge recharge)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                //获取对应充值卡信息
                recharge[] info = toolsHelpers.selectToolsController.selectRecharge(u => u.rechargeId == recharge.rechargeId);
                if (info == null || info.Length == 0)
                {
                    return Content("没有此充值卡！");
                }
                if (toolsHelpers.updateToolsController.updateRecharge(u => u.rechargeId == recharge.rechargeId, recharge) == true)
                {
                    Response.Redirect("/backStage/payManagement/Index");
                    return Content("success");
                }
                return Content("修改失败!");
            }
            catch
            {
                return Content("修改充值卡信息出错！");
            }
        }

        #endregion
        #region 删除充值卡
        public ActionResult deleteCard()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public string deleteCard(string rechargeId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                if (toolsHelpers.deleteToolsController.deleteRecharge(u => u.rechargeId == rechargeId) == true)
                {
                    Response.Redirect("/backStage/payManagement/Index");
                    return "success";
                }
                else
                {
                    return "不存在充值卡！";
                }
            }
            catch
            {
                return "删除错误！";
            }
        }
        #endregion
        
        #endregion
    }
}