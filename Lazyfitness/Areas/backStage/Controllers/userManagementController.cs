using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class userManagementController : Controller
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排列 lambda表达式</param>
        /// <returns></returns>
        public userInfo[] GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<userInfo, bool>> whereLambda, Expression<Func<userInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.userInfo.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.userInfo.ToList().Count;
                if ((listSum != 0) && listSum % pageSize == 0)
                {
                    return (listSum / pageSize);
                }
                return ((listSum / pageSize) + 1);
            }
        }

        public ActionResult Index()
        {         
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                Response.Redirect("/backStage/manager/login");
                return Content("未登录");
            }
            ViewBag.nowPage = 1;
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(1, 10, x=>x == x , u => u.userId);
            //通过用户状态表中的的数据返回对应的状态名
            userStatusName[] nameArray = toolsHelpers.selectToolsController.selectUserStatusName(x => x == x, u => u.userStatus);
            ViewBag.nameArray = nameArray;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int id)
        {        
            ViewBag.managerId = null;
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                return Content("未登录");
            }         
            ViewBag.nowPage = id;
            ViewBag.sumPage = GetSumPage(10);
            ViewBag.allInfo = GetPagedList(Convert.ToInt32(id), 10, x=>x == x, u => u.userId);
            //通过用户状态表中的的数据返回对应的状态名
            userStatusName[] nameArray = toolsHelpers.selectToolsController.selectUserStatusName(x => x == x, u => u.userStatus);
            ViewBag.nameArray = nameArray;
            return View();
        }

        // GET: backStage/manage
        #region 增加用户
        public ActionResult add()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            return View();
        }
        [HttpPost]
        public string add(userSecurity security)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //先判断登录Id是否可用
                    var isLoginId = db.userSecurity.Where(u => u.loginId == security.loginId.Trim());
                    if (isLoginId.ToList().Count != 0)
                    {
                        return "用户已存在";
                    }
                    userSecurity user = new userSecurity
                    {
                        loginId = security.loginId,
                        userPwd = MD5Helper.MD5Helper.encrypt(security.userPwd)
                    };
                    //得到插入成功的数据对象以获得userId
                    userSecurity successUser = toolsHelpers.insertToolsController.insertUserSecurity(user);
                    if (successUser == null)
                    {
                        return "false";
                    }                    
                    //把这个userId写入userInfo表中
                    userInfo newUserInfo = new userInfo
                    {
                        userId = successUser.userId,
                        userName = successUser.loginId,
                        userAge = 0,
                        userSex = 2,
                        userEmail = null,
                        userStatus = 1,
                        userAccount = 0,
                        userIntroduce = "这个人很懒，没有说什么",
                        userHeaderPic = "/Resource/picture/DefaultHeadPic1.png",
                    };
                    if (toolsHelpers.insertToolsController.insertUserInfo(newUserInfo) == true)
                    {
                        Response.Redirect("/backStage/userManagement/Index");
                        return "success";
                    }
                    else
                    {
                        return "false";
                    }
                }      
            }
            catch
            {
                return "false";
            }
        }
        #endregion

        #region 删除用户
        public ActionResult delete()
        {
            return View();
        }
        [HttpPost]
        public string delete(userInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                //此处判断有没有这个userId
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userSecurity> dataObject = db.userSecurity.Where(u => u.userId == info.userId) as DbQuery<userSecurity>;
                    int dataNum = dataObject.ToList().Count;
                    if (dataNum == 0)
                    {
                        return "没有此用户！";
                    }
                }
                if (toolsHelpers.deleteToolsController.deleteUserUserId(info.userId))
                {
                    Response.Redirect("/backStage/userManagement/Index");
                    return "success";
                }
                else
                {
                    return "false";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region 查询用户
        public ActionResult search()
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }         
            return View();
        }
        [HttpPost]
        public ActionResult searchList(userInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {         

                //通过用户名查询用户信息
                userInfo[] infoArray = toolsHelpers.selectToolsController.selectUserInfo(u => u.userName == info.userName, u=>u.userId);
                if (infoArray.Length == 0)
                {
                    return Content("没有此用户");
                }
                ViewBag.infoArray = infoArray;
                //通过用户状态表中的的数据返回对应的状态名
                userStatusName[] nameArray = toolsHelpers.selectToolsController.selectUserStatusName(x => x == x, u => u.userStatus);
                ViewBag.nameArray = nameArray;
                return View();
            }
            catch
            {
                return Content("查询失败");
            }
        }
        #endregion

        #region 修改用户信息
        [HttpPost]
        public ActionResult update(userInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return Content("未登录");
            }
            try
            {
                userInfo[] List = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == info.userId, u => u.userId);
                if (List == null)
                {
                    return Content("查询用户失败");
                }
                userInfo userInformation = List[0];
                ViewBag.userInformation = userInformation;
                //获取用户状态表中的数据
                userStatusName[] nameArray = toolsHelpers.selectToolsController.selectUserStatusName(x => x == x, u => u.userStatus);
                ViewBag.nameArray = nameArray;
                return View();
            }
            catch
            {
                return Content("查询用户出错");
            }
        }
        [HttpPost]
        public string updateUserInfo(userInfo info)
        {
            if (Request.Cookies["managerId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["managerId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
            }
            else
            {
                return "未登录";
            }
            try
            {
                if (toolsHelpers.updateToolsController.updateUserInfo(u=>u.userId == info.userId, info) == true)
                {
                    Response.Redirect("/backStage/userManagement/Index");
                    return "success";
                }
                else
                {
                    return "修改失败";
                }
            }
            catch
            {
                return "false";
            }
        }
        #endregion
    }
}