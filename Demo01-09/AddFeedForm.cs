using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo01_09
{
    public partial class AddFeedForm : Form
    {
        public bool HasChanges { get; set; }
        private readonly NewsFeedManager _newsManager;
        public AddFeedForm(NewsFeedManager newsManager)
        {
            _newsManager = newsManager;
            InitializeComponent();
        }

        private void AddFeedForm_Load(object sender, EventArgs e)
        {
            var publishers = _newsManager.GetNewsFeed();
            foreach(var publisher in publishers)
            {
                cbbPublisher.Items.Add(publisher.Name);
            }    
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var publisherName = cbbPublisher.Text;
            var categoryName = txtCategoryName.Text;
            var rssLink = txtRssLink.Text;
            if (string.IsNullOrWhiteSpace(publisherName) ||
                string.IsNullOrWhiteSpace(categoryName) ||
                string.IsNullOrWhiteSpace(rssLink))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ dữ liệu", "lỗi");
                return;

            }
            HasChanges = true;
            var success = _newsManager.AddCategory(publisherName, categoryName, rssLink, false);
            if (success)
            {
                ClearForm();
                return;
            }
            if (MessageBox.Show("Chuyên mục này đã tồn tại, bạn có muốn cập nhật RSS Link không?","Thông báo",MessageBoxButtons.YesNo)==DialogResult.Yes )
            {
                _newsManager.AddCategory(publisherName, categoryName, rssLink, true);
            }    
        }
        private void ClearForm()
        {
            cbbPublisher.Text = "";
            txtCategoryName.Text = "";
            txtRssLink.Text = "";
        }

        
        
    }
}
