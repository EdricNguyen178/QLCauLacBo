using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Do_An_QLCauLacBo.DAO
{
  public  class login
    {
        public static login instance;
       
        public static login Instance
        {
            get { if (instance == null) instance = new login(); return  instance; }
            private set  => instance = value;
        }
        public static bool logintest(string userName, string passWord)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            string query = "SELECT * FROM ThanhVien WHERE MaSV LIKE N'" + userName + "' AND MatKhau = N'" + hasPass + "' ";
            
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }
        public static bool kiemtratontai()
        {
            SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");
            con.Open();
            SqlCommand data = new SqlCommand("select COUNT(*) as dem from TKDangDN ", con);
            SqlDataReader dr = data.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count = int.Parse(dr[0].ToString());
            }
            con.Close();
            if (count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
