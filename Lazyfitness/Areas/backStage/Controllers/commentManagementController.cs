using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Lazyfitness.Filter;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class commentManagementController : Controller
    {
        [BackStageFilter]
        public ActionResult Index()
        {
            return View();
        }
        #region 分页类
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public postReply[] GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<postReply, bool>> whereLambda, Expression<Func<postReply, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.postReply.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }

        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.postReply.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public quesAnswReply[] GetPagedListQues<TKey>(int pageIndex, int pageSize, Expression<Func<quesAnswReply, bool>> whereLambda, Expression<Func<quesAnswReply, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.quesAnswReply.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }

        public int GetSumPageQues(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.quesAnswReply.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }
        #endregion
        #region 论坛评论管理
        //论坛评论管理
        [BackStageFilter]
        public ActionResult forumComment()
        {
            return View();
        }
        #region 增加论坛评论
        [BackStageFilter]
        public ActionResult addForumComment()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public ActionResult sureForum(int postId)
        {
            try
            {
                //有此帖子时候返回帖子所属分区名，帖子的作者，帖子的信息

                //先返回帖子信息
                postInfo[] infoList = toolsHelpers.selectToolsController.selectPostInfo(u => u.postId == postId, u => u.postId);
                //查看有无此贴
                if (infoList == null || infoList.Length == 0)
                {
                    return Content("没有这篇帖子");
                }
                //得到分区名
                int areaId = infoList[0].areaId;
                postArea[] areaList = toolsHelpers.selectToolsController.selectPostArea(u => u.areaId == areaId, u => u.areaId);
                //得到帖子作者
                int userId = infoList[0].userId.Value;
                userInfo[] userList = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == userId, u => u.userId);
                if (areaList == null || userList == null || areaList.Length == 0 || userList.Length == 0)
                {
                    return Content("此片论坛帖子存在错误信息不可读，请及时删除此论坛帖子");
                }
                postInfo forumInfo = infoList[0];
                string areaName = areaList[0].areaName;
                string ownerName = userList[0].userName;
                ViewBag.areaName = areaName;
                ViewBag.ownerName = ownerName;
                ViewBag.forumInfo = forumInfo;

                return View();
            }
            catch
            {
                return Content("获取论坛帖子信息出错！");
            }
           
        }
        [HttpPost]
        [BackStageFilter]
        [ValidateInput(false)]
        public ActionResult add(postReply info)
        {
            try
            {
                //先判断有无回复的userId
                if (toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == info.userId, u => u.userId).Length == 0)
                {
                    return Content("回复者不存在，请重新输入回复者Id");
                }
                //判断有误此postId
                if (toolsHelpers.selectToolsController.selectPostInfo(u => u.postId == info.postId, u => u.postId).Length == 0)
                {
                    return Content("此论坛不存在！");
                }
                info.replyTime = DateTime.Now;
                //判断插入是否成功
                if (toolsHelpers.insertToolsController.insertPostReply(info) == true)
                {
                    return Content("增加评论成功！");
                }
                return Content("增加评论失败！");
            }
            catch
            {
                return Content("增加评论出错！");
            }

        }
        #endregion

        [BackStageFilter]
        #region 删除评论
        public ActionResult deleteForumComment()
        {
            return View();
        }

        [HttpPost]
        [BackStageFilter]
        public ActionResult sureReply(int postId, string id)
        {
            try
            {
                //先判断此postId是否存在
                if (toolsHelpers.selectToolsController.selectPostInfo(u=>u.postId == postId, u=>u.postId).Length == 0)
                {
                    return Content("没有此文章！");
                }              
                if (id == null)
                {
                    id = 1.ToString();
                }
                int sumPage = GetSumPage(10);
                int nowPage = Convert.ToInt32(id);
                postReply[] allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.replyTime);


                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                ViewBag.rightPostId = postId;
                return View();
            }
            catch
            {
                return Content("查找论坛帖子出错！");
            }
        }

        [HttpPost]
        [BackStageFilter]
        public string delete(int id)
        {
            try
            {
                if (toolsHelpers.deleteToolsController.deletePostReply(u=>u.id == id) == true)
                {
                    return "T";
                }
                return "F";
            }
            catch
            {
                return "NF";
            }
            
        }
        #endregion

        #endregion

        #region 问答评论管理
        [BackStageFilter]
        public ActionResult quesAnswComment()
        {
            return View();
        }

        //增加回答
        [BackStageFilter]
        public ActionResult addQueAnswComment()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public ActionResult sureAnswer(int quesAnswId)
        {
            try
            {
                //有此帖子时候返回帖子所属分区名，帖子的作者，帖子的信息

                //先返回帖子信息
                quesAnswInfo[] infoList = toolsHelpers.selectToolsController.selectQuesAnswInfo(u => u.quesAnswId == quesAnswId, u => u.quesAnswId);
                //查看有无此贴
                if (infoList == null || infoList.Length == 0)
                {
                    return Content("没有这篇问答帖子");
                }
                //得到分区名
                int areaId = infoList[0].areaId;
                quesArea[] areaList = toolsHelpers.selectToolsController.selectQuesArea(u => u.areaId == areaId, u => u.areaId);
                //得到帖子作者
                int userId = infoList[0].userId.Value;
                userInfo[] userList = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == userId, u => u.userId);
                if (areaList == null || userList == null || areaList.Length == 0 || userList.Length == 0)
                {
                    return Content("此片问答帖子存在错误信息不可读，请及时删除此问答帖子");
                }
                quesAnswInfo quesAnswInfo = infoList[0];
                string areaName = areaList[0].areaName;
                string ownerName = userList[0].userName;
                ViewBag.areaName = areaName;
                ViewBag.ownerName = ownerName;
                ViewBag.quesAnswInfo = quesAnswInfo;

                return View();
            }
            catch
            {
                return Content("获取问答帖子信息出错！");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [BackStageFilter]
        public ActionResult addAnswer(quesAnswReply info)
        {
            try
            {
                //先判断有无回复的userId
                if (toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == info.userId, u => u.userId).Length == 0)
                {
                    return Content("回复者不存在，请重新输入回复者Id");
                }
                //判断有误此postId
                if (toolsHelpers.selectToolsController.selectQuesAnswInfo(u => u.quesAnswId == info.quesAnswId, u => u.quesAnswId).Length == 0)
                {
                    return Content("此论坛不存在！");
                }
                info.replyTime = DateTime.Now;
                info.isAgree = 0;
                //判断插入是否成功
                if (toolsHelpers.insertToolsController.insertQuesAnswReply(info) == true)
                {
                    return Content("增加回答成功！");
                }
                return Content("增加回答失败！");
            }
            catch
            {
                return Content("增加回答出错！");
            }
        }

        //删除回答
        [BackStageFilter]
        public ActionResult deleteQueAnswComment()
        {
            return View();
        }
        [HttpPost]
        [BackStageFilter]
        public ActionResult sureReplyAnswer(int quesAnswId, string id)
        {
            try
            {
                //先判断此quesAnswId是否存在
                if (toolsHelpers.selectToolsController.selectQuesAnswInfo(u => u.quesAnswId == quesAnswId, u => u.quesAnswId).Length == 0)
                {
                    return Content("没有此问答帖子！");
                }
                if (id == null)
                {
                    id = 1.ToString();
                }
                int sumPage = GetSumPageQues(10);
                int nowPage = Convert.ToInt32(id);
                quesAnswReply[] allInfo = GetPagedListQues(Convert.ToInt32(id), 10, x => x == x, u => u.replyTime);


                ViewBag.nowPage = nowPage;
                ViewBag.sumPage = sumPage;
                ViewBag.allInfo = allInfo;
                ViewBag.rightQuesAnswId = quesAnswId;
                return View();
            }
            catch
            {
                return Content("查找论坛帖子出错！");
            }
        }
        [HttpPost]
        [BackStageFilter]
        public string deleteAnswer(int id)
        {
            try
            {
                if (toolsHelpers.deleteToolsController.deleteQuesAnswReply(u => u.id == id) == true)
                {
                    return "T";
                }
                return "F";
            }
            catch
            {
                return "NF";
            }
        }
        #endregion
    }
}