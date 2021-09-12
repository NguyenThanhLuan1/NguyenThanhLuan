using Demo01_09.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_09.RssFeed
{
    public class RssReader
    {
        private readonly NewsParser _parser;

        public RssReader()
        {
        }

        public RssReader(NewsParser parser)
        {
            _parser = parser;
        }
        public List<Article>GetNews(string rssLink)
        {
            var feedData = DownloadFeed(rssLink);
            return _parser.ParseXml((string)feedData);
        }

        private object DownloadFeed(string rssLink)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            return client.DownloadString(rssLink);
        }
    }
}
