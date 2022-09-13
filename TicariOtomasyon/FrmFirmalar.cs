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
    public partial class FrmFirmalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSektor.Text = "";
            txtYetkili.Text = "";
            txtYgorev.Text = "";
            mskdYetkiliTC.Text = "";
            mskdTel1.Text = "";
            mskdTel2.Text = "";
            mskdTel3.Text = "";
            mskdTelFax.Text = "";
            txtMail.Text = "";
            cmbxIl.Text = "";
            cmbxIlce.Text = "";
            txtVergiDairesi.Text = "";
            rchtxAdres.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
        }
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            kod1listele();
            if (rchKod1.Text != null)
            {
                txtKod1.Text = "Var";
            }

            //Şehirleri Combobox'a Getirme
            SqlCommand komut1 = new SqlCommand("select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                cmbxIl.Properties.Items.Add(dr1[0]);
            }
            bgl.baglanti().Close();

        }
        private void kod1listele()
        {
            SqlCommand komut1 = new SqlCommand("select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                rchKod1.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSektor.Text = dr["SEKTOR"].ToString();
                txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                txtYgorev.Text = dr["YETKILISTATU"].ToString();
                mskdYetkiliTC.Text = dr["YETKILITC"].ToString();
                mskdTel1.Text = dr["TELEFON1"].ToString();
                mskdTel2.Text = dr["TELEFON2"].ToString();
                mskdTel3.Text = dr["TELEFON3"].ToString();
                mskdTelFax.Text = dr["FAX"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbxIl.Text = dr["IL"].ToString();
                cmbxIlce.Text = dr["ILCE"].ToString();
                txtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
                rchtxAdres.Text = dr["ADRES"].ToString();
                txtKod1.Text = dr["OZELKOD1"].ToString();
                txtKod2.Text = dr["OZELKOD2"].ToString();
                txtKod3.Text = dr["OZELKOD3"].ToString();
            }

        }

        private void cmbxIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Şehir seçildiğinde bir önceki şehrin ilçelerini sıfırlamak için
            cmbxIlce.Properties.Items.Clear();


            //İllere Göre İlçe Getirme
            SqlCommand komut2 = new SqlCommand("select ILCE from TBL_ILCELER where SEHIR=(select ID from TBL_ILLER where SEHIR=@p1)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", cmbxIl.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbxIlce.Properties.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Firma Ekleme
            SqlCommand komutekle = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1," +
                "TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values " +
                "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komutekle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtYgorev.Text);
            komutekle.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komutekle.Parameters.AddWithValue("@p4", mskdYetkiliTC.Text);
            komutekle.Parameters.AddWithValue("@p5", txtSektor.Text);
            komutekle.Parameters.AddWithValue("@p6", mskdTel1.Text);
            komutekle.Parameters.AddWithValue("@p7", mskdTel2.Text);
            komutekle.Parameters.AddWithValue("@p8", mskdTel3.Text);
            komutekle.Parameters.AddWithValue("@p9", txtMail.Text);
            komutekle.Parameters.AddWithValue("@p10", mskdTelFax.Text);
            komutekle.Parameters.AddWithValue("@p11", cmbxIl.Text);
            komutekle.Parameters.AddWithValue("@p12", cmbxIlce.Text);
            komutekle.Parameters.AddWithValue("@p13", txtVergiDairesi.Text);
            komutekle.Parameters.AddWithValue("@p14", rchtxAdres.Text);
            komutekle.Parameters.AddWithValue("@p15", txtKod1.Text);
            komutekle.Parameters.AddWithValue("@p16", txtKod2.Text);
            komutekle.Parameters.AddWithValue("@p17", txtKod3.Text);
            komutekle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Firma Eklenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Firma Silme
            SqlCommand komutsil = new SqlCommand("delete TBL_FIRMALAR where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Firma Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Firmaları Güncelleme
            SqlCommand komutguncelle = new SqlCommand("update TBL_FIRMALAR set AD=@p1, YETKILISTATU=@p2, YETKILIADSOYAD=@p3, " +
                "YETKILITC=@p4, SEKTOR=@p5, TELEFON1=@p6, TELEFON2=@p7, TELEFON3=@p8, MAIL=@p9, IL=@p10, ILCE=@p11, VERGIDAIRE=@p12, " +
                "ADRES=@p13, OZELKOD1=@p14, OZELKOD2=@p15, OZELKOD3=@p16, FAX=@p17 where ID=@p18", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtYgorev.Text);
            komutguncelle.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komutguncelle.Parameters.AddWithValue("@p4", mskdYetkiliTC.Text);
            komutguncelle.Parameters.AddWithValue("@p5", txtSektor.Text);
            komutguncelle.Parameters.AddWithValue("@p6", mskdTel1.Text);
            komutguncelle.Parameters.AddWithValue("@p7", mskdTel2.Text);
            komutguncelle.Parameters.AddWithValue("@p8", mskdTel3.Text);
            komutguncelle.Parameters.AddWithValue("@p9", txtMail.Text);
            komutguncelle.Parameters.AddWithValue("@p10", cmbxIl.Text);
            komutguncelle.Parameters.AddWithValue("@p11", cmbxIlce.Text);
            komutguncelle.Parameters.AddWithValue("@p12", txtVergiDairesi.Text);
            komutguncelle.Parameters.AddWithValue("@p13", rchtxAdres.Text);
            komutguncelle.Parameters.AddWithValue("@p14", txtKod1.Text);
            komutguncelle.Parameters.AddWithValue("@p15", txtKod2.Text);
            komutguncelle.Parameters.AddWithValue("@p16", txtKod3.Text);
            komutguncelle.Parameters.AddWithValue("@p17", mskdTelFax.Text);
            komutguncelle.Parameters.AddWithValue("@p18", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Firmal Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
