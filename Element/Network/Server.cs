using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Element.Network {

    /// <summary>
    /// Example of how we can make this work with an async socket server
    /// https://msdn.microsoft.com/en-us/library/bew39x2a(v=vs.110).aspx 
    /// 
    /// </summary>
    public class Server {

        // TODO: Change from being hard coded
        public static int MAX_CLIENTS_CHANGE_FROM_BEING_HARDCODED = 4;

        public string Ip { get; set; }

        public int Port { get; set; }

        public SocketPermission SocketPermission { get; set; }

        public Socket Socket { get; set; }

        public IPEndPoint IpEndPoint { get; set; }

        public ServerStatus ConnectionStatus => Socket != null && Socket.Connected ? ServerStatus.Connected : ServerStatus.Disconnected;

        public Server(string ip, int port, SocketPermission socketPermission = null) {
            Ip = ip;
            Port = port;

            SocketPermission = socketPermission ??
                new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, Ip, Port);
        }


        public void Connect() {
            try {
                IpEndPoint = new IPEndPoint(IPAddress.Parse(Ip), Port);

                SocketPermission.Demand();

                Socket = new Socket(IpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Socket.Bind(IpEndPoint);
            } catch (Exception e) {
                Debug.WriteLine("Error while creating socket: " + e.StackTrace);
            }
        }

        /// <summary>
        /// Listens for any incomming requests to the socket server
        /// </summary>
        public void Listen() {
            if (ConnectionStatus == ServerStatus.Disconnected) {
                Connect();
            }
            Socket.Listen(MAX_CLIENTS_CHANGE_FROM_BEING_HARDCODED);

            Socket.BeginAccept(AcceptIncommingRequests, Socket);
        }

        public bool CloseConnection() {
            try {
                if (ConnectionStatus != ServerStatus.Connected) {
                    return true;
                }

                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
                return true;
            } catch (Exception e) {
                Debug.WriteLine("Error while closing socket connection: " + e.StackTrace);
            }
            return false;
        }

        public void AcceptIncommingRequests(IAsyncResult result) {
            
        }
    }
}
