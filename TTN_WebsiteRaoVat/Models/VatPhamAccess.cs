using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class VatPhamAccess : DatabaseAccess
    {
        public List<TinhThanh> LayTinhThanh()
        {
            List<TinhThanh> kq = new List<TinhThanh>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from Tinh_Thanh";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TinhThanh temp = new TinhThanh();
                temp.MaTinhThanh = reader.GetInt32(0);
                temp.TenTinhThanh = reader.GetString(1);
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }
        public List<DanhMuc> LayDanhMuc()
        {
            List<DanhMuc> kq = new List<DanhMuc>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from TheLoai";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DanhMuc temp = new DanhMuc();
                temp.MaDanhMuc = reader.GetInt32(0);
                temp.TenDanhMuc = reader.GetString(1);
                temp.Icon = reader.GetString(2);
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }
        public List<VatPham> LayToanBoVatPham()
        {
            List<VatPham> dsvp = new List<VatPham>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from dbo.ThongTinVatPham(0)";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VatPham vp = new VatPham();
                vp.MaVP = reader.GetInt32(0);
                vp.TenVP = reader.GetString(1);
                vp.TenNguoiBan = reader.GetString(2);
                vp.SDT = reader.GetString(3);
                vp.ThanhPho = reader.GetString(4);
                vp.MoTa = reader.GetString(5);
                vp.TinhTrang = reader.GetString(6);
                vp.GiaTien = reader.GetInt64(7);
                vp.TheLoai = reader.GetString(8);
                int temp = reader.GetInt32(9);
                vp.NgayDang = ChuyenThoiGian(temp);
                vp.LinkHinhAnh = new List<string>();
                vp.LinkHinhAnh.Add(reader.GetString(10));
                vp.ChatLuong = reader.GetInt32(11);
                vp.DiaDiem = reader.GetString(12);
                dsvp.Add(vp);
            }
            reader.Close();
            return dsvp;
        }
        public List<VatPham> LayVatPham(int MaDM,int Trang)
        {
            int start = (Trang - 1) * 12;
            List<VatPham> dsvp = new List<VatPham>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            if(Trang > 0)
            {
                command.CommandText = "select * from dbo.DanhSachVatPham(@MaDM) order by NgayDang OFFSET " + start + " ROWS FETCH NEXT 12 ROWS ONLY;";
            }
            else
            {
                command.CommandText = "select * from dbo.DanhSachVatPham(@MaDM)";
            }
            command.Connection = conn;
            command.Parameters.Add("@MaDM", SqlDbType.Int).Value = MaDM;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VatPham vp = new VatPham();
                vp.MaVP = reader.GetInt32(0);
                vp.TenVP = reader.GetString(1);
                vp.TenNguoiBan = reader.GetString(2);
                vp.SDT = reader.GetString(3);
                vp.ThanhPho = reader.GetString(4);
                vp.MoTa = reader.GetString(5);
                vp.TinhTrang = reader.GetString(6);
                vp.GiaTien = reader.GetInt64(7);
                vp.TheLoai = reader.GetString(8);
                int temp = reader.GetInt32(9);
                vp.NgayDang = ChuyenThoiGian(temp);
                vp.LinkHinhAnh = new List<string>();
                vp.LinkHinhAnh.Add(reader.GetString(10));
                vp.ChatLuong = reader.GetInt32(11);
                vp.DiaDiem = reader.GetString(12);
                vp.LoaiTK = reader.GetInt32(13);
                vp.MaTP = reader.GetInt32(14);
                dsvp.Add(vp);
            }
            reader.Close();
            return dsvp;
        }
        public VatPham ThongTinChiTietVatPham(int MaVP)
        {
            VatPham vp = new VatPham();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from dbo.ThongTinVatPham(@MaVP)";
                command.Connection = conn;
                command.Parameters.Add("@MaVP", SqlDbType.Int).Value = MaVP;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    vp.MaVP = reader.GetInt32(0);
                    vp.TenVP = reader.GetString(1);
                    vp.TenNguoiBan = reader.GetString(2);
                    vp.SDT = reader.GetString(3);
                    vp.ThanhPho = reader.GetString(4);
                    vp.MoTa = reader.GetString(5);
                    vp.TinhTrang = reader.GetString(6);
                    vp.GiaTien = reader.GetInt64(7);
                    vp.TheLoai = reader.GetString(8);
                    int temp = reader.GetInt32(9);
                    vp.NgayDang = ChuyenThoiGian(temp);
                    vp.ChatLuong = reader.GetInt32(11);
                    vp.DiaDiem = reader.GetString(12);
                    vp.MaDM = reader.GetInt32(13);
                    vp.LuotThich = reader.GetInt32(14);
                    reader.Close();
                    vp.LinkHinhAnh = LayHinhAnh(vp.MaVP);
                }
                
            }
            catch
            {

            }
            return vp;
        }
        List<string> LayHinhAnh(int MaVP)
        {
            List<string> linkAnh = new List<string>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from dbo.LayHinhAnh(@MaVP)";
            command.Connection = conn;
            command.Parameters.Add("@MaVP", SqlDbType.Int).Value = MaVP;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string link = reader.GetString(0);
                linkAnh.Add(link);
            }
            reader.Close();
            return linkAnh;
        }
        public bool ThemVatPham(
            string SDT, string HoTen, int MaTinhThanh, string DiaChi, string TenVP, string MoTa,
            string TinhTrang, long GiaTien, int MaTL, string LinkHinhAnh, string strLink
            )
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemVatPham";
            command.Connection = conn;

            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = HoTen;
            command.Parameters.Add("@MaTinhThanh", SqlDbType.Int).Value = MaTinhThanh;
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = DiaChi;

            command.Parameters.Add("@TenVP", SqlDbType.NVarChar).Value = TenVP;
            command.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = MoTa;
            command.Parameters.Add("@TinhTrang", SqlDbType.NVarChar).Value = TinhTrang;
            command.Parameters.Add("@GiaTien", SqlDbType.BigInt).Value = GiaTien;
            command.Parameters.Add("@MaTL", SqlDbType.Int).Value = MaTL;
            command.Parameters.Add("@LinkHinhAnh", SqlDbType.NVarChar).Value = LinkHinhAnh;

            command.Parameters.Add("@strLink", SqlDbType.NVarChar).Value = strLink;

            int ret = command.ExecuteNonQuery();

            if (ret > 0)
            {
                return true;
            }
            return false;
        }
        public bool BaoXauVatPham(string SDT,int MaVP,string LyDo,string GhiChu)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BaoXauVatPham";
            command.Connection = conn;

            command.Parameters.Add("@sdt", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Parameters.Add("@LyDo", SqlDbType.NVarChar).Value = LyDo;
            command.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = GhiChu;

            int ret = command.ExecuteNonQuery();

            if (ret > 0)
            {
                return true;
            }
            return false;
        }
        public bool DatMuaSanPham(string SDT,string SDTNB ,int MaVP, string TenNM, string Email,string DiaChi,string GhiChu)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DatMuaSanPham";
            command.Connection = conn;

            command.Parameters.Add("@sdt", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Parameters.Add("@tennm", SqlDbType.NVarChar).Value = TenNM;
            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = Email;
            command.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = DiaChi;
            command.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = GhiChu;
            int ret = command.ExecuteNonQuery();

            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ThongBaoTaiKhoan";
            comm.Connection = conn;
            comm.Parameters.Add("@sdt", SqlDbType.NChar).Value = SDTNB;
            comm.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            comm.Parameters.Add("@noidungtb", SqlDbType.NVarChar).Value = "Bạn có đơn hàng cần giải quyết";
            comm.Parameters.Add("@src", SqlDbType.NVarChar).Value = "/DonDatHang/"+ SDTNB;
            int ret2 = comm.ExecuteNonQuery();


            if (ret > 0 && ret2 > 0)
            {
                return true;
            }
            return false;
        }
        public bool ThichVatPham(ThichVatPham temp)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into YeuThich(SDT,MaVP) values (@sdt,@mavp)";
            command.Parameters.Add("@sdt", SqlDbType.NChar).Value = temp.SDT;
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = Int32.Parse(temp.MaVP);
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();

            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ThongBaoTaiKhoan";
            comm.Connection = conn;
            comm.Parameters.Add("@sdt", SqlDbType.NChar).Value = temp.SDT;
            comm.Parameters.Add("@mavp", SqlDbType.Int).Value = Int32.Parse(temp.MaVP);
            comm.Parameters.Add("@noidungtb", SqlDbType.NVarChar).Value = "Có người thích sản phẩm của bạn";
            comm.Parameters.Add("@src", SqlDbType.NVarChar).Value = "/ChiTietVatPham/"+temp.MaVP;
            int ret2 = comm.ExecuteNonQuery();
            if (ret > 0 && ret2 > 0) return true;

            return false;

        }
        public bool DaThich(String SDT, int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from YeuThich where MaVP = @mavp and SDT= @sdt";
            command.Parameters.Add("@sdt", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        public bool BoThich(ThichVatPham temp)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from YeuThich where MaVP = @mavp and SDT= @sdt";
            command.Parameters.Add("@sdt", SqlDbType.NChar).Value = temp.SDT;
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = Int32.Parse(temp.MaVP);
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            if (ret > 0) return true;
            return false;
        }
        string ChuyenThoiGian(int gio)
        {
            if (gio < 24)
            {
                return gio.ToString() + " giờ trước";
            }
            else if (gio >= 24 && gio < 168)
            {
                return (gio / 24).ToString() + " ngày trước";
            }
            else if (gio >= 168 && gio < 672)
            {
                return (gio / 168).ToString() + " tuần trước";
            }
            else if (gio >= 672 && gio < 8064)
            {
                return (gio / 672).ToString() + " tháng trước";
            }
            else
            {
                return (gio / 8064).ToString() + " năm trước";
            }
        }
        public List<VatPhamAdmin> DanhSachVP()
        {
            List<VatPhamAdmin> kq = new List<VatPhamAdmin>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from VatPhamAdmin";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                VatPhamAdmin temp = new VatPhamAdmin();
                temp.MaVP = reader.GetInt32(0);
                temp.TenNguoiBan = reader.GetString(1);
                temp.TenVatPham = reader.GetString(2);
                temp.Mota = reader.GetString(3);
                temp.TinhTrang = reader.GetString(4);
                temp.GiaTien = reader.GetInt64(5);
                temp.TenTL = reader.GetString(6);
                temp.LinkHinhAnh = reader.GetString(7);
                temp.NgayDang = reader.GetDateTime(8).ToString("dd/MM/yyyy");
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }
        public List<DuyetVP> DuyetVP()
        {
            List<DuyetVP> kq = new List<DuyetVP>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from VatPhamChoDuyet";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DuyetVP temp = new DuyetVP();
                temp.MaVP = reader.GetInt32(0);
                temp.TenNguoiBan = reader.GetString(1);
                temp.TenVatPham = reader.GetString(2);
                temp.Mota = reader.GetString(3);
                temp.TinhTrang = reader.GetString(4);
                temp.GiaTien = reader.GetInt64(5);
                temp.TenTL = reader.GetString(6);
                temp.LinkHinhAnh = reader.GetString(7);
                temp.DaDuyet = reader.GetInt32(8);
                temp.NgayDang = reader.GetDateTime(9).ToString("dd/MM/yyyy");
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }
        public List<VatPhamBiKhoa> VPBiKhoa()
        {
            List<VatPhamBiKhoa> kq = new List<VatPhamBiKhoa>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from VatPhamBiKhoa";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VatPhamBiKhoa temp = new VatPhamBiKhoa();
                temp.MaVP = reader.GetInt32(0);
                temp.TenNguoiBan = reader.GetString(1);
                temp.TenVatPham = reader.GetString(2);
                temp.Mota = reader.GetString(3);
                temp.TinhTrang = reader.GetString(4);
                temp.GiaTien = reader.GetInt64(5);
                temp.TenTL = reader.GetString(6);
                temp.LinkHinhAnh = reader.GetString(7);
                temp.BiKhoa = reader.GetInt32(8);
                temp.NgayDang = reader.GetDateTime(9).ToString("dd/MM/yyyy");
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }

        public string RutGonTen(string temp)
        {
            string kq;
            if (temp.Length > 13)
            {
                kq = temp.Substring(0, 13) + "...";
            }
            else
            {
                kq = temp;
            }         
            
            return kq;
        }
        public bool KhoaVatPham(int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;         
            command.CommandText = "update VatPham set BiKhoa = 1 where MaVP =@mavp";
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            return ret > 0;
        }
        public bool MoKhoaVatPham(int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "update VatPham set BiKhoa = 0 where MaVP =@mavp";
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            return ret > 0;
        }
        public bool DuyetVatPham(int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "update VatPham set DaDuyet = 1 where MaVP =@mavp";
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            return ret > 0;
        }
        public bool NgungBan(int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "update VatPham set NgungBan = 1 where MaVP =@mavp";
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            return ret > 0;
        }
        public bool BanTiep(int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "update VatPham set NgungBan = 0 where MaVP =@mavp";
            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            return ret > 0;
        }
        public bool XoaVatPham(int MaVP)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "XoaVatPham";
            command.Connection = conn;

            command.Parameters.Add("@mavp", SqlDbType.Int).Value = MaVP;

            int ret = command.ExecuteNonQuery();

            if (ret > 0)
            {
                return true;
            }
            return false;
        }
        public List<VatPhamMoiNhat> LayVatPhamMoiNhat()
        {
            List<VatPhamMoiNhat> kq = new List<VatPhamMoiNhat>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from MatHangMoiNhat";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VatPhamMoiNhat temp = new VatPhamMoiNhat();
                temp.MaVP = reader.GetInt32(0);
                temp.TenVP = reader.GetString(1);
                temp.GiaTien = reader.GetInt64(2);                
                temp.LinkAnh = reader.GetString(3);
                temp.NgayDang = ChuyenThoiGian(reader.GetInt32(4));
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }
        public List<VatPhamMoiNhat> MatHangNoiBat()
        {
            List<VatPhamMoiNhat> kq = new List<VatPhamMoiNhat>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from VatPhamHotNhat";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VatPhamMoiNhat temp = new VatPhamMoiNhat();
                temp.MaVP = reader.GetInt32(0);
                temp.TenVP = reader.GetString(1);
                temp.GiaTien = reader.GetInt64(2);
                temp.LinkAnh = reader.GetString(3);                
                kq.Add(temp);
            }
            reader.Close();
            return kq;
        }
        public List<VatPham> TimKiemVP(string str, int MaTL)
        {
            List<VatPham> dsvp = new List<VatPham>();
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from dbo.TimKiem(@str,@MaTL)";
            command.Connection = conn;
            command.Parameters.Add("@MaTL", SqlDbType.Int).Value = MaTL;
            command.Parameters.Add("@str", SqlDbType.NVarChar).Value = str;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VatPham vp = new VatPham();
                vp.MaVP = reader.GetInt32(0);
                vp.TenVP = reader.GetString(1);
                vp.TenNguoiBan = reader.GetString(2);
                vp.SDT = reader.GetString(3);
                vp.ThanhPho = reader.GetString(4);
                vp.MoTa = reader.GetString(5);
                vp.TinhTrang = reader.GetString(6);
                vp.GiaTien = reader.GetInt64(7);
                vp.TheLoai = reader.GetString(8);
                int temp = reader.GetInt32(9);
                vp.NgayDang = ChuyenThoiGian(temp);
                vp.LinkHinhAnh = new List<string>();
                vp.LinkHinhAnh.Add(reader.GetString(10));
                vp.ChatLuong = reader.GetInt32(11);
                vp.DiaDiem = reader.GetString(12);
                vp.LoaiTK = reader.GetInt32(13);
                dsvp.Add(vp);
            }
            reader.Close();
            return dsvp;
        }
    }
}