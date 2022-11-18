using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTN_WebsiteRaoVat.Common;
using TTN_WebsiteRaoVat.Models;

namespace TTN_WebsiteRaoVat.Areas.Admin.Models
{
    public class CheckPermissionAttribute : AuthorizeAttribute
    {
        public string permissionAdmin { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            AdminLogin adminloggin = (AdminLogin)HttpContext.Current.Session[Common.CommonConstants.ADMIN_SESSION];
            NhanVienAccess nvac = new NhanVienAccess();
            if (adminloggin != null && nvac.KiemTraDangNhap(adminloggin.username, adminloggin.password, permissionAdmin))
            {               
                return true;
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/AdminHome/LoginAdmin.cshtml"
            };


        }
    }
}