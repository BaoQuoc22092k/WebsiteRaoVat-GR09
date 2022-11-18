using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class VatPhamAdmin
    {
        public int MaVP { get; set; }
        public string TenNguoiBan { get; set; }
        public string TenVatPham { get; set; }
        public string Mota { get; set; }
        public string TinhTrang { get; set; }
        public long GiaTien { get; set; }
        public string TenTL { get; set; }
        public string LinkHinhAnh { get; set; }
        public string NgayDang { get; set; }
    }
}