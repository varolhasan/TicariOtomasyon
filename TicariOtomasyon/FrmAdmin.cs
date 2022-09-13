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
    public partial class FrmAdmin : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            btnGirisYap.BackColor = Color.LightSteelBlue;
        }
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komutgiris = new SqlCommand("select * from TBL_ADMIN where KULLANICIAD=@p1 and KULLANICISIFRE=@p2", bgl.baglanti());
            komutgiris.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
            komutgiris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komutgiris.ExecuteReader();
            if (dr.Read())
            {
                Form1 fr1 = new Form1();
                fr1.kullanici = txtKullaniciAdi.Text;
                fr1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
