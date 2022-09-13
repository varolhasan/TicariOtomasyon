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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            mskdtxtYil.Text = "";
            nmrcStok.Value = 0;
            txtAlisFiyat.Text = "";
            txtSatisFiyat.Text = "";
            rchtxtDetay.Text = "";
            
        }
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_URUNLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Ürün Kaydet
            SqlCommand komutkaydet = new SqlCommand("insert into TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY)" +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", txtAd.Text);
            komutkaydet.Parameters.AddWithValue("@p2", txtMarka.Text);
            komutkaydet.Parameters.AddWithValue("@p3", txtModel.Text); 
            komutkaydet.Parameters.AddWithValue("@p4", mskdtxtYil.Text);
            komutkaydet.Parameters.AddWithValue("@p5", int.Parse((nmrcStok.Value).ToString()));
            komutkaydet.Parameters.AddWithValue("@p6", decimal.Parse((txtAlisFiyat.Text).ToString()));
            komutkaydet.Parameters.AddWithValue("@p7", decimal.Parse((txtSatisFiyat.Text).ToString()));
            komutkaydet.Parameters.AddWithValue("@p8", rchtxtDetay.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydedilmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Ürün Silme
            SqlCommand komutsil = new SqlCommand("delete TBL_URUNLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Ürün Güncelleme
            SqlCommand komutguncelle = new SqlCommand("update TBL_URUNLER set URUNAD=@p1, MARKA=@p2, MODEL=@p3, YIL=@p4, " +
                "ADET=@p5, ALISFIYAT=@p6, SATISFIYAT=@p7, DETAY=@p8 where ID=@p9", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtMarka.Text);
            komutguncelle.Parameters.AddWithValue("@p3", txtModel.Text);
            komutguncelle.Parameters.AddWithValue("@p4", mskdtxtYil.Text);
            komutguncelle.Parameters.AddWithValue("@p5", int.Parse((nmrcStok.Value).ToString()));
            komutguncelle.Parameters.AddWithValue("@p6", decimal.Parse((txtAlisFiyat.Text).ToString()));
            komutguncelle.Parameters.AddWithValue("@p7", decimal.Parse((txtSatisFiyat.Text).ToString()));
            komutguncelle.Parameters.AddWithValue("@p8", rchtxtDetay.Text);
            komutguncelle.Parameters.AddWithValue("@p9", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Gridden araçlara veri taşıma
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            mskdtxtYil.Text = dr["YIL"].ToString();
            nmrcStok.Value = int.Parse(dr["ADET"].ToString());
            txtAlisFiyat.Text = dr["ALISFIYAT"].ToString();
            txtSatisFiyat.Text = dr["SATISFIYAT"].ToString();
            rchtxtDetay.Text = dr["DETAY"].ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
