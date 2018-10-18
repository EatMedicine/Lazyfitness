using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Lazyfitness.Areas.MD5Helper
{
    public class MD5Helper
    {
        public static string encrypt(string userPwd)
        {
            //MD5加密
            byte[] password = Encoding.UTF8.GetBytes(userPwd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] pwOutput = md5.ComputeHash(password);
            string pwd = BitConverter.ToString(pwOutput).Replace("-", "");
            return pwd;
        }
    }
}