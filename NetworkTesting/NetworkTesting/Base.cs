using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lidgren.Network;

namespace NetworkTesting
{
    public partial class Base : Form
    {
        public Base()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            this.Hide();
            Client client = new Client();
            client.Show();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Server server = new Server();
            server.Show();
        }
    }
}
