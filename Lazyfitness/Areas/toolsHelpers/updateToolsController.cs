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
    }
}