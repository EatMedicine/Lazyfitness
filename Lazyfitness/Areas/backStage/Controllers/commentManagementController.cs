using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class commentManagementController : Controller
    {
        // GET: backStage/commentManagement
        public ActionResult Index()
        {
            return View();
        }

        #region 论坛区评论管理
        #region 屏蔽
        public ActionResult forumcommentShield()
        {
            return View();
        }

        [HttpPost]
        public string forumcommentShield(postReply reply)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postReply> dbReply = db.postReply.Where(u => u.postId == reply.postId).Where(u => u.userId == reply.userId) as DbQuery<postReply>;
                    postReply _postReply = dbReply.FirstOrDefault();
                    //通过帖子ID和评论者ID查询评论信息
                    if (_postReply == null)
                    {
                        return ("评论信息不存在!");
                    }
                    _postReply.replyContent = "黑猫警长已经查办!";
                    db.SaveChanges();
                }
                return ("屏蔽成功");
            }
            catch
            {
                return ("屏蔽操作失败");
            }
        }
        #endregion
        #region 删除
        public ActionResult forumcommentDelete()
        {
            return View();
        }

        [HttpPost]
        public string forumcommentDelete(postReply reply)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postReply> dbReply = db.postReply.Where(u => u.postId == reply.postId).Where(u => u.userId == reply.userId) as DbQuery<postReply>;
                    postReply _postReply = dbReply.FirstOrDefault();
                    //通过帖子ID和评论者ID查询评论信息
                    if (_postReply == null)
                    {
                        return ("评论信息不存在!");
                    }
                    db.Entry<postReply>(_postReply).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                    return ("删除成功");
            }
            catch
            {
                return ("删除操作失败");
            }
        }
        #endregion
        #endregion

        #region 问答区评论管理
        #region 屏蔽
        public ActionResult quesAnswcommentShield()
        {
            return View();
        }

        [HttpPost]
        public string quesAnswcommentShield(quesAnswReply reply)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswReply> dbReply = db.quesAnswReply.Where(u => u.quesAnswId == reply.quesAnswId).Where(u => u.userId == reply.userId) as DbQuery<quesAnswReply>;
                    quesAnswReply _quesAnswReply = dbReply.FirstOrDefault();
                    //通过帖子ID和评论者ID查询评论信息
                    if (_quesAnswReply == null)
                    {
                        return ("评论信息不存在");
                    }
                    _quesAnswReply.replyContent = "黑猫警长已经查办";
                    db.SaveChanges();
                }
                    return ("屏蔽成功");
            }
            catch
            {
                return ("屏蔽操作失败");
            }
        }
        #endregion
        #region 删除
        public ActionResult quesAnswcommentDelete()
        {
            return View();
        }

        [HttpPost]
        public string quesAnswcommentDelete(quesAnswReply reply)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswReply> dbReply = db.quesAnswReply.Where(u => u.quesAnswId == reply.quesAnswId).Where(u => u.userId == reply.userId) as DbQuery<quesAnswReply>;
                    quesAnswReply _quesAnswReply = dbReply.FirstOrDefault();
                    //通过帖子ID和评论者ID查询评论信息
                    if(_quesAnswReply == null)
                    {
                        return ("评论信息不存在!");
                    }
                    db.Entry<quesAnswReply>(_quesAnswReply).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                    return ("删除成功");
            }
            catch
            {
                return ("删除操作失败");
            }
        }
        #endregion
        #endregion
    }
}