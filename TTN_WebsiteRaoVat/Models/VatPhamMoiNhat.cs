using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class VatPhamMoiNhat
    {
        public int MaVP { get; set; }
        public string TenVP { get; set; }
        public long GiaTien { get; set; }
        public string LinkAnh { get; set; }
        public string NgayDang { get; set; }
    }
}