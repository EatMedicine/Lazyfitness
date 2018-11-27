using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Filter;
using Lazyfitness.Models;
namespace Lazyfitness.Controllers
{

    public class APIController : Controller
    {
        #region 创建
        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public void ArticleSubmit(string title,string editor,int userId, int areaId)
        {
            resourceInfo rInfo = new resourceInfo
            {
                areaId = 1,      
                resourceTime = DateTime.Now,
                pageView = 0,
                isTop = 0,
                resourceName = title,
                resourceContent = editor,
                userId = 1
            };
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                db.resourceInfo.Add(rInfo);
                db.SaveChanges();
                Response.Redirect(Url.Action("ArticlePart", "Home", new { partId = areaId }));
            }
        }
    
        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public void QuestionSubmit(int areaId,int userId,string title, string editor,int money)
        {
            userInfo info = Tools.GetUserInfo(userId);
            if (info.userAccount == null)
                info.userAccount = 0;
            if (info.userAccount < money)
                money = (int)info.userAccount;
            else
            {
                using(LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbsearch = db.userInfo.Where(u => u.userId == userId) as DbQuery<userInfo>;
                    userInfo _userinfo = dbsearch.FirstOrDefault();
                    _userinfo.userAccount -= money;
                    db.SaveChanges();
                }
            }
            quesAnswInfo qaInfo = new quesAnswInfo
            {
                areaId = areaId,
                quesAnswTitle = title,
                userId = userId,
                quesAnswTime = DateTime.Now,
                pageView = 0,
                isPost = 0,
                quesAnswStatus = 1,               
                amount = money,
                quesAnswContent = editor
            };
            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                db.quesAnswInfo.Add(qaInfo);
                db.SaveChanges();
                Response.Redirect(Url.Action("QuestionPart", "Home", new { partId = areaId }));
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public void forumSubmit(int areaId, int userId, string title, string editor)
        {
            postInfo pInfo = new postInfo
            {
                areaId = areaId,
                postTitle = title,
                userId = userId,
                postTime = DateTime.Now,
                pageView = 0,
                isPost = 0,
                postStatus = 1,
                postContent = editor
            };
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                db.postInfo.Add(pInfo);
                db.SaveChanges();
                Response.Redirect(Url.Action("forumPart", "Home", new { partId = areaId }));
            }
        }
        #endregion
        #region 回复

        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public void QuestionReply(int quesId,string reply, int userId)
        {
            quesAnswReply qarInfo = new quesAnswReply
            {
                quesAnswId = quesId,
                userId = userId,
                replyTime = DateTime.Now,
                replyContent = reply,
                isAgree = 0
            };
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                db.quesAnswReply.Add(qarInfo);
                db.SaveChanges();
                Response.Redirect(Url.Action("QuestionDetail", "Home", new { num=quesId }));
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public void forumReply(int quesId, string reply,int userId)
        {
            postReply prInfo = new postReply
            {
                postId = quesId,
                userId = userId,
                replyTime = DateTime.Now,
                replyContent = reply
            };
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                db.postReply.Add(prInfo);
                db.SaveChanges();
                Response.Redirect(Url.Action("forumDetail", "Home", new { num = quesId }));
            }
        }
        #endregion
        #region 获取数据
        #endregion
    }

}