using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vd2
{
    public partial class frmGiaoVien : Form
    {
        private QuanLyGiaoVien quanLyGV;
        public frmGiaoVien()
        {
            InitializeComponent();
        }
        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            string lienhe = "http://it.dlu.edu.vn/e-learning/Default.aspx";
            this.linklbLienHe.Links.Add(0, lienhe.Length, lienhe);
            cboMaSo.SelectedItem = cboMaSo.Items[0];
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            int idx = lbDanhSachMH.SelectedItems.Count - 1;
            while (idx >= 0)
            {
                lbMoncHocDay.Items.Add(lbDanhSachMH.SelectedItems[idx]);
                lbDanhSachMH.Items.Remove(lbDanhSachMH.SelectedItems[idx]);
                idx--;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int idx = lbMoncHocDay.SelectedItems.Count - 1;
            while (idx >= 0)
            {
                lbDanhSachMH.Items.Add(lbMoncHocDay.SelectedItems[idx]);
                lbMoncHocDay.Items.Remove(lbMoncHocDay.SelectedItems[idx]);
                idx--;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            cboMaSo.Text = "";
            txtHoTen.Text = "";
            txtMail.Text = "";
            mtxtSoDT.Text = "";
            rdNam.Checked = true;

            for (int i = 0; i < chklbNgoaiNgu.Items.Count; i++)
            {
                chklbNgoaiNgu.SetItemChecked(i, false);
            }

            foreach (var monHoc in lbMoncHocDay.Items)
            {
                lbDanhSachMH.Items.Add(monHoc);
            }
            lbMoncHocDay.Items.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linklbLienHe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = e.Link.LinkData.ToString();
            Process.Start(link);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var frmTBGiaoVien = new frmTBGiaoVien();
            frmTBGiaoVien.SetText(GetGiaoVien().ToString());
            frmTBGiaoVien.ShowDialog();
        }

        public GiaoVien GetGiaoVien()
        {
            string gt = "Nam";
            if (rdNu.Checked)
                gt = "Nữ";
            GiaoVien gv = new GiaoVien();
            gv.MaSo = this.cboMaSo.Text;
            gv.GioiTinh = gt;
            gv.HoTen = this.txtHoTen.Text;
            gv.NgaySinh = this.dtpNgaySinh.Value;
            gv.Mail = this.txtMail.Text;
            gv.SoDT = this.mtxtSoDT.Text;
            //Lấy thông tin ngoại ngữ
            string ngoaingu = "";
            for (int i = 0; i < chklbNgoaiNgu.Items.Count - 1; i++)
                if (chklbNgoaiNgu.GetItemChecked(i))
                    ngoaingu += chklbNgoaiNgu.Items[i] + ";";
            gv.NgoaiNgu = ngoaingu.Split(';');
            //lấy thông tin dnah sách môn học
            DanhMucMonHoc mh = new DanhMucMonHoc();
            foreach (object ob in lbMoncHocDay.Items)
                mh.Them(new MonHoc(ob.ToString()));
            gv.dsMonHoc = mh;
            return gv;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var giaoVien = GetGiaoVien();

            var success = quanLyGV.Them(giaoVien);
            if (!success)
                MessageBox.Show("Giáo viên có mã số " + giaoVien.MaSo + " đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Thêm giáo viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var TimKiemForm = new TimKiemForm(quanLyGV);
            TimKiemForm.ShowDialog();
        }

       
    }

}
