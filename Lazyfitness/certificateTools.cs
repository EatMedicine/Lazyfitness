using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazyfitness
{
    public static class certificateTools
    {
        //创建盐
        public static string salt = "OliverKaimarEason";

        /// <summary>
        /// 生成凭证
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string makeCertification(string userId)
        {
            return Areas.MD5Helper.MD5Helper.encrypt(userId + salt);
        }

        /// <summary>
        /// 验证凭证
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="certification"></param>
        /// <returns></returns>
        public static bool verifyCertification(string userId, string certification)
        {          
            string encryptCertification = Areas.MD5Helper.MD5Helper.encrypt(userId + salt);
            if (certification == encryptCertification)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断cookie是否为无效
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns>true为有效 false为无效</returns>
        public static bool IsCookieEmpty(HttpCookie cookie)
        {
            if(cookie == null)
                return false;
            if (cookie.Values.Count == 0)
                return false;
            if (cookie.Value == "")
                return false;
            return true;
        }

        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static bool IsAdmin(string userId)
        {
            int id;
            if (Int32.TryParse(userId,out id) == false)
            {
                return false;
            }
            userInfo info = Tools.GetUserInfo(id);
            if (info != null && info.userStatus == 3)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 确认3个Cookie是否合法
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="loginId">登录ID</param>
        /// <param name="certification">凭证</param>
        /// <returns></returns>
        public static bool IsUserCookieCorrect(HttpCookie userId, HttpCookie loginId, HttpCookie certification)
        {
            if (IsCookieEmpty(userId) == false || IsCookieEmpty(loginId) == false || IsCookieEmpty(certification) == false)
                return false;
            if (verifyCertification(userId.Value, certification.Value) == false) 
                return false;

            return true;

        }
    }
}