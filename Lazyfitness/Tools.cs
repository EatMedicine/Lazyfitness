using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazyfitness
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {

        #region 资源分区数据获取

        /// <summary>
        /// 获取资源区分区的名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string GetArticleName(int partId)
        {
            return "分区："+partId.ToString()+"的名字";
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartName(int partId, int pageNum)
        {
            string[] names = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                names[count] = "姓名" + count.ToString();
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
            string[] titles = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                titles[count] = "标题" + count.ToString();
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
            string[] url = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                url[count] = "#";
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
            string[] url = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                url[count] = "/Resource/picture/DefaultHeadPic.jpg";
            }
            return url;
        }

        /// <summary>
        /// 获取资源区分区的帖子简介
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetArticlePartIntroduction(int partId, int pageNum)
        {
            string[] introducitons = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                introducitons[count] = "简介" + count.ToString();
            }
            return introducitons;
        }
        #endregion
        #region 问答分区数据获取

        /// <summary>
        /// 获取资源区分区的名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string GetQuestionName(int partId)
        {
            return "分区：" + partId.ToString() + "的名字";
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartName(int partId, int pageNum)
        {
            string[] names = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                names[count] = "姓名" + count.ToString();
            }
            return names;
        }

        /// <summary>
        /// 获取资源区分区的帖子标题
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartTitle(int partId, int pageNum)
        {
            string[] titles = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                titles[count] = "标题" + count.ToString();
            }
            return titles;
        }

        /// <summary>
        /// 获取资源区分区的帖子Url
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartUrl(int partId, int pageNum)
        {
            string[] url = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                url[count] = "#";
            }
            return url;
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖人头像
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartHeadAdr(int partId, int pageNum)
        {
            string[] url = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                url[count] = "/Resource/picture/DefaultHeadPic.jpg";
            }
            return url;
        }

        /// <summary>
        /// 获取资源区分区的帖子简介
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetQuestionPartIntroduction(int partId, int pageNum)
        {
            string[] introducitons = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                introducitons[count] = "简介" + count.ToString();
            }
            return introducitons;
        }

        /// <summary>
        /// 获取资源区分区的帖子悬赏金额
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static int[] GetQuestionPartMoney(int partId, int pageNum)
        {
            int[] moneys = new int[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                moneys[count] = count;
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
            quesAnswInfo info = new quesAnswInfo
            {
                areaId = 1,
                quesAnswId = quesId,
                quesAnswTitle = "这是一个"+quesId+"的标题？",
                userId = 666,
                quesAnswTime = DateTime.Now,
                pageView = 999,
                isPost = 0,
                quesAnswStatus = 0,
                amount = 99,
                quesAnswContent = "这是帖子"+quesId+"的内容",
                quesArea = null,
            };
            return info;
        }

        /// <summary>
        /// 获取问答帖子的回帖信息
        /// </summary>
        /// <param name="quesId">问答帖子id</param>
        /// <returns></returns>
        public static quesAnswReply[] GetQuestionReply(int quesId)
        {
            quesAnswReply[] replys = new quesAnswReply[5];
            for (int count = 0; count < 5; count++)
            {
                replys[count] = new quesAnswReply
                {
                    quesAnswId = quesId,
                    userId = count,
                    replyTime = DateTime.Now,
                    replyContent = "这是回帖内容" + count,
                    isAgree = 0,
                };
            }
            return replys;
        }
        #endregion
        #region 论坛数据获取

        /// <summary>
        /// 获取资源区分区的名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string GetforumName(int partId)
        {
            return "分区：" + partId.ToString() + "的名字";
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖名字
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartName(int partId, int pageNum)
        {
            string[] names = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                names[count] = "姓名" + count.ToString();
            }
            return names;
        }

        /// <summary>
        /// 获取资源区分区的帖子标题
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartTitle(int partId, int pageNum)
        {
            string[] titles = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                titles[count] = "标题" + count.ToString();
            }
            return titles;
        }

        /// <summary>
        /// 获取资源区分区的帖子Url
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartUrl(int partId, int pageNum)
        {
            string[] url = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                url[count] = "#";
            }
            return url;
        }

        /// <summary>
        /// 获取资源区分区的帖子发帖人头像
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartHeadAdr(int partId, int pageNum)
        {
            string[] url = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                url[count] = "/Resource/picture/DefaultHeadPic.jpg";
            }
            return url;
        }

        /// <summary>
        /// 获取资源区分区的帖子简介
        /// </summary>
        /// <param name="partId">分区id‘</param>
        /// <param name="pageNum">第几页</param>
        /// <returns></returns>
        public static string[] GetforumPartIntroduction(int partId, int pageNum)
        {
            string[] introducitons = new string[5];
            //此处应该获取数据 假装获取了
            for (int count = 0; count < 5; count++)
            {
                introducitons[count] = "简介" + count.ToString();
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
            postInfo info = new postInfo
            {
                areaId = 1,
                postId = postId,
                postTitle = "这是" + postId + "的标题",
                userId = 1,
                postTime = DateTime.Now,
                pageView = 999,
                isPost = 0,
                isPay = 0,
                amount = 0,
                postStatus = 0,
                postContent = "这是" + postId + "的内容",
                postArea = null,
            };
            return info;
        }

        /// <summary>
        /// 获取帖子的回帖
        /// </summary>
        /// <param name="postId">帖子id</param>
        /// <returns></returns>
        public static postReply[] GetforumReply(int postId)
        {
            postReply[] replys = new postReply[5];
            for (int count = 0; count < 5; count++)
            {
                replys[count] = new postReply
                {
                    postId = postId,
                    userId = count,
                    replyTime = DateTime.Now,
                    replyContent = "这是" + postId + "的回帖" + count,
                };
            }
            return replys;
        }
        #endregion

        /// <summary>
        /// 获取用户名字
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static string GetUserName(int userId)
        {
            return userId + "的ID";
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static string GetUserPicAdr(int userId)
        {
            return "/Resource/picture/DefaultHeadPic.jpg";
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static userInfo GetUserInfo(int userId)
        {
            userInfo info = new userInfo
            {
                userName = userId + "的名字",
                userId = userId,
                userAge = 18,
                userSex = 0,
                userTel = "123456789101",
                userStatus = 1,
                userAccount = 0,
                userSecurity = null,

            };
            return info;
        }

    }
}