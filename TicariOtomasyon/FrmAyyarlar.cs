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
    public partial class FrmAyyarlar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmAyyarlar()
        {
            InitializeComponent();
        }
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmAyyarlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnIslem_Click(object sender, EventArgs e)
        {
            if (btnIslem.Text == "Kaydet")
            {
                SqlCommand komutekle = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", bgl.baglanti());
                komutekle.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                komutekle.Parameters.AddWithValue("@p2", txtSifre.Text);
                komutekle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni admin sisteme kaydedildi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (btnIslem.Text == "Güncelle")
            {
                SqlCommand komutguncelle = new SqlCommand("update TBL_ADMIN set KULLANICIAD=@p1, KULLANICISIFRE=@p2 where KULLANICIAD=@p3", bgl.baglanti());
                komutguncelle.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                komutguncelle.Parameters.AddWithValue("@p2", txtSifre.Text);
                komutguncelle.Parameters.AddWithValue("@p3", txtKullaniciAdi.Text);
                komutguncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kullanıcı bilgileri güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtKullaniciAdi.Text = dr["KULLANICIAD"].ToString();
            txtSifre.Text = dr["KULLANICISIFRE"].ToString();
        }

        private void txtKullaniciAdi_EditValueChanged(object sender, EventArgs e)
        {

            if (txtKullaniciAdi.Text != "")
            {
                btnIslem.Text = "Güncelle";
                btnIslem.BackColor = Color.LightSeaGreen;
            }
            else
            {
                btnIslem.Text = "Kaydet";
                btnIslem.BackColor = Color.LightGoldenrodYellow;
            }
        }
    }
}
