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
    public partial class TimKiemForm : Form
    {
        private QuanLyGiaoVien quanLyGV;
        public TimKiemForm()
        {
            InitializeComponent();
        }
        public TimKiemForm(QuanLyGiaoVien qlgv) : this()
        {
            quanLyGV = qlgv;
        }

        private void rdMaGV_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMaGV.Checked)
            {
                label1.Text = rdMaGV.Text;
                txtSearch.Text = "";
            }
        }

        private void rdHoTen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdHoTen.Checked)
            {
                label1.Text = rdHoTen.Text;
                txtSearch.Text = "";
            }
        }

        private void rdSoDT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSDT.Checked)
            {
                label1.Text = rdSDT.Text;
                txtSearch.Text = "";
            }
        }

        private void btnSearchOK_Click(object sender, EventArgs e)
        {
            var kieuTim = KieuTim.TheoHoTen;
            if (rdMaGV.Checked)
            {
                kieuTim = KieuTim.TheoMa;
            }
            else if (rdHoTen.Checked)
            {
                kieuTim = KieuTim.TheoHoTen;
            }
            else if (rdSDT.Checked)
            {
                kieuTim = KieuTim.TheoSDT;
            }

            var ketQua = quanLyGV.Tim(txtSearch.Text, kieuTim);

            if (ketQua is null)
            {
                MessageBox.Show("Không tìm thấy", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var frmTBGiaoVien = new frmTBGiaoVien();
                frmTBGiaoVien.SetText(ketQua.ToString());
                frmTBGiaoVien.ShowDialog();
            }
        }

    }
}
