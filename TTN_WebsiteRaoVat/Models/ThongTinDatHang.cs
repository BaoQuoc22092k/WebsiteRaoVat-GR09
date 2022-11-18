using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class ThongTinDatHang
    {
        public int MaVP { get; set; }
        public string TenVP { get; set; }
        public string SDT { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}