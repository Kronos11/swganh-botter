using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SWGANH.Core
{
    public enum BotStatus
    {
        Disconnected,
        Connecting,
        Connected,
        Disconnecting
    }

    public class BotClient
    {
        public BotClient()
        {
            Session = new Session();
        }
        public string Name { get; set; }

        public Vec3 Position { get; set; }

        public Quaternion Orientation { get; set; }

        public Session Session { get; set; }

        public BotStatus ConnectionStatus { get; set; }

        public void Connect(EndPoint remoteEndpoint)
        {
            Session.NetworkEndpoint = remoteEndpoint;
            if (Session.Connect())
                ConnectionStatus = BotStatus.Connecting;
            else
                ConnectionStatus = BotStatus.Disconnected;
        }
    }
}
