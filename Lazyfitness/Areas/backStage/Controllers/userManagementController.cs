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
        public List<userInfo> GetPagedList<TKey>(int pageIndex, int pageSize/*, Expression<Func<userInfo, bool>> whereLambda*/, Expression<Func<userInfo, TKey>> orderBy)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页时一定注意：Skip之前一定要OrderBy
                return db.userInfo/*.Where(whereLambda)*/.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }


        public int GetSumPage(int pageSize)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                int listSum = db.userInfo.ToList().Count;
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
            ViewBag.allInfo = GetPagedList(1, 10, u => u.userId);

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
            ViewBag.allInfo = GetPagedList(Convert.ToInt32(id), 10, u => u.userId);
            return View();
        }

        // GET: backStage/manage
        #region 增加用户
        public ActionResult add()
        {
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
                    db.userSecurity.Add(user);
                    db.SaveChanges();
                }
                return "增加成功";
            }
            catch
            {
                return ("增加失败");
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
                //根据不可重复的用户名找到userSecurity里面的userId,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<userInfo> dbInfo = db.userInfo.Where(u => u.userName == info.userName.Trim()) as DbQuery<userInfo>;
                    userInfo obInfo = dbInfo.FirstOrDefault();

                    //创建一个要删除的对象
                    userSecurity deleSecurity = new userSecurity
                    {
                        userId = obInfo.userId,
                    };
                    //附加到ef中
                    db.userSecurity.Attach(deleSecurity);
                    //标记为删除--标记当前对象为删除状态
                    db.userSecurity.Remove(deleSecurity);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch(Exception ex)
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
                @ViewBag.managerId = cookieText.ToString();
            }
            else
            {
                return Content("未登录");
            }         
            return View();
        }
        [HttpPost]
        public ActionResult search(userInfo info)
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
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbInfosearch = db.userInfo.Where(u => u.userName == info.userName.Trim()) as DbQuery<userInfo>;
                    userInfo _userInfo = dbInfosearch.FirstOrDefault();
                    DbQuery<userSecurity> dbSecuritysearch = db.userSecurity.Where(u => u.userId == _userInfo.userId) as DbQuery<userSecurity>;
                    userSecurity _userSecurity = dbSecuritysearch.FirstOrDefault();
                    if (_userInfo != null)
                    {
                        ViewBag.userId = _userSecurity.userId;
                        ViewBag.loginId = _userSecurity.loginId;
                        ViewBag.userPwd = _userSecurity.userPwd;
                        ViewBag.userName = _userInfo.userName;
                        ViewBag.userAge = _userInfo.userAge;
                        ViewBag.userSex = _userInfo.userSex;
                        ViewBag.userTel = _userInfo.userTel;
                        ViewBag.userStatus = _userInfo.userStatus;
                        ViewBag.userAccount = _userInfo.userAccount;
                        //db.SaveChanges();
                    }
                    else
                    {
                        return View("update");
                    }
                }
                ViewBag.IsSearchSuccess = true;
                return View("update");
            }
            catch
            {
                return View("update");
            }
        }
        #endregion

        #region 修改用户信息
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
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var userInfo = db.userInfo.Where(u => u.userName == info.userName.Trim()).FirstOrDefault();
                var userSecurity = db.userSecurity.Where(u => u.loginId == info.userName.Trim()).FirstOrDefault();
                ViewBag.userInfo = userInfo;
                ViewBag.userSecurity = userSecurity;

                var statusList = db.userInfo.OrderBy(u => u.userStatus).ToList();                
            }
            //构造和状态对应的状态名字
            ArrayList statusNameList = new ArrayList();
            statusNameList.Add("禁止发言");
            statusNameList.Add("注册会员");
            statusNameList.Add("正式会员");
            statusNameList.Add("管理员");            

            return View();
        }
        [HttpPost]
        public string update(userInfo info, userSecurity security)
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
                    DbQuery<userInfo> dbInfosearch = db.userInfo.Where(u => u.userName == info.userName) as DbQuery<userInfo>;
                    userInfo _userInfo = dbInfosearch.FirstOrDefault();
                    DbQuery<userSecurity> dbSecuritysearch = db.userSecurity.Where(u => u.userId == _userInfo.userId) as DbQuery<userSecurity>;
                    userSecurity _userSecurity = dbSecuritysearch.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    //_userSecurity.userId = security.userId;
                    //_userSecurity.loginId = security.loginId;
                    //_userSecurity.userPwd = security.userPwd;
                    //_userInfo.userId = info.userId;
                    _userInfo.userName = info.userName;
                    _userInfo.userAge = info.userAge;
                    _userInfo.userSex = info.userSex;
                    _userInfo.userTel = info.userTel;
                    _userInfo.userStatus = info.userStatus;
                    _userInfo.userAccount = info.userAccount;
                    db.SaveChanges(); //将修改之后的值保存到数据库中
                }
                return "修改成功";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
    }
}