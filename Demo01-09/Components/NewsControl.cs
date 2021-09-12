using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo01_09.Model;
using System.Diagnostics;

namespace Demo01_09.Components
{
    public partial class NewsControl : UserControl
    {
        public NewsControl()
        {
            InitializeComponent();
        }
        public void SetArticle(Article news)
        {
            lblTitle.Text = news.Title;
            lblDescription.Text = news.Description;
            lblPublsherDate.Text = news.PublishedDate.ToString("dd/MM/yyyy HH:mm");
            lblDetail.LinkClicked += (Send, args) =>
              {
                  Process.Start(news.Link);
  
              };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.Black, 0, 1, Width - 1, Height - 2);

        }
    }
}
