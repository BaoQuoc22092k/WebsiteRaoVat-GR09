using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TTN_WebsiteRaoVat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Điều hướng User
            routes.MapRoute(
                name: "Đăng nhập",
                url: "DangNhap",
                defaults: new { controller = "User", action = "DangNhap" }
            );
            routes.MapRoute(
                name: "Đăng ký",
                url: "DangKy",
                defaults: new { controller = "User", action = "DangKy" }
            );
            routes.MapRoute(
                name: "Trang cá nhân",
                url: "TrangCaNhan/{sdt}",
                defaults: new { controller = "User", action = "TrangCaNhan", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Danh sách đặt hàng",
                url: "DonDatHang/{sdt}",
                defaults: new { controller = "User", action = "DanhSachDatHang", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Thay đổi trang cá nhân",
                url: "ThayDoiThongTinCaNhan/{sdt}",
                defaults: new { controller = "User", action = "ThayDoiThongTinCaNhan", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Đăng xuất",
                url: "DangXuat",
                defaults: new { controller = "User", action = "DangXuat" }
            );

            // Điều hướng vật phẩm
            routes.MapRoute(
                name: "Tất cả danh mục",
                url: "TatCaDanhMuc/{MaDM}-{TrangSo}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Chi tiết vật phẩm",
                url: "ChiTietVatPham/{MaVP}",
                defaults: new { controller = "Product", action = "ChiTietVatPham", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Đăng tin bán",
                url: "DangTinBan/{SDT}",
                defaults: new { controller = "Product", action = "DangTinBan", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "Like san pham",
                 url: "ThichVatPham",
                 defaults: new { controller = "Product", action = "ThichVatPham"}
             );
            // Hết điều hướng sản phẩm

            // Điều hướng của trang Home
            routes.MapRoute(
                name: "Trợ giúp",
                url: "TroGiup",
                defaults: new { controller = "Home", action = "TroGiup" }
            );
            routes.MapRoute(
                name: "Cách Hoạt Động",
                url: "CachHoatDong",
                defaults: new { controller = "Home", action = "CachHoatDong" }
            );
            routes.MapRoute(
                name: "Phản Hồi",
                url: "PhanHoi",
                defaults: new { controller = "Home", action = "PhanHoi" }
            );
            routes.MapRoute(
                name: "Liên Hệ",
                url: "LienHe",
                defaults: new { controller = "Home", action = "LienHe" }
            );
            routes.MapRoute(
                name: "Điều khoản sử dụng",
                url: "DieuKhoanSuDung",
                defaults: new { controller = "Home", action = "DieuKhoanSuDung" }
            );
            routes.MapRoute(
                name: "Chính sách bảo mật",
                url: "ChinhSachBaoMat",
                defaults: new { controller = "Home", action = "ChinhSachBaoMat" }
            );
            

            //Điều hướng mặc định (Không để cái nào dưới cái này)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
