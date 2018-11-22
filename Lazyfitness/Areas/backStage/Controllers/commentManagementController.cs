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
        public ActionResult Index()
        {
            return View();
        }
        #region 论坛评论管理
        //论坛评论管理
        public ActionResult forumComment()
        {
            return View();
        }
        //增加论坛评论
        public ActionResult addForumComment()
        {
            return View();
        }
        #endregion
        //问答评论管理
        public ActionResult quesAnswComment()
        {
            return View();
        }
    }
}