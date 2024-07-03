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
    public partial class ThemTaiKhoang : Form
    {
        public ThemTaiKhoang()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private bool kiemtratontai(string a)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand data = new SqlCommand("select COUNT(*) as dem from ThanhVien where MaSV = '" + a + "' ", con);
                SqlDataReader dr;
                dr = data.ExecuteReader();
                while (dr.Read())
                {
                    count = int.Parse(dr["dem"].ToString());
                }
                con.Close();
                if (count >= 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {

            pictureBox3.BackgroundImageLayout = ImageLayout.Center;
            pictureBox3.Image = Image.FromFile(@"C:\\Users\\ASUS\\OneDrive\\Máy tính\\HK5\\Công nghệ .NET\\Do_An_QLCauLacBo\\pic\\\nút\\add_24px.png");
            txtAnh.Text = txtChucVu.Text = txtHoTenSV.Text = txtMASV.Text = txtKTMK.Text = txtMK.Text = comboBoxMTeam.Text = txtSDT.Text = dateTimeNgaySinh.Text = dateTimeNgayVaoCLB.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string a;
            if(comboBoxMTeam.Text == "Team Kỹ Năng")
            {
                a = "T1";
            }
            if (comboBoxMTeam.Text == "Team Văn Nghệ")
            {
                a = "T2";
            }
            if (comboBoxMTeam.Text == "Team MC - Hoạt Náo")
            {
                a = "T3";
            }
            else
            {
                a = "T4";
            }
            if (txtMK.Text == txtKTMK.Text)
            {
                string gt;
                byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtMK.Text);
                byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

                string hasPass = "";

                foreach (byte item in hasData)
                {
                    hasPass += item;
                }

                if (rBnam.Checked)
                    gt = "Nam";
                else
                    gt = "Nữ";
                if (kiemtratontai(txtMASV.Text) == true)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    //com.CommandText = "set dateformat DMY";
                    //com.CommandText = "INSERT INTO ThanhVien VALUES('" + txtMASV.Text + "',N'" + txtHoTenSV.Text + "',N'" + gt + "','" + dateTimeNgaySinh.Value + "',N'" + txtChucVu.Text + "','" + comboBoxMTeam.Text + "',N'" + txtSDT.Text + "','" + dateTimeNgayVaoCLB.Value + "','" + hasPass + "','" + txtAnh.Text + "')";
                    com.CommandText = "set dateformat DMY INSERT INTO ThanhVien VALUES(@MS,@HT,@GT,@NS,@CV,@MT,@SDT,@NVCLB,@MK,@A)";
                    com.Parameters.AddWithValue("@MS", txtMASV.Text);
                    com.Parameters.AddWithValue("@HT", txtHoTenSV.Text);
                    com.Parameters.AddWithValue("@GT", gt);
                    com.Parameters.AddWithValue("@NS", dateTimeNgaySinh.Text);
                    com.Parameters.AddWithValue("@CV", txtChucVu.Text);
                    com.Parameters.AddWithValue("@MT", a);
                    com.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    com.Parameters.AddWithValue("@NVCLB", dateTimeNgayVaoCLB.Text);
                    com.Parameters.AddWithValue("@MK", hasPass);
                    com.Parameters.AddWithValue("@A", txtAnh.Text);
                    com.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    MessageBox.Show("thêm thành công ", "thông báo ");
                }
                else
                {
                    MessageBox.Show("trùng khoá ngoại", "thông báo ");
                }
            }
            else
            {
                MessageBox.Show("nhập mật khẩu ko giống", "thông báo");
            }
        }

        private void ThemTaiKhoang_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.ImageLocation = openFileDialog1.FileName;
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            txtAnh.Text = openFileDialog1.FileName;
        }


    }
}
