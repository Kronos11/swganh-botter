using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SWGANH.Core
{
    public enum ConnectionStatus
    {
        Disconnected,
        Connecting,
        Connected,
        Disconnecting
    }

    public class Session
    {
        public int LocalPort { get; set; }

        public ConnectionStatus SessionStatus { get; set; }

        public byte[] LastMessage { get; set; }

        private UdpClient udpClient;

        public Session(int localPort)
        {
            SessionStatus = ConnectionStatus.Connecting;
            // LocalPort to listen to
            udpClient = new UdpClient(localPort);
            LocalPort = localPort;
        }
        ~Session()
        {
            SessionStatus = ConnectionStatus.Disconnecting;
            Disconnect();
            SessionStatus = ConnectionStatus.Disconnected;
        }
        public void Connect(EndPoint serverEndPoint)
        {
            udpClient.Connect((IPEndPoint)serverEndPoint);
            SessionStatus = ConnectionStatus.Connected;
        }
        public void Disconnect()
        {
            udpClient.Close();
        }
        public async void SendAsync(byte[] data)
        {
            await udpClient.SendAsync(data, data.Length);
        }

        public async void ReceiveAsync()
        {
            UdpReceiveResult result = await udpClient.ReceiveAsync();
            LastMessage = result.Buffer;
            Console.WriteLine("Data Received: {0}", Encoding.ASCII.GetString(result.Buffer));
            // Do Something with the data here

            ReceiveAsync();
        }

    }
}
