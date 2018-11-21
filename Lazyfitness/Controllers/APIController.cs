using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Controllers
{

    public class APIController : Controller
    {
        #region 创建
        [HttpPost]
        [ValidateInput(false)]
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
        public string QuestionSubmit(int areaId,int userId,string title, string editor,int money)
        {
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
        public string forumSubmit(int areaId, int userId, string title, string editor, int money)
        {
            postInfo pInfo = new postInfo
            {
                areaId = areaId,
                postTitle = title,
                userId = userId,
                postTime = DateTime.Now,
                pageView = 0,
                isPost = 0,
                amount = money,
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
        public string forumReply(int quesId, string reply, int userId)
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