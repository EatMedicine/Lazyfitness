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
        /// 删除论坛分区表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deletePostArea(Expression<Func<postArea, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postArea> dbDelete = db.postArea.Where(whereLambda) as DbQuery<postArea>;
                    List<postArea> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.postArea.RemoveRange(obDelete);
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
        /// 删除问答分区表中符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteQuesArea(Expression<Func<quesArea, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesArea> dbDelete = db.quesArea.Where(whereLambda) as DbQuery<quesArea>;
                    List<quesArea> obDelete = dbDelete.ToList();
                    if (obDelete.Count == 0)
                    {
                        return true;
                    }
                    db.quesArea.RemoveRange(obDelete);
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

        /// <summary>
        /// 通过userId删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除文章分区表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteResourceAreaInfo(Expression<Func<resourceArea, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceArea> infoList = db.resourceArea.Where(whereLambda) as DbQuery<resourceArea>;                    
                    List<resourceArea> listResourceInfoAreaId = infoList.ToList();                   
                    db.resourceArea.RemoveRange(listResourceInfoAreaId);
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
        /// 通过areaId删除和文章相关表中所有与areaId相关的数据
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public static Boolean deleteResourceArea(int areaId)
        {
            try
            {
                Boolean flag1 = deleteResourceInfo(u => u.areaId == areaId);
                Boolean flag2 = deleteResourceAreaInfo(u => u.areaId == areaId);
                if (flag1 == flag2 == true)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除帖子分区表的所有符合条件的内容
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public static Boolean deleteAllPostAreaInfo(int areaId)
        {
            try
            {
                //首先查出这个分区里面有多少个帖子
                postInfo[] infoList = selectToolsController.selectPostInfo(u => u.areaId == areaId, u => u.postId);
                //先删除每个帖子回复表中的回帖数据
                Boolean flag1 = true;
                foreach (var item in infoList)
                {
                   if(deletePostReply(u => u.postId == item.postId) == false)
                    {
                        return false;
                    }
                }
                //再删除每个帖子表中的数据
                Boolean flag2 = true;
                foreach (var item in infoList)
                {
                    if (deletePostInfo(u=>u.postId == item.postId) == false)
                    {
                        return false;
                    }
                }
                if (flag1 == flag2 == true)
                {
                    //删除论坛分区表
                    if(deletePostArea(u=>u.areaId == areaId) == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除论坛帖子表中所有符合条件的数据
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Boolean deleteAllPostInfo(int postId)
        {
            try
            {
                //查出这个论坛下有多少个回帖
                postReply[] prList = selectToolsController.selectPostReply(u => u.postId == postId, u => u.postId);
                //删除所有的回帖
                Boolean flag1 = true;
                foreach (var item in prList)
                {
                    if (deletePostReply(u=>u.postId == postId) == false)
                    {
                        return false;
                    }
                }
                if (flag1 == true)
                {
                    //删除帖子
                    if (deletePostInfo(u=>u.postId == postId) == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除问答分区表的所有内容
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public static Boolean deleteAllQuesAreaInfo(int areaId)
        {
            try
            {
                //首先查出这个分区里面有多少个帖子
                quesAnswInfo[] infoList = selectToolsController.selectQuesAnswInfo(u => u.areaId == areaId, u => u.quesAnswId);
                //先删除每个问答帖子回复表中的回帖数据
                Boolean flag1 = true;
                foreach (var item in infoList)
                {
                    if (deleteQuesAnswReply(u => u.quesAnswId == item.quesAnswId) == false)
                    {
                        return false;
                    }
                }
                //再删除每个帖子表中的数据
                Boolean flag2 = true;
                foreach (var item in infoList)
                {
                    if (deleteQuesAnswInfo(u => u.quesAnswId == item.quesAnswId) == false)
                    {
                        return false;
                    }
                }
                if (flag1 == flag2 == true)
                {
                    //删除论坛分区表
                    if (deleteQuesArea(u => u.areaId == areaId) == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 删除问答帖子表中所有符合条件的数据
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Boolean deleteAllQuesAnswInfo(int quesAnswId)
        {
            try
            {
                //查出这个论坛下有多少个回帖
                quesAnswReply[] prList = selectToolsController.selectQuesAnswReply(u => u.quesAnswId == quesAnswId, u => u.quesAnswId);
                //删除所有的回帖
                Boolean flag1 = true;
                foreach (var item in prList)
                {
                    if (deleteQuesAnswReply(u => u.quesAnswId == quesAnswId) == false)
                    {
                        return false;
                    }
                }
                if (flag1 == true)
                {
                    //删除帖子
                    if (deleteQuesAnswInfo(u => u.quesAnswId == quesAnswId) == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 删除充值卡表中符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static Boolean deleteRecharge(Expression<Func<recharge, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<recharge> dbDelete = db.recharge.Where(whereLambda) as DbQuery<recharge>;
                    List<recharge> obDelete = dbDelete.ToList();
                    if(obDelete.Count == 0)
                    {
                        return false;
                    }
                    db.recharge.RemoveRange(obDelete);
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