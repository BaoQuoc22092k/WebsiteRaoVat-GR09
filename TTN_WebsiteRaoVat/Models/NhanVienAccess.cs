using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class NhanVienAccess : DatabaseAccess
    {
        public bool KiemTraDangNhap(string username, string password,string check)
        {
            if(check == "Admin")
            {
                OpenConnection();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from NhanVien where username = @username and password = @password";
                command.Connection = conn;
                command.Parameters.Add("@username", SqlDbType.NChar).Value = username;
                command.Parameters.Add("@password", SqlDbType.NChar).Value = password;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}