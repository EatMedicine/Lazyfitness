using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.account.Controllers
{
    public class accountSysController : Controller
    {
        #region 注册
        // GET: account/register
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public string register(userSecurity security, userInfo info)
        {
            //使用entity framework 进行数据的插入
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    userSecurity obSecurity = new userSecurity
                    {
                        loginId = info.userName,
                        userPwd = security.userPwd,
                    };               
                    db.userSecurity.Add(obSecurity);
                    db.SaveChanges();                               
                    int uniformId;
                    DbQuery<userSecurity> dbSecuritySureUserId = db.userSecurity.Where(u => u.loginId == info.userName) as DbQuery<userSecurity>;
                    userSecurity dbSecurity = dbSecuritySureUserId.FirstOrDefault();
                    uniformId = dbSecurity.userId;
                    userInfo obInfo = new userInfo
                    {
                        userId = uniformId,
                        userName = info.userName,
                        userAge = info.userAge,
                        userSex = info.userSex,
                        userTel = info.userTel,
                        userStatus = 1,
                        userAccount = 0
                    };
                    db.userInfo.Add(obInfo);
                    db.SaveChanges();
                }
                return "T";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region 登录
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public string login(userSecurity security)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userSecurity> dbSecuritySureId = db.userSecurity.Where(u => u.loginId == security.loginId) as DbQuery<userSecurity>;
                    userSecurity obSureId = dbSecuritySureId.FirstOrDefault();
                    if (obSureId == null)
                    {
                        return "未注册";
                    }
                    DbQuery<userSecurity> dbSecuritySurePwd = db.userSecurity.Where(u => u.loginId == security.loginId).Where(u => u.userPwd == security.userPwd) as DbQuery<userSecurity>;
                    userSecurity obSurePwd = dbSecuritySurePwd.FirstOrDefault();
                    if (obSurePwd != null)
                    {
                        return "登录成功";
                    }
                    else
                    {
                        return "密码错误";
                    }
                }
            }
            catch
            {
                return "登录失败";
            }
        }
        #endregion
    }
}