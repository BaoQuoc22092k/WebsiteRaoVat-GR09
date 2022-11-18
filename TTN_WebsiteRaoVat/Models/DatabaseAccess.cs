using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Models
{
    public class DatabaseAccess
    {
        string strConn = @"Data Source=.\SQLEXPRESS;Database=Website_RaoVat;Integrated Security=True";
        //string strConn = "Data Source=DESKTOP-UE7MK69;Database=Website_RaoVat1;Integrated Security=True";
        protected SqlConnection conn = null;
        public void OpenConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection(strConn);
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch
            {
                // ket noi that bai
            }

        }

        public void CloseConnection()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch
            {
                // dong that bai
            }
        }
    }
}