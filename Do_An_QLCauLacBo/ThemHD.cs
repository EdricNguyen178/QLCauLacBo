using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_QLCauLacBo
{
    public partial class ThemHD : Form
    {
        public ThemHD()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxMTeam.Text=txtDiaDiem.Text = txtMHD.Text = txtNoiDungHoatDong.Text = txtTenHD.Text = "";
            txtMHD.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string a = " ";
                if (comboBoxMTeam.Text == "Team Kỹ Năng")
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
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                //com.CommandText = "set dateformat DMY";
                //com.CommandText = "INSERT INTO ThanhVien VALUES('" + txtMASV.Text + "',N'" + txtHoTenSV.Text + "',N'" + gt + "','" + dateTimeNgaySinh.Value + "',N'" + txtChucVu.Text + "','" + comboBoxMTeam.Text + "',N'" + txtSDT.Text + "','" + dateTimeNgayVaoCLB.Value + "','" + hasPass + "','" + txtAnh.Text + "')";
                com.CommandText = "set dateformat DMY INSERT INTO HoatDong VALUES(@MHD,@THD,@NDHD,@MT,@NHD,@DDHD)";
                com.Parameters.AddWithValue("@MHD", txtMHD.Text);
                com.Parameters.AddWithValue("@THD", txtTenHD.Text);
                com.Parameters.AddWithValue("@NDHD", txtNoiDungHoatDong.Text);
                com.Parameters.AddWithValue("@MT", a);
                com.Parameters.AddWithValue("@NHD", dateTimeHD.Text);
                com.Parameters.AddWithValue("@DDHD", txtDiaDiem.Text);
                com.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show("thêm thành công ", "thông báo ");
            }
            catch (Exception)
            {

                MessageBox.Show("thêm thất bại ", "Thông Báo!");
            }
           
        }
    }
}
