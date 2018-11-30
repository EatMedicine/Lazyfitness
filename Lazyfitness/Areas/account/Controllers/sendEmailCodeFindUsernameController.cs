using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Lazyfitness.Areas.account.Controllers
{
    public class sendEmailCodeFindUsernameController : Controller
    {
        //盐因子
        protected string saltFactor = "OliverKaimarEason";

        //发送邮箱验证码
        [HttpPost]
        public string sendVerification(string mailAddress)
        {
            //生成6位数验证码
            Random randomNum = new Random();
            string code = randomNum.Next(100000, 999999).ToString();

            //加密验证码和邮箱地址
            string encryptCode = MD5Helper.MD5Helper.encrypt(code + saltFactor);
            string encryptMailAddress = MD5Helper.MD5Helper.encrypt(mailAddress + saltFactor);

            //把加密后的邮箱和验证码写入cookie 过期时间为30分钟。
            Response.Cookies.Add(CookiesHelper.CookiesHelper.creatCookieMinutes("emailCodeFindUsername", encryptCode, 30));
            Response.Cookies.Add(CookiesHelper.CookiesHelper.creatCookieMinutes("emailAddressFindUsername", encryptMailAddress, 30));
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();
                //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
                mailMessage.From = new MailAddress("1092519577@qq.com", "小懒人团队");
                //收件人邮箱地址。
                mailMessage.To.Add(new MailAddress(mailAddress));
                //邮件标题。          
                mailMessage.Subject = "小懒人健身注册验证码";
                //邮件内容。
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<div>尊敬的<a href='http://www.lazyfitness.cn'>小懒人</a>用户您好：<br/>您的找回用户名的注册验证码为：<strong>" + code + "</strong>，请妥善保管。验证码30分钟内有效。</div>";
                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();
                //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
                client.Host = "smtp.qq.com";
                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential("1092519577@qq.com", "xbqavdypesgxghif");
                //发送
                client.Send(mailMessage);
                return "true";
            }
            catch
            {
                return "false";
            }
        }

        /// <summary>
        /// 在提交注册页面之前验证邮箱所对应的验证码是否正确
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public string isRightCode(string emailAddress, string code)
        {
            if (Request.Cookies["emailCodeFindUsername"] != null && Request.Cookies["emailAddressFindUsername"] != null)
            {
                //获取Cookies的值
                HttpCookie cookieName = Request.Cookies["emailCodeFindUsername"];
                var rightCode = Server.HtmlEncode(cookieName.Value);

                HttpCookie cookieAddress = Request.Cookies["emailAddressFindUsername"];
                var rightEmail = Server.HtmlEncode(cookieAddress.Value);

                //把用户输入的验证码 和邮箱地址进行加密验证
                string encryptCode = MD5Helper.MD5Helper.encrypt(code.Trim() + saltFactor);
                string encryptemailAddress = MD5Helper.MD5Helper.encrypt(emailAddress.Trim() + saltFactor);
                if (rightEmail != encryptemailAddress || rightCode != encryptCode)
                {
                    //验证码错误
                    return "CF";
                }
                //清空找回密码相关的cookies
                Response.Cookies.Add(CookiesHelper.CookiesHelper.clearCookie("emailCodeFindUsername"));
                Response.Cookies.Add(CookiesHelper.CookiesHelper.clearCookie("emailAddressFindUsername"));
                return "T";
            }
            //验证码为空
            return "F";
        }
    }
}