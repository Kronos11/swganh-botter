using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SWGANH.Core
{
    public class Session
    {
        public int LocalPort { get; set; }

        public EndPoint NetworkEndpoint { get; set; }

        private Socket socket;

        public Session()
        {
            this.NetworkEndpoint = new IPEndPoint(0,0);
        }
        public bool Connect()
        {
            socket = new Socket(SocketType.Raw, ProtocolType.Udp);

            IAsyncResult result = socket.BeginConnect(NetworkEndpoint, null, null);
            if (!socket.Connected)
            {
                socket.Close();
                throw new ApplicationException(string.Format("Failed to connect to {0}", NetworkEndpoint.ToString())); 
            }
            return true;
        }
    }
}
