using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
        public string registerUser(userSecurity security)
        {
            Session["loginId"] = null;
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
                        userAge = 0,
                        userSex = 0,
                        userTel = null,
                        userStatus = 1,
                        userAccount = 0,
                    };
                    db.userInfo.Add(obInfo);
                    db.SaveChanges();
                }
                Session["loginId"] = security.loginId.Trim();
                RedirectToRoute(new { controller = "userManagerment", action = "registerInfo" });
                Response.Redirect("~/account/userManagement/registerInfo");
                    return "Ok";
            }
            catch (Exception EX)
            {
                return EX.ToString();
            }
        }

        public ActionResult registerInfo()
        {
            return View();
        }
        [HttpPost]
        public string registerInfo(userInfo info)
        {
            try
            {
                if (Session["loginId"] == null)
                {
                    return "F";
                }
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    string userName = Session["loginId"].ToString();
                    userInfo obInfo = db.userInfo.Where(u => u.userName == userName).FirstOrDefault();
                    obInfo.userAge = info.userAge;
                    obInfo.userSex = info.userSex;
                    obInfo.userTel = info.userTel;
                    db.SaveChanges();
                    return "T";
                }
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
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["loginId"];
                var cookieText = Server.HtmlEncode(cookieName.Value);
                return Content(cookieText);
            }
            return Content("未登录");
                    
        }
        
    }
}