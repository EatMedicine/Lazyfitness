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
        public string ArticleSubmit(string resourceName,string editor)
        {

            return "标题："+resourceName+"\n"+editor;
        }
    }
}