using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazyfitness
{
    public class PageViewRecord
    {
        private int _regionId;
        private int _id;
        private int _pageView;

        /// <summary>
        /// 大区ID 0文章区 1问答区 2论坛
        /// </summary>
        public int RegionId
        {
            get
            {
                return _regionId;
            }

            set
            {
                _regionId = value;
            }
        }
        /// <summary>
        /// 文章问答帖子对应的唯一指定ID
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        /// <summary>
        /// 新增浏览量
        /// </summary>
        public int PageView
        {
            get
            {
                return _pageView;
            }

            set
            {
                _pageView = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionId">大区ID 0文章区 1问答区 2论坛</param>
        /// <param name="id">文章问答帖子对应的唯一指定ID</param>
        /// <param name="pageView">新增浏览量</param>
        public PageViewRecord(int regionId,int id,int pageView)
        {
            RegionId = regionId;
            Id = id;
            PageView = pageView;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionId">大区ID 0文章区 1问答区 2论坛</param>
        /// <param name="id">文章问答帖子对应的唯一指定ID</param>
        public PageViewRecord(int regionId, int id) : this(regionId, id, 0) { }
    }
}