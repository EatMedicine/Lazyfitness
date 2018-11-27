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
                using(LazyfitnessEntities db = new LazyfitnessEntities())
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

        #region 判断有无数据
        public static Boolean isExistResourceArea(Expression<Func<resourceArea, bool>> whereLambda)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceArea> dataObject = db.resourceArea.Where(whereLambda) as DbQuery<resourceArea>;
                    if (dataObject.ToList().Count == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}