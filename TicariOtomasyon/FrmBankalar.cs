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
    public partial class FrmBankalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmBankalar()
        {
            InitializeComponent();
        }
        private void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void temizle()
        {
            txtId.Text = "";
            txtBankaAdi.Text = "";
            cmbxIl.Text = "";
            cmbxIlce.Text = "";
            txtSube.Text = "";
            txtIban.Text = "";
            txtHesapNo.Text = "";
            txtYetkili.Text = "";
            mskdTel.Text = "";
            mskdTarih.Text = "";
            txtHesapTuru.Text = "";
            txtHesapNo.Text = "";
            cmbFirma.Text = "";
            txtId.Focus();
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
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

            //Firmaları Getirme
            SqlCommand komut2 = new SqlCommand("select AD from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbFirma.Properties.Items.Add(dr2[0].ToString());
            }
        }

        private void cmbxIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seçilen İle Göre İlçe getirme
            cmbxIlce.Properties.Items.Clear();
            cmbxIlce.Text = "";

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
            SqlCommand komutekle = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKLI,TELEFON,TARIH," +
                "HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komutekle.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            komutekle.Parameters.AddWithValue("@p2", cmbxIl.Text);
            komutekle.Parameters.AddWithValue("@p3", cmbxIlce.Text);
            komutekle.Parameters.AddWithValue("@p4", txtSube.Text);
            komutekle.Parameters.AddWithValue("@p5", txtIban.Text);
            komutekle.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komutekle.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komutekle.Parameters.AddWithValue("@p8", mskdTel.Text);
            komutekle.Parameters.AddWithValue("@p9", mskdTarih.Text);
            komutekle.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komutekle.Parameters.AddWithValue("@p11", cmbFirma.SelectedIndex+1);
            komutekle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Firma Eklenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //GridContol'den araçlara veri taşıma
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["ID"].ToString();
            txtBankaAdi.Text = dr["BANKAADI"].ToString();
            cmbxIl.Text = dr["IL"].ToString();
            cmbxIlce.Text = dr["ILCE"].ToString();
            txtSube.Text = dr["SUBE"].ToString();
            txtIban.Text = dr["IBAN"].ToString();
            txtHesapNo.Text = dr["HESAPNO"].ToString();
            txtYetkili.Text = dr["YETKLI"].ToString();
            mskdTel.Text = dr["TELEFON"].ToString();
            mskdTarih.Text = dr["TARIH"].ToString();
            txtHesapTuru.Text = dr["HESAPTURU"].ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Banka Silme İşlemi
            SqlCommand komutsil = new SqlCommand("delete TBL_BANKALAR where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Banka Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Banka Güncelleme İşlemi
            SqlCommand komutguncelle = new SqlCommand("update TBL_BANKALAR set BANKAADI=@p1, IL=@p2, ILCE=@p3, SUBE=@p4, IBAN=@p5, " +
                "HESAPNO=@p6, YETKLI=@p7, TELEFON=@p8, TARIH=@p9, HESAPTURU=@p10, FIRMAID=(select ID from TBL_FIRMALAR where " +
                "AD=@p11) where ID=@p12", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            komutguncelle.Parameters.AddWithValue("@p2", cmbxIl.Text);
            komutguncelle.Parameters.AddWithValue("@p3", cmbxIlce.Text);
            komutguncelle.Parameters.AddWithValue("@p4", txtSube.Text);
            komutguncelle.Parameters.AddWithValue("@p5", txtIban.Text);
            komutguncelle.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komutguncelle.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komutguncelle.Parameters.AddWithValue("@p8", mskdTel.Text);
            komutguncelle.Parameters.AddWithValue("@p9", mskdTarih.Text);
            komutguncelle.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komutguncelle.Parameters.AddWithValue("@p11", cmbFirma.Text);
            komutguncelle.Parameters.AddWithValue("@p12", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Banka Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
