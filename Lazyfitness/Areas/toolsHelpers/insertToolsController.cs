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

        /// <summary>
        /// 向文章表中插入信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertResourceInfo(resourceInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.resourceInfo.Add(info);
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
        /// 论坛分区表增加数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertPostArea(postArea info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.postArea.Add(info);
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
        /// 论坛表增加数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertPostInfo(postInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.postInfo.Add(info);
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
        /// 向论坛回复表中插入信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertPostReply(postReply info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.postReply.Add(info);
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
        /// 向问答回复表中插入信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertQuesAnswReply(quesAnswReply info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.quesAnswReply.Add(info);
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
        /// 问答分区表增加数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertQuesArea(quesArea info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.quesArea.Add(info);
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
        /// 问答表增加数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertQuesAnswInfo(quesAnswInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.quesAnswInfo.Add(info);
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
        /// 充值卡表增加数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertRecharge(recharge info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.recharge.Add(info);
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
        /// 展示表增加数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean insertServerShowInfo(serverShowInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    db.serverShowInfo.Add(info);
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