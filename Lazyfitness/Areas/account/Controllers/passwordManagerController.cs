using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
using Lazyfitness.Filter;

namespace Lazyfitness.Areas.account.Controllers
{
    public class passwordManagerController : Controller
    {
        #region 忘记密码
        // GET: account/passwordManager
        public ActionResult findPassword()
        {
            return View();
        }
        #region 验证身份信息
        public ActionResult VerifyInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyInfo(userSecurity security, userInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    userSecurity[] securityInfo = toolsHelpers.selectToolsController.selectUserSecurity(u => u.loginId == security.loginId, u=>u.userId);
                    if (securityInfo == null || securityInfo.Length == 0)
                    {
                        Tools.AlertAndRedirect("没有此用户", Url.Action("VerifyInfo", "passwordManager", new { area = "account" }));
                        return Content("没有此用户");
                    }
                    int userId = securityInfo[0].userId;
                    userInfo[] user = toolsHelpers.selectToolsController.selectUserInfo(u => u.userId == userId, u => u.userId);
                    if (user == null || user.Length == 0)
                    {
                        Tools.AlertAndRedirect("登录名与邮箱不匹配", Url.Action("VerifyInfo", "passwordManager", new { area = "account" }));
                        return Content("登录名与邮箱不匹配");
                    }
                    userSecurity obSecurity = db.userSecurity.Where(u => u.userId == userId).FirstOrDefault();
                    obSecurity.userPwd = MD5Helper.MD5Helper.encrypt(security.userPwd.Trim());
                    db.SaveChanges();
                    Tools.AlertAndRedirect("修改成功", Url.Action("Index", "Home", new { area = "account" }));
                    return Content("修改成功！");
                }
            }
            catch
            {
                Tools.AlertAndRedirect("验证出错,请与管理员联系", Url.Action("VerifyInfo", "passwordManager", new { area = "account" }));
                return Content("验证出错");
            }
        }
        #endregion

        #endregion

        #region 修改密码
        [LoginStatusFilter]
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        [LoginStatusFilter]
        public string changePassword(string loginId, string userOldPwd, string userPwd)
        {
            try
            {
                string MD5Pwd = MD5Helper.MD5Helper.encrypt(userOldPwd.Trim());
                string MD5NewPwd = MD5Helper.MD5Helper.encrypt(userPwd.Trim());
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userSecurity> dbSecurity = db.userSecurity.Where(u => u.loginId == loginId.Trim()).Where(u => u.userPwd == MD5Pwd) as DbQuery<userSecurity>;
                    userSecurity obSecurity = dbSecurity.FirstOrDefault();
                    if (obSecurity == null)
                    {
                        Tools.AlertAndRedirect("用户名或密码错误", Url.Action("changePassword", "passwordManager", new { area = "account" }));
                        return "用户名或密码错误";
                    }
                    obSecurity.userPwd = MD5NewPwd;
                    db.SaveChanges();
                    Tools.AlertAndRedirect("修改成功", Url.Action("Logout", "userManagement"));
                    return "修改成功";
                }
            }
            catch
            {
                Tools.AlertAndRedirect("修改失败,请与管理员联系", Url.Action("changePassword", "passwordManager", new { area = "account" }));
                return "修改失败";
            }
        }
        #endregion

        #region 找回用户名
        public ActionResult findUserName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult findUsername(userInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    userInfo[] user = toolsHelpers.selectToolsController.selectUserInfo(u => u.userEmail == info.userEmail.Trim(), u => u.userId);
                    if (user.Length == 0 || user == null)
                    {
                        Tools.AlertAndRedirect("没有此用户", Url.Action("findUserName", "passwordManager", new { area = "account" }));
                        return Content("此邮箱没有注册！");
                    }
                    int userId = user[0].userId;
                    userSecurity[] securityInfo = toolsHelpers.selectToolsController.selectUserSecurity(u => u.userId == userId, u=>u.userId);
                    string rightLoginName = securityInfo[0].loginId;
                    Tools.AlertAndRedirect("您的登录名："+rightLoginName, Url.Action("Index", "Home", new { area = "" }));
                    return Content(rightLoginName);
                }
            }
            catch
            {
                Tools.AlertAndRedirect("寻找用户名出错，请联系管理员", Url.Action("findUserName", "passwordManager", new { area = "account" }));
                return Content("找用户名出错!");
            }
            
        }
        #endregion
    }
}