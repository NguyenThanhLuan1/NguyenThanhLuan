using Demo01_09.Io;
using Demo01_09.RssFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo01_09
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            INewsRepository repository = new NewsRepository();
            RssReader reader = new RssReader(parser);
            NewsParser parser = new NewsParser();
            var manager = new NewsFeedManager(repository);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(manager));
        }
    }
}
