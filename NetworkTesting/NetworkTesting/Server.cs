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
    public partial class Server : Form
    {
        NetServer server;

        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Testing");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.Port = 50001;

            server =  new NetServer(config);
            server.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Program.program.Show();
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            while (true)
            {
                //use local message variable
                NetIncomingMessage msgIn;
                //standard receive loop - loops through all received messages, until none is left
                while ((msgIn = server.ReadMessage()) != null)
                {
                    //create message type handling with a switch
                    switch (msgIn.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            //This type handles all data that have been send by you.
                            break;
                        //All other types are for library related events (some examples)
                        case NetIncomingMessageType.DiscoveryRequest:
                            NetOutgoingMessage message = server.CreateMessage();
                            message.Write("Yo!");
                            server.SendDiscoveryResponse(message, msgIn.SenderEndPoint);
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            msgIn.SenderConnection.Approve();
                            break;
                    }
                    //Recycle the message to create less garbage
                    server.Recycle(msgIn);
                }
            }
        }
    }
}
