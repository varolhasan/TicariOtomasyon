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
    public partial class FrmStoklar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmStoklar()
        {
            InitializeComponent();
        }

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //((SideBySideBarSeriesView)chartControl1.Series[0].View).ColorEach = true;
            //chartControl1.Series[0].LegendTextPattern = "{S} - {A}: {V:F1}";

            /*chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 8);
            chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 6);
            chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 7);
            chartControl1.Series["Series 1"].Points.AddPoint("Trabzon", 5);*/

            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,Sum(ADET) As 'Miktar' from TBL_URUNLER group by URUNAD", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;


            //Charta Stok Miktarı Listeleme
            SqlCommand komut = new SqlCommand("Select URUNAD,Sum(ADET) As 'Miktar' from TBL_URUNLER group by URUNAD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();
        }

    }
}
