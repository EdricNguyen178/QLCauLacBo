using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_QLCauLacBo
{
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");
       
        private void B1an_Click(object sender, EventArgs e)
        {
            if (txtMKCu.PasswordChar == '\0')
            {
                B1hien.BringToFront();
                txtMKCu.PasswordChar = '*';
            }
        }

        private void B1hien_Click(object sender, EventArgs e)
        {
            if (txtMKCu.PasswordChar == '*')
            {
                B1an.BringToFront();
                txtMKCu.PasswordChar = '\0';
            }
        }

        private void B2an_Click(object sender, EventArgs e)
        {
            if (txtMKMoi.PasswordChar == '\0')
            {
                B2hien.BringToFront();
                txtMKMoi.PasswordChar = '*';
            }
        }

        private void B2hien_Click(object sender, EventArgs e)
        {
            if (txtMKMoi.PasswordChar == '*')
            {
                B2an.BringToFront();
                txtMKMoi.PasswordChar = '\0';
            }
        }

        private void B3an_Click(object sender, EventArgs e)
        {
            if (txtNhapLai.PasswordChar == '\0')
            {
                B3hien.BringToFront();
                txtNhapLai.PasswordChar = '*';
            }
        }

        private void B3hien_Click(object sender, EventArgs e)
        {
            if (txtNhapLai.PasswordChar == '*')
            {
                B3an.BringToFront();
                txtNhapLai.PasswordChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn đỏi mật khẩu ?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                if(txtMKMoi.Text==txtNhapLai.Text)
                {
                    if (txtMKCu.Text != txtMKMoi.Text)
                    {
                        byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtMKMoi.Text);
                        byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandText = "UPDATE ThanhVien SET MatKhau = '" + hasData + "' WHERE MaSV = (SELECT * FROM TKDangDN) ";
                        com.ExecuteNonQuery();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        MessageBox.Show("Sửa Thành Công!", "Thông báo ");
                    }
                    else
                    {
                        MessageBox.Show("mật khẩu mới không được giống mật khẩu cũ", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("mật khẩu nhập lại phải giống mật khẩu mới!", "CẢNH BÁO ");
                }
            }    
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
