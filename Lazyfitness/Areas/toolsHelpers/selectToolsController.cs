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
    public class selectToolsController : Controller
    {

        /// <summary>
        /// 查找用户安全表中符合条件的信息
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static userSecurity[] selectUserSecurity<TKey>(Expression<Func<userSecurity, bool>> whereLambda, Expression<Func<userSecurity, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userSecurity> dataObject = db.userSecurity.Where(whereLambda).OrderBy(orderBy) as DbQuery<userSecurity>;
                    userSecurity[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                userSecurity[] nullInfo = new userSecurity[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找用户信息表中符合条件的信息
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static userInfo[] selectUserInfo<TKey>(Expression<Func<userInfo, bool>> whereLambda, Expression<Func<userInfo, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dataObject = db.userInfo.Where(whereLambda).OrderBy(orderBy) as DbQuery<userInfo>;
                    userInfo[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                userInfo[] nullInfo = new userInfo[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找用户状态表中符合条件的信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static userStatusName[] selectUserStatusName<TKey>(Expression<Func<userStatusName, bool>> whereLambda, Expression<Func<userStatusName, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userStatusName> dataObject = db.userStatusName.Where(whereLambda).OrderBy(orderBy) as DbQuery<userStatusName>;
                    userStatusName[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                userStatusName[] nullInfo = new userStatusName[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找文章分区表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static resourceArea[] selectResourceArea<TKey>(Expression<Func<resourceArea, bool>> whereLambda, Expression<Func<resourceArea, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceArea> dataObject = db.resourceArea.Where(whereLambda).OrderBy(orderBy) as DbQuery<resourceArea>;
                    resourceArea[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                resourceArea[] nullInfo = new resourceArea[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找文章信息表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static resourceInfo[] selectResourceInfo<TKey>(Expression<Func<resourceInfo, bool>> whereLambda, Expression<Func<resourceInfo, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceInfo> dataObject = db.resourceInfo.Where(whereLambda).OrderBy(orderBy) as DbQuery<resourceInfo>;
                    resourceInfo[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                resourceInfo[] nullInfo = new resourceInfo[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找论坛分区表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static postArea[] selectPostArea<TKey>(Expression<Func<postArea, bool>> whereLambda, Expression<Func<postArea, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postArea> dataObject = db.postArea.Where(whereLambda).OrderBy(orderBy) as DbQuery<postArea>;
                    postArea[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                postArea[] nullInfo = new postArea[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找论坛信息表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static postInfo[] selectPostInfo<TKey>(Expression<Func<postInfo, bool>> whereLambda, Expression<Func<postInfo, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dataObject = db.postInfo.Where(whereLambda).OrderBy(orderBy) as DbQuery<postInfo>;
                    postInfo[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                postInfo[] nullInfo = new postInfo[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找论坛回复表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static postReply[] selectPostReply<TKey>(Expression<Func<postReply, bool>> whereLambda, Expression<Func<postReply, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postReply> dataObject = db.postReply.Where(whereLambda).OrderBy(orderBy) as DbQuery<postReply>;
                    postReply[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                postReply[] nullInfo = new postReply[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找问答分区表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static quesArea[] selectQuesArea<TKey>(Expression<Func<quesArea, bool>> whereLambda, Expression<Func<quesArea, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesArea> dataObject = db.quesArea.Where(whereLambda).OrderBy(orderBy) as DbQuery<quesArea>;
                    quesArea[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                quesArea[] nullInfo = new quesArea[0];
                return nullInfo;
            }
        }


        /// <summary>
        /// 查找问答表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static quesAnswInfo[] selectQuesAnswInfo<TKey>(Expression<Func<quesAnswInfo, bool>> whereLambda, Expression<Func<quesAnswInfo, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswInfo> dataObject = db.quesAnswInfo.Where(whereLambda).OrderBy(orderBy) as DbQuery<quesAnswInfo>;
                    quesAnswInfo[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                quesAnswInfo[] nullInfo = new quesAnswInfo[0];
                return nullInfo;
            }
        }


        /// <summary>
        /// 查找问答回复表中的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static quesAnswReply[] selectQuesAnswReply<TKey>(Expression<Func<quesAnswReply, bool>> whereLambda, Expression<Func<quesAnswReply, TKey>> orderBy)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswReply> dataObject = db.quesAnswReply.Where(whereLambda).OrderBy(orderBy) as DbQuery<quesAnswReply>;
                    quesAnswReply[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                quesAnswReply[] nullInfo = new quesAnswReply[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查询充值卡表
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static recharge[] selectRecharge(Expression<Func<recharge, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<recharge> dataObject = db.recharge.Where(whereLambda) as DbQuery<recharge>;
                    recharge[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                recharge[] nullInfo = new recharge[0];
                return nullInfo;
            }
        }


        /// <summary>
        /// 查询展示表
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static serverShowInfo[] selectServerShowInfo(Expression<Func<serverShowInfo, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {

                    DbQuery<serverShowInfo> dataObject = db.serverShowInfo.Where(whereLambda) as DbQuery<serverShowInfo>;
                    serverShowInfo[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                serverShowInfo[] nullInfo = new serverShowInfo[0];
                return nullInfo;
            }
        }
    }
}