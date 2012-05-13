using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWGANH.Core;


namespace SWGANH.Core.Tests
{
    [TestClass]
    public class BotClientTest
    {
        [TestMethod]
        public void BotClientHasName()
        {
            using (BotClient client = new BotClient(){ Name = "Bot 1" })
            {
                Assert.AreEqual<string>("Bot 1", client.Name);

            }
        }
        [TestMethod]
        public void BotClientHasPositionAndOrientation()
        {
            using (BotClient client = new BotClient(){
                        Name = "Bot 1" ,
                        Position = new Vec3(0.0, 0.0, 0.0) ,
                        Orientation = new Quaternion(0.0, 0.0, 0.0, 0.0) })
            {
                Assert.AreEqual(0.0, client.Position.x);   
            };
            

        }
        [TestMethod]
        public void BotClientCanStoreSession()
        {
            using (BotClient client = new BotClient())
            {

                Assert.AreEqual<int>(44499, client.Session.LocalPort);

            }
        }
        [TestMethod]
        public void CanAttemptToConnectToRemotePort()
        {
            using (BotClient client = new BotClient())
            {
                Assert.AreEqual<ConnectionStatus>(ConnectionStatus.Connecting, client.Session.SessionStatus);
            }
        }
        [TestMethod]
        public void CanReceiveMessage()
        {
            UdpClient udpServer = new UdpClient(44453);
            
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 44499);
            using (BotClient client = new BotClient())
            {
                // Connect to the remote server
                // Allows the client to receive data asynchronously
                client.Session.ReceiveAsync();

                byte[] message = Encoding.ASCII.GetBytes("Test Message");
                Task<int> t = udpServer.SendAsync(message, message.Length, endpoint);

                Thread.Sleep(10);
                Assert.AreEqual<string>("Test Message", Encoding.ASCII.GetString(client.Session.LastMessage));

                // close server
            }
            udpServer.Close();
            
        }
        [TestMethod]
        public void CanSendMessageAsync()
        {
            UdpClient udpServer = new UdpClient(44453);
            Task<UdpReceiveResult> result = udpServer.ReceiveAsync();

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 44453);
            using (BotClient client = new BotClient())
            {
                client.Session.Connect(endpoint);
                byte[] message = Encoding.ASCII.GetBytes("Test Message");

                client.Session.SendAsync(message);

                Assert.AreEqual<string>("Test Message", Encoding.ASCII.GetString(result.Result.Buffer));

            }
            udpServer.Close();
        }
    }
}
