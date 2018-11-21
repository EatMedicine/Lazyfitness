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
        public string ArticleSubmit(string title,string editor,int userId, int areaId)
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
                return "T";
            }
        }
    
        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public string QuestionSubmit(int areaId,int userId,string title, string editor,int money)
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
                quesAnswStatus = 0,               
                amount = money,
                quesAnswContent = editor
            };
            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                db.quesAnswInfo.Add(qaInfo);
                db.SaveChanges();
                return "T";
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public string forumSubmit(int areaId, int userId, string title, string editor)
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
                return "T";
            }
        }
        #endregion
        #region 回复

        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public string QuestionReply(int quesId,string reply, int userId)
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
                return "T";
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [LoginStatusFilter]
        public string forumReply(int quesId, string reply,int userId)
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
                return "T";
            }
        }
        #endregion
        #region 获取数据
        #endregion
    }

}