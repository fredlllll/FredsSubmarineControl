using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FredsSubmarineControlServer
{
    public class ControlClient
    {
        private readonly ControlServer server;
        private readonly TcpClient tcpClient;

        public ControlClient(ControlServer server, TcpClient tcpClient)
        {
            this.server = server;
            this.tcpClient = tcpClient;
        }
    }
}
