﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
namespace TicariOtomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMailAdresi.Text = mail;
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajım = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("varoool4@outlook.com", "kartopu12875163");
            istemci.Port = 587;
            istemci.Host = "smtp.live.com";
            istemci.EnableSsl = true;
            mesajım.To.Add(txtMailAdresi.Text);
            mesajım.From = new MailAddress("varoool4@outlook.com");
            mesajım.Subject = txtKonu.Text;
            mesajım.Body = rchMesaj.Text;
            istemci.Send(mesajım);
        }
    }
}
