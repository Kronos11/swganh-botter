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
        LoggedIn,
        CharacterSelect,
        Zoning,
        InGame
    }
    public class BotClient : IDisposable
    {
        public BotClient(int localPort)
        {
            Session = new Session(localPort);
        }
        public BotClient()
        {
            Session = new Session(44499);
        }

        public string Name { get; set; }

        public float Speed { get; set; }

        public Vec3 Position { get; set; }

        public Quaternion Orientation { get; set; }

        public Session Session { get; set; }

        public void Dispose()
        {
            Session.Disconnect();
        }
    }
}
