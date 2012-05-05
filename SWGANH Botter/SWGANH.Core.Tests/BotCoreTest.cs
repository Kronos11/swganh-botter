using System;
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
        }
    }
}
