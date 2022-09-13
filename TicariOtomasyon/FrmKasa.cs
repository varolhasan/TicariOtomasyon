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
using DevExpress.Charts;
namespace TicariOtomasyon
{
    public partial class FrmKasa : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmKasa()
        {
            InitializeComponent();
        }
        void Musterihareket()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("execute MUSTERIHAREKET", bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;
        }
        void Firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("execute FIRMAHAREKET", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }
        void Giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_GIDERLER order by ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            Musterihareket();
            Firmahareket();
            lblAktifKullanici.Text = ad;

            //Toplam tutarı hesaplama
            SqlCommand komut1 = new SqlCommand("select sum(tutar) from TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamTutar.Text = dr1[0] + " TL";
            }
            bgl.baglanti().Close();

            //Son ayın faturaları
            SqlCommand komut2 = new SqlCommand("select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0] + " TL";
            }
            bgl.baglanti().Close();

            //Son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("select MAASLAR FROM TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPerMaas.Text = dr3[0] + " TL";
            }
            bgl.baglanti().Close();

            //Toplam müşteri sayısı
            SqlCommand komut4 = new SqlCommand("select count(*) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam firma sayısı
            SqlCommand komut5 = new SqlCommand("select count(*) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam müşteri şehir sayısı
            SqlCommand komut6 = new SqlCommand("select count(distinct(IL)) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblMSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam firma şehir sayısı
            SqlCommand komut7 = new SqlCommand("select count(*) FROM TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblFSehirSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam personel sayısı
            SqlCommand komut8 = new SqlCommand("select count(distinct(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblPerSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam ürün sayısı
            SqlCommand komut9 = new SqlCommand("select count(distinct(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblStokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            

           
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac > 0 && sayac <= 5)
            {
                //1.chartkonrole elektrik faturası son 4 ay listeleme
                SqlCommand komut10 = new SqlCommand("select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac > 5 && sayac <= 10)
            {
                chartControl1.Series["AYLAR"].Points.Clear();
                //2.chartkonrole elektrik faturası son 4 ay listeleme
                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
        }
    }
}
