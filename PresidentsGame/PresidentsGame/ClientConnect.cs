using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Sockets;
using System.Net;


namespace PresidentsGame
{
    public class ClientConnect
    {
        Socket clientSocket; 

        public ClientConnect()
        {
            try {
              
              clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
              clientSocket.Connect(IPAddress.Loopback, 7007);
             }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
            }
        }

        public int mysend(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);
            byte[] recvbytes = new byte[1024];
            try
            {
                clientSocket.Send(msg, 0, msg.Length,SocketFlags.None);

                // Read the response bytes.
                int bytesReceived = 0;
                byte[] incomingMessageBuffer = new byte[4096];

                bytesReceived = clientSocket.Receive(incomingMessageBuffer, 0, incomingMessageBuffer.Length, SocketFlags.None);
            }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                return (e.ErrorCode);
            }
            return 0;
        }
    }
}
