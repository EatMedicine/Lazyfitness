using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
using System.Data.Entity.Infrastructure;
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
                    DbQuery<userInfo> dbInfo = db.userInfo.Where(u => u.userName == info.userName.Trim()).Where(u => u.userTel == info.userTel.Trim()) as DbQuery<userInfo>;
                    userInfo obInfo = dbInfo.FirstOrDefault();
                    if (obInfo == null)
                    {
                        return Content("用户名或电话不正确");
                    }
                    Session["findPwdUserName"] = info.userName.Trim();
                    return View("findPassword");
                }
            }
            catch
            {
                return Content("验证失败");
            }
        }
        #endregion

        #region 身份验证成功后修改密码
        [HttpPost]
        public string findPassword(string userPwd)
        {
            if (Session["findPassword"] == null)
            {
                return "F";
            }
            string findPassword = Session["findPassword"].ToString();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //实例化一个要修改的对象
                userSecurity obSecurity = db.userSecurity.Where(u => u.loginId == findPassword).FirstOrDefault();
                obSecurity.userPwd = MD5Helper.MD5Helper.encrypt(userPwd.Trim());
                db.SaveChanges();
                return "修改成功";
            }
        }
        #endregion

        #endregion

        #region 修改密码
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
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
                        return "用户名或密码错误";
                    }
                    obSecurity.userPwd = MD5NewPwd;
                    db.SaveChanges();
                    return "修改成功";
                }
            }
            catch
            {
                return "修改失败";
            }
        }
        #endregion
    }
}