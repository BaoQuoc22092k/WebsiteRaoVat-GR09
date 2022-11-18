using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTN_WebsiteRaoVat.Areas.Admin.Models;
using TTN_WebsiteRaoVat.Common;
using TTN_WebsiteRaoVat.Models;
namespace TTN_WebsiteRaoVat.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        
        NhanVienAccess nvac = new NhanVienAccess();

        // GET: Admin/Home
        [CheckPermission(permissionAdmin = "Admin")]
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult DuyetVatPham()
        {
            
            return View();
        }
        public ActionResult NhanPhanHoi()
        {

            return View();
        }
        public ActionResult VatPhamBiKhoa()
        {
            
            return View();
        }
       

        public ActionResult LoginAdmin()
        {
            return View();
        }
        public ActionResult LogoutAdmin()
        {
            Session[CommonConstants.ADMIN_SESSION] = null;
            return Redirect("/");
        }
        public ActionResult ResultLoginAdmin(string username, string pass)
        {
            if (ModelState.IsValid)
            {
                var AdminSesstion = new AdminLogin();
                AdminSesstion.username = username;
                AdminSesstion.password = pass;
                Session.Add(CommonConstants.ADMIN_SESSION, AdminSesstion);
                return RedirectToAction("Index", "AdminHome");
            }
            return View("LoginAdmin");
        }
        
        

    }
}