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
    public partial class inTV : Form
    {
        public inTV()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");
        SqlDataAdapter da;
        DataSet ds;

        private void loadDL()
        {
            CrystalReportTV rpt = new CrystalReportTV();
            rpt.SetDatabaseLogon("sa", "123");
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
        private void inTV_Load(object sender, EventArgs e)
        {
            loadDL();
        }
        private void locData(string mk)
        {
            string sql = " select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv where tv.MaTeam like  '" + mk + "'";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            CrystalReportTV rpt = new CrystalReportTV();
            rpt.SetDatabaseLogon("sa", "123");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
        private void locDataTen(string mk)
        {
            string sql = "select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv where tv.HoTenSV like N'%" + mk + "%'";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CrystalReportTV rpt = new CrystalReportTV();
            rpt.SetDatabaseLogon("sa", "123");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.ToString() == "Team Kỹ Năng ")
            {
                locData("T1");
            }
            if (comboBox1.Text.ToString() == "Team Văn Nghệ")
            {
                locData("T2");
            }
            if (comboBox1.Text.ToString() == "Team MC-Hoạt Náo")
            {
                locData("T3");
            }
            if (comboBox1.Text.ToString() == "Team Truyền Thông ")
            {
                locData("T4");
            }
            if (comboBox1.Text.ToString() == "Tất Cả")
            {
                loadDL();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                locDataTen(textBox1.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa nhập tên cần tìm ", "Thông Báo!!");
            }
        }
    }
}
