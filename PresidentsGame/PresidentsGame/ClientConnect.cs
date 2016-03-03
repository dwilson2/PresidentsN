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

        public int RecvResponse(byte[] response)
        {
            byte[] sizebuffer = new byte[(sizeof(int))];
            byte[] msg = new byte[4096];

            try
            {
                //Small enough that it can all be recieved in a single shot
                clientSocket.Receive(sizebuffer, sizeof(int), SocketFlags.None);

                int bytesSent = 0;

                int size = BitConverter.ToInt32(sizebuffer, 0);

                do
                {
                    bytesSent += clientSocket.Receive(msg, 0, size, SocketFlags.None);
                } while (bytesSent < size);

            }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                return (e.ErrorCode);
            }

            response = msg;
            return 0;
        }

        //message will hold a list of card (positions)
        public int SendCards(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);
            byte[] sizebuffer = BitConverter.GetBytes(message.Length);

            try
            {
                //Small enough that it can all be sent in a single shot
                clientSocket.Send(sizebuffer,sizeof(int),SocketFlags.None);

                int bytesSent = 0;

                do {
                bytesSent += clientSocket.Send(msg, 0, msg.Length,SocketFlags.None);

                } while (bytesSent < msg.Length);

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
