using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace PresidentsServer
{
    class Program
    {
        public const int numplayers = 2;

        private Socket m_soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream
           , ProtocolType.Tcp);
        


        static void Main(string[] args)
        {
           	//
			// Create a Sockets based echo server.
			//
            const int CONNECTION_BACKLOG = 5;
            const int BUFFER_SIZE = 4096;

            Socket soc = null;
            try 
            {
                soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                soc.Bind(new IPEndPoint(IPAddress.Any, 7007));
                soc.Listen(CONNECTION_BACKLOG);
            } 
            catch ( Exception e ) 
            {
                Console.WriteLine( "Server failed to initialized.  Exception: " + e.Message );
            }
            byte[] incomingMessageBuffer = new byte[BUFFER_SIZE];	// Receive buffer
            int bytesReceived = 0;                      // Received byte count


                // Start loop to listen for clients
                Socket[] clients = new Socket[numplayers];

                // Wait for a client connection
                for (int i = 0; i < clients.Length; i++)
                {
                    try
                    {
                        clients[i] = soc.Accept();

                        // Display the client endpoint
                        Console.WriteLine("Server: Received connection request from client {0} at " + clients[i].RemoteEndPoint,i);
                    }
                    catch (Exception ex)
                    {
                        // Handle errors (will happen after Dispose is called)
                        Console.WriteLine("Server Error (will be called after Dispose): " + ex.Message);

                        if (clients[i] != null && clients[i].Connected)
                        {
                            // Close the client
                            clients[i].Close();
                        }
                    }
                }

           bool winindicator = false;
           int turn = 0;

           do
           {
               int byteCount = 0;

                //Received data, send the data back to the client
              bytesReceived = clients[turn].Receive(incomingMessageBuffer, 0, incomingMessageBuffer.Length, SocketFlags.None);
              clients[turn].Send(incomingMessageBuffer, 0, bytesReceived, SocketFlags.None);
              byteCount += bytesReceived;

               string result = System.Text.Encoding.UTF8.GetString(incomingMessageBuffer).Substring(0,bytesReceived);
               Char delimiter = ',';
               result = result.Replace(" ",string.Empty);
               String[] messages = result.Split(delimiter);
               foreach (var substring in messages)
               {
                   Console.WriteLine(substring);
               }

               Console.WriteLine("Server: Successfully recieved {0} bytes from client.", byteCount);

               if (turn == 1) turn = 0;
               else
                   turn = 1;

           } while (winindicator != true);

            for (int i = 1; i < clients.Length; i++)
            {
                clients[i].Close();
            }
		} 
        }
    }
