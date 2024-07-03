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
    public partial class phancong : Form
    {
        public phancong()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");
        SqlDataAdapter da;
        DataSet ds;
        DataSet dsTV = new DataSet();
        DataColumn[] key = new DataColumn[1];
        String MaSv = "";
        private void loadDATA()
        {
            dsTV = new DataSet();
            SqlDataAdapter dataTV = new SqlDataAdapter("select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv", con);
            dataTV.Fill(dsTV, "ThanhVien");
            dataGridView1.DataSource = dsTV.Tables["ThanhVien"];
        }
        private void locData(string mk)
        {
            string sql = " select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB from ThanhVien tv where tv.HoTenSV like  N'%" + mk + "%'";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void phancong_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from HoatDong ", con);
            DataTable Dt1 = new DataTable();
            da1.Fill(Dt1);
            comboBox1.DataSource = Dt1;
            comboBox1.DisplayMember = "MaHD";
            loadDATA();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtTenSV.Text!="")
            {
                locData(txtTenSV.Text);
            }   
            else
            {
                MessageBox.Show("bạn Chưa Nhập Tên SV", "Thông Báo!!!!");
            }    
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex != -1)
            {
                MaSv = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if(kiemtraDaPC(MaSv)==true)
                {
                    //DataGridView row = this.dataGridView1.Rows[e.RowIndex];
                    txtTenSV.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }   
                else
                {
                    MessageBox.Show("Thành Viên đã phân công trong hoạt động đó ", "thông báo");
                }    
                
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadDATA();
            txtTenSV.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadDATA();
            txtTenSV.Text = "";
            txtND.Text = "";
            comboBox1.Text = "";
        }
        private bool kiemtraDaPC(string a)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand data = new SqlCommand("select COUNT(*) as dem from ThanhVien tv full outer join PhanCongViec pc on tv.MaSV=pc.MaSV  where tv.MaSV =N'" + a + "' and pc.MaHD=N'"+comboBox1.Text+"' ", con);
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
        
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                //com.CommandText = "set dateformat DMY";
                //com.CommandText = "INSERT INTO ThanhVien VALUES('" + txtMASV.Text + "',N'" + txtHoTenSV.Text + "',N'" + gt + "','" + dateTimeNgaySinh.Value + "',N'" + txtChucVu.Text + "','" + comboBoxMTeam.Text + "',N'" + txtSDT.Text + "','" + dateTimeNgayVaoCLB.Value + "','" + hasPass + "','" + txtAnh.Text + "')";
                com.CommandText = "set dateformat DMY INSERT INTO PhanCongViec VALUES(@MHD,@MSV,@NDCV)";
                com.Parameters.AddWithValue("@MHD", comboBox1.Text);
                com.Parameters.AddWithValue("@MSV", MaSv);
                com.Parameters.AddWithValue("@NDCV", txtND.Text);
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
