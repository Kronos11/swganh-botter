using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWGANH.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanAddBot()
        {
            Core c = new Core();
            BotClient client = c.AddBot();
            
            Assert.IsNotNull(client);
            Assert.AreEqual<int>(1, c.BotClients.Count);
            client.Dispose();
        }
        [TestMethod]
        public void CanAddMultipleBots()
        {
            Core c = new Core();
            BotClient client = c.AddBot();
            BotClient client2 = c.AddBot();

            Assert.AreEqual<int>(2, c.BotClients.Count);
            Assert.AreEqual<int>(44501, c.CurrentPort);
            client.Dispose();
            client2.Dispose();
        }
        [TestMethod]
        public void CanLoadThousandBots()
        {
            UdpClient udpServer = new UdpClient(44453);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 44453);
            Core c = new Core();
            for (int i = 0; i < 1000; i++)
            {
                Task<UdpReceiveResult> result = udpServer.ReceiveAsync();
                BotClient client = c.AddBot();
                client.Session.Connect(endpoint);
                client.Session.SendAsync(Encoding.ASCII.GetBytes("Testing: " + i));

                Assert.AreEqual<string>("Testing: " + i, Encoding.ASCII.GetString(result.Result.Buffer));
            }
            Assert.AreEqual<int>(1000, c.BotClients.Count);
        }
    }
}
