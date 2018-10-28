using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;

namespace Lazyfitness.Areas.backStage.Controllers
{
    public class forumManagementController : Controller
    {
        // GET: backStage/forumManagement
        public ActionResult Index()
        {
            return View();
        }

        #region 论坛分区管理
        #region 增加
        public ActionResult forumAreaAdd()
        {
            return View();
        }
        [HttpPost]
        public string forumAreaAdd(postArea area)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //先判断登录Id是否可用
                    var isareaId = db.postArea.Where(u => u.areaId == area.areaId);
                    if (isareaId.ToList().Count != 0)
                    {
                        return "论坛分区已存在";
                    }
                    postArea _area = new postArea
                    {
                        areaId = area.areaId,
                        areaName = area.areaName,
                        areaBrief = area.areaBrief
                    };
                    db.postArea.Add(_area);
                    db.SaveChanges();
                }
                return "论坛分区增加成功";
            }
            catch
            {
                return ("论坛分区增加失败");
            }
        }
        #endregion
        #region 查询
        public ActionResult forumAreaSearch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forumAreaSearch(postArea area)
        {
            try
            {
                //先查询
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postArea> dbAreasearch = db.postArea.Where(u => u.areaName == area.areaName) as DbQuery<postArea>;
                    postArea _postArea = dbAreasearch.FirstOrDefault();
                    if (_postArea != null)
                    {
                        ViewBag.areaId = _postArea.areaId;
                        ViewBag.areaName = _postArea.areaName;
                        ViewBag.areaBrief = _postArea.areaBrief;
                    }
                    else
                    {
                        return View("forumAreaUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("forumAreaUpdate");
            }
            catch
            {
                return View("forumAreaUpdate");
            }
        }
        #endregion
        #region 删除
        public ActionResult forumAreaDelete()
        {
            return View();
        }
        [HttpPost]
        public string forumAreaDelete(postArea area)
        {
            try
            {
                //根据不可重复的用户名找到postArea里面的areaName,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<postArea> dbArea = db.postArea.Where(u => u.areaName == area.areaName.Trim()) as DbQuery<postArea>;
                    postArea _postArea = dbArea.FirstOrDefault();
                    if (_postArea == null)
                    {
                        return "删除的论坛分区不存在";
                    }
                    db.Entry<postArea>(_postArea).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "论坛分区删除成功";
                }
            }
            catch
            {
                return "论坛分区删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult forumAreaUpdate()
        {
            return View();
        }
        [HttpPost]
        public string forumAreaUpdate(postArea area)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postArea> dbArea = db.postArea.Where(u => u.areaId == area.areaId) as DbQuery<postArea>;
                    postArea _postArea = dbArea.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _postArea.areaId = area.areaId;
                    _postArea.areaName = area.areaName;
                    _postArea.areaBrief = area.areaBrief;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "论坛分区修改成功";
            }
            catch
            {
                return "论坛分区修改失败";
            }
        }
        #endregion
        #endregion

        #region 论坛帖子管理
        #region 增加

        public ActionResult forumInvitationAdd()
        {
            return View();
        }
        [HttpPost]
        public string forumInvitationAdd(postInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //先判断登录Id是否可用
                    var isareaId = db.postArea.Where(u => u.areaId == info.areaId);
                    var ispostId = db.postInfo.Where(u => u.postId == info.postId);
                    if (isareaId.ToList().Count == 0)
                    {
                        return "论坛分区不存在";
                    }
                    if (ispostId.ToList().Count != 0)
                    {
                        return "论坛帖子ID已存在";
                    }
                    postInfo _info = new postInfo
                    {
                        areaId = info.areaId,
                        postId = info.postId,
                        postTitle = info.postTitle,
                        userId = info.userId,
                        postTime = DateTime.Now,
                        pageView = 0,
                        isPost = info.isPost,
                        isPay = info.isPay,
                        amount = info.amount,
                        postStatus = info.postStatus,
                        postContent = info.postContent
                    };
                    db.postInfo.Add(_info);
                    db.SaveChanges();
                }
                return "论坛帖子增加成功";
            }
            catch
            {
                return ("论坛帖子增加失败");
            }
        }
        #endregion
        #region 查询
        public ActionResult forumInvitationSearch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forumInvitationSearch(postInfo info)
        {
            try
            {
                //先查询,后修改
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dbInvitationsearch = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postinfo = dbInvitationsearch.FirstOrDefault();
                    if (_postinfo != null)
                    {
                        ViewBag.areaId = _postinfo.areaId;
                        ViewBag.postId = _postinfo.postId;
                        ViewBag.postTitle = _postinfo.postTitle;
                        ViewBag.userId = _postinfo.userId;
                        ViewBag.postTime = _postinfo.postTime;
                        ViewBag.pageView = _postinfo.pageView;
                        ViewBag.isPost = _postinfo.isPost;
                        ViewBag.isPay = _postinfo.isPay;
                        ViewBag.amount = _postinfo.amount;
                        ViewBag.postStatus = _postinfo.postStatus;
                        ViewBag.postContent = _postinfo.postContent;
                    }
                    else
                    {
                        return View("forumInvitationUpdate");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("forumInvitationUpdate");
            }
            catch
            {
                return View("forumInvitationUpdate");
            }
        }
        #endregion
        #region 删除
        public ActionResult forumInvitationDelete()
        {
            return View();
        }
        [HttpPost]
        public string forumInvitationDelete(postInfo info)
        {
            try
            {
                //根据不可重复的用户名找到postInfo里面的postId,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<postInfo> dbInvitation = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postInfo = dbInvitation.FirstOrDefault();
                    if (_postInfo == null)
                    {
                        return "删除的论坛帖子不存在";
                    }
                    db.Entry<postInfo>(_postInfo).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return "论坛帖子删除成功";
                }
            }
            catch
            {
                return "论坛帖子删除失败";
            }
        }
        #endregion
        #region 修改
        public ActionResult forumInvitationUpdate()
        {
            return View();
        }
        [HttpPost]
        public string forumInvitationUpdate(postInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dbInvitation = db.postInfo.Where(u => u.postId == info.postId) as DbQuery<postInfo>;
                    postInfo _postInfo = dbInvitation.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _postInfo.areaId = info.areaId;
                    _postInfo.postId = info.postId;
                    _postInfo.postTitle = info.postTitle;
                    _postInfo.userId = info.userId;
                    _postInfo.isPost = info.isPost;
                    _postInfo.isPay = info.isPay;
                    _postInfo.postStatus = info.postStatus;
                    _postInfo.postContent = info.postContent;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "论坛帖子修改成功";
            }
            catch
            {
                return "论坛帖子修改失败";
            }
        }
        #endregion
        #endregion

        #region 论坛主页管理
        public ActionResult forumIndex()
        {
            return View();
        }

        #endregion
    }
}