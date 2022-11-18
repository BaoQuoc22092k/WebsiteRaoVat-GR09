using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class VatPham 
    {
        public int MaVP { get; set; }
        public string TenVP { get; set; }
        public string TenNguoiBan { get; set; }
        public string SDT { get; set; }
        public string MoTa { get; set; }
        public string TinhTrang { get; set; }
        public long GiaTien { get; set; }
        public List<string> LinkHinhAnh { get; set; }
        public string TheLoai { get; set; }
        public string ThanhPho { get; set; }
        public string DiaDiem { get; set; }
        public string NgayDang { get; set; }
        public int ChatLuong { get; set; }
        public int MaDM { get; set; }
        public int KiemDuyet { get; set; }
        public int NgungBan { get; set; }
        public int LoaiTK { get; set; }
        public int LuotThich { get; set; }
        public int TrongSo { get; set; }
        public int MaTP { get; set; }
    }   
}