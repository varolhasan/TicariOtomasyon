using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace TicariOtomasyon
{
    public partial class FrmFaturaUrunler : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmFaturaUrunler()
        {
            InitializeComponent();
        }
        public string faturaId;
        public void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FATURADETAY where FATURAID=" + faturaId, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmFaturaUrunler_Load(object sender, EventArgs e)
        {
            lblId.Visible = false;
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme fr = new FrmFaturaUrunDuzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.urunid = dr["FATURAURUNID"].ToString();

            }
            fr.Show();
            
        }
    }
}
