using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PathFindGui
{
    public class AsyncClient
    {
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

        public void StartConnecting()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    var serverIP = "85.101.35.13";//"35.204.77.48";
                    attempts++;
                    _clientSocket.Connect(IPAddress.Parse(serverIP), 8953);
                }
                catch (SocketException ex)
                {
                }
            }
        }

        public string SendToServer(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _clientSocket.Send(buffer);

            byte[] receivedBuffer = new byte[1024];
            int rec = _clientSocket.Receive(receivedBuffer);
            byte[] data = new byte[rec];
            Array.Copy(receivedBuffer,data,rec);
            return Encoding.UTF8.GetString(data);
        }
    }
}
