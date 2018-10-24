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
        [HttpPost]
        public string findPassword(userSecurity security, userInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbInfo = db.userInfo.Where(u => u.userName == info.userName.Trim()).Where(u => u.userTel == info.userTel.Trim()) as DbQuery<userInfo>;
                    userInfo obInfo = dbInfo.FirstOrDefault();
                    if (obInfo == null)
                    {
                        return "用户名或电话不正确";
                    }
                    //实例化一个要修改的对象
                    userSecurity obSecurity = db.userSecurity.Where(u => u.loginId == info.userName.Trim()).FirstOrDefault();
                    obSecurity.userPwd = MD5Helper.MD5Helper.encrypt(security.userPwd.Trim());
                    db.SaveChanges();
                    return "修改成功";
                }
            }
            catch
            {
                return "验证失败";
            }
        }
        #endregion

        #region 修改密码
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public string changePassword(userSecurity security, string userNewPwd)
        {
            try
            {
                string MD5Pwd = MD5Helper.MD5Helper.encrypt(security.userPwd.Trim());
                string MD5NewPwd = MD5Helper.MD5Helper.encrypt(userNewPwd.Trim());
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userSecurity> dbSecurity = db.userSecurity.Where(u => u.loginId == security.loginId.Trim()).Where(u => u.userPwd == MD5Pwd) as DbQuery<userSecurity>;
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