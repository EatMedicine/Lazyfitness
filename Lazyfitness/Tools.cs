using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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

        /// <summary>
        /// 获取文章区的大区信息
        /// </summary>
        /// <returns></returns>
        public static resourceArea[] GetArticleAreaInfo()
        {
            resourceArea[] info = new resourceArea[4];
            int[] id = new int[]
            {
                1,2,3,4
            };
            string[] name = new string[]
            {
                "首页",
                "食物",
                "器材",
                "技巧",
            };
            for(int count = 0; count < id.Length; count++)
            {
                info[count] = new resourceArea
                {
                    areaId = id[count],
                    areaName = name[count],
                    areaBrief = "默认简介",
                };
            }
            return info;
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

        /// <summary>
        /// 获取问答区的大区信息
        /// </summary>
        /// <returns></returns>
        public static quesArea[] GetQuestionAreaInfo()
        {
            quesArea[] info = new quesArea[4];
            int[] id = new int[]
            {
                1,2,3,4
            };
            string[] name = new string[]
            {
                "首页",
                "已解决",
                "未解决",
                "我提出的问题",
            };
            for (int count = 0; count < id.Length; count++)
            {
                info[count] = new quesArea
                {
                    areaId = id[count],
                    areaName = name[count],
                    areaBrief = "默认简介",
                };
            }
            return info;
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

        /// <summary>
        /// 获取论坛的大区信息
        /// </summary>
        /// <returns></returns>
        public static postArea[] GetforumAreaInfo()
        {
            postArea[] info = new postArea[4];
            int[] id = new int[]
            {
                1,2,3,4
            };
            string[] name = new string[]
            {
                "分区1",
                "分区2",
                "分区3",
                "分区4",
            };
            for (int count = 0; count < id.Length; count++)
            {
                info[count] = new postArea
                {
                    areaId = id[count],
                    areaName = name[count],
                    areaBrief = "默认简介",
                };
            }
            return info;
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
                userEmail = "1111@qq.com",
                userStatus = 1,
                userAccount = 0,
                userSecurity = null,
                userHeaderPic = "/Resource/picture/DefaultHeadPic.jpg",
                userIntroduce = "这是" + userId + "的简介"
                

            };
            return info;
        }
        #region 首页数据
        /// <summary>
        /// 获取首页的轮播图数据
        /// </summary>
        /// <returns></returns>
        public static serverShowInfo[] GetIndexScroll()
        {
            //伪数据
            string[] picAdr = new string[]
            {
                "/Resource/picture/pic1.jpg",
                "/Resource/picture/pic2.jpg",
                "/Resource/picture/pic3.png",
            };
            string[] title = new string[]
            {
                "震惊！XX居然。。。",
                "吓人！XX居然。。。",
                "美女荷官在线发牌",
                "来贪玩蓝月，你从未见过的船新版本",
                "女朋友失误，竟炼出史前尸鲲",
                "想不出要说啥了但这里估计也看不到（",
                "但现在 你看到了",
                "因为 增加到第10个了",
                "这里是第九个",
                "终于编完了嘤"
            };
            //暂定areaId 0为首页  1为文章区 
            //暂定flag 0为轮播图 1为公告
            //暂定InfoStatus 0为禁用 1为启用
            //首页只要3条轮播图
            serverShowInfo[] info = new serverShowInfo[3];
            for(int count = 0; count < 3; count++)
            {
                info[count] = new serverShowInfo
                {
                    id = count,
                    areaId = 0,
                    title = title[count],
                    pictureAdr = picAdr[count],
                    //考虑到这里地址不一定是本网站的文章地址 所以还是写完整URL比较好
                    //比如说广告啥的（雾
                    url = "/Home/ArticleDetail?num=" + count,
                    flag = 0,
                    Infostatus = 1
                };
            }
            return info;
        }

        /// <summary>
        /// 获取首页的公告数据
        /// </summary>
        /// <returns></returns>
        public static serverShowInfo[] GetIndexNotice()
        {
            //伪数据
            string[] title = new string[]
            {
                "震惊！XX居然。。。",
                "吓人！XX居然。。。",
                "美女荷官在线发牌",
                "来贪玩蓝月，你从未见过的船新版本",
                "女朋友失误，竟炼出史前尸鲲",
                "想不出要说啥了但这里估计也看不到（",
                "但现在 你看到了",
                "因为 增加到第10个了",
                "这里是第九个",
                "终于编完了嘤"
            };
            //暂定areaId 0为首页  1为文章区 
            //暂定flag 0为轮播图 1为公告
            //暂定InfoStatus 0为禁用 1为启用
            //首页公告有5条
            serverShowInfo[] info = new serverShowInfo[5];
            for (int count = 0; count < 5; count++)
            {
                info[count] = new serverShowInfo
                {
                    id = count,
                    areaId = 0,
                    title = title[count],
                    pictureAdr = "",
                    //考虑到这里地址不一定是本网站的文章地址 所以还是写完整URL比较好
                    //比如说广告啥的（雾
                    url = "/Home/ArticleDetail?num=" + count,
                    flag = 1,
                    Infostatus = 1
                };
            }
            return info;
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
            resourceInfo[] info = new resourceInfo[num];
            for(int count = 0; count < num; count++)
            {
                info[count] = new resourceInfo
                {
                    resourceName = "这是第" + count + "条文章数据",
                    resourceId = count,
                };
            }
            return info;
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
            quesAnswInfo[] info = new quesAnswInfo[num];
            for (int count = 0; count < num; count++)
            {
                info[count] = new quesAnswInfo
                {
                    quesAnswTitle = "这是第" + count + "条文章数据",
                    quesAnswId = count,
                };
            }
            return info;
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
            postInfo[] info = new postInfo[num];
            for (int count = 0; count < num; count++)
            {
                info[count] = new postInfo
                {
                    postTitle = "这是第" + count + "条文章数据",
                    postId = count,
                };
            }
            return info;
        }

        #endregion

        #region 文章大区数据
        /// <summary>
        /// 获取首页的轮播图数据
        /// </summary>
        /// <returns></returns>
        public static serverShowInfo[] GetArticleScroll()
        {
            //伪数据
            string[] picAdr = new string[]
            {
                "/Resource/picture/pic1.jpg",
                "/Resource/picture/pic2.jpg",
                "/Resource/picture/pic3.png",
            };
            string[] title = new string[]
            {
                "震惊！XX居然。。。",
                "吓人！XX居然。。。",
                "美女荷官在线发牌",
                "来贪玩蓝月，你从未见过的船新版本",
                "女朋友失误，竟炼出史前尸鲲",
                "想不出要说啥了但这里估计也看不到（",
                "但现在 你看到了",
                "因为 增加到第10个了",
                "这里是第九个",
                "终于编完了嘤"
            };
            //暂定areaId 0为首页  1为文章区 
            //暂定flag 0为轮播图 1为公告
            //暂定InfoStatus 0为禁用 1为启用
            //首页只要3条轮播图
            serverShowInfo[] info = new serverShowInfo[3];
            for (int count = 0; count < 3; count++)
            {
                info[count] = new serverShowInfo
                {
                    id = count,
                    areaId = 1,
                    title = title[count],
                    pictureAdr = picAdr[count],
                    //考虑到这里地址不一定是本网站的文章地址 所以还是写完整URL比较好
                    //比如说广告啥的（雾
                    url = "/Home/ArticleDetail?num=" + count,
                    flag = 0,
                    Infostatus = 1
                };
            }
            return info;
        }
        #endregion
    }
}