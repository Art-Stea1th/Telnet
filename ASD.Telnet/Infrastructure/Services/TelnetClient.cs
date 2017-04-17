using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Telnet.Infrastructure.Services {

    internal sealed class TelnetClient {



        TcpClient client = null;

        public bool IsConnected => client.Connected;




    }
}