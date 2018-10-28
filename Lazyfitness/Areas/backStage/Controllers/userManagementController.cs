using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.backStage.Controllers
{
    public class userManagementController : Controller
    {
        public ActionResult Index()
        {
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
            //显示用户
            //ViewBag.allData = null;
            //using (LazyfitnessEntities db = new LazyfitnessEntities())
            //{
            //    List<userInfo> data = db.userInfo.ToList<userInfo>();
            //    ViewBag.allData = data;
            //}
            return View();
        }
        [HttpPost]
        public string delete(string userName)
        {
            try
            {
                //根据不可重复的用户名找到userSecurity里面的userId,将其删除
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<userInfo> dbInfo = db.userInfo.Where(u => u.userName == userName.Trim()) as DbQuery<userInfo>;
                    userInfo obInfo = dbInfo.FirstOrDefault();
                    if(obInfo == null)
                    {
                        return "删除的用户不存在";
                    }
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
                    return "删除成功";
                }
            }
            catch
            {
                return "删除失败";
            }
        }
        #endregion

        #region 查询用户
        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(userInfo info)
        {
            try
            {
                ViewBag.IsSearchSuccess = false;
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbInfosearch = db.userInfo.Where(u => u.userName == info.userName) as DbQuery<userInfo>;
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
        public ActionResult update()
        {
            return View();
        }
        [HttpPost]
        public string update(userInfo info, userSecurity security)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbInfosearch = db.userInfo.Where(u => u.userName == info.userName) as DbQuery<userInfo>;
                    userInfo _userInfo = dbInfosearch.FirstOrDefault();
                    DbQuery<userSecurity> dbSecuritysearch = db.userSecurity.Where(u => u.userId == _userInfo.userId) as DbQuery<userSecurity>;
                    userSecurity _userSecurity = dbSecuritysearch.FirstOrDefault();
                    //将要修改的值，放到数据上下文中
                    _userSecurity.userId = security.userId;
                    _userSecurity.loginId = security.loginId;
                    _userSecurity.userPwd = security.userPwd;
                    _userInfo.userId = info.userId;
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
            catch
            {
                return "修改失败";
            }
        }
        #endregion
    }
}