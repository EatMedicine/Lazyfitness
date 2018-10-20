using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.account.Controllers
{
    public class userManagerController : Controller
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
                    var isLoginID = db.userSecurity.Where(u => u.loginId == info.userName.Trim());
                    if (isLoginID.ToList() != null)
                    {
                        return "此账户已经注册";
                    }
                    userSecurity obSecurity = new userSecurity
                    {
                        loginId = info.userName.Trim(),
                        userPwd = MD5Helper.MD5Helper.encrypt(security.userPwd.Trim()),
                    };               
                    db.userSecurity.Add(obSecurity);
                    db.SaveChanges();                               
                    int uniformId;
                    DbQuery<userSecurity> dbSecuritySureUserId = db.userSecurity.Where(u => u.loginId == info.userName.Trim()) as DbQuery<userSecurity>;
                    userSecurity dbSecurity = dbSecuritySureUserId.FirstOrDefault();
                    uniformId = dbSecurity.userId;
                    userInfo obInfo = new userInfo
                    {
                        userId = uniformId,
                        userName = info.userName.Trim(),
                        userAge = info.userAge,
                        userSex = info.userSex,
                        userTel = info.userTel.Trim(),
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
                    DbQuery<userSecurity> dbSecuritySureId = db.userSecurity.Where(u => u.loginId == security.loginId.Trim()) as DbQuery<userSecurity>;
                    userSecurity obSureId = dbSecuritySureId.FirstOrDefault();
                string MD5Pwd = MD5Helper.MD5Helper.encrypt(security.userPwd.Trim());
                    if (obSureId == null)
                    {
                        return "未注册";
                    }
                    DbQuery<userSecurity> dbSecuritySurePwd = db.userSecurity.Where(u => u.loginId == security.loginId.Trim()).Where(u => u.userPwd == MD5Pwd) as DbQuery<userSecurity>;
                    userSecurity obSurePwd = dbSecuritySurePwd.FirstOrDefault();
                    if (obSurePwd != null)
                    {
                        HttpCookie cookieName = new HttpCookie("loginId");
                        cookieName.Value = security.loginId.Trim();
                        cookieName.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(cookieName);                        
                        return "登录成功";
                    }
                    else
                    {
                        return "密码错误";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        public ActionResult Index()
        {
            if (Request.Cookies["loginId"] != null)
            {
                HttpCookie cookieName = Request.Cookies["loginId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                return Content(cookieText);
            }
            return Content("未登录");
                    
        }
        
    }
}