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
    public partial class InPC : Form
    {
        public InPC()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = EDRICNGUYEN\\SQLEXPRESS; Initial Catalog = DA_QL_CLB; User ID = sa; Password = 123");
        SqlDataAdapter da;
        DataSet ds;

        private void loadDL()
        {
            string sql = " select  hd.MaHD,hd.TenHD,hd.NgayThucHien,hd.DiaDiem,tv.MaSV,tv.HoTenSV,pc.NoiDungCV from ThanhVien tv, PhanCongViec pc,inHD ,HoatDong hd where tv.MaSV=pc.MaSV and pc.MaHD=hd.MaHD and hd.MaHD=inHD.MaHD ";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CrystalReportPC rpt = new CrystalReportPC();
            rpt.SetDatabaseLogon("sa", "123");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
        private void InPC_Load(object sender, EventArgs e)
        {
            loadDL();
        }
    }
}
