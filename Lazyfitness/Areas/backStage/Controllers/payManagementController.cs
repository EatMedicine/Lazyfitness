using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using Lazyfitness.Filter;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class payManagementController : Controller
    {
        // GET: backStage/payManagement
        [BackStageFilter]
        public ActionResult Index()
        {

            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPageCard(10);
            ViewBag.allInfo = GetPagedListCard(1, 10, x => x == x, u => u.rechargeId);
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public ActionResult Index(int id)
        {
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
        [BackStageFilter]
        public ActionResult addCard()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public ActionResult addCard(recharge recharge)
        {
            try
            {
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
        [BackStageFilter]
        public ActionResult searchCard()
        {
            return View();
        }
        //详细充值卡信息界面
        [BackStageFilter]
        public ActionResult changeCard()
        {
            return View();
        }
        //查询对应序列号的充值卡
        [HttpPost]
        [BackStageFilter]
        public ActionResult changeCard(string rechargeId)
        {
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
        [BackStageFilter]
        public ActionResult changeCardInfo(recharge recharge)
        {
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
        [BackStageFilter]
        public ActionResult deleteCard()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public string deleteCard(string rechargeId)
        {
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