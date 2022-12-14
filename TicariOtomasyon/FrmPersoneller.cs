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
    public partial class FrmPersoneller : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        private void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_PERSONELLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskdtxtTel1.Text = "";
            mskdtxtTC.Text = "";
            txtMail.Text = "";
            cmbxIl.Text = "";
            cmbxIlce.Text = "";
            txtGorev.Text = "";
            rchtxAdres.Text = "";
        }
        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            listele();
            //İller Getirme
            SqlCommand komut1 = new SqlCommand("select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                cmbxIl.Properties.Items.Add(dr1[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //GridControl1'den verileri araçlara taşıma
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["AD"].ToString();
            txtSoyad.Text = dr["SOYAD"].ToString();
            mskdtxtTel1.Text = dr["TELEFON"].ToString();
            mskdtxtTC.Text = dr["TC"].ToString();
            txtMail.Text = dr["MAIL"].ToString();
            cmbxIl.Text = dr["IL"].ToString();
            cmbxIlce.Text = dr["ILCE"].ToString();
            rchtxAdres.Text = dr["ADRES"].ToString();
            txtGorev.Text = dr["GOREV"].ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Personel Ekleme
            SqlCommand komutekle = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komutekle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komutekle.Parameters.AddWithValue("@p3", mskdtxtTel1.Text);
            komutekle.Parameters.AddWithValue("@p4", mskdtxtTC.Text);
            komutekle.Parameters.AddWithValue("@p5", txtMail.Text);
            komutekle.Parameters.AddWithValue("@p6", cmbxIl.Text);
            komutekle.Parameters.AddWithValue("@p7", cmbxIlce.Text);
            komutekle.Parameters.AddWithValue("@p8", rchtxAdres.Text);
            komutekle.Parameters.AddWithValue("@p9", txtGorev.Text);
            komutekle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel Eklenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Personel Silme
            SqlCommand komutsil = new SqlCommand("delete TBL_PERSONELLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("update TBL_PERSONELLER set AD=@p1, SOYAD=@p2, TELEFON=@p3, TC=@p4, " +
                "MAIL=@p5, IL=@p6, ILCE=@p7, ADRES=@p8, GOREV=@p9 where ID=@p10", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@p3", mskdtxtTel1.Text);
            komutguncelle.Parameters.AddWithValue("@p4", mskdtxtTC.Text);
            komutguncelle.Parameters.AddWithValue("@p5", txtMail.Text);
            komutguncelle.Parameters.AddWithValue("@p6", cmbxIl.Text);
            komutguncelle.Parameters.AddWithValue("@p7", cmbxIlce.Text);
            komutguncelle.Parameters.AddWithValue("@p8", rchtxAdres.Text);
            komutguncelle.Parameters.AddWithValue("@p9", txtGorev.Text);
            komutguncelle.Parameters.AddWithValue("@p10", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void cmbxIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Her il seçiminden önece ilçelerin sıfırlanması için
            cmbxIlce.Properties.Items.Clear();

            //İllere Göre İlçeleri Listeleme
            SqlCommand komut2 = new SqlCommand("select ILCE from TBL_ILCELER where SEHIR=(select ID from TBL_ILLER where SEHIR=@p1)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", cmbxIl.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbxIlce.Properties.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }
    }
}
