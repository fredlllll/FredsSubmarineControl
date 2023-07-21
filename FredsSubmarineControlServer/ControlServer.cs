using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FredsSubmarineControlServer
{
    public class ControlServer
    {
        readonly TcpListener listener;

        readonly List<ControlClient> clients = new List<ControlClient>();

        public ControlServer()
        {
            listener = new TcpListener(System.Net.IPAddress.Any, 10000);
            listener.Start();

            listener.BeginAcceptTcpClient(OnNewTcpClient, null);
        }

        public void RemoveClient(ControlClient client)
        {
            lock (clients)
            {
                clients.Remove(client);
            }
        }

        void OnNewTcpClient(IAsyncResult asyncResult)
        {
            var client = listener.EndAcceptTcpClient(asyncResult);
            var controlClient = new ControlClient(this, client);

            lock (clients)
            {
                clients.Add(controlClient);
            }

            listener.BeginAcceptTcpClient(OnNewTcpClient, null);
        }
    }
}
