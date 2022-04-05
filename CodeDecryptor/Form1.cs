using SharedClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeDecryptor
{
    public partial class Form1 : Form
    {
        string key = "b14ca5898a4e4133bbce2ea2315a1916";
        public Form1()
        {
            InitializeComponent();
        }

        string generateRandomCode()
        {
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("D6");
            return s;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtRandomCode.Text = generateRandomCode();
            txtEncrypt.Text = txtRandomCode.Text;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var str = CodeHandler.DecryptString(key, txtDecrypt.Text);
            txtDecryptResult.Text = str;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txtEncrypt.Text = CodeHandler.EncryptString(key, txtRandomCode.Text);
        }
    }
}
