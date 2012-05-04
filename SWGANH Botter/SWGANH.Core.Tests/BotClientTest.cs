using System;
using System.Net;
using System.Net.Sockets;
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
            BotClient client = new BotClient() { Name = "Bot 1" };
            Assert.AreEqual<string>("Bot 1", client.Name);
        }
        [TestMethod]
        public void BotClientHasPositionAndOrientation()
        {
            BotClient client = new BotClient() { 
                Name = "Bot 1" ,
                Position = new Vec3(0.0, 0.0, 0.0) ,
                Orientation = new Quaternion(0.0, 0.0, 0.0, 0.0) 
            };
            Assert.AreEqual(0.0, client.Position.x);
        }
        [TestMethod]
        public void BotClientCanStoreSession()
        {
            BotClient client = new BotClient();
            client.Session = new Session() { 
                LocalPort = 123456,
                NetworkEndpoint = new IPEndPoint(new IPAddress(12700001),44453)
            };
            Assert.AreEqual<int>(123456, client.Session.LocalPort);
        }
        [TestMethod]
        public void BotClientStatusIsDisconnectedByDefault()
        {
            BotClient client = new BotClient();
            Assert.AreEqual<BotStatus>(BotStatus.Disconnected, client.ConnectionStatus);
        }
        [TestMethod]
        public void CanAttemptToConnectToRemotePort()
        {
            BotClient client = new BotClient();
            client.Connect(new IPEndPoint(new IPAddress(12700001), 44453));
            Assert.AreEqual(BotStatus.Connecting, client.ConnectionStatus);
            
        }
    }
}
