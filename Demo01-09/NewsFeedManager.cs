using Demo01_09.Io;
using Demo01_09.Model;
using Demo01_09.RssFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_09
{
    public class NewsFeedManager
    {
        private readonly INewsRepository _newsRepository;
        private List<Publisher> _publisher;
        private readonly RssReader _rssReader;
        public NewsFeedManager(INewsRepository newRepository)
        {
            _newsRepository = newRepository;
        }
        public List<Publisher> GetNewsFeed()
        {
            if (_publisher == null)
            {
                _publisher = _newsRepository.GetNews();
            }
            return _publisher;
        }
        public void SaveChanges()
        {
            _newsRepository.Save(_publisher);
        }
        public void RemovePublisher(string publisherName)
        {
            _publisher.RemoveAll(x => x.Name == publisherName);
            SaveChanges();
        }
        public void Removecategory(string publisherName, string categoryName)
        {
            var publisher = _publisher.Find(x => x.Name == publisherName);
            if (publisher == null) return;
            publisher.RemoveCategory(categoryName);
            SaveChanges();
        }
        public bool AddCategory(string publisherName, string categoryName, string rssLink, bool updateIfExisted)
        {
            var publisher = _publisher.Find(x => x.Name == publisherName);
            if (publisher == null)
            {
                publisher = new Publisher()
                {
                    Name = publisherName
                };
                _publisher.Add(publisher);
            }
            return publisher.AddCategory(categoryName, rssLink, updateIfExisted);
        }
        public List<Article>GetNews(string publisherName, string categoryName)
        {
            var publisher = _publisher.Find(x => x.Name == publisherName);
            if (publisher == null) return new List<Article>();
            var category= publisher.Categories.Find(x => x.Name == categoryName);
            if (category == null) return new List<Article>();
            if(category.Articles.Count==0)
            {
                category.Articles = _rssReader.GetNews(category.RSSLink);

            }
            return category.Articles;
        }
    }
}
