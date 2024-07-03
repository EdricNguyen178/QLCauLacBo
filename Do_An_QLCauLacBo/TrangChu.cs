using Do_An_QLCauLacBo.DAO;
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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");
        SqlDataAdapter da;
        DataSet ds;
        DataSet dsTV = new DataSet();
        DataColumn[] key = new DataColumn[1];
        string tam = "";
        
        private void loadDATA()
        {
            dsTV = new DataSet();
            SqlDataAdapter dataTV = new SqlDataAdapter("select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv", con);
            dataTV.Fill(dsTV, "ThanhVien");
            dataGridView1.DataSource = dsTV.Tables["ThanhVien"];
        }
        private void loadDATAHD()
        {
            
            dsTV = new DataSet();
            SqlDataAdapter dataTV = new SqlDataAdapter("select hd.MaHD,hd.TenHD,hd.NoiDungHD,hd.MaTeam,hd.NgayThucHien,hd.DiaDiem from HoatDong hd", con);
            dataTV.Fill(dsTV, "HoatDong");
            dataGridView2.DataSource = dsTV.Tables["HoatDong"];
        }
 
        private void loadDATAPC(string pc)
        {
            dsTV = new DataSet();
            SqlDataAdapter dataTV = new SqlDataAdapter("select pc.MaHD,tv.HoTenSV,pc.NoiDungCV from PhanCongViec pc full outer join ThanhVien tv on pc.MaSV=tv.MaSV  where pc.MaHD='"+pc+"'", con);
            dataTV.Fill(dsTV, "PhanCongViec");
            dataGridView3.DataSource = dsTV.Tables["PhanCongViec"];
        }
        private void locData(string mk)
        {
            string sql = " select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv where tv.MaTeam like  '" + mk + "'";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void locDATAHD(string mk)
        {
            string sql ="select hd.MaHD,hd.TenHD,hd.NoiDungHD,hd.MaTeam,hd.NgayThucHien,hd.DiaDiem from HoatDong hd where hd.MaTeam = '"+mk+"'";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void TrangChu_Load(object sender, EventArgs e)
        {
            time1.Text = DateTime.Now.ToString("hh:mm:ss");
            time2.Text = DateTime.Now.ToString("hh:mm:ss");
            time3.Text = DateTime.Now.ToString("hh:mm:ss");
            timer1.Start();
            ///////
            loadDATA();
            loadDATAHD();
            //loadGridView();
            ////////////////
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = con;
            sqlcomm.CommandText = "SELECT * FROM ThanhVien FULL OUTER JOIN TKDangDN ON  ThanhVien.MaSV=TKDangDN.MaSV where ThanhVien.MaSV=TKDangDN.MaSV ";
            con.Open();
            SqlDataReader dr = sqlcomm.ExecuteReader();
            
            while (dr.Read())
            {
                txtMSSV.Text = dr["MaSV"].ToString();
                txtHoTen.Text = dr["HoTenSV"].ToString();
                txtNVCLB.Text = dr["NgayVaoCLB"].ToString();
                txtNS.Text = dr["NgaySinh"].ToString();
                txtCV.Text = dr["ChucVu"].ToString();
                txtSDT.Text = dr["SDT"].ToString();
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                string z = dr["Anh"].ToString();
                pictureBox1.Image = Image.FromFile(@z);
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            dr.Close();
            con.Close();
            string mtam = txtCV.Text.ToString();
            if (mtam.Contains("Thành Viên")|| mtam.Contains("thành viên")|| mtam.Contains("Thành viên")|| mtam.Contains("thành Viên"))
            {
                button5.Enabled = button4.Enabled = button6.Enabled = button7.Enabled = BTHEMHD.Enabled = BSuaHD.Enabled = XoaHD.Enabled = button8.Enabled = false;       
            }    
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTeam.Text.ToString() == "Team Kỹ Năng ")
            {
                locData("T1");
            }
            if (comboBoxTeam.Text.ToString() == "Team Văn Nghệ")
            {
                locData("T2");
            }
            if (comboBoxTeam.Text.ToString() == "Team MC-Hoạt Náo")
            {
                locData("T3");
            }
            if (comboBoxTeam.Text.ToString() == "Team Truyền Thông ")
            {
                locData("T4");
            }
            if (comboBoxTeam.Text.ToString() == "Tất Cả")
            {
                loadDATA();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time1.Text = DateTime.Now.ToString("hh:mm:ss");
            time2.Text = DateTime.Now.ToString("hh:mm:ss");
            time3.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "delete  from TKDangDN;";
            com.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            fDangNhap a = new fDangNhap();
            this.Hide();
            a.ShowDialog();
            this.Close();
           
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        //private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            ThemTaiKhoang a = new ThemTaiKhoang();
            this.Hide();
            a.ShowDialog();

            loadDATA();
            this.Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.ImageLocation = openFileDialog1.FileName;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            txtAnh.Text = openFileDialog1.FileName;
        }
        //private void Databindings(DataTable dt)
        //{
       

        //    txtMASV.DataBindings.Add("Text", dt, "MaSV");
        //    txtHoTenSV.DataBindings.Add("Text", dt, "HoTenSV");
        //    comboBoxGT.DataBindings.Add("Text", dt, "GioiTinh");
        //    dateTimeNgaySinh.DataBindings.Add("Text", dt, "NgaySinh");
        //    txtChucVu.DataBindings.Add("Text", dt, "ChucVu");
        //    comboBoxMTeam.DataBindings.Add("Text", dt, "MaTeam");
        //    txtSDTSV.DataBindings.Add("Text", dt, "SDT");
        //    dateTimeNgayVaoCLB.DataBindings.Add("Text", dt, "NgayVaoCLB");
        //    txtAnh.DataBindings.Add("Text", dt, "Anh");
        //    pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
        //    string z = txtAnh.Text;
        //    pictureBox2.Image = Image.FromFile(@z);
        //    pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;

        //}
        //private void loadGridView()
        //{

        //    string selectString = "SELECT MaSV,HoTenSV,GioiTinh,NgaySinh,ChucVu,MaTeam,SDT,NgayVaoCLB,Anh FROM ThanhVien ";
        //    dsTV = new DataSet();
        //    da = new SqlDataAdapter(selectString, con);
        //    da.Fill(dsTV);
        //    dataGridView1.DataSource = dsTV.Tables[0];
        //    Databindings(dsTV.Tables[0]);


        //}
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                 txtMASV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                //DataGridView row = this.dataGridView1.Rows[e.RowIndex];
                txtMASV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtHoTenSV.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBoxGT.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimeNgaySinh.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtChucVu.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBoxMTeam.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtSDTSV.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                dateTimeNgayVaoCLB.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtAnh.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox2.Image = Image.FromFile(txtAnh.Text.ToString());
                pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn xoá không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandText = "DELETE FROM ThanhVien Where MaSV = ('" + txtMASV.Text + "')";
                    com.ExecuteNonQuery();
                    MessageBox.Show("Xoá Thành Công!", "Thông báo ");
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    loadDATA();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xoá không Thành Công!", "Thông báo ");
                    throw;
                }

            }
            //DataRow dr = dsTV.Tables["ThanhVien"].Rows.Find(txtMASV.Text);
            //if(dr !=null)
            //{
            //    dr.Delete();
            //}
            //da.Update(dsTV, "ThanhVien");
            txtHoTenSV.Text = txtChucVu.Text = txtSDTSV.Text = txtAnh.Text = txtMASV.Text = comboBoxMTeam.Text = comboBoxGT.Text = "";
            pictureBox2.BackgroundImageLayout = ImageLayout.Center;
            pictureBox2.Image = Image.FromFile(@"C:\\Users\\ASUS\\OneDrive\\Máy tính\\HK5\\Công nghệ .NET\\Do_An_QLCauLacBo\\pic\\\nút\\add_24px.png");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            txtHoTenSV.ReadOnly = txtChucVu.ReadOnly = txtSDTSV.ReadOnly = txtAnh.ReadOnly = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DoiMatKhau a = new DoiMatKhau();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn lưu không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandText = "set dateformat DMY UPDATE ThanhVien SET HoTenSV =N'" + txtHoTenSV.Text + "',GioiTinh =N'" + comboBoxGT.Text + "',NgaySinh =N'" + dateTimeNgaySinh.Text + "',ChucVu =N'" + txtChucVu.Text + "',MaTeam =N'" + comboBoxMTeam.Text + "',SDT =N'" + txtSDTSV.Text + "',NgayVaoCLB =N'" + dateTimeNgayVaoCLB.Text + "',Anh =N'" + txtAnh.Text + "' Where MaSV = ('" + txtMASV.Text + "')";
                    com.ExecuteNonQuery();
                    MessageBox.Show("Sửa Thành Công!", "Thông báo ");
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    txtMASV.ReadOnly = txtHoTenSV.ReadOnly = txtChucVu.ReadOnly = txtSDTSV.ReadOnly = txtAnh.ReadOnly = true;
                    loadDATA();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sửa Không Thành Công!", "Thông báo ");
                   
                }
            }
        }
        string maHD = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //DataGridView row = this.dataGridView1.Rows[e.RowIndex];
                txtThongTin.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                maHD = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                loadDATAPC(maHD);
                tam= dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

            }
        }

        private void BSuaHD_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandText = "UPDATE HoatDong SET NoiDungHD =N'" + txtThongTin.Text + "' Where MaHD = ('" + maHD + "')";
                    com.ExecuteNonQuery();
                    MessageBox.Show("Sửa Thành Công!", "Thông báo ");
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    loadDATAHD();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sửa Không Thành Công!", "Thông báo ");
                    throw;
                }
            }
           
}

        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult dlr = MessageBox.Show("Có Đăng Xuất Trước Khi Thoát Không ?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //if (dlr == DialogResult.Yes)
            //{

            //    if (con.State == ConnectionState.Closed)

            //    {
            //        con.Open();
            //    }
            //    SqlCommand com = new SqlCommand();
            //    com.Connection = con;
            //    com.CommandText = "delete  from TKDangDN;";
            //    com.ExecuteNonQuery();
            //    if (con.State == ConnectionState.Open)
            //    {
            //        con.Close();
            //    }
            //    //TrangChu a = new TrangChu();
            //    //a.Close();
            //    System.Windows.Forms.Application.Exit();
            //    //this.Hide();
            //}
            //if (dlr == DialogResult.No)
            //{
            //    //TrangChu a = new TrangChu();
            //    //a.Close();
            //    System.Windows.Forms.Application.Exit();
            //    //this.Close();
            //}
            //if (dlr == DialogResult.Cancel)
            //{
            //    e.Cancel = true;

            //}

        }

        private void BTHEMHD_Click(object sender, EventArgs e)
        {
            ThemHD a = new ThemHD();
            this.Hide();
            a.ShowDialog();
            loadDATAHD();
            this.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "delete  from inHD;";
            com.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand comm = new SqlCommand();
            comm.Connection = con;
            comm.CommandText = "delete  from inHD;";
            comm.CommandText = "INSERT INTO inHD VALUES('" + tam + "')";
            comm.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            InPC a = new InPC();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            inTV a = new inTV();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (CBteamHD.Text.ToString() == "Team Kỹ Năng ")
            {
                locDATAHD("T1");
            }
            if (CBteamHD.Text.ToString() == "Team Văn Nghệ")
            {
                locDATAHD("T2");
            }
            if (CBteamHD.Text.ToString() == "Team MC-Hoạt Náo")
            {
                locDATAHD("T3");
            }
            if (CBteamHD.Text.ToString() == "Team Truyền Thông ")
            {
                locDATAHD("T4");
            }
            if (CBteamHD.Text.ToString() == "Tất Cả")
            {
                loadDATAHD();

            }
            
        }
        private void locTenHD(string mk)
        {
            string sql = "select hd.MaHD,hd.TenHD,hd.NoiDungHD,hd.MaTeam,hd.NgayThucHien,hd.DiaDiem from HoatDong hd where hd.TenHD like N'%" + mk + "%'";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                locTenHD(txtTenHD.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("bạn chua nhập tên hoạt động ", "thông báo ");
            }
            
        }

        private void XoaHD_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn xoá không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandText = "DELETE FROM HoatDong Where MaHD = (N'" + tam + "')";
                    com.ExecuteNonQuery();
                    MessageBox.Show("Xoá Thành Công!", "Thông báo ");
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    loadDATAHD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xoá không Thành Công!", "Thông báo ");
                    throw;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            phancong a = new phancong();
            this.Hide();
            a.ShowDialog();
            
            this.Show();
          
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

       
        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
