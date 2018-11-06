using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazyfitness.Areas.CookiesHelper
{
    public class CookiesHelper
    {
        /// <summary>
        /// 生成一个cookie
        /// </summary>
        /// <param name="_cookieName">cookie的名字</param>
        /// <param name="_cookieValue">cookie的值</param>
        /// <param name="outDate">cookie过期时间整数</param>
        public static HttpCookie creatCookieHours(string _cookieName, string _cookieValue, int outDate)
        {
            HttpCookie cookieName = new HttpCookie(_cookieName);
            cookieName.Value = _cookieValue.Trim();
            cookieName.Expires = DateTime.Now.AddHours(outDate);
            return cookieName;
        }
        /// <summary>
        /// 生成一个cookie
        /// </summary>
        /// <param name="_cookieName">cookie的名字</param>
        /// <param name="_cookieValue">cookie的值</param>
        /// <param name="outDate">cookie过期时间整数</param>
        public static HttpCookie creatCookieMinutes(string _cookieName, string _cookieValue, int outDate)
        {
            HttpCookie cookieName = new HttpCookie(_cookieName);
            cookieName.Value = _cookieValue.Trim();
            cookieName.Expires = DateTime.Now.AddMinutes(outDate);
            return cookieName;
        }
        
        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <param name="_cookieName">cookie名字</param>
        public static HttpCookie clearCookie(string _cookieName)
        {
            HttpCookie cookieName = new HttpCookie(_cookieName);
            cookieName.Expires = DateTime.Now.AddDays(-1);
            return cookieName;
        }

    }
}