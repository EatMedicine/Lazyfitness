using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Lazyfitness.Models;
namespace Lazyfitness.Areas.toolsHelpers
{
    public class deleteToolsController : Controller
    {

        /// <summary>
        ///删除用户表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteUserInfo(Expression<Func<userInfo, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dbDelete = db.userInfo.Where(whereLambda) as DbQuery<userInfo>;
                    List<userInfo> obDelete = dbDelete.ToList();
                    db.userInfo.RemoveRange(obDelete);
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
        /// 删除发帖表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deletePostInfo(Expression<Func<postInfo, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dbDelete = db.postInfo.Where(whereLambda) as DbQuery<postInfo>;
                    List<postInfo> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.postInfo.RemoveRange(obDelete);
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
        ///删除帖子回复表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deletePostReply(Expression<Func<postReply, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postReply> dbDelete = db.postReply.Where(whereLambda) as DbQuery<postReply>;
                    List<postReply> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.postReply.RemoveRange(obDelete);
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
        /// 删除问答表中符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteQuesAnswInfo(Expression<Func<quesAnswInfo, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswInfo> dbDelete = db.quesAnswInfo.Where(whereLambda) as DbQuery<quesAnswInfo>;
                    List<quesAnswInfo> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.quesAnswInfo.RemoveRange(obDelete);
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
        /// 删除回答表中符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteQuesAnswReply(Expression<Func<quesAnswReply, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswReply> dbDelete = db.quesAnswReply.Where(whereLambda) as DbQuery<quesAnswReply>;
                    List<quesAnswReply> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.quesAnswReply.RemoveRange(obDelete);
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
        /// 删除文章表中符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteResourceInfo(Expression<Func<resourceInfo, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceInfo> dbDelete = db.resourceInfo.Where(whereLambda) as DbQuery<resourceInfo>;
                    List<resourceInfo> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.resourceInfo.RemoveRange(obDelete);
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
        /// 删除用户安全表中符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteUserSecurity(Expression<Func<userSecurity, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userSecurity> dbDelete = db.userSecurity.Where(whereLambda) as DbQuery<userSecurity>;
                    List<userSecurity> obDelete = dbDelete.ToList();
                    db.userSecurity.RemoveRange(obDelete);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public static Boolean deleteUserUserId(int userId)
        {
            try
            {
                Boolean status1 = deletePostReply(u=>u.userId == userId);
                Boolean status2 = deleteQuesAnswReply(u=>u.userId == userId);
                Boolean status3 = deleteResourceInfo(u=>u.userId == userId);
                Boolean status4 = deletePostInfo(u=>u.userId == userId);
                Boolean status5 = deleteQuesAnswInfo(u=>u.userId == userId);
                Boolean status6 = deleteUserInfo(u=>u.userId == userId);
                Boolean status7 = deleteUserSecurity(u=>u.userId == userId);
                if (status1 == status2 == status3 == status4 == status5 == status6 == status7 == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}