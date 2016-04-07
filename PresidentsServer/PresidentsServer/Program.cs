using System;
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
    //
    //Creates the console app (server), connects clients, handles game logic
    //
    class Program
    {
        public const int numplayers = 2;

        //Configuration of the server
        private Socket m_soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream
           , ProtocolType.Tcp);

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
        static int SendResponse(int clientnum, byte[] move)
        
        NAME
            SendResponse - Send a message to a connected client.
        
        SYNOPSIS
            static int SendResponse(int clientnum, byte[] move)
         
            int clientnum - The position in the array of clients the move should be sent to
            move - The message to be sent.
         
         DESCRIPTION
            Sends a message to a client containing either cards (what's left in the deck to be drawn) 
            or an error code (indicating an invalid move).         
         
         RETURNS
            int - 0 sucessful return
         
         AUTHOR
            David Wilson
         */
        /**/
        static int SendResponse(Socket[] clients, int clientnum, byte[] move)
        {
            int byteCount = 0;

            byte[] Messagesize = BitConverter.GetBytes(move.Length);

            int opp = (clientnum == 0) ? 1 : 0;

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

                if (e.ErrorCode == 10054)
                {
                    Byte[] resp = GetBytes("E6");
                    if (!(clients[opp].Poll(1, SelectMode.SelectRead) && clients[opp].Available == 0))
                      SendResponse(clients, opp, resp);
                    Environment.Exit(0);
                }
            }

            return 0;
        }

        /**/
        /*
        static int RecieveMove(int clientnum, ref byte[] movebuff)
        
        NAME
            RecieveMove - Receive a message from a connected client.
        
        SYNOPSIS
            static int RecieveMove(int clientnum, ref byte[] movebuff)
         
            int clientnum - The position in the array of clients the move should be received from
            movebuff - byte array in which the move will be stored.
         
         DESCRIPTION
            Receives a message from a client containing indexes into their array of cards,
            indicating which cards are part of the move they will play        
         
         RETURNS
            int - 0 sucessful return
         
         AUTHOR
            David Wilson
         */
        /**/
        static int RecieveMove(Socket[] clients, int clientnum, ref byte[] movebuff)
        {
            int byteCount = 0;

            //Populate a byte array in this function which can be processed in the loop below
            byte[] Messagesize = new byte[sizeof(int)];
            byte[] message = new byte[4096];

            int opp = (clientnum == 0) ? 1 : 0;

            try
            {
                //Set time out
                clients[clientnum].ReceiveTimeout = 30000;
                clients[clientnum].Receive(Messagesize, 0, Messagesize.Length, SocketFlags.None);

                int size = BitConverter.ToInt32(Messagesize, 0);

                do
                {
                    byteCount += clients[clientnum].Receive(message, byteCount, (size-byteCount), SocketFlags.None);
                } while (byteCount < size);
            }

            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);

                if (e.ErrorCode == 10060)
                {
                    Byte[] resp = GetBytes("E5");
                    SendResponse(clients, clientnum, resp);
                    return 1;
                }

                if (e.ErrorCode == 10054)
                {
                    Byte[] resp = GetBytes("E6");
                    if (!(clients[opp].Poll(1, SelectMode.SelectRead) && clients[opp].Available == 0))
                        SendResponse(clients, opp, resp);
                    Environment.Exit(0);
                }
            }

            movebuff = message;
            return 0;
        }

        /**/
        /*
        static string[] ProcessMessage(byte[] message)
        
        NAME
            static string[] ProcessMessage(byte[] message) - Divide a message up into its parts (coma-seperated cards to be played)
        
        SYNOPSIS
            static string[] ProcessMessage(byte[] message)
         
            message - 
         
         DESCRIPTION
            Receives a message from a client containing indexes into their array of cards,
            indicating which cards are part of the move they will play and divides it into individual cards.       
         
         RETURNS
            string[] - the message divided into an array of strings
         
         AUTHOR
            David Wilson
         */
        /**/
        static string[] ProcessMessage(byte[] message)
        {
            string result = GetString(message);
            result = result.Substring(0, result.Length - 1);
            Char delimiter = ',';
            String[] messages = result.Split(delimiter);

            return messages;
        }

        static int HandleBeginning(Socket[] clients, ref Game G)
        {
            int turn, turn2;

            //Deal out cards initially and send them to players
            byte[] p1cards = G.Buildmessagecards(0, G.AIdeck);
            byte[] p2cards = G.Buildmessagecards(1, G.pdeck);

            SendResponse(clients, 0, p1cards);
            SendResponse(clients, 1, p2cards);

            //First thing to do is find out who has the ace of diamonds
            if (G.AIdeck.FindCard(1, Suite.diamonds))
                turn = 0;
            else
                turn = 1;

            turn2 = (turn == 0) ? 1 : 0;

            byte[] turni = BitConverter.GetBytes('T');
            byte[] nturni = BitConverter.GetBytes('N');
            SendResponse(clients, turn, turni);
            SendResponse(clients, turn2, nturni);

            return turn;
        }

        static bool HandleEnd(Socket[] clients, int turn, ref byte[] movebuff)
        {
            string winmsg = "WIN";
            byte[] winresp = GetBytes(winmsg);
            SendResponse(clients, turn, winresp);
            RecieveMove(clients, turn, ref movebuff);

            string winnerresponse = GetString(movebuff);

            turn = (turn == 0) ? 1 : 0;
            string lossmsg = "LOS";
            byte[] lossresp = GetBytes(lossmsg);
            SendResponse(clients, turn, lossresp);

            Array.Clear(movebuff, 0, movebuff.Length);
            RecieveMove(clients, turn, ref movebuff);
            string loserresponse = GetString(movebuff);

            if (winnerresponse == "RP" && loserresponse == "RP")
            {
                byte[] playagin = GetBytes("PA");
                SendResponse(clients, 0, playagin);
                SendResponse(clients, 1, playagin);
                return true;
            }

            return false;
        }

        /**/
        /*
        static void Main(string[] args)
        
        NAME
            Main - Entry point for the application handles the main task of playing the game according
            to the functions defined in the Game class.
        
        SYNOPSIS
            static void Main(string[] args)
         
            args - command line arguments
         
         DESCRIPTION
            static void Main(string[] args) - The entry point for the server console app
            1. Starts up server and waits for clients to connect
            2. Facilitates game by continuing to send and recieve messages (cards, errors, etc).
         
         RETURNS
           N/A
         
         AUTHOR
            David Wilson
         */
        /**/
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
                    //Create a socket using port 7007
                    soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    soc.Bind(new IPEndPoint(IPAddress.Any, 7007));
                    soc.Listen(CONNECTION_BACKLOG);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Server failed to initialized.  Exception: " + e.Message);
                }

                byte[] incomingMessageBuffer = new byte[BUFFER_SIZE];	// Receive buffer

                bool response;

                do
                {

                 //An Array used to keep all the connections to the server (clients) organized
                Socket[] clients = new Socket[numplayers];

                // Wait for a client connection
                for (int i = 0; i < clients.Length; i++)
                {
                    try
                    {
                        clients[i] = soc.Accept();

                        // Display the client endpoint
                        Console.WriteLine("Server: Received connection request from client {0} at " + clients[i].RemoteEndPoint, i);
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
                
                Game G = new Game();

                int turn = HandleBeginning(clients, ref G);

                byte[] movebuff = new byte[4096];
                
                //Turn taking loop
               while (true) {
                    Array.Clear(movebuff, 0, movebuff.Length);

                    int rret = RecieveMove(clients, turn, ref movebuff);

                    if (rret == 1) continue;

                    string charst = GetString(movebuff);

                    if (charst == "Pass")
                    {
                        SendResponse(clients, turn, BitConverter.GetBytes('N'));

                        turn = (turn == 0) ? 1 : 0;
                        SendResponse(clients, turn, BitConverter.GetBytes('T'));

                        G.ResetLP();

                        byte[] p1 = G.Buildmessage(0, G.AIdeck);
                        byte[] p2 = G.Buildmessage(1, G.pdeck);

                        SendResponse(clients, 0, p1);
                        SendResponse(clients, 1, p2);

                        continue;
                    }

                    string[] cards = ProcessMessage(movebuff);

                    int ec = G.VerifyHand(cards, turn);

                    if (ec == 0)
                    {
                        G.PlayCards();

                        if ((winindicator = G.Checkwin()) == true)
                            break;

                        byte[] p1 = G.Buildmessage(0,G.AIdeck);
                        byte[] p2 = G.Buildmessage(1,G.pdeck);

                        SendResponse(clients, 0, p1);
                        SendResponse(clients, 1, p2);

                    }
                    else
                    {
                        G.ResetTD();
                        string msg = "E" + ec.ToString();
                        Byte[] resp = GetBytes(msg);
                        SendResponse(clients, turn, resp);
                        continue;
                    }

                    turn = (turn == 0) ? 1 : 0;
               };

               response = HandleEnd(clients, turn, ref movebuff);
     
            }
            while (response);
		} 
        }
    }
