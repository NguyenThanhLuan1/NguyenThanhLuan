using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vd2
{
    public partial class frmTBGiaoVien : Form
    {
        public frmTBGiaoVien()
        {
            InitializeComponent();
        }

       public void SetText(string s)
        {
            this.label1.Text = s;
        }

        private void TTGiaoVien_Load(object sender, EventArgs e)
        {

        }
    }
}
