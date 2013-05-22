using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lidgren.Network;
using System.Net;

namespace NetworkTesting
{
    public partial class Client : Form
    {
        NetClient client;
        IPEndPoint serverIP;
        Int64 seconds;
        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Testing");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            client = new NetClient(config);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Program.program.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstServers.Items.Clear();
            client.DiscoverLocalPeers(50001);
            seconds = DateTime.Now.Second;
            NetIncomingMessage msg;
            while ((seconds + 10) > DateTime.Now.Second)
            {
                while ((msg = client.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.DiscoveryResponse:
                            lstServers.Items.Add(msg.SenderEndPoint);
                            break;
                    }

                }
            }
            if (lstServers.Items.Count == 0)
                lstServers.Items.Add("No Servers Found");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.Connect((IPEndPoint)lstServers.SelectedItem);
        }
    }
}
