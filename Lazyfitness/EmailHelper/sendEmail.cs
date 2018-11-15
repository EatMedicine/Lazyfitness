using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Lazyfitness.EmailHelper
{
    public class sendEmail
    {
        public static Boolean sendVerification(string mailAddress,string code)
        {
            if (mailAddress == null || code == null )
            {
                return false;
            }
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
                mailMessage.Body = "<div>尊敬的<a href='http://www.lazyfitness.cn'>小懒人</a>用户您好：<br/>欢迎注册小懒人健身，您的注册验证码为：<strong>" + code+"</strong>，请妥善保管。验证码30分钟内有效。</div>";
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
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}