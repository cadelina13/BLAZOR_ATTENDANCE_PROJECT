using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace CodeDecryptor
{
    public partial class frmMain : Form
    {
        private static readonly string apiKey = "1vI6Ri0Ek3oAriAOlmKakibB9Lb";
        private static readonly string apiSecret = "LQpj7KEpoyaUgHjqw8xuZ43sgxAF3y14Kuh9nF0t";
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Getbalance();
        }
        async void Getbalance()
        {
            btnRefresh.Enabled = false;
            btnRefresh.Text = "Please wait...";
            HttpClient http = new HttpClient();
            string data = $"api_key={apiKey}&api_secret={apiSecret}";
            var reqMessage = new HttpRequestMessage();
            reqMessage.RequestUri = new Uri("https://api.movider.co/v1/balance");
            reqMessage.Method = HttpMethod.Post;
            reqMessage.Headers.Add("Accept", "application/json");
            reqMessage.Content = new StringContent(data, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            var postReq = await http.SendAsync(reqMessage);

            var strjson = await postReq.Content.ReadAsStringAsync();
            var a = System.Text.Json.JsonSerializer.Deserialize<BalanceViewModel>(strjson);
            var remaining = a.amount / 0.006;
            txtBalance.Text = $"{a.amount} {a.type}";
            txtRemainingSMS.Text = $"{string.Format("{0:n0}", remaining)} SMS";
            btnRefresh.Enabled = true;
            btnRefresh.Text = "REFRESH";
        }

        class BalanceViewModel
        {
            public string type { get; set; }
            public double amount { get; set; }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Getbalance();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
