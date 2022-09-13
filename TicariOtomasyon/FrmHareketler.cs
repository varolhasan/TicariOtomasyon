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
    public partial class FrmHareketler : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmHareketler()
        {
            InitializeComponent();
        }
        void FirmaHareketleri()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("execute FIRMAHAREKET", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }
        void MusteriHareketleri()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("execute MUSTERIHAREKET", bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;

        }
        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            FirmaHareketleri();
            MusteriHareketleri();
        }
    }
}
