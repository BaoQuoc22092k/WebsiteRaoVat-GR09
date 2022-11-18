using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class TaiKhoan
    {
        public string SDT { get; set; }
        public string MatKhau { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public string NgayTao { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string QueQuan { get; set; }
        public string GioiTinh { get; set; }
        public string AnhDaiDien { get; set; }
        public DateTime NgaySinh { get; set; }
    }
}