﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
namespace PresidentsServer
{
    class Program
    {
        public const int numplayers = 2;

        private Socket m_soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream
           , ProtocolType.Tcp);

        public static Socket[] clients = new Socket[numplayers];

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
        
        //Either cards (what's left in the deck to be drawn) 
        //or an error code (indicating an invalid move)
        static int SendResponse(int clientnum, byte[] move)
        {
            int byteCount = 0;

            byte[] Messagesize = BitConverter.GetBytes(move.Length);

            try
            {
                clients[clientnum].Send(Messagesize, 0, Messagesize.Length, SocketFlags.None);

                int size = BitConverter.ToInt32(Messagesize, 0);

                do
                {
                    byteCount += clients[clientnum].Send(move, byteCount, (size-byteCount), SocketFlags.None);
                } while (byteCount < size);
            }

            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                return (e.ErrorCode);
            }

            return 0;
        }

        //A list of positions which correspond to cards in their deck
        static int RecieveMove(int clientnum, ref byte[] movebuff)
        {
            int byteCount = 0;

            //Populate a byte array in this function which can be processed in the loop below
            byte[] Messagesize = new byte[sizeof(int)];
            byte[] message = new byte[4096];

            try
            {
                clients[clientnum].ReceiveTimeout = 10000;
                clients[clientnum].Receive(Messagesize, 0, Messagesize.Length, SocketFlags.None);

                int size = BitConverter.ToInt32(Messagesize, 0);

                if (size == 0)
                {
                    Byte[] resp = new byte[2 * sizeof(char)];
                    System.Buffer.BlockCopy("E5".ToCharArray(), 0, resp, 0, resp.Length);
                    SendResponse(clientnum, resp);
                }


                do
                {
                    byteCount += clients[clientnum].Receive(message, byteCount, (size-byteCount), SocketFlags.None);
                } while (byteCount < size);
            }

            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                return (e.ErrorCode);
            }

            movebuff = message;
            return 0;
        }

        static string[] ProcessMessage(byte[] message)
        {
            string result = System.Text.Encoding.UTF8.GetString(message).Substring(0, message.Length);
            Char delimiter = ',';
            result = result.Replace("\0", string.Empty);
            String[] messages = result.Split(delimiter);

            return messages;
        }

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
           int turn, turn2;

           Game G = new Game();

           byte[] p1cards = G.Buildmessagecards(0);
           byte[] p2cards = G.Buildmessagecards(1);

           SendResponse(0, p1cards);
           SendResponse(1, p2cards);
            
           //First thing to do is find out who has the ace of diamonds
           if (Game.AIdeck.FindCard(1, Suite.diamonds))
           {
               turn = 0;
               turn2 = 1;
           }
           else
           {
               turn = 1;
               turn2 = 0;
           }
           
           byte[] turni = BitConverter.GetBytes('T');
           byte[] nturni = BitConverter.GetBytes('N');
           SendResponse(turn, turni);
           SendResponse(turn2, nturni);

           do
           {
               byte[] movebuff = new byte[4096];
               RecieveMove(turn, ref movebuff);

               char[] chars = new char[movebuff.Length / sizeof(char)];
               System.Buffer.BlockCopy(movebuff, 0, chars, 0, movebuff.Length);
               string charst = new string(chars);
               charst = charst.Replace("\0", string.Empty);

               if (charst == "Pass")
               {
                   if (turn == 1)
                       turn = 0;
                   else
                       turn = 1;


                   G.ResetLP();
                   continue;
               }

               string[] cards = ProcessMessage(movebuff);

               int ec = G.VerifyHand(cards, turn);

               if ( ec == 0)
               {
                   G.PlayCards();

                   byte[] lp = G.BuildmessageLP();
                   byte[] p1 = G.Buildmessagecards(0);
                   byte[] p2 = G.Buildmessagecards(1);

                   SendResponse(0, lp);
                   SendResponse(0, p1);
                   SendResponse(1, lp);
                   SendResponse(1, p2); 
               }
               else
               {
                   char echar = Convert.ToChar(ec);
                   string msg = "E" + echar;
                   Byte[] resp = new byte[2 * sizeof(char)];
                   System.Buffer.BlockCopy(msg.ToCharArray(), 0, resp, 0, resp.Length);
                   SendResponse(turn, resp);
               }

               winindicator = G.Checkwin();

               if (!winindicator && ec == 0)
               {
                   if (turn == 1)
                       turn = 0;
                   else
                       turn = 1;
               }

           } while (winindicator != true);

           string winmsg = "WIN";
           byte[] winresp = new byte[winmsg.Length * sizeof(char)];
           System.Buffer.BlockCopy(winmsg.ToCharArray(), 0, winresp, 0, winmsg.Length);
           SendResponse(turn, winresp);

            for (int i = 0; i < clients.Length; i++)
            {
                clients[i].Close();
            }
		} 
        }
    }
