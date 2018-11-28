using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class commentManagementController : Controller
    {
        public ActionResult Index()
        {

            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
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
        #endregion
        #region 论坛评论管理
        //论坛评论管理
        public ActionResult forumComment()
        {

            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        #region 增加论坛评论
        public ActionResult addForumComment()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public ActionResult sureForum(int postId)
        {

            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

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
        [ValidateInput(false)]
        public ActionResult add(postReply info)
        {
            string cookieText = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                cookieText = Server.HtmlEncode(cookieName.Value).ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
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

        #region 删除评论
        public ActionResult deleteForumComment()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }

        [HttpPost]
        public ActionResult sureReply(int postId, string id)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

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
                postReply[] allInfo = GetPagedList(Convert.ToInt32(id), 10, x => x == x, u => u.userId);


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
        public string delete(int id)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    var dbReply = db.postReply.Where(u => u.id == id);
                    var obReply = dbReply.FirstOrDefault();
                    db.postReply.Remove(obReply);
                    db.SaveChanges();
                    return "T";
                }
            }
            catch
            {
                return "F";
            }
            
        }
        #endregion

        #endregion

        #region 问答评论管理
        public ActionResult quesAnswComment()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            return View();
        }

        //增加回答
        public ActionResult addQueAnswComment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sureAnswer(int quesAnswId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbQues = db.quesAnswInfo.Where(u => u.quesAnswId == quesAnswId);
                if (dbQues == null)
                {
                    return Content("没有这个问题");
                }
                var obQuesInfo = dbQues.FirstOrDefault();
                if (obQuesInfo != null)
                {
                    string areaName = db.quesArea.Where(u => u.areaId == obQuesInfo.areaId).FirstOrDefault().areaName;
                    string ownerName = db.userInfo.Where(u => u.userId == obQuesInfo.userId).FirstOrDefault().userName;
                    ViewBag.areaName = areaName;
                    ViewBag.ownerName = ownerName;
                    ViewBag.obQuesInfo = obQuesInfo;
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addAnswer(int quesAnswId, string replyContent)
        {
            string cookieText = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                cookieText = Server.HtmlEncode(cookieName.Value).ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {

                var getUserId = Int32.Parse(cookieText);

                quesAnswReply obReply = new quesAnswReply
                {
                    quesAnswId = quesAnswId,
                    userId = getUserId,
                    replyTime = DateTime.Now,
                    replyContent = replyContent,
                    isAgree = 0
                };
                db.quesAnswReply.Add(obReply);
                db.SaveChanges();
                return Content("T");
            }
        }

        //删除回答
        public ActionResult deleteQueAnswComment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sureReplyAnswer(int quesAnswId)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var replyList = db.quesAnswReply.Where(u => u.quesAnswId == quesAnswId).ToList();
                ViewBag.replyList = replyList;
                return View();
            }
        }
        [HttpPost]
        public string deleteAnswer(int id)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    var dbReply = db.quesAnswReply.Where(u => u.id == id);
                    var obReply = dbReply.FirstOrDefault();
                    db.quesAnswReply.Remove(obReply);
                    db.SaveChanges();
                    return "T";
                }
            }
            catch
            {
                return "F";
            }
        }
        #endregion
    }
}