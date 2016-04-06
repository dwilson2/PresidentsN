using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Sockets;
using System.Net;

//
// Class to precipitate network connection for clients
//
namespace PresidentsGame
{


    public class ClientConnect
    {
        Socket clientSocket;

        /**/
        /*
        static byte[] GetBytes(string str)
        
        NAME
            GetBytes - converts a string into an array of bytes.
        
        SYNOPSIS
            static byte[] GetBytes(string str)
         
            str     --> the string to be converted.
         
         DESCRIPTION
            Because sockets send information back and forth as arrays of bytes,
            throughout the program it is necessary to convert back and forth between strings, arrays of bytes,
            and arrays of strings. This function will be used by both the clients and server to convert a string into bytes.
         
         RETURNS
            An array of bytes holding the equivalent data that was in string str.
         
         AUTHOR
            David Wilson
         */
        /**/
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /**/
        /*
        static string GetString(byte[] bytes)
        
        NAME
            GetBytes - converts a string into an array of bytes.
        
        SYNOPSIS
           static string GetString(byte[] bytes)
         
            bytes     --> An array of bytes to be converted.
         
         DESCRIPTION
            Because sockets send information back and forth as arrays of bytes,
            throughout the program it is necessary to convert back and forth between strings, arrays of bytes,
            and arrays of strings. This function will be used by both the clients and server to convert an array of bytes into a single string.
         
         RETURNS
            A string that holds the equivalent data that was in the bytes array.
         
         AUTHOR
            David Wilson
         */
        /**/
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            
            string charst = new string(chars);
            charst = charst.Replace("\0", string.Empty);

            return charst;
        }

        /**/
        /*
        public ClientConnect() - constructor
        
        NAME
            ClientConnect - Enables online connectivity.
        
        SYNOPSIS
         
         DESCRIPTION
            Connects this program up to a server running on the local machine (loopback address)
            on port 7007 uses TCP/IP sockets.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        public int RecvResponse(ref byte[] response)
        
        NAME
            RecvResponse - Receives a message from the server.
        
        SYNOPSIS
            public int RecvResponse(ref byte[] response)
         
            response - populated as a result of this function call, passed in as an empty array of bytes
         
         DESCRIPTION
            Receives an entire message from the server (as a byte array) and stores the message in its response argument.
         
         RETURNS
            int - 0 - sucessful return
         
         AUTHOR
            David Wilson
         */
        /**/
        public int RecvResponse(ref byte[] response)
        {
            byte[] sizebuffer = new byte[(sizeof(int))];
            byte[] msg = new byte[8192];

            try
            {
                //Small enough that it can all be recieved in a single shot
                clientSocket.Receive(sizebuffer, sizeof(int), SocketFlags.None);

                int bytesSent = 0;

                int size = BitConverter.ToInt32(sizebuffer, 0);

                do
                {
                    bytesSent += clientSocket.Receive(msg, bytesSent, (size-bytesSent), SocketFlags.None);
                } while (bytesSent < size);

            }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);

                if (e.ErrorCode == 10054)
                    return -1;
            }

            response = msg;
            return 0;
        }

        /**/
        /*
        public int SendCards(string message)
        
        NAME
            SendCards - Sends a message to the server informing the server which cards have been played
        
        SYNOPSIS
            public int SendCards(string message)
         
            message - A string (comma-seperated) indicating which card indexes (positions in player deck) were selected by the user to "play."   
         
         DESCRIPTION
            Allows the user to tell the server which cards he wants to play.
         
         RETURNS
            int - 0 - sucessful return
         
         AUTHOR
            David Wilson
         */
        /**/
        public int SendCards(string message)
        {
            byte[] msg = GetBytes(message);
            byte[] sizebuffer = BitConverter.GetBytes(msg.Length);

            try
            {
                //Small enough that it can all be sent in a single shot
                int sendlen = clientSocket.Send(sizebuffer,sizeof(int),SocketFlags.None);

                if (sendlen == 0)
                    return -1;

                int bytesSent = 0;

                do {
                bytesSent += clientSocket.Send(msg, bytesSent, (msg.Length-bytesSent), SocketFlags.None);

                } while (bytesSent < msg.Length);

            }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);

                if (e.ErrorCode == 10054)
                    return -1;
            }

            return 0;
        }
    }
}
