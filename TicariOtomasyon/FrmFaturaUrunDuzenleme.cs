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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunid;

        private void temizle()
        {
            txtUrunId.Text = "";
            txtUrunAd.Text = "";
            txtTutar.Text = "";
            txtMiktar.Text = "";
            txtFiyat.Text = "";
        }
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunId.Text = urunid;
            SqlCommand komut1 = new SqlCommand("select * from TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", urunid);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                txtUrunAd.Text = dr1[1].ToString();
                txtMiktar.Text = dr1[2].ToString();
                txtFiyat.Text = dr1[3].ToString();
                txtTutar.Text = dr1[4].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnTemizle1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle1_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("update TBL_FATURADETAY set URUNAD=@p1,MIKTAR=@p2,FIYAT=@p3,TUTAR=@p4 " +
                "where FATURAURUNID=@p5", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", Convert.ToDouble((txtMiktar.Text).ToString()));
            komutguncelle.Parameters.AddWithValue("@p3", Convert.ToDouble((txtFiyat.Text).ToString()));
            komutguncelle.Parameters.AddWithValue("@p4", Convert.ToDouble((txtTutar.Text).ToString()));
            komutguncelle.Parameters.AddWithValue("@p5", urunid);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Ürün Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void btnSil1_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtUrunId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }
    }
}
