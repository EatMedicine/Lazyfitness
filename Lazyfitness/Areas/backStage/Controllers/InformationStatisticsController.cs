using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class InformationStatisticsController : Controller
    {
        // GET: backStage/InformationStatistics
        public ActionResult Index()
        {
            return View();
        }

        #region 用户信息统计
        public ActionResult userInfoStatistics()
        {
            //需要增加验证是否登录
            try
            {
                //获取用户总数量
                userInfo[] allUser = toolsHelpers.selectToolsController.selectUserInfo(x => x == x, u => u.userId);
                int totallUserNumber = allUser.Length;

                //获取禁言用户数量
                userInfo[] ZeroUser = toolsHelpers.selectToolsController.selectUserInfo(u=>u.userStatus == 0, u => u.userId);
                int totallZeroUser = ZeroUser.Length;

                //获取注册会员数量
                userInfo[] OneUser = toolsHelpers.selectToolsController.selectUserInfo(u => u.userStatus == 1, u => u.userId);
                int totallOneUser = OneUser.Length;

                //获取正式会员数量
                userInfo[] TwoUser = toolsHelpers.selectToolsController.selectUserInfo(u => u.userStatus == 2, u => u.userId);
                int totallTwoUser = TwoUser.Length;
                
                //获取管理员数量
                userInfo[] ThreeUser = toolsHelpers.selectToolsController.selectUserInfo(u => u.userStatus == 3, u => u.userId);
                int totallThreeUser = ThreeUser.Length;

                ViewBag.totallUserNumber = totallUserNumber;
                ViewBag.totallZeroUser = totallZeroUser;
                ViewBag.totallOneUser = totallOneUser;
                ViewBag.totallTwoUser = totallTwoUser;
                ViewBag.totallThreeUser = totallThreeUser;

                return View();
            }
            catch
            {
                return Content("获取数据失败！");
            }
        }
        #endregion

        #region 文章信息统计
        public ActionResult articleInfo()
        {
            try
            {
                //获取文章分区数量
                resourceArea[] allArea = toolsHelpers.selectToolsController.selectResourceArea(x => x == x, u => u.areaId);
                int areaNumber = allArea.Length;
                //获取文章数量
                resourceInfo[] allResource = toolsHelpers.selectToolsController.selectResourceInfo(x => x == x, u => u.resourceId);
                int resourceNumer = allResource.Length;

                ViewBag.areaNumber = areaNumber;
                ViewBag.resourceNumer = resourceNumer;
                return View();
            }
            catch
            {
                return Content("获取数据失败！");
            }
        }
        #endregion

        #region 论坛信息统计
        public ActionResult forumInfo()
        {
            try
            {
                //获取论坛分区数量
                postArea[] allArea = toolsHelpers.selectToolsController.selectPostArea(x => x == x, u => u.areaId);
                int areaNumber = allArea.Length;
                //获取论坛数量
                postInfo[] allPost = toolsHelpers.selectToolsController.selectPostInfo(x => x == x, u => u.postId);
                int postNumber = allPost.Length;

                ViewBag.areaNumber = areaNumber;
                ViewBag.postNumber = postNumber;
                return View();
            }
            catch
            {
                return Content("获取数据失败！");
            }
        }
        #endregion

        #region 问答信息统计
        public ActionResult quesAnswInfo()
        {
            try
            {
                //获取问答分区数量
                quesArea[] allArea = toolsHelpers.selectToolsController.selectQuesArea(x => x == x, u => u.areaId);
                int areaNumber = allArea.Length;
                //获取问答帖子数量
                quesAnswInfo[] allQuesAnsw = toolsHelpers.selectToolsController.selectQuesAnswInfo(x=>x == x, u=>u.quesAnswId);
                int quesAnswNumber = allQuesAnsw.Length;

                ViewBag.areaNumber = areaNumber;
                ViewBag.quesAnswNumber = quesAnswNumber;
                return View();
            }
            catch
            {
                return Content("获取数据失败！");
            }
        }
        #endregion

        #region 评论信息统计
        public ActionResult commentInfo()
        {
            try
            {
                //获取论坛的评论总数
                postReply[] allForumReply = toolsHelpers.selectToolsController.selectPostReply(x => x == x, u => u.replyTime);
                int forumRelpyNumber = allForumReply.Length;

                //获取问答评论总数
                quesAnswReply[] allQuesAnswReply = toolsHelpers.selectToolsController.selectQuesAnswReply(x => x == x, u => u.replyTime);
                int quesAnswNumber = allForumReply.Length;

                ViewBag.forumRelpyNumber = forumRelpyNumber;
                ViewBag.quesAnswNumber = quesAnswNumber;
                return View();
            }
            catch
            {
                return Content("获取数据失败！");
            }
        }
        #endregion

        #region 充值卡信息统计
        public ActionResult rechargeInfo()
        {
            try
            {
                //总的充值卡
                recharge[] allRecharge = toolsHelpers.selectToolsController.selectRecharge(x => x == x);
                int rechargeNumber = allRecharge.Length;
                //可用的充值卡
                recharge[] availableRecharge = toolsHelpers.selectToolsController.selectRecharge(u => u.isAvailable == 1);
                int availableNumber = availableRecharge.Length;

                ViewBag.rechargeNumber = rechargeNumber;
                ViewBag.availableNumber = availableNumber;
                return View();
            }
            catch
            {
                return Content("获取数据失败！");
            }
        }
        #endregion
    }
}