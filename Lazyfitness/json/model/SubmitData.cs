using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazyfitness.json.model
{
    public class SubmitData
    {
        public string password { get; set; }
        public string webname { get; set; }
        public ArticlePartData[] articlePartData { get; set; }
        public ForumPartData[] forumPartData { get; set; }
        public QuesPartData[] quesPartData { get; set; }
        public class ArticlePartData
        {
            public string pname { get; set; }
        }
        public class ForumPartData
        {
            public string pname { get; set; }
        }
        public class QuesPartData
        {
            public string pname { get; set; }
        }
    }
}