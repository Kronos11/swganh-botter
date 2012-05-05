using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWGANH.Core
{
    public class Core
    {
        public Dictionary<int, BotClient> BotClients { get; set; }

        public int CurrentPort { get; set; }

        public EndPoint ConnectionServerEndpoint { get; set; }
        public EndPoint LoginEndpoint { get; set; }

        public Core()
        {
            BotClients = new Dictionary<int, BotClient>();
            CurrentPort = 44499;
            ConnectionServerEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 44463);
            LoginEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 44453);
        }

        public BotClient AddBot()
        {
            BotClient client = new BotClient(CurrentPort);
            BotClients.Add(CurrentPort, client);
            CurrentPort++;

            return client;
        }

        public void StartLogin(BotClient client)
        {
            // Connect to current server
            client.Session.Connect(ConnectionServerEndpoint);
            byte[] loginData = new byte[1024];
            client.Session.SendAsync(loginData);
        }

        //public async void Run()
        //{
        //    var t = await Task.Factory.StartNew(() =>
        //    {
        //        foreach (var client in BotClients)
        //        {
        //            client.Value.Session.c
        //        }
        //    });
        //}

    }
}
