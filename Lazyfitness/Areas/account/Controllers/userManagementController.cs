using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.account.Controllers
{
    public class userManagementController : Controller
    {       
        #region 注册
        // GET: account/register
        public ActionResult registerUser()
        {
            return View();
        }
        [HttpPost]
        public string registerUser(userSecurity security, userInfo info, string code)
        {           
            //使用entity framework 进行数据的插入
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    //先把用户写入userSecurity表
                    var isLoginID = db.userSecurity.Where<userSecurity>(u => u.loginId == security.loginId.Trim());
                    if (isLoginID.ToList().Count != 0)
                    {

                        return "已经有账户";
                    }                   
                    userSecurity obSecurity = new userSecurity
                    {
                        loginId = security.loginId.Trim(),
                        userPwd = MD5Helper.MD5Helper.encrypt(security.userPwd.Trim()),
                    };
                    db.userSecurity.Add(obSecurity);
                    db.SaveChanges();

                    //把userInfo表写入默认数据
                    int uniformId;
                    DbQuery<userSecurity> dbSecuritySureUserId = db.userSecurity.Where(u => u.loginId == security.loginId.Trim()) as DbQuery<userSecurity>;
                    userSecurity dbSecurity = dbSecuritySureUserId.FirstOrDefault();
                    uniformId = dbSecurity.userId;
                    userInfo obInfo = new userInfo
                    {
                        userId = uniformId,
                        userName = security.loginId.Trim(),
                        userAge = info.userAge,
                        userSex = info.userSex,
                        userEmail = info.userEmail,
                        userStatus = 1,
                        userAccount = 0,
                        userIntroduce = "这个人很懒，没有说什么",
                        userHeaderPic = "/Resource/picture/DefaultHeadPic1.png"
                    };
                    db.userInfo.Add(obInfo);
                    db.SaveChanges();
                }
                Response.Redirect("/");
                return "Ok";
            }
            catch (Exception EX)
            {
                return EX.ToString();
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

                        System.Web.HttpContext.Current.Response.Cookies.Add(CookiesHelper.CookiesHelper.creatCookieHours("userId", obSurePwd.userId.ToString(), 1));
                        string encryptCertification = certificateTools.makeCertification(obSureId.userId.ToString());
                        System.Web.HttpContext.Current.Response.Cookies.Add(CookiesHelper.CookiesHelper.creatCookieHours("certification", encryptCertification, 1));
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookieName);                        
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

        #region 注销
        public void Logout()
        {
            System.Web.HttpContext.Current.Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["loginId"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["certification"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("/");
        }
        #endregion

        public ActionResult Index()
        {
            if (Request.Cookies["loginId"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["loginId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                return Content(cookieText);
            }
            return Content("未登录");
                    
        }
        
    }
}