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
        #endregion
        #region 资源分区数据获取

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
        #endregion
    }
}