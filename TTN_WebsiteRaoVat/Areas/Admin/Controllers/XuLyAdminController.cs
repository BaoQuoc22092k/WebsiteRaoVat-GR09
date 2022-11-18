using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTN_WebsiteRaoVat.Models;

namespace TTN_WebsiteRaoVat.Areas.Admin.Controllers
{
    public class XuLyAdminController : Controller
    {
        // GET: Admin/XuLyAdmin
        VatPhamAccess vpa = new VatPhamAccess();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult KhoaVatPham(int MaVP)
        {
            if (vpa.KhoaVatPham(MaVP))
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
        public JsonResult MoKhoaVatPham(int MaVP)
        {
            if (vpa.MoKhoaVatPham(MaVP))
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
        public JsonResult DuyetVatPham(int MaVP)
        {
            if (vpa.DuyetVatPham(MaVP))
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
        public JsonResult XoaVatPham(int MaVP)
        {
            if (vpa.XoaVatPham(MaVP))
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
    }
}