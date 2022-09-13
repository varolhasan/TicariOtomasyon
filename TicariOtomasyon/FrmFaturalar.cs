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
    public partial class FrmFaturalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        private void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FATURABILGI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void temizle()
        {
            txtSeriNo.Focus();
            txtId.Text = "";
            txtSeriNo.Text = "";
            txtSiraNo.Text = "";
            mskdSaat.Text = "";
            mskdTarih.Text = "";
            txtVergiDaire.Text = "";
            txtAlici.Text = "";
            txtTeslimAlan.Text = "";
            txtTeslimEden.Text = "";

            txtUrunAd.Text = "";
            txtUrunId.Text = "";
            txtMiktar.Text = "";
            txtFiyat.Text = "";
            txtTutar.Text = "";
            txtFaturaId.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            //FaturaBilgi Kaydetme
            SqlCommand komutkaydet = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", txtSeriNo.Text);
            komutkaydet.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            komutkaydet.Parameters.AddWithValue("@p3", mskdTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p4", mskdSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p5", txtVergiDaire.Text);
            komutkaydet.Parameters.AddWithValue("@p6", txtAlici.Text);
            komutkaydet.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komutkaydet.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Fatura Kaydedilmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["FATURABILGIID"].ToString();
            txtSeriNo.Text = dr["SERI"].ToString();
            txtSiraNo.Text = dr["SIRANO"].ToString();
            mskdTarih.Text = dr["TARIH"].ToString();
            mskdSaat.Text = dr["SAAT"].ToString();
            txtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
            txtAlici.Text = dr["ALICI"].ToString();
            txtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
            txtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
        }

        private void btnKaydet1_Click(object sender, EventArgs e)
        {
            //Faturaya Ait Ürün Kaydetme
            double fiyat, miktar, tutar;
            fiyat = Convert.ToDouble(txtFiyat.Text);
            miktar = Convert.ToDouble(txtMiktar.Text);
            tutar = fiyat * miktar;
            txtTutar.Text = tutar.ToString();
            SqlCommand komutkaydet = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values " +
                "(@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komutkaydet.Parameters.AddWithValue("@p2", txtMiktar.Text);
            komutkaydet.Parameters.AddWithValue("@p3", txtFiyat.Text);
            komutkaydet.Parameters.AddWithValue("@p4", txtTutar.Text);
            komutkaydet.Parameters.AddWithValue("@p5", txtFaturaId.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Faturaya Ait Ürünler Kaydedilmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Fatura Silme
            SqlCommand komutsil = new SqlCommand("delete TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Fatura Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Fatura Güncelleme
            SqlCommand komutguncelle = new SqlCommand("update TBL_FATURABILGI set SERI=@p1,SIRANO=@p2,TARIH=@p3,SAAT=@p4,VERGIDAIRE=@p5," +
                "ALICI=@p6,TESLIMEDEN=@p7,TESLIMALAN=@p8 where FATURABILGIID=@p9", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", txtSeriNo.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            komutguncelle.Parameters.AddWithValue("@p3", mskdTarih.Text);
            komutguncelle.Parameters.AddWithValue("@p4", mskdSaat.Text);
            komutguncelle.Parameters.AddWithValue("@p5", txtVergiDaire.Text);
            komutguncelle.Parameters.AddWithValue("@p6", txtAlici.Text);
            komutguncelle.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komutguncelle.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            komutguncelle.Parameters.AddWithValue("@p9", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Fatura Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler fr = new FrmFaturaUrunler();
            fr.faturaId = txtId.Text;
            fr.Show();
        }
    }
}
