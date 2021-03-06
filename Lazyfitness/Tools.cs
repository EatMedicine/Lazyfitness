﻿using Lazyfitness.Models;
using System;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Reflection;
namespace Lazyfitness
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {
        //每页显示的数据条数
        static int pageSize = 5;
        //更改这里后 3个大区的Ajax里的Js代码也要相应修改

        #region 资源分区数据获取

        /// <summary>
        /// 返回表对象用来显示对应的信息
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static resourceInfo[] GetArticle(int partId, int pageNum)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<resourceInfo> dbResourceInfo = db.resourceInfo.Where(u => u.areaId == partId).OrderByDescending(u => u.resourceTime).Skip(pageSize * (pageNum - 1)).Take(pageSize) as DbQuery<resourceInfo>;
                return dbResourceInfo.ToArray();
            }
        }
        /// <summary>
        /// 返回表对象用来显示对应的信息
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static resourceInfo[] GetArticleAll(int pageNum)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<resourceInfo> dbResourceInfo = db.resourceInfo.OrderByDescending(u => u.resourceTime).Skip(pageSize * (pageNum - 1)).Take(pageSize) as DbQuery<resourceInfo>;
                return dbResourceInfo.ToArray();
            }
        }

        /// <summary>
        /// 获取文章的信息
        /// </summary>
        /// <param name="resourceId">文章id</param>
        /// <returns></returns>
        public static resourceInfo GetArticleInfo(int resourceId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var checkDbInfo = db.resourceInfo.Where(u => u.resourceId == resourceId);
                if (checkDbInfo.ToList().Count == 0)
                {
                    return null;
                }
                var dbInfo = checkDbInfo.FirstOrDefault();
                resourceInfo info = new resourceInfo
                {
                    areaId = dbInfo.areaId,
                    resourceId = dbInfo.resourceId,
                    resourceName = dbInfo.resourceName,
                    userId = dbInfo.userId,
                    resourceTime = dbInfo.resourceTime,
                    pageView = dbInfo.pageView,
                    isTop = dbInfo.isTop,
                    resourceContent = dbInfo.resourceContent,
                };
                return info;
            }
        }

        /// <summary>
        /// 获取文章区多个文章信息
        /// </summary>
        /// <param name="whereLambda">筛选文章</param>
        /// <param name="IsDes">是否按照时间倒序排列</param>
        /// <param name="skipNum">跳过数量</param>
        /// <param name="takeNum">取数量</param>
        /// <returns>多个文章信息</returns>
        public static resourceInfo[] GetArticlesInfo(Expression<Func<resourceInfo,bool>> whereLambda,bool IsDes,int skipNum,int takeNum)
        {
            //不合法情况
            if (whereLambda == null || skipNum < 0 || takeNum < 0) 
                return null;
            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                DbQuery<resourceInfo> dbInfo;
                if (IsDes == true) 
                    dbInfo = db.resourceInfo.Where(whereLambda).OrderByDescending(u=>u.resourceTime).Skip(skipNum).Take(takeNum) as DbQuery<resourceInfo>;
                else
                    dbInfo = db.resourceInfo.Where(whereLambda).OrderBy(u => u.resourceTime).Skip(skipNum).Take(takeNum) as DbQuery<resourceInfo>;
                return dbInfo.ToArray();
            }
        }

        /// <summary>
        /// 获取资源区分区的名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <returns></returns>
        public static string GetArticleName(int partId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                resourceArea areainfo = db.resourceArea.Where(u => u.areaId == partId).FirstOrDefault();
                if (areainfo == null)
                {
                    return "未找到分区名";
                }
                else
                {
                    return areainfo.areaName;
                }
            }            
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖人名字
        /// </summary>
        /// <param name="partId">分区id</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartName(int partId, int pageNum)
        {
            resourceInfo[] getOb = GetArticle(partId, pageNum);
            string[] names = new string[pageSize];
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < getOb.Length; i++)
                {
                    int eachUserId = getOb[i].userId.Value;
                    var name = db.userInfo.Where(u => u.userId == eachUserId).FirstOrDefault().userName;
                    names[i] = name;
                }
            }                        
            return names;
        }

        /// <summary>
        /// 获取资源区分区的帖子标题
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartTitle(int partId, int pageNum)
        {
            var getOb = GetArticle(partId, pageNum);
            string[] titles = new string[pageSize];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < getOb.Length; count++)
            {
                string eachTitle = getOb[count].resourceName;
                titles[count] = eachTitle;
            }
            return titles;
        }

        /// <summary>
        /// 获取资源区分区的帖子Url
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartUrl(int partId, int pageNum)
        {
            string[] url = new string[pageSize];
            var getOb = GetArticle(partId, pageNum);
            string basicUrl = "/Home/forumDetail";
            for (int count = 0; count < getOb.Length; count++)
            {
                url[count] = basicUrl + "?num=" + getOb[count].resourceId;
            }
            return url;
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖人头像
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartHeadAdr(int partId, int pageNum)
        {
            string[] urls = new string[pageSize];
            var getOb = GetArticle(partId, pageNum);            
            for (int count = 0; count < getOb.Length; count++)
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    int eachUserId = getOb[count].userId.Value;
                    string url = db.userInfo.Where(u => u.userId == eachUserId).FirstOrDefault().userHeaderPic;
                    urls[count] = url;
                }
            }
            return urls;
        }
        

        /// <summary>
        /// 获取资源区分区的帖子简介
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartIntroduction(int partId, int pageNum)
        {
            string[] introducitons = new string[pageSize];
            var getOb = GetArticle(partId, pageNum);
            for (int count = 0; count < getOb.Length; count++)
            {
                introducitons[count] = GetNoHTMLString(getOb[count].resourceContent);
            }
            return introducitons;
        }

        /// <summary>
        /// 获取文章区的大区信息
        /// </summary>
        /// <returns></returns>
        public static resourceArea[] GetArticleAreaInfo()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                DbQuery<resourceArea> dbResourceArea = db.resourceArea as DbQuery<resourceArea>;
                int listLength = dbResourceArea.ToArray().Length;
                resourceArea[] info = new resourceArea[listLength];            
                for (int count = 0; count < listLength; count++)
                {
                    info[count] = new resourceArea
                    {
                        areaId = dbResourceArea.ToArray()[count].areaId,
                        areaName = dbResourceArea.ToArray()[count].areaName,
                        areaBrief = dbResourceArea.ToArray()[count].areaBrief,
                    };
                }
                return info;
            }                 
        }
        #endregion
        #region 问答分区数据获取

        /// <summary>
        /// 返回表对象用来显示对应的信息
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static quesAnswInfo[] GetQuestion(int partId, int pageNum)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<quesAnswInfo> dbQuestionInfo = db.quesAnswInfo.Where(u => u.areaId == partId).OrderByDescending(u => u.quesAnswTime).Skip(pageSize * (pageNum - 1)).Take(pageSize) as DbQuery<quesAnswInfo>;
                return dbQuestionInfo.ToArray();
            }
        }

        /// <summary>
        /// 返回表对象用来显示对应的信息
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static quesAnswInfo[] GetQuestionAll(int pageNum)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<quesAnswInfo> dbQuestionInfo = db.quesAnswInfo.OrderByDescending(u => u.quesAnswTime).Skip(pageSize * (pageNum - 1)).Take(pageSize) as DbQuery<quesAnswInfo>;
                return dbQuestionInfo.ToArray();
            }
        }

        /// <summary>
        /// 获取问答区分区的名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string GetQuestionName(int partId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var QuestionName = db.quesArea.Where(u => u.areaId == partId).FirstOrDefault().areaName;
                return QuestionName;
            }            
        }

        /// <summary>
        /// 获取问答区分区的帖子发帖名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartName(int partId, int pageNum)
        {

            var getOb = GetQuestion(partId, pageNum);
            string[] names = new string[pageSize];
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < getOb.Length; i++)
                {
                    int eachUserId = getOb[i].userId.Value;
                    var name = db.userInfo.Where(u => u.userId == eachUserId).FirstOrDefault().userName;
                    names[i] = name;
                }
            }
            return names;
        }

        /// <summary>
        /// 获取问答区分区的帖子标题
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartTitle(int partId, int pageNum)
        {

            var getOb = GetQuestion(partId, pageNum);
            string[] titles = new string[pageSize];
            for (int count = 0; count < getOb.Length; count++)
            {
                string eachTitle = getOb[count].quesAnswTitle;
                titles[count] = eachTitle;
            }
            return titles;
        }

        /// <summary>
        /// 获取问答区分区的帖子Url
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartUrl(int partId, int pageNum)
        {
            string[] url = new string[pageSize];
            var getOb = GetQuestion(partId, pageNum);
            string basicUrl = "/Home/QuestionDetail";
            for (int count = 0; count < getOb.Length; count++)
            {
                url[count] = basicUrl + "?num=" + getOb[count].quesAnswId;
            }
            return url;
        }

        /// <summary>
        /// 获取问答区分区的帖子发帖人头像
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartHeadAdr(int partId, int pageNum)
        {

            string[] urls = new string[pageSize];
            var getOb = GetQuestion(partId, pageNum);
            for (int count = 0; count < getOb.Length; count++)
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    int eachUserId = getOb[count].userId.Value;
                    string url = db.userInfo.Where(u => u.userId == eachUserId).FirstOrDefault().userHeaderPic;
                    urls[count] = url;
                }
            }
            return urls;
        }


        /// <summary>
        /// 过滤不符合要求的内容
        /// </summary>
        /// <param name="resourceContent"></param>
        /// <returns></returns>
        public static string filterQuestionContent(string questionContent)
        {
            return questionContent;
        }

        /// <summary>
        /// 获取问答区分区的帖子简介
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartIntroduction(int partId, int pageNum)
        {

            string[] introducitons = new string[pageSize];
            var getOb = GetQuestion(partId, pageNum);
            for (int count = 0; count < getOb.Length; count++)
            {
                introducitons[count] = GetNoHTMLString(getOb[count].quesAnswContent);
            }
            return introducitons;
        }

        /// <summary>
        /// 获取资源区分区的帖子悬赏金额
        /// </summary>
        /// <param name="partId">分区id</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static int[] GetQuestionPartMoney(int partId, int pageNum)
        {
            int[] moneys = new int[pageSize];
            var getOb = GetQuestion(partId, pageNum);        
            for (int count = 0; count < getOb.Length; count++)
            {
                moneys[count] = getOb[count].amount.Value;
            }
            return moneys;
        }

        /// <summary>
        /// 获取问答帖子的信息
        /// </summary>
        /// <param name="quesId">问答帖子id</param>
        /// <returns></returns>
        public static quesAnswInfo GetQuestionInfo(int quesId)
        {
            //此处应从数据库获取对应问答帖子的信息

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var checkDbInfo = db.quesAnswInfo.Where(u => u.quesAnswId == quesId);
                if (checkDbInfo.ToList().Count == 0)
                {
                    return null;
                }
                var dbInfo = checkDbInfo.FirstOrDefault();
                quesAnswInfo info = new quesAnswInfo
                {
                    areaId = dbInfo.areaId,
                    quesAnswId = quesId,
                    quesAnswTitle = dbInfo.quesAnswTitle,
                    userId = dbInfo.userId,
                    quesAnswTime = dbInfo.quesAnswTime,
                    pageView = dbInfo.pageView,
                    isPost = dbInfo.isPost,
                    quesAnswStatus = dbInfo.quesAnswStatus,
                    amount = dbInfo.amount,
                    quesAnswContent = dbInfo.quesAnswContent,
                };
                return info;
            }          
        }

        /// <summary>
        /// 获取问答帖子的回帖信息
        /// </summary>
        /// <param name="quesId">问答帖子id</param>
        /// <returns></returns>
        public static quesAnswReply[] GetQuestionReply(int quesId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<quesAnswReply> dbQuesAnswReplyInfo = db.quesAnswReply.Where(u => u.quesAnswId == quesId).OrderBy(u=>u.replyTime) as DbQuery<quesAnswReply>;
                int listLength = dbQuesAnswReplyInfo.ToList().Count;
                quesAnswReply[] replys = new quesAnswReply[listLength];
                var obInfo = dbQuesAnswReplyInfo.ToArray();
                for (int count = 0; count < listLength; count++)
                {
                    replys[count] = new quesAnswReply
                    {
                        quesAnswId = obInfo[count].quesAnswId,
                        userId = obInfo[count].userId,
                        replyTime = obInfo[count].replyTime,
                        replyContent = obInfo[count].replyContent,
                        isAgree = obInfo[count].isAgree,
                    };
                }
                return dbQuesAnswReplyInfo.ToArray();
            }     
        }

        /// <summary>
        /// 获取问答区的大区信息
        /// </summary>
        /// <returns></returns>
        public static quesArea[] GetQuestionAreaInfo()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbInfo = db.quesArea.OrderBy(u => u.areaId);
                int listLength = dbInfo.ToList().Count;
                var obInfo = dbInfo.ToArray();
                quesArea[] info = new quesArea[listLength];                
                for (int count = 0; count < obInfo.Length; count++)
                {
                    info[count] = new quesArea
                    {
                        areaId = obInfo[count].areaId,
                        areaName = obInfo[count].areaName,
                        areaBrief = obInfo[count].areaBrief,
                    };
                }
                return info;
            }
        }
        #endregion
        #region 论坛数据获取

        /// <summary>
        /// 返回表对象用来显示对应的信息
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static postInfo[] GetForum(int partId, int pageNum)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<postInfo> dbForumInfo = db.postInfo.Where(u => u.areaId == partId).OrderByDescending(u => u.postTime).Skip(pageSize * (pageNum - 1)).Take(pageSize) as DbQuery<postInfo>;
                return dbForumInfo.ToArray();
            }
        }
        /// <summary>
        /// 返回表对象用来显示对应的信息
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static postInfo[] GetForumAll(int pageNum)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<postInfo> dbForumInfo = db.postInfo.OrderByDescending(u => u.postTime).Skip(pageSize * (pageNum - 1)).Take(pageSize) as DbQuery<postInfo>;
                return dbForumInfo.ToArray();
            }
        }


        /// <summary>
        /// 获取论坛区分区的名字
        /// </summary>
        /// <param name="partId">分区id</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string GetforumName(int partId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var forumName = db.postArea.Where(u => u.areaId == partId).FirstOrDefault().areaName;
                return forumName;
            }
        }

        /// <summary>
        /// 获取论坛区分区的帖子发帖名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartName(int partId, int pageNum)
        {
            var getOb = GetForum(partId, pageNum);
            string[] names = new string[pageSize];
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int i = 0; i < getOb.Length; i++)
                {
                    int eachUserId = getOb[i].userId.Value;
                    var name = db.userInfo.Where(u => u.userId == eachUserId).FirstOrDefault().userName;
                    names[i] = name;
                }
            }
            return names;
        }

        /// <summary>
        /// 获取论坛区分区的帖子标题
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartTitle(int partId, int pageNum)
        {

            var getOb = GetForum(partId, pageNum);
            string[] titles = new string[pageSize];
            for (int count = 0; count < getOb.Length; count++)
            {
                string eachTitle = getOb[count].postTitle;
                titles[count] = eachTitle;
            }
            return titles;
        }

        /// <summary>
        /// 获取论坛区分区的帖子Url
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartUrl(int partId, int pageNum)
        {

            string[] url = new string[pageSize];
            var getOb = GetForum(partId, pageNum);
            string basicUrl = "/Home/forumDetail";
            for (int count = 0; count < getOb.Length; count++)
            {
                url[count] = basicUrl + "?num=" + getOb[count].postId;
            }
            return url;
        }

        /// <summary>
        /// 获取论坛区分区的帖子发帖人头像
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartHeadAdr(int partId, int pageNum)
        {

            string[] urls = new string[pageSize];
            var getOb = GetForum(partId, pageNum);
            for (int count = 0; count < getOb.Length; count++)
            {
                using (LazyfitnessEntities db = new LazyfitnessEntities())
                {
                    int eachUserId = getOb[count].userId.Value;
                    string url = db.userInfo.Where(u => u.userId == eachUserId).FirstOrDefault().userHeaderPic;
                    urls[count] = url;
                }
            }
            return urls;
        }

        /// <summary>
        /// 获取论坛区分区的帖子简介
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartIntroduction(int partId, int pageNum)
        {

            string[] introducitons = new string[pageSize];
            var getOb = GetForum(partId, pageNum);
            for (int count = 0; count < getOb.Length; count++)
            {
                introducitons[count] = GetNoHTMLString(getOb[count].postContent);
            }
            return introducitons;
        }

        /// <summary>
        /// 获取帖子的发帖信息
        /// </summary>
        /// <param name="postId">帖子id</param>
        /// <returns></returns>
        public static postInfo GetforumInfo(int postId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbinfo = db.postInfo.Where(u => u.postId == postId);
                if (dbinfo.ToList().Count == 0)
                {
                    return null;
                }
                var obInfo = dbinfo.FirstOrDefault();
                postInfo info = new postInfo
                {
                    areaId = obInfo.areaId,
                    postId = postId,
                    postTitle = obInfo.postTitle,
                    userId = obInfo.userId,
                    postTime = obInfo.postTime,
                    pageView = obInfo.pageView,
                    isPost = obInfo.isPost,
                    amount = obInfo.amount,
                    postStatus = obInfo.postStatus,
                    postContent = obInfo.postContent,
                };
                return info;
            }            
        }

        /// <summary>
        /// 获取帖子的回帖
        /// </summary>
        /// <param name="postId">帖子id</param>
        /// <returns></returns>
        public static postReply[] GetforumReply(int postId)
        {

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //分页查询返回的对象
                DbQuery<postReply> dbQuesAnswReplyInfo = db.postReply.Where(u => u.postId == postId).OrderBy(u => u.replyTime) as DbQuery<postReply>;
                int listLength = dbQuesAnswReplyInfo.ToList().Count;
                postReply[] replys = new postReply[listLength];
                var obInfo = dbQuesAnswReplyInfo.ToArray();
                for (int count = 0; count < listLength; count++)
                {
                    replys[count] = new postReply
                    {
                        postId = obInfo[count].postId,
                        userId = obInfo[count].userId,
                        replyTime = obInfo[count].replyTime,
                        replyContent = obInfo[count].replyContent,
                    };
                }
                return replys;
            }
        }

        /// <summary>
        /// 获取论坛的大区信息
        /// </summary>
        /// <returns></returns>
        public static postArea[] GetforumAreaInfo()
        {

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbInfo = db.postArea.OrderBy(u => u.areaId);
                int listLength = dbInfo.ToList().Count;
                var obInfo = dbInfo.ToArray();
                postArea[] info = new postArea[listLength];
                for (int count = 0; count < obInfo.Length; count++)
                {
                    info[count] = new postArea
                    {
                        areaId = obInfo[count].areaId,
                        areaName = obInfo[count].areaName,
                        areaBrief = obInfo[count].areaBrief,
                    };
                }
                return info;
            }
        }
        #endregion
        #region 用户信息获取
        /// <summary>
        /// 获取用户名字
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static string GetUserName(int userId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var userName = db.userInfo.Where(u => u.userId == userId).FirstOrDefault().userName;
                return userName;
            }
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static string GetUserPicAdr(int userId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var userPicAdr = db.userInfo.Where(u => u.userId == userId).FirstOrDefault().userHeaderPic;
                return userPicAdr;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static userInfo GetUserInfo(int userId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var obInfo = db.userInfo.Where(u => u.userId == userId).FirstOrDefault();
                if (obInfo == null)
                    return null;
                userInfo info = new userInfo
                {
                    userName = obInfo.userName,
                    userId = userId,
                    userAge = obInfo.userAge,
                    userSex = obInfo.userSex,
                    userEmail = obInfo.userEmail,
                    userStatus = obInfo.userStatus,
                    userAccount = obInfo.userAccount,
                    userSecurity = null,
                    userHeaderPic = obInfo.userHeaderPic,
                    userIntroduce = obInfo.userIntroduce
                };
                return info;
            }
        }

        /// <summary>
        /// 获取多个用户信息
        /// </summary>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>若存在查询不到的ID则对应userInfo为空</returns>
        public static userInfo[] GetUserInfo(int[] userIds)
        {
            //为空返回0长度数组
            if (userIds == null)
            {
                return new userInfo[0];
            }
            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                userInfo[] infos = new userInfo[userIds.Length];
                for(int count = 0; count < userIds.Length; count++)
                {
                    userInfo info = db.userInfo.Where(user => user.userId == userIds[count]).FirstOrDefault();
                    infos[count] = info;
                }
                return infos;
            }
        }

        public static userInfo[] GetUserInfo(object[] containUserIdObj)
        {
            //不合法情况
            if (containUserIdObj == null)
                return new userInfo[0];
            //获取类型
            Type typeObj = containUserIdObj[0].GetType();
            PropertyInfo[] proInfo =typeObj.GetProperties();
            int index = -1;
            //获取属性index
            for(int count = 0; count < proInfo.Length; count++)
            {
                if (proInfo[count].Name == "userId")
                {
                    index = count;
                    break;
                }
            }
            //未找到userId
            if (index == -1)
            {
                return new userInfo[0];
            }
            userInfo[] info = new userInfo[containUserIdObj.Length];
            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                for (int count = 0; count < info.Length; count++)
                {
                    int id = (int)containUserIdObj[count].GetType().GetProperties()[index].GetValue(containUserIdObj[count], null);
                    info[count] = db.userInfo.Where(user => user.userId == id).FirstOrDefault();
                }
            }
            return info;

        }
        #endregion

        //以下函数就算没获取到数据也要返回一个长度为0的数组 不要返回NULL
        #region 首页数据
        /// <summary>
        /// 获取首页的轮播图数据
        /// </summary>
        /// <returns></returns>
        public static serverShowInfo[] GetIndexScroll()
        {

            using(LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //读取serverShowInfo中的数据 flag = 0 为轮播图
                var dbInfo = db.serverShowInfo.Where(u=>u.flag == 0).Where(u=>u.Infostatus == 1 && u.areaId == 0);
                //获取serverShowInfo中数据的条数
                var obInfo = dbInfo.ToArray();
                int listLenth = obInfo.Length;
                //如果serverShowInfo为空则返回一个长度为0的数组
                if (listLenth == 0)
                {
                    serverShowInfo[] nullInfo = new serverShowInfo[0];
                    return nullInfo;
                }
                //对象不为空的时候 把对象中的数据封装到相应的字符串数组中
                //注：picAdr为空时候存入的值也是为空（需要修改请联系Oliver
                ArrayList picAdr = new ArrayList();
                ArrayList title = new ArrayList();
                //flag = 0即轮播图下 所有轮播图的url
                ArrayList url = new ArrayList();
                foreach (var item in obInfo)
                {
                    picAdr.Add(item.pictureAdr);
                    title.Add(item.title);
                    url.Add(item.url);
                }
                //暂定areaId 0为首页  1为文章区 
                //暂定flag 0为轮播图 1为公告
                //暂定InfoStatus 0为禁用 1为启用
                //首页只要3条轮播图
                serverShowInfo[] info;
                if (listLenth>3)
                {
                    info = new serverShowInfo[3];
                }
                else
                {
                    info = new serverShowInfo[listLenth];
                }
                for (int count = 0; count < listLenth && count < 3; count++) 
                {
                    info[count] = new serverShowInfo
                    {
                        id = count,
                        areaId = 0,
                        title = title[count].ToString(),
                        pictureAdr = picAdr[count].ToString(),
                        //考虑到这里地址不一定是本网站的文章地址 所以还是写完整URL比较好
                        //比如说广告啥的（雾
                        //url已经构造好，使用的时候解除注释

                        url = url[count].ToString(),
                        flag = 0,
                        Infostatus = 1
                    };
                }
                return info;

            }
        }

        /// <summary>
        /// 获取首页的公告数据
        /// </summary>
        /// <returns></returns>
        public static serverShowInfo[] GetIndexNotice()
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                //读取serverShowInfo中的数据 flag = 1 为公告
                var dbInfo = db.serverShowInfo.Where(u => u.flag == 1).Where(u=>u.Infostatus == 1 && u.areaId == 0);
                //获取serverShowInfo中数据的条数
                var obInfo = dbInfo.ToArray();
                int listLenth = obInfo.Length;
                //如果serverShowInfo为空则返回一个长度为0的数组
                if (listLenth == 0)
                {
                    serverShowInfo[] nullInfo = new serverShowInfo[0];
                    return nullInfo;
                }
                ArrayList title = new ArrayList();
                //flag = 1即公告下 所有公告的url
                ArrayList url = new ArrayList();
                foreach (var item in obInfo)
                {
                    title.Add(item.title);
                    url.Add(item.url);
                }

                //暂定areaId 0为首页  1为文章区 
                //暂定flag 0为轮播图 1为公告
                //暂定InfoStatus 0为禁用 1为启用
                //首页公告有5条
                serverShowInfo[] info = new serverShowInfo[title.Count];
                for (int count = 0; count < 5 && count < title.Count; count++) 
                {
                    info[count] = new serverShowInfo
                    {
                        id = count,
                        areaId = 0,
                        title = title[count].ToString(),
                        pictureAdr = "",
                        //考虑到这里地址不一定是本网站的文章地址 所以还是写完整URL比较好
                        //比如说广告啥的（雾
                        //url已经构造好，使用的时候解除注释

                        url = url[count].ToString(),
                        flag = 1,
                        Infostatus = 1
                    };
                }
                return info;
            }            
        }

        /// <summary>
        /// 获取一些文章信息
        /// </summary>
        /// <param name="sortFlag">排序规则，
        /// 暂定0为默认排序
        /// 1为时间排序
        /// 2为访问量排序</param>
        /// <param name="IsDes">是否是降序排序</param>
        /// <param name="num">取的数量</param>
        /// <returns></returns>
        public static resourceInfo[] GetArticleDetailInfo(int sortFlag,bool IsDes,int num)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbInfo = db.resourceInfo;           
                //获取数据条数
                int maxNum = dbInfo.ToList().Count;
                //当所取条数大于数据条数时，取数据条数
                if (num > maxNum)
                {
                    num = maxNum;
                }
                resourceInfo[] info = new resourceInfo[num];

                //对开始进行条件筛选
                switch (IsDes)
                {
                    case false:
                        switch (sortFlag)
                        {
                            case 1:
                                var obInfof1 = dbInfo.OrderBy(u => u.resourceTime).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new resourceInfo
                                    {
                                        resourceName = obInfof1[count].resourceName,
                                        resourceId = obInfof1[count].resourceId,
                                    };
                                }
                                break;
                            case 2:
                                var obInfof2 = dbInfo.OrderBy(u => u.pageView).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new resourceInfo
                                    {
                                        resourceName = obInfof2[count].resourceName,
                                        resourceId = obInfof2[count].resourceId,
                                    };
                                }
                                break;
                            default:
                                var obInfo = dbInfo.ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new resourceInfo
                                    {
                                        resourceName = obInfo[count].resourceName,
                                        resourceId = obInfo[count].resourceId,
                                    };
                                }
                                break;
                        }
                        break;
                    case true:
                        switch (sortFlag)
                        {                            
                            case 1:
                                var obInfot1 = dbInfo.OrderByDescending(u => u.resourceTime).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new resourceInfo
                                    {
                                        resourceName = obInfot1[count].resourceName,
                                        resourceId = obInfot1[count].resourceId,
                                    };
                                }
                                break;
                            case 2:
                                var obInfot2 = dbInfo.OrderByDescending(u => u.pageView).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new resourceInfo
                                    {
                                        resourceName = obInfot2[count].resourceName,
                                        resourceId = obInfot2[count].resourceId,
                                    };
                                }
                                break;
                            default:
                                var obInfo = dbInfo.ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new resourceInfo
                                    {
                                        resourceName = obInfo[count].resourceName,
                                        resourceId = obInfo[count].resourceId,
                                    };
                                }
                                break;
                        }
                        break;
                }
                return info;
            }
        }

        /// <summary>
        /// 获取一些问答信息
        /// </summary>
        /// <param name="sortFlag">排序规则，
        /// 暂定0为默认排序
        /// 1为时间排序
        /// 2为访问量排序</param>
        /// <param name="IsDes">是否是降序排序</param>
        /// <param name="num">取的数量</param>
        /// <returns></returns>
        public static quesAnswInfo[] GetQuestionDetailInfo(int sortFlag, bool IsDes, int num)
        {

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbInfo = db.quesAnswInfo;
                //获取数据条数
                int maxNum = dbInfo.ToList().Count;
                //当所取条数大于数据条数时，取数据条数
                if (num > maxNum)
                {
                    num = maxNum;
                }
                quesAnswInfo[] info = new quesAnswInfo[num];

                //对开始进行条件筛选
                switch (IsDes)
                {
                    case false:
                        switch (sortFlag)
                        {
                            case 1:
                                var obInfof1 = dbInfo.OrderBy(u => u.quesAnswTime).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new quesAnswInfo
                                    {
                                        quesAnswTitle = obInfof1[count].quesAnswTitle,
                                        quesAnswId = obInfof1[count].quesAnswId,
                                    };
                                }
                                break;
                            case 2:
                                var obInfof2 = dbInfo.OrderBy(u => u.pageView).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new quesAnswInfo
                                    {
                                        quesAnswTitle = obInfof2[count].quesAnswTitle,
                                        quesAnswId = obInfof2[count].quesAnswId,
                                    };
                                }
                                break;
                            default:
                                var obInfo = dbInfo.ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new quesAnswInfo
                                    {
                                        quesAnswTitle = obInfo[count].quesAnswTitle,
                                        quesAnswId = obInfo[count].quesAnswId,
                                    };
                                }
                                break;
                        }
                        break;
                    case true:
                        switch (sortFlag)
                        {
                            case 1:
                                var obInfot1 = dbInfo.OrderByDescending(u => u.quesAnswTime).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new quesAnswInfo
                                    {
                                        quesAnswTitle = obInfot1[count].quesAnswTitle,
                                        quesAnswId = obInfot1[count].quesAnswId,
                                    };
                                }
                                break;
                            case 2:
                                var obInfot2 = dbInfo.OrderByDescending(u => u.pageView).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new quesAnswInfo
                                    {
                                        quesAnswTitle = obInfot2[count].quesAnswTitle,
                                        quesAnswId = obInfot2[count].quesAnswId,
                                    };
                                }
                                break;
                            default:
                                var obInfo = dbInfo.ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new quesAnswInfo
                                    {
                                        quesAnswTitle = obInfo[count].quesAnswTitle,
                                        quesAnswId = obInfo[count].quesAnswId,
                                    };
                                }
                                break;
                        }
                        break;
                }
                return info;
            }
        }

        /// <summary>
        /// 获取一些论坛信息
        /// </summary>
        /// <param name="sortFlag">排序规则，
        /// 暂定0为默认排序
        /// 1为时间排序
        /// 2为访问量排序</param>
        /// <param name="IsDes">是否是降序排序</param>
        /// <param name="num">取的数量</param>
        /// <returns></returns>
        public static postInfo[] GetforumDetailInfo(int sortFlag, bool IsDes, int num)
        {

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbInfo = db.postInfo;
                //获取数据条数
                int maxNum = dbInfo.ToList().Count;
                //当所取条数大于数据条数时，取数据条数
                if (num > maxNum)
                {
                    num = maxNum;
                }
                postInfo[] info = new postInfo[num];

                //对开始进行条件筛选
                switch (IsDes)
                {
                    case false:
                        switch (sortFlag)
                        {
                            case 1:
                                var obInfof1 = dbInfo.OrderBy(u => u.postTime).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new postInfo
                                    {
                                        postTitle = obInfof1[count].postTitle,
                                        postId = obInfof1[count].postId,
                                    };
                                }
                                break;
                            case 2:
                                var obInfof2 = dbInfo.OrderBy(u => u.pageView).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new postInfo
                                    {
                                        postTitle = obInfof2[count].postTitle,
                                        postId = obInfof2[count].postId,
                                    };
                                }
                                break;
                            default:
                                var obInfo = dbInfo.ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new postInfo
                                    {
                                        postTitle = obInfo[count].postTitle,
                                        postId = obInfo[count].postId,
                                    };
                                }
                                break;
                        }
                        break;
                    case true:
                        switch (sortFlag)
                        {
                            case 1:
                                var obInfot1 = dbInfo.OrderByDescending(u => u.postTime).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new postInfo
                                    {
                                        postTitle = obInfot1[count].postTitle,
                                        postId = obInfot1[count].postId,
                                    };
                                }
                                break;
                            case 2:
                                var obInfot2 = dbInfo.OrderByDescending(u => u.pageView).ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new postInfo
                                    {
                                        postTitle = obInfot2[count].postTitle,
                                        postId = obInfot2[count].postId,
                                    };
                                }
                                break;
                            default:
                                var obInfo = dbInfo.ToArray();
                                for (int count = 0; count < num; count++)
                                {
                                    info[count] = new postInfo
                                    {
                                        postTitle = obInfo[count].postTitle,
                                        postId = obInfo[count].postId,
                                    };
                                }
                                break;
                        }
                        break;
                }
                return info;
            }
        }

        #endregion
        #region 文章大区数据
        /// <summary>
        /// 获取首页的轮播图数据
        /// </summary>
        /// <returns></returns>
        public static serverShowInfo[] GetArticleScroll()
        {

            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                var dbInfo = db.serverShowInfo;
                var obInfo = dbInfo.Where(u => u.flag == 0 && u.Infostatus == 1 && u.areaId == 1).ToArray();
                int listLenth = obInfo.Length;
                //如果serverShowInfo为空则返回一个长度为0的数组
                if (listLenth == 0)
                {
                    serverShowInfo[] nullInfo = new serverShowInfo[0];
                    return nullInfo;
                }
                ArrayList picAdr = new ArrayList();
                ArrayList title = new ArrayList();
                ArrayList url = new ArrayList();
                foreach (var item in obInfo)
                {
                    picAdr.Add(item.pictureAdr);
                    title.Add(item.title);
                    url.Add(item.url);
                }
                //暂定areaId 0为首页  1为文章区 
                //暂定flag 0为轮播图 1为公告
                //暂定InfoStatus 0为禁用 1为启用
                //首页只要3条轮播图
                serverShowInfo[] info;
                if (listLenth > 3)
                {
                    info = new serverShowInfo[3];
                }
                else
                {
                    info = new serverShowInfo[listLenth];
                }
                for (int count = 0; count < listLenth && count < 3; count++)
                {
                    info[count] = new serverShowInfo
                    {
                        id = count,
                        areaId = 1,
                        title = title[count].ToString(),
                        pictureAdr = picAdr[count].ToString(),
                        //考虑到这里地址不一定是本网站的文章地址 所以还是写完整URL比较好
                        //比如说广告啥的（雾
                        //url已经构造好，使用的时候解除注释

                        //url = url[count].ToString(),
                        url = url[count].ToString(),
                        flag = 0,
                        Infostatus = 1
                    };
                }
                return info;
            }                       
        }
        #endregion     
        public static string GetNoHTMLString(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        /// <summary>
        /// 获取后台数据
        /// </summary>
        /// <param name="managerId">后台登录ID</param>
        /// <returns></returns>
        public static backManager GetBackManagerInfo(int managerId)
        {
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                backManager info = db.backManager.Where(u => u.managerId == managerId).FirstOrDefault();
                if (info == null)
                {
                    return null;
                }
                else
                {
                    return info;
                }
            }
        }

        /// <summary>
        /// 弹窗并跳到指定Url
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="Url"></param>
        public static void AlertAndRedirect(string msg,string Url)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, Url));
            HttpContext.Current.Response.End();
        }
        #region 获取网站显示数据
        /// <summary>
        /// 获取本网站名字，用于设置网站title后缀
        /// </summary>
        /// <returns></returns>
        public static string GetWebsiteName()
        {
            //此处应取出数据，这里先用该数据代替
            serverShowInfo[] serverInfo = Areas.toolsHelpers.selectToolsController.selectServerShowInfo(u=>u.flag == 2 && u.areaId == 3);
            string title = "Default Name";            
            if (serverInfo.Length == 0 || serverInfo == null)
            {
                return title;    
            }
            title = serverInfo[0].title;
            return title;
        }

        /// <summary>
        /// 获取Hello页面的背景大图链接
        /// </summary>
        /// <returns></returns>
        public static string GetHelloPic()
        {
            serverShowInfo[] serverInfo = Areas.toolsHelpers.selectToolsController.selectServerShowInfo(u => u.flag == 2 && u.areaId == 0);
            string picAddress = "/Resource/picture/man-2037255_1920.jpg";
            if (serverInfo.Length == 0 || serverInfo == null)
            {
                return picAddress;
            }
            if(serverInfo[0].pictureAdr == "" || serverInfo[0].pictureAdr == null)
            {
                return picAddress;
            }
            picAddress = serverInfo[0].pictureAdr;
            return picAddress;
        }
        
        /// <summary>
        /// 获取Hello页面的Slogan
        /// </summary>
        /// <returns></returns>
        public static string GetHelloSlogan()
        {
            serverShowInfo[] serverInfo = Areas.toolsHelpers.selectToolsController.selectServerShowInfo(u => u.flag == 2 && u.areaId == 0);
            string slogan = "Please contact Oliver Eason or Kaimar";
            if (serverInfo.Length == 0 || serverInfo == null)
            {
                return slogan;
            }
            if(serverInfo[0].title == null||serverInfo[0].title == "")
            {
                return slogan;
            }
            slogan = serverInfo[0].title;
            return slogan;
        }
        
        /// <summary>
        /// 获取模板页面的头图
        /// </summary>
        /// <returns></returns>
        public static string GetLayoutPic()
        {
            serverShowInfo[] serverInfo = Areas.toolsHelpers.selectToolsController.selectServerShowInfo(u => u.flag == 2 && u.areaId == 1);
            string headPicAddress = "/Resource/picture/running-1705716_1920.jpg";
            if (serverInfo.Length == 0 || serverInfo == null)
            {
                return headPicAddress;
            }
            headPicAddress = serverInfo[0].pictureAdr;
            return headPicAddress;
        }
        #endregion
    }
            
}