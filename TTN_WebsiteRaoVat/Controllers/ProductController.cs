using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TTN_WebsiteRaoVat.Models;

namespace TTN_WebsiteRaoVat.Controllers
{
    public class ProductController : Controller
    {
        VatPhamAccess vpa = new VatPhamAccess();

        public ActionResult Index(int MaDM, int Trang)
        {
            List<VatPham> dsvp = vpa.LayVatPham(MaDM, Trang);
            // mặc định là mới nhất lên đầu
            dsvp = dsvp.OrderBy(x => x.NgayDang).ToList();
            ViewBag.MaDM = MaDM;
            ViewBag.Trang = Trang;
            return View(dsvp);
        }
        public ActionResult TimKiem(string strTimKiem, int TheLoai)
        {
            /*
            List<VatPham> dsvp = vpa.LayVatPham(TheLoai,0);
            foreach(var i in dsvp)
            {
                i.TrongSo = XauConChungDaiNhat(strTimKiem, i.TenVP);
            }
            dsvp = dsvp.Where(x => x.TrongSo > 0).OrderBy(x=>x.TrongSo).ToList(); 
            */
            List<VatPham> dsvp = vpa.TimKiemVP(strTimKiem, TheLoai);
            ViewBag.MaDM = TheLoai;
            return View(dsvp);
        }
        public ActionResult TimKiemVPAJ(string TenVP,int MaTL)
        {
            List<VatPham> dsvp = vpa.TimKiemVP(TenVP, MaTL);            
            ViewBag.MaDM = MaTL;
            ViewBag.TieuChi = 0;          
            return PartialView(dsvp);
        }
        public ActionResult ShowVatPham(int MaDM, int tieuchi)
        {
            List<VatPham> dsvp = vpa.LayVatPham(MaDM,1);
            ViewBag.MaDM = MaDM;
            ViewBag.TieuChi = tieuchi;
            if (tieuchi == 0)
            {                               
                dsvp = dsvp.OrderBy(x => x.NgayDang).ToList();                
            }
            else if(tieuchi == 1)
            {               
                dsvp = dsvp.OrderBy(x => x.GiaTien).ToList();                               
            }
            else
            {               
                dsvp = dsvp.OrderBy(x => x.GiaTien).ToList();
                dsvp.Reverse();               
            }
            return PartialView(dsvp);
        }
        public ActionResult LocDiaDiem(int MaTP)
        {
            List<VatPham> dsvp = vpa.LayVatPham(0, 0);
            if(MaTP != 0)
            {
                dsvp = dsvp.Where(x => x.MaTP == MaTP).ToList();
            }        
            ViewBag.MaDM = 0;
            ViewBag.TieuChi = 0;
            dsvp = dsvp.OrderBy(x => x.NgayDang).ToList();
            return PartialView(dsvp);
        }
        public ActionResult LocTheoGia(int MaDM,long min,long max,int tieuchi)
        {
            List<VatPham> dsvp = vpa.LayVatPham(MaDM,1);
            ViewBag.MaDM = MaDM;
            ViewBag.TieuChi = tieuchi;
            ViewBag.min = min;
            ViewBag.max = max;
            min = min * 1000000;
            if (max < 1500)
            {                
                max = max * 1000000;
                dsvp = dsvp.Where(x => x.GiaTien > min && x.GiaTien < max).ToList();
            }
            else
            {
                dsvp = dsvp.Where(x => x.GiaTien > min).ToList();
            }
            if (tieuchi == 0)
            {
                dsvp = dsvp.OrderBy(x => x.NgayDang).ToList();
            }
            else if (tieuchi == 1)
            {
                dsvp = dsvp.OrderBy(x => x.GiaTien).ToList();
            }
            else
            {
                dsvp = dsvp.OrderBy(x => x.GiaTien).ToList();
                dsvp.Reverse();
            }
            return PartialView(dsvp);
        }
        public ActionResult ChiTietVatPham(int MaVP)
        {

            VatPham vp = vpa.ThongTinChiTietVatPham(MaVP);
            return View(vp);
        }
        public ActionResult BaoXau(string SDT,int MaVP,string LyDo,string GhiChu)
        {
            if (vpa.BaoXauVatPham(SDT, MaVP, LyDo, GhiChu))
            {
                return RedirectToAction("ChiTietVatPham", "Product", new { MaVP = MaVP });
            }
            return RedirectToAction("ChiTietVatPham", "Product", new { MaVP = MaVP });
        }
        public ActionResult DatMua(string SDT,string SDTNB, int MaVP, string TenNM,string Email,string DiaChi ,string GhiChu)
        {
            if (vpa.DatMuaSanPham(SDT,SDTNB, MaVP, TenNM,Email,DiaChi, GhiChu))
            {
                return RedirectToAction("ChiTietVatPham", "Product", new { MaVP = MaVP });
            }
            return RedirectToAction("ChiTietVatPham", "Product", new { MaVP = MaVP });
        }
        public JsonResult ThichVatPham(ThichVatPham temp)
        {
            if (vpa.DaThich(temp.SDT, Int32.Parse(temp.MaVP)) == false && vpa.ThichVatPham(temp) == true)
            {
                return Json(new
                {
                    status = true
                });
            }
            else if (vpa.BoThich(temp))
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public ActionResult DangTinBan(string SDT)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangTinBan
            (string SDT, string TheLoai, string TieuDe, string MoTa, string TinhTrang, string GiaTien,
                string HoTen, string ThanhPho, string QuanHuyen, string Email, ImageFile objImage)
        {
            string strLink = "";
            VatPhamAccess vpa = new VatPhamAccess();
            foreach (var file in objImage.files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.Combine(Server.MapPath("/Content/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName));
                    strLink += fileName + ',';
                    file.SaveAs(fileName);
                }
            }
            string[] temp = strLink.Split(',');
            int MaTinhThanh = Int32.Parse(ThanhPho);
            long giaTien = long.Parse(GiaTien);
            int theloai = Int32.Parse(TheLoai);
            if (vpa.ThemVatPham(SDT, HoTen, MaTinhThanh, QuanHuyen, TieuDe, MoTa, TinhTrang, giaTien, theloai, temp[0], strLink))
            {
                return RedirectToAction("Index", "Product", new {MaDM = theloai,Trang = 1});
            }
            return View("DangTinBan");
        }
        int XauConChungDaiNhat(string s1, string s2)
        {
            string[] t1 = s1.Split(' ');
            string[] t2 = s2.Split(' ');
            int len = 0;
            if (t1.Length < t2.Length) len = t2.Length;
            else len = t1.Length;
            int[,] kq = new int[len, len];
            int max = 0;

            for (int i = 0; i < t1.Length; i++)
            {
                for (int j = 0; j < t2.Length; j++)
                {
                    if (t1[i].ToLower() == t2[j].ToLower())
                    {
                        if (i == 0 || j == 0)
                        {
                            kq[i, j] = 1;
                        }
                        else
                        {
                            kq[i, j] = kq[i - 1, j - 1] + 1;
                        }
                        max = kq[i, j];
                    }
                    else
                    {
                        if (i != 0 && j != 0)
                        {
                            if (kq[i - 1, j] >= kq[i, j - 1]) kq[i, j] = kq[i - 1, j];
                            else
                            {
                                kq[i, j] = kq[i, j - 1];
                            }
                        }
                    }
                }
            }
            return max;
        }
    }
    
    public class ImageFile
    {
        public List<HttpPostedFileBase> files { get; set; }
    }
}