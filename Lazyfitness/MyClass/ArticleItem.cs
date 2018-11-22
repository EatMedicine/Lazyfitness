using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazyfitness.MyClass
{
    public class ArticleItem
    {
        private string _title;
        private string _introduction;
        private string _headAdr;
        private string _name;
        private string _url;

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Introduction
        {
            get
            {
                return _introduction;
            }

            set
            {
                _introduction = value;
            }
        }

        public string HeadAdr
        {
            get
            {
                return _headAdr;
            }

            set
            {
                _headAdr = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }
    }
}