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
    public partial class FrmNotlar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmNotlar()
        {
            InitializeComponent();
        }
        private void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_NOTLAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void temizle()
        {
            txtBaslik.Focus();
            txtId.Text = "";
            txtBaslik.Text = "";
            txtHitap.Text = "";
            txtOlusturan.Text = "";
            mskdSaat.Text = "";
            mskdTarih.Text = "";
            rchDetay.Text = "";
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Not Kaydetme
            SqlCommand komutkaydet = new SqlCommand("insert into TBL_NOTLAR (NOTTARIH,NOTSAAT,NOTBASLIK,NOTDETAY,NOTOLUSTURAN,NOTHITAP) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", mskdTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2", mskdSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komutkaydet.Parameters.AddWithValue("@p4", rchDetay.Text);
            komutkaydet.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komutkaydet.Parameters.AddWithValue("@p6", txtHitap.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Not Oluşturuldu.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //GridControl'den Araçlara Veri Çekmek
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["NOTID"].ToString();
            mskdTarih.Text = dr["NOTTARIH"].ToString();
            mskdSaat.Text = dr["NOTSAAT"].ToString();
            txtBaslik.Text = dr["NOTBASLIK"].ToString();
            rchDetay.Text = dr["NOTDETAY"].ToString();
            txtOlusturan.Text = dr["NOTOLUSTURAN"].ToString();
            txtHitap.Text = dr["NOTHITAP"].ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Not Silme
            SqlCommand komutsil = new SqlCommand("delete TBL_NOTLAR where NOTID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Not Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Not Güncelleme
            SqlCommand komutguncelle = new SqlCommand("update TBL_NOTLAR set NOTTARIH=@p1,NOTSAAT=@p2,NOTBASLIK=@p3,NOTDETAY=@p4," +
                "NOTOLUSTURAN=@p5,NOTHITAP=@p6 where NOTID=@p7", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", mskdTarih.Text);
            komutguncelle.Parameters.AddWithValue("@p2", mskdSaat.Text);
            komutguncelle.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komutguncelle.Parameters.AddWithValue("@p4", rchDetay.Text);
            komutguncelle.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komutguncelle.Parameters.AddWithValue("@p6", txtHitap.Text);
            komutguncelle.Parameters.AddWithValue("@p7", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti();

            MessageBox.Show("Not Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            fr.notdetay = dr["NOTDETAY"].ToString();
            fr.Show();
        }
    }
}
