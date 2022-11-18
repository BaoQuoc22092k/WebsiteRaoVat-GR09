using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTN_WebsiteRaoVat.Common;
using TTN_WebsiteRaoVat.Models;

namespace TTN_WebsiteRaoVat.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        TaiKhoanAccess tka = new TaiKhoanAccess();
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string SDT, string MatKhau)
        {
            if (tka.KiemTraDangNhap(SDT, MatKhau) == true && ModelState.IsValid)
            {
                TaiKhoan tk = tka.LayThongTinTaiKhoan(SDT);
                var userSesstion = new UserLogin();
                userSesstion.SDT = tk.SDT;
                Session.Add(CommonConstants.USER_SESSION, userSesstion);
                return Redirect("Home");
            }
            else
            {
                ModelState.AddModelError("", "Ôi bạn ơi, Thông tin đăng nhập sai rồi bạn ạ.");
            }
            return View("DangNhap");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(string HoTen, string SDT, string Email, string MatKhau, string LoaiTK)
        {
            TaiKhoan tk = new TaiKhoan();
            tk.HoTen = HoTen;
            tk.SDT = SDT;
            tk.Email = Email;
            tk.MatKhau = MatKhau;
            tk.LoaiTaiKhoan = Int32.Parse(LoaiTK);

            if (tka.DangKyTaiKhoan(tk))
            {
                return RedirectToAction("DangNhap","User");
            }
            else
            {
                ModelState.AddModelError("", "Số điện thoại đã tồn tại, Hãy dùng số điện thoại khác");
            }
            return View("DangKy");
        }

        public ActionResult TrangCaNhan(string sdt)
        {
            TaiKhoan tk = tka.LayThongTinTaiKhoan(sdt);
            ViewBag.sdt = sdt;
            return View(tk);
        }
        public ActionResult DanhSachDatHang(string sdt)
        {
            List<ThongTinDatHang> tk = tka.LayThongTinDatHang(sdt);            
            return View(tk);
        }
        public ActionResult DanhSachThich(string sdt)
        {
            List<VatPham> dsvp = tka.VatPhamDaThich(sdt);
            return View(dsvp);
        }
        string ChuyenDoiNgayThang(string s)
        {
            try
            {
                string[] temp = s.Split('/');
                return temp[1] + '/' + temp[0] + '/' + temp[2];
            }
            catch
            {
                return "";
            }

        }
        public ActionResult ThayDoiThongTinCaNhan(string sdt)
        {
            TaiKhoan tk = tka.LayThongTinTaiKhoan(sdt);
            return View(tk);
        }
        [HttpPost]
        public ActionResult ThayDoiThongTinCaNhan(TaiKhoan temp,string tempNgaySinh)
        {
            try
            {
                DateTime ngaysinh = new DateTime();
                if(DateTime.TryParse(ChuyenDoiNgayThang(tempNgaySinh),out ngaysinh))
                {
                    temp.NgaySinh = ngaysinh;
                }
            }
            catch
            {
                temp.NgaySinh = new DateTime();
            }
            if (tka.ThayDoiThongTinTaiKhoan(temp))
            {
                return RedirectToAction("TrangCaNhan","User",new {@sdt = temp.SDT });
            }
            return RedirectToAction("ThayDoiThongTinCaNhan", "User", new { @sdt = temp.SDT });
        }
        public string ThayDoiAnhDaiDien(HttpPostedFileBase file,string SDT)
        {
            SDT = SDT.Trim();
            string tenanh = SDT + Path.GetExtension(file.FileName);
            string duongdan = Path.Combine(Server.MapPath("/Content/images"), tenanh);
            file.SaveAs(duongdan);
            if(tka.ThayDoiAnhDaiDien(SDT, tenanh))
            {
                return tenanh;
            }
            return "";
        }
        public JsonResult NgungBan(int MaVP)
        {
            VatPhamAccess vpa = new VatPhamAccess();
            if (vpa.NgungBan(MaVP))
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        public JsonResult XoaDonHang(string sdtnm,int MaVP)
        {           
            if (tka.XoaDonHang(MaVP,sdtnm))
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        public JsonResult TiepTucBan(int MaVP)
        {
            VatPhamAccess vpa = new VatPhamAccess();
            if (vpa.BanTiep(MaVP))
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        public ActionResult DangXuat()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("Home");
        }    
    }   
    
}