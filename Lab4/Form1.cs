using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private QuanLySV qlsv;

        public Form1()
        {
            InitializeComponent();
        }


        private void btnBrown_Click(object sender, EventArgs e)
        {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Select Picture";// "Add Photos";
			dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
						  + "*.jpg;*.jpeg;*.gif;*.bmp;"
						  + "*.tif;*.tiff;*.png|"
						+ "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
						+ "GIF files (*.gif)|*.gif|"
						+ "BMP files (*.bmp)|*.bmp|"
						+ "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
						+ "PNG files (*.png)|*.png|"
						+ "All files (*.*)|*.*";

			dlg.InitialDirectory = Environment.CurrentDirectory;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				var fileName = dlg.FileName;
				txtHinh.Text = fileName;
				pbHinhAnh.Load(fileName);
			}
		}

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
			this.mtxtMSSV.Text = "";
			this.txtHoTen.Text = "";
			this.txtHoTen.Text = "";
			this.txtEmail.Text = "";
			this.txtDiaChi.Text = "";
			this.dateTimePicker1.Value = DateTime.Now;
			this.cbLop.Text = this.cbLop.Items[0].ToString();
			this.txtHinh.Text = "";
			this.rdNam.Checked = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
			Application.Exit();
        }

       private SinhVien GetSinhVien()
        {
			SinhVien sv = new SinhVien();
			bool gt = true;
			List<string> cn = new List<string>();
			sv.MaSo = this.mtxtMSSV.Text;
			sv.Hoten = this.txtHoTen.Text;
			sv.NgaySinh = this.dateTimePicker1.Value;
			sv.DiaChi = this.txtDiaChi.Text;
			sv.Lop = this.cbLop.Text;
			sv.SDT = this.mtxtSDT.Text;
			sv.Email = this.txtEmail.Text;
			sv.Hinh = this.txtHinh.Text;
			if (rdNu.Checked)
				gt = false;
			sv.GioiTinh = gt;
			return sv;
        }
		private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
			SinhVien sv = new SinhVien();
			sv.MaSo = lvitem.SubItems[0].Text;
			sv.Hoten = lvitem.SubItems[1].Text;
			sv.GioiTinh = false;
			if (lvitem.SubItems[2].Text == "Nam")
				sv.GioiTinh = true;
			sv.NgaySinh = DateTime.Parse(lvitem.SubItems[3].Text);
			sv.Lop = lvitem.SubItems[4].Text;
			sv.SDT = lvitem.SubItems[5].Text;
			sv.Email = lvitem.SubItems[6].Text;
			sv.DiaChi = lvitem.SubItems[7].Text;
			sv.Hinh = lvitem.SubItems[8].Text;
			return sv;
        }
		private void ThietLapThongTin(SinhVien sv)
        {
			this.mtxtMSSV.Text = sv.MaSo;
			this.txtHoTen.Text = sv.Hoten;
			if (sv.GioiTinh)
				this.rdNam.Checked = true;
			else
				this.rdNu.Checked = true;
			this.dateTimePicker1.Value = sv.NgaySinh;
			this.cbLop.Text = sv.Lop;
			this.mtxtSDT.Text = sv.SDT;
			this.txtEmail.Text = sv.Email;
			this.txtDiaChi.Text = sv.DiaChi;
			this.txtHinh.Text = sv.Hinh;
        }
		private void ThemSV(SinhVien sv)
        {
			ListViewItem lvitem = new ListViewItem(sv.MaSo);
			lvitem.SubItems.Add(sv.Hoten);
			string gt = "Nữ";
			if(sv.GioiTinh)
				gt="Nam";
			lvitem.SubItems.Add(gt);
			lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
			lvitem.SubItems.Add(sv.Lop);
			lvitem.SubItems.Add(sv.SDT);
			lvitem.SubItems.Add(sv.Email);
			lvitem.SubItems.Add(sv.DiaChi);
			lvitem.SubItems.Add(sv.Hinh);
			this.lvSV.Items.Add(lvitem);
        }
		private void LoadListView()
        {
            this.lvSV.Items.Clear();
            foreach (SinhVien sv in qlsv.DanhSach)
            {
				ThemSV(sv);
            }				
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			qlsv = new QuanLySV();
            qlsv.DocTufile();
            LoadListView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
			SinhVien sv = GetSinhVien();
			SinhVien kq = qlsv.Tim(sv.MaSo, delegate (object obj1, object obj2)
			   {
				   return (obj2 as SinhVien).MaSo.CompareTo(obj1.ToString());
			   });
			if (kq != null)
				MessageBox.Show("Sinh vien da ton tai", "Loi them du",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
				this.qlsv.Them(sv);
				this.LoadListView();
            }
        }
		private SinhVien GetSinhVienOnListViewItem(ListViewItem item)
        {
			string maSo = item.SubItems[0].Text;
			return qlsv.Tim(new SinhVien { MaSo = maSo });
        }

        private void lvSV_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (lvSV.SelectedItems.Count == 0) return;
			var sinhvien = GetSinhVienOnListViewItem(lvSV.SelectedItems[0]);
			ThietLapThongTin(sinhvien);
        }

		private int SoSanhTheoMa(object obj1,object obj2)
        {
			SinhVien sv = obj2 as SinhVien;
			return sv.MaSo.CompareTo(obj1);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
			int coutn, i;
			ListViewItem lvitem;
			coutn = this.lvSV.Items.Count - 1;
			for(i=coutn;i>=0;i--)
            {
				lvitem = this.lvSV.Items[i];
				if (lvitem.Checked)
					qlsv.Xoa(lvitem.SubItems[0].Text, SoSanhTheoMa);

            }
			
		}
		
	}
}
