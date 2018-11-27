using Lazyfitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Lazyfitness
{
    public class PageViewHelper
    {
        //浏览量数据
        List<PageViewRecord> records;
        Timer saveTimer;
        //读写锁
        ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
        public PageViewHelper()
        {
            records = new List<PageViewRecord>();
            saveTimer = new Timer(func => SaveToDB(), null, 0, 5000);
        }

        /// <summary>
        /// 添加浏览量
        /// </summary>
        /// <param name="regionId">大区ID 0文章区 1问答区 2论坛</param>
        /// <param name="id">文章问答帖子对应的唯一指定ID</param>
        /// <param name="addNum">新增浏览量</param>
        /// <returns>是否成功</returns>
        public bool PageViewAdd(int regionId,int id,int addNum)
        {
            bool IsSuccess = false;
            try
            {
                rwl.EnterWriteLock();
                PageViewRecord record = records.Find((r) => r.RegionId == regionId && r.Id == id);
                if (record != null)
                {
                    record.PageView += addNum;
                }
                else
                {
                    PageViewRecord tmp = new PageViewRecord(regionId, id, addNum);
                    records.Add(tmp);
                }
                IsSuccess = true;
            }
            catch
            {
                IsSuccess = false;
            }
            finally
            {
                rwl.ExitWriteLock();
            }
            return IsSuccess;
        }

        /// <summary>
        /// 将所保存的记录保存在数据库并清除现有记录
        /// </summary>
        /// <returns></returns>
        public bool SaveToDB()
        {
            rwl.EnterWriteLock();
            using (LazyfitnessEntities db = new LazyfitnessEntities())
            {
                foreach(PageViewRecord record in records)
                {
                    switch (record.RegionId)
                    {
                        case 0:
                            resourceInfo rinfo =  db.resourceInfo.Where(article => article.resourceId == record.Id).FirstOrDefault();
                            if (rinfo == null)
                                break;
                            rinfo.pageView += record.PageView;
                            break;
                        case 1:
                            quesAnswInfo qinfo = db.quesAnswInfo.Where(ques => ques.quesAnswId == record.Id).FirstOrDefault();
                            if (qinfo == null)
                                break;
                            qinfo.pageView += record.PageView;
                            break;
                        case 2:
                            postInfo pinfo = db.postInfo.Where(post => post.postId == record.Id).FirstOrDefault();
                            if (pinfo == null)
                                break;
                            pinfo.pageView += record.PageView;
                            break;
                        default:break;
                    }
                }
                db.SaveChanges();
                records.Clear();
                rwl.ExitWriteLock();
            }
            return true;
        }
    }
}