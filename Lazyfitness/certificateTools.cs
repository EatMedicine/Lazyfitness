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
    }
}