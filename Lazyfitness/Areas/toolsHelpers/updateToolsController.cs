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
    public class updateToolsController : Controller
    {
        /// <summary>
        /// 修改userInfo表的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updateUserInfo(Expression<Func<userInfo, bool>> whereLambda, userInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<userInfo> dataObject = db.userInfo.Where(whereLambda) as DbQuery<userInfo>;
                    userInfo oldInfo = dataObject.FirstOrDefault();
                    oldInfo.userName = info.userName;
                    oldInfo.userAge = info.userAge;
                    oldInfo.userSex = info.userSex;
                    oldInfo.userEmail = info.userEmail;
                    oldInfo.userStatus = info.userStatus;
                    oldInfo.userAccount = info.userAccount;
                    oldInfo.userIntroduce = info.userIntroduce;
                    oldInfo.userHeaderPic = info.userHeaderPic;
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
        /// 往文章分区表修改数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updateResourceArea(Expression<Func<resourceArea, bool>> whereLambda, resourceArea info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceArea> dataObject = db.resourceArea.Where(whereLambda) as DbQuery<resourceArea>;
                    resourceArea oldInfo = dataObject.FirstOrDefault();
                    oldInfo.areaName = info.areaName;
                    oldInfo.areaBrief = info.areaBrief;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static Boolean updateResourceInfo(Expression<Func<resourceInfo, bool>> whereLambda, resourceInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<resourceInfo> dataObject = db.resourceInfo.Where(whereLambda) as DbQuery<resourceInfo>;
                    resourceInfo oldInfo = dataObject.FirstOrDefault();
                    oldInfo.areaId = info.areaId;
                    oldInfo.resourceName = info.resourceName;
                    oldInfo.pageView = info.pageView;
                    oldInfo.isTop = info.isTop;
                    oldInfo.resourceContent = info.resourceContent;
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