using Demo01_09.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Demo01_09.RssFeed
{
    public class NewsParser
    {
        public List<Article> ParserXml(string xmlContent)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlContent);
            var articles = new List<Article>();
            var itemNodes = document.SelectNodes("//item");
            foreach (XmlNode node in itemNodes)
            {
                var news = new Article()
                {

                    Title = node.SelectSingleNode("title").InnerText,
                    Description = StripHtml(node.SelectSingleNode("description").InnerText),
                    Link = node.SelectSingleNode("link").InnerText,
                    PublishedDate = ParseDate(node.SelectSingleNode("pubDate").InnerText)

                };
                articles.Add(news);
            }    
        }
        private string StripHtml(string content)
        {
            return Regex.Replace(content, "<.*?", string.Empty).Trim();
        }
        private DateTime ParseDate(string dateStr)
        {
            try
            {
                return DateTime.Parse(dateStr);
            }
            catch
            {
                return DateTime.Now;
            }
        }
       
    }
}
