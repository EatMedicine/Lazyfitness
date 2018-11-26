using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.toolsHelpers
{
    public class insertToolsController : Controller
    {
        /// <summary>
        /// 往用户安全表中插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns>插入数据后的对象</returns>
        public static userSecurity insertUserSecurity(userSecurity info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.userSecurity.Add(info);
                    db.SaveChanges();

                    DbQuery<userSecurity> data = db.userSecurity.Where(u => u.loginId == info.loginId) as DbQuery<userSecurity>;
                    userSecurity objectUser = data.FirstOrDefault();
                    return objectUser;                  
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 往用户信息表中插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertUserInfo(userInfo info)
        {
            try
            {
                using(LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.userInfo.Add(info);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
       
        /// <summary>
        /// 往文章分区表插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertResourceArea(resourceArea info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.resourceArea.Add(info);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }
    }
}