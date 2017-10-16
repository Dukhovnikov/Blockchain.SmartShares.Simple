using System.Net;
using System.Net.PeerToPeer;
using System.ServiceModel;
using System.Configuration;

namespace p2p.implementation
{
    public class PeerEntry
    {
        public PeerName PeerName { get; set; }
        public IP2PService ServiceProxy { get; set; }
        public string DisplayString { get; set; }
        public bool ButtonsEnabled { get; set; }

    }
}