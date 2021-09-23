using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public delegate int SoSanh(object sv1, object sv2);
    public class QuanLySV
    {
        public List<SinhVien> DanhSach;
        public QuanLySV()
        {
            DanhSach = new List<SinhVien>();
        }
        public void Them(SinhVien sv)
        {
            this.DanhSach.Add(sv);
        }
        public SinhVien this[int index]
        {
            get { return DanhSach[index]; }
            set { DanhSach[index] = value; }
        }
        public void Xoa(object obj, SoSanh ss)
        {
            int i = DanhSach.Count - 1;
            for (; i >= 0; i--)
                if (ss(obj, this[i]) == 0)
                    this.DanhSach.RemoveAt(i);
        }
        public SinhVien Tim(object obj,SoSanh ss)
        {
            SinhVien svresult = null;
            foreach(SinhVien sv in DanhSach)
                if(ss(obj,sv)==0)
                {
                    svresult = sv;
                    break;
                }
            return svresult;
        }
        public SinhVien Tim(SinhVien sv) => DanhSach.Find(s => s.MaSo == sv.MaSo);
        public void DocTufile()
        {
            string filename = "DanhSachSV.txt", t;
            string[] s;
            SinhVien sv;
            using(var stream=new FileStream(filename,FileMode.Open))
            { 
                using(var reader =new StreamReader(stream))
                {
                    while ((t = reader.ReadLine()) != null)
                    {
                        s = t.Split('\t');
                        sv = new SinhVien();
                        sv.MaSo = s[0].Trim();
                        sv.Hoten = s[1].Trim();
                        sv.GioiTinh = false;
                        if (s[2].Trim() == "1")
                            sv.GioiTinh = true;
                        sv.NgaySinh = DateTime.Parse(s[3].Trim());
                        sv.Lop = s[4].Trim();
                        sv.SDT = s[5].Trim();
                        sv.Email = s[6].Trim();
                        sv.DiaChi = s[7].Trim();
                        sv.Hinh = s[8].Trim();
                        this.Them(sv);
                    }
                }
            }
            
        }
    }
}
