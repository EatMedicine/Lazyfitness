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

        /// <summary>
        /// 修改文章表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 修改论坛分区表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updatePostArea(Expression<Func<postArea, bool>> whereLambda, postArea info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postArea> dataObject = db.postArea.Where(whereLambda) as DbQuery<postArea>;
                    postArea oldInfo = dataObject.FirstOrDefault();
                    oldInfo.areaBrief = info.areaBrief;
                    oldInfo.areaName = info.areaName;
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
        /// 修改论坛帖子表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updatePostInfo(Expression<Func<postInfo, bool>> whereLambda, postInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<postInfo> dataObject = db.postInfo.Where(whereLambda) as DbQuery<postInfo>;
                    postInfo oldInfo = dataObject.FirstOrDefault();
                    oldInfo.areaId = info.areaId;
                    oldInfo.postTitle = info.postTitle;
                    oldInfo.pageView = info.pageView;
                    oldInfo.isPost = info.isPost;
                    oldInfo.amount = info.amount;
                    oldInfo.postStatus = info.postStatus;
                    oldInfo.postContent = info.postContent;
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
        /// 修改问答分区表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updateQuesArea(Expression<Func<quesArea, bool>> whereLambda, quesArea info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesArea> dataObject = db.quesArea.Where(whereLambda) as DbQuery<quesArea>;
                    quesArea oldInfo = dataObject.FirstOrDefault();
                    oldInfo.areaBrief = info.areaBrief;
                    oldInfo.areaName = info.areaName;
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
        /// 修改问答帖子表中的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updateQuesAnswInfo(Expression<Func<quesAnswInfo, bool>> whereLambda, quesAnswInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<quesAnswInfo> dataObject = db.quesAnswInfo.Where(whereLambda) as DbQuery<quesAnswInfo>;
                    quesAnswInfo oldInfo = dataObject.FirstOrDefault();
                    oldInfo.areaId = info.areaId;
                    oldInfo.quesAnswTitle = info.quesAnswTitle;
                    oldInfo.pageView = info.pageView;
                    oldInfo.isPost = info.isPost;
                    oldInfo.amount = info.amount;
                    oldInfo.quesAnswStatus = info.quesAnswStatus;
                    oldInfo.quesAnswContent = info.quesAnswContent;
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
        /// 往充值卡表修改数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updateRecharge(Expression<Func<recharge, bool>> whereLambda, recharge info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<recharge> dataObject = db.recharge.Where(whereLambda) as DbQuery<recharge>;
                    recharge oldInfo = dataObject.FirstOrDefault();
                    oldInfo.rechargePwd = info.rechargePwd;
                    oldInfo.amount = info.amount;
                    oldInfo.isAvailable = info.isAvailable;
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
        /// 往展示表修改数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Boolean updateServerShowInfo(Expression<Func<serverShowInfo, bool>> whereLambda, serverShowInfo info)
        {
            try
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    DbQuery<serverShowInfo> dataObject = db.serverShowInfo.Where(whereLambda) as DbQuery<serverShowInfo>;
                    serverShowInfo oldInfo = dataObject.FirstOrDefault();
                    oldInfo.title = info.title;
                    oldInfo.pictureAdr = info.pictureAdr;
                    oldInfo.url = info.url;
                    oldInfo.Infostatus = info.Infostatus;
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