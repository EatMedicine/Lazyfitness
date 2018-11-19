using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Controllers
{

    public class APIController : Controller
    {
        #region 创建
        [HttpPost]
        [ValidateInput(false)]
        public string ArticleSubmit(string resourceName,string editor,int userId)
        {

            return "标题："+resourceName+"\n"+editor+"\n"+userId;
        }
    
        [HttpPost]
        [ValidateInput(false)]
        public string QuestionSubmit(int areaId,int userId,string title,
            string editor,int money)
        { 
            return areaId+"\n"+userId + "\n" +title + "\n" +editor + "\n" +money;
        }

        [HttpPost]
        [ValidateInput(false)]
        public string forumSubmit(int areaId, int userId, string title,
            string editor, int money)
        {
            return areaId + "\n" + userId + "\n" + title + "\n" + editor + "\n" + money;
        }
        #endregion
        #region 回复

        [HttpPost]
        [ValidateInput(false)]
        public string QuestionReply(int quesId,string reply)
        {
            return quesId + "\n" + reply;
        }

        [HttpPost]
        [ValidateInput(false)]
        public string forumReply(int quesId, string reply)
        {
            return quesId + "\n" + reply;
        }
        #endregion
        #region 获取数据
        #endregion
    }

}