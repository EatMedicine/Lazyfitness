using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Controllers
{

    public class APIController : Controller
    {
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
    }

}