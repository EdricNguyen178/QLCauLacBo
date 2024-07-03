using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Do_An_QLCauLacBo.DAO;

namespace Do_An_QLCauLacBo
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");

        int a = 2;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("liên hệ: \nSĐT:0947267178 \ngmail:ntrhieu01@gmail.com ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TXTMatKhau.PasswordChar == '\0')
            {
                button3.BringToFront();
                TXTMatKhau.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TXTMatKhau.PasswordChar == '*')
            {
                button2.BringToFront();
                TXTMatKhau.PasswordChar = '\0';
            }
        }
        private bool KTDaDangNhap()
        {
            return login.kiemtratontai();
        }
        private void DangNhap_Load(object sender, EventArgs e)
        {
            if(KTDaDangNhap()==true)
            {
                this.Close();
                TrangChu a = new TrangChu();
                this.Hide();
                a.ShowDialog();
            }  
            else
            {
                TXTMatKhau.PasswordChar = '*';
                pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox1.Image = Image.FromFile(@"C:\\Users\\ASUS\\OneDrive\\Máy tính\\HK5\\Công nghệ .NET\\Do_An_QLCauLacBo\\pic\\DN\\1.jpg");


            }

        }
        private bool Login1(string userName, string passWord)
        {
            return login.logintest(userName, passWord);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TXTMatKhau.Text != "" &&  TXTMSSV.Text != "")
            {
                string userName = TXTMSSV.Text;
                string passWord = TXTMatKhau.Text;
                if (Login1(userName, passWord))
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandText = "INSERT INTO TKDangDN VALUES('" + TXTMSSV.Text + "')";
                    com.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    TrangChu a = new TrangChu();
                    this.Hide();
                    a.ShowDialog();
                    this.Close();
                    

                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!", "Thông Báo!");
                }
            }
            else
            {
                MessageBox.Show("khong được để trống mật khẩu hoặc tên đăng nhập ", "thông báo ");
            }    
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(a==8)
            { 
                a = 1;
            }
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = Image.FromFile("C:\\Users\\ASUS\\OneDrive\\Máy tính\\HK5\\Công nghệ .NET\\Do_An_QLCauLacBo\\pic\\DN\\"+a+".jpg");
            a++;
        }

        private void DangNhap_MaximizedBoundsChanged(object sender, EventArgs e)
        {
           

        }

        

        private void fDangNhap_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //fDangNhap a = new fDangNhap();
            //a.Close();
            //System.Windows.Forms.Application.Exit();
            //this.Close();
        }



        //private void fDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    //if (con.State == ConnectionState.Closed)
        //    //{
        //    //    con.Open();
        //    //}
        //    //SqlCommand com = new SqlCommand();
        //    //com.Connection = con;
        //    //com.CommandText = "delete  from TKDangDN;";
        //    //com.ExecuteNonQuery();
        //    //if (con.State == ConnectionState.Open)
        //    //{
        //    //    con.Close();
        //    //}
        //    this.Close();
        //}
    }
}
