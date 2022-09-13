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
    public partial class FrmGiderler : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmGiderler()
        {
            InitializeComponent();
        }
        private void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_GIDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void temizle()
        {
            txtId.Text = "";
            cmbxAy.Text = "";
            cmbxYil.Text = "";
            txtElektrik.Text = "";
            txtSu.Text = "";
            txtDogalgaz.Text = "";
            txtInternet.Text = "";
            txtMaaslar.Text = "";
            txtEkstra.Text = "";
            rchNotlar.Text = "";
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            cmbxAy.Properties.Items.Add("Ocak");
            cmbxAy.Properties.Items.Add("Şubat");
            cmbxAy.Properties.Items.Add("Mart");
            cmbxAy.Properties.Items.Add("Nisan");
            cmbxAy.Properties.Items.Add("Mayıs");
            cmbxAy.Properties.Items.Add("Haziran");
            cmbxAy.Properties.Items.Add("Temmuz");
            cmbxAy.Properties.Items.Add("Ağustos");
            cmbxAy.Properties.Items.Add("Eylül");
            cmbxAy.Properties.Items.Add("Ekim");
            cmbxAy.Properties.Items.Add("Kasım");
            cmbxAy.Properties.Items.Add("Aralık");

            cmbxYil.Properties.Items.Add("2022");
            cmbxYil.Properties.Items.Add("2023");
            cmbxYil.Properties.Items.Add("2024");
            cmbxYil.Properties.Items.Add("2025");
            cmbxYil.Properties.Items.Add("2026");

            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["ID"].ToString();
            cmbxAy.Text = dr["AY"].ToString();
            cmbxYil.Text = dr["YIL"].ToString();
            txtElektrik.Text = dr["ELEKTRIK"].ToString();
            txtSu.Text = dr["SU"].ToString();
            txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
            txtInternet.Text = dr["INTERNET"].ToString();
            txtMaaslar.Text = dr["MAASLAR"].ToString();
            txtEkstra.Text = dr["EKSTRA"].ToString();
            rchNotlar.Text = dr["NOTLAR"].ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Gider Ekleme
            SqlCommand komutekle = new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komutekle.Parameters.AddWithValue("@p1", cmbxAy.Text);
            komutekle.Parameters.AddWithValue("@p2", cmbxYil.Text);
            komutekle.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komutekle.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komutekle.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komutekle.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komutekle.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            komutekle.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komutekle.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komutekle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Gider Eklenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Gider Silme
            SqlCommand komutsil = new SqlCommand("delete TBL_GIDERLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Gider Silinmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Gider Güncelleme
            SqlCommand komutguncelle = new SqlCommand("update TBL_GIDERLER set AY=@p1, YIL=@p2, ELEKTRIK=@p3, SU=@p4, DOGALGAZ=@p5, " +
                "INTERNET=@p6, MAASLAR=@p7, EKSTRA=@p8, NOTLAR=@p9 where ID=@p10", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", cmbxAy.Text);
            komutguncelle.Parameters.AddWithValue("@p2", cmbxYil.Text);
            komutguncelle.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komutguncelle.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komutguncelle.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komutguncelle.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komutguncelle.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            komutguncelle.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komutguncelle.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komutguncelle.Parameters.AddWithValue("@p10", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Gider Güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
