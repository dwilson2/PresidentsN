using System;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Threading;

//
// Class to update player form and allow player to play moves (validated by server)
//
namespace PresidentsGame
{
    public partial class ClientForm : Form
    {
        //Global objects
        public List<DrawInfo> Carddraw;
        public static Dictionary<string, Bitmap> CardImages;
        public static Dictionary<int, string> ErrorCodes;
        public static Dictionary<string, string> EndCodes;
        public static ClientConnect connection;
        public static byte[] cards;
        public List<DrawInfo> pcards;

        /**/
        /*
        public ClientForm()
        
        NAME
            public ClientForm() - constructor for clientform
        
        SYNOPSIS
            public ClientForm()
                  
         DESCRIPTION
            Creates instances of necessary objects (globals) and populates them with data. 
            Creates a seperate thread to constantly interact with the server. (Ensures GUI is always responsive).
         
         RETURNS
            A ClientForm object
         
         AUTHOR
            David Wilson
         */
        /**/
        public ClientForm()
        {
            InitializeComponent();
            CardImages = new Dictionary<string, Bitmap>();
            Carddraw = new List<DrawInfo>();
            pcards = new List<DrawInfo>();
            ErrorCodes = new Dictionary<int, string>()
            {
                {1,"No cards played."},
                {2, "Incorrect number of cards played."},
                {3, "Invalid hand. Is your hand a 'poker' hand?"},
                {4, "Your hand is not better than your opponents."},
                {5, "Sorry your time expired so you automatically pass."},
                {6, "Your opponent disconnected so you win!! Goodbye"},
                {7, "Sorry, the server has crashed. Goodbye"}
            };

            EndCodes = new Dictionary<string, string>()
            {
                {"WIN", "Congrats! You won! Would you like to play again?"},
                {"LOS", "Sorry, you lost. Would you like to play again?"}
            };

            this.DoubleBuffered = true;

            PopulateCardVP();
            connection = new ClientConnect();
            cards = new byte[4096];

            Thread mThread = new Thread(new ThreadStart(main));
            mThread.IsBackground = true;
            mThread.Start();
        }

        /**/
        /*
         public void main()        
         
        NAME
            public void main() - 
        
        SYNOPSIS
             public void main() - This is the function that allows the game to be played utilizes multiple threads to ensure
             Main thread does the GUI work and other thread performs all other processing.
                  
         DESCRIPTION
            1. Receive an initial message which contains the cards a player begins the game with
            2. Receive the message that informs the player whether or not he has the first turn
            3. Continue to listen for messages in order to play the game
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void main()
        {
            byte[] resp = new byte[4096];

            if (connection.RecvResponse(ref resp) == -1)
            {
                HandleError("E7");
            }

            string result = ClientConnect.GetString(resp);
            result = result.Substring(0, result.Length - 1);

            char delimiter = ',';
            String[] pcards = result.Split(delimiter);

            DrawPlayerCards(pcards);
            String[] blank = { "" };
            Drawc(pcards, blank);

            byte[] turnp = new byte[sizeof(char)];

            if (connection.RecvResponse(ref turnp) == -1)
            {
                HandleError("E7");
            }

            string charst = ClientConnect.GetString(turnp);

            switchplay(charst);

            while (true)
            {
                HandleResponse();
            }
        }

        //delegate for a function with a single string argument (used for switchplay)
        delegate void sDelegate(string t);

        /**/
        /*
         private void switchplay (string t)        
         
        NAME
            private void switchplay (string t) - Activates/ Deactivates play button to begin turn taking
        
        SYNOPSIS
            private void switchplay (string t)
         
            string t - A string indicating whether or not it is a player's turn (T - yes)
      
         DESCRIPTION
            Enables or disables play button to enable turn taking. Uses delegates to ensure only the Main thread
            attempts to make changes to the GUI.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        private void switchplay (string t)
        {
            if (this.InvokeRequired == false)
            {
                if (t != "T")
                {
                    Play.Enabled = false;
                    Pass.Enabled = false;
                }
                else
                {
                    Play.Enabled = true;
                    Pass.Enabled = true;
                }
            }
            else
            {
                sDelegate switchp =
                  new sDelegate(switchplay);
                this.Invoke(switchp, new object[] {t});
            }

        }

        /**/
        /*
         public static void PopulateCardVP()      
         
        NAME
           public static void PopulateCardVP() - Populates the CardImages Dictionary so that given a name, 
           the ClientForm knows what image to draw,
        
        SYNOPSIS
            public static void PopulateCardVP()     
      
         DESCRIPTION
            Iterates through all cards from the Ace of diamonds to the king of spades populating the
            cardImages dictionary with images saved in the forms resources folder.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public static void PopulateCardVP()
        {
            for (int i = 1; i <= 13; i++)
            {
                string dname = "d";
                string cname = "c";
                string hname = "h";
                string sname = "s";

                try
                {
                    object d = PresidentsGame.Properties.Resources.ResourceManager.GetObject(dname+i);
                    Bitmap dimage = new Bitmap((Image)d);
                    CardImages.Add(dname + i, dimage);

                    object c = PresidentsGame.Properties.Resources.ResourceManager.GetObject(cname + i);
                    Bitmap cimage = new Bitmap((Image)c);
                    CardImages.Add(cname + i, cimage);

                    object h = PresidentsGame.Properties.Resources.ResourceManager.GetObject(hname + i);
                    Bitmap himage = new Bitmap((Image)h);
                    CardImages.Add(hname + i, himage);

                    object s = PresidentsGame.Properties.Resources.ResourceManager.GetObject(sname + i);
                    Bitmap simage = new Bitmap((Image)s);
                    CardImages.Add(sname + i, simage);
                }
                catch
                {
                    Console.WriteLine("ERROR: Bad param {0:G}, {0:G}, {0:G}, {0:G}" + dname,cname,hname,sname);
                }
            }
        }

        /**/
        /*
         public void DrawPlayerCards(string[] playercards)      
         
        NAME
           public void DrawPlayerCards(string[] playercards) - Update the form with the player's current cards.
        
        SYNOPSIS
            public void DrawPlayerCards(string[] playercards) 
         
            string[] playercards - An array of strings containing the name of each of the player's cards
         
            Note: Image obtained using name from CardImages dictionary
      
         DESCRIPTION
            Iterates through all player's cards providing the info to the Carddraw object. Call Invalidate to draw at the end.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void DrawPlayerCards(string[] playercards)
        {
            //Draw the player's cards on the screen
         
            DrawInfo cardpos = new DrawInfo();


            Control CC = this.C1;

            for (int i = 0; i < playercards.Length; i++)
            {

                cardpos.x = CC.Location.X;
                cardpos.y = CC.Location.Y;
                cardpos.img = CardImages[ playercards[i]] ;

                Carddraw.Add(cardpos);

                CC = this.GetNextControl(CC, true);
            }

            //this.Invalidate();
        }

        /**/
        /*
         public void DrawPlayedCards(string[] playedcards)     
         
        NAME
           public void DrawPlayedCards(string[] playedcards) - Update the form with the most recently played cards.
        
        SYNOPSIS
            public void DrawPlayedCards(string[] playedcards) 
         
            string[] playedcards - An array of strings containing the name of each of the cards which were just played.
         
            Note: Image obtained using name from CardImages dictionary
      
         DESCRIPTION
            Iterates through all cards just played providing the info to the Carddraw object. Call Invalidate to draw at the end.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void DrawPlayedCards(string[] playedcards)
        {
            DrawInfo cardpos = new DrawInfo();

            Control CC = this.P1;

            for (int i = 0; i < playedcards.Length; i++)
            {
                cardpos.x = CC.Location.X;
                cardpos.y = CC.Location.Y;
                cardpos.img = CardImages[playedcards[i]];

                Carddraw.Add(cardpos);

                CC = this.GetNextControl(CC, true);
            }

           //this.Invalidate();
        }

        private void SoloForm_Load(object sender, EventArgs ev)
        {
        }

        //Eventually if someone has won enable the ability to restart or automatically restart?
        private void Deal_Click(object sender, EventArgs e)
        {
        }

        /**/
        /*
          private void SoloForm_Paint(object sender, PaintEventArgs e)     
         
        NAME
            private void SoloForm_Paint(object sender, PaintEventArgs e) - Redraws the form using the last played and
            player cards.
        
        SYNOPSIS
            private void SoloForm_Paint(object sender, PaintEventArgs e)   
         
            object sender - required arguments for paint function event handler
            PaintEventArgs e - ""
      
         DESCRIPTION
            Draws all the cards populated in the DrawInfo array DrawArray 
            
            Note: Automatically called when using invalidate() function
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/

        delegate void Pdelegate(object sender, PaintEventArgs e);
        private void SoloForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.InvokeRequired == false)
            {
                    DrawInfo cardpos = new DrawInfo();
                    DrawInfo[] DrawArray = (Carddraw.Count > 0) ? Carddraw.ToArray() : pcards.ToArray();

                    System.Console.WriteLine("Paint call " + Carddraw.Count.ToString() + " " + pcards.ToArray().Length);

                    if (Carddraw.Count > 0)
                    {
                        pcards = Carddraw;

                        //Erase the list after the cards have been drawn to the screen as the list will be repopulated before future draws (after each hand)
                        Carddraw.Clear();
                    }

                    for (int i = 0; i < DrawArray.Length; i++)
                    {
                        cardpos = DrawArray[i];
                        //reduce width and height by 5%
                        float rwidth = 0.95F * this.C1.Width;
                        float rheight = 0.98F * this.C1.Height;

                        e.Graphics.DrawImage(cardpos.img, cardpos.x, cardpos.y, rwidth, rheight);
                    }
                }
                else
                {
                    Pdelegate fp =
                      new Pdelegate(SoloForm_Paint);
                    this.Invoke(fp, new object[] { sender, e });
                }
        }

        /**/
        /*
          private void HandleError(String emessage)    
         
        NAME
            private void HandleError(String emessage) - Informs user of error in played cards (invalid move of some sort).
        
        SYNOPSIS
            private void HandleError(String emessage) 
         
            String messages - A messages representing the response from the server (in this case the error code).
      
         DESCRIPTION
            Uses message boxes to tell the user why their move was invalid
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        private void HandleError(String emessage)
        {
            if (this.InvokeRequired == false)
            {
                //Single character ( 0-4 indicating error code)
                string code = emessage.Substring(1, 1);

                string message;

                if (code == "0")
                {
                    message = ErrorCodes[0];
                }
                else if (code == "1")
                {
                    message = ErrorCodes[1];
                }
                else if (code == "2")
                {
                    message = ErrorCodes[2];
                }
                else if (code == "3")
                {
                    message = ErrorCodes[3];
                }
                else if (code == "4")
                {
                    message = ErrorCodes[4];
                }
                else if (code == "5")
                {
                    message = ErrorCodes[5];
                    EventArgs e = new EventArgs();
                    Pass_Click(this, e);
                }
                else if (code == "6")
                {
                    message = ErrorCodes[6];
                    var btn = MessageBox.Show(message, null, MessageBoxButtons.OK);
                    
                    Exit();
                }
                else if (code == "7")
                {
                    message = ErrorCodes[7];
                    var btn = MessageBox.Show(message, null, MessageBoxButtons.OK);
                    
                    Exit();
                }
                else
                {
                    message = "Incorrect error recevied";
                }

                MessageBox.Show(message);
            }
            else
            {
                sDelegate HE =
                  new sDelegate(HandleError);
                this.Invoke(HE, new object[] { emessage });
            }
        }

        /**/
        /*
          private void HandleEnd(String enmessage)  
         
        NAME
           private void HandleEnd(String enmessage) - Allows user to choose if they'd like to play another game.
        
        SYNOPSIS
            private void HandleEnd(String enmessage)
         
            String enmessage - A message representing the response from the server (in this case the error code).
      
         DESCRIPTION
            Uses message boxes to ask user if they'd like to play again
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        private void HandleEnd(String enmessage)
        {
            if (this.InvokeRequired == false)
            {
                if (enmessage == "WIN")
                {
                    enmessage = EndCodes["WIN"];
                }
                else if (enmessage == "LOS")
                {
                    enmessage = EndCodes["LOS"];
                }
                else
                {
                    enmessage = "Incorrect end game message recevied";
                }

                const string caption = "Replay?";
                var result = MessageBox.Show(enmessage, caption, MessageBoxButtons.YesNo);
 
                if (result == DialogResult.No)
                {
                    connection.SendCards("NP");
                    Exit();
                }
                else
                {
                    connection.SendCards("RP");
                  
                    byte[] resp = new byte [4096];
                    connection.RecvResponse(ref resp);
                    string opresult = ClientConnect.GetString(resp);

                    if (opresult == "PA")
                    {
                        this.Hide();
                        EntryScreen E2 = new EntryScreen();
                        E2.Show();
                    }
                    else
                    {
                        Exit();
                    }
                }
            }
            else
            {
                sDelegate HEN =
                  new sDelegate(HandleEnd);
                this.Invoke(HEN, new object[] { enmessage });
            }
        }

        //delegate for a function with a single string argument (used for switchplay)
        delegate void mDelegate(String[] messages, String[] messages1);

        /**/
        /*
        private void Drawc(String[] messages, String[] messages1)         
        
         NAME
            private void Drawc(String[] messages, String[] messages1) - Safely calls the functions to draw cards to the screen
        
        SYNOPSIS
            private void Drawc(String[] messages, String[] messages1)
         
            String[] messages - A list of the cards in the previous player's move
            String[] messages1 - A list of the cards left in a player's hand (comma seperated)
      
         DESCRIPTION
            Ensures the draw functions defined above are called by the Main thread
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        private void Drawc(String[] messages, String[] messages1)
        {
            if (this.InvokeRequired == false)
            {
                DrawPlayerCards(messages);

                if (messages1[0].Length > 0)
                {
                    DrawPlayedCards(messages1[0].Substring(0, messages1[0].Length - 1).Split(','));

                    if (Play.Enabled == false)
                    {
                        Play.Enabled = true;
                        Pass.Enabled = true;
                    }
                    else
                    {
                        Play.Enabled = false;
                        Pass.Enabled = false;
                    }
                }

                Refresh();
            }
            else
            {
                mDelegate Draw =
                  new mDelegate(Drawc);
                this.Invoke(Draw, new object[] { messages, messages1 });
            }
        }

        /*
         private void HandleResponse()        
        
         NAME
            private void HandleResponse()  - Waits for a message from the server and either displays the errors or redraws 
                                             the form to reflect the updated state of the game.
        
        SYNOPSIS
            private void HandleResponse() 
         
         DESCRIPTION
            1. Recieves a message from the server
            2. Determines if a player has won the game
            3. Divides the message into its pieces
            4. Updates the status of the game.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        private void HandleResponse()
        {
            byte[] resp = new byte [8192];

            if (connection.RecvResponse(ref resp) == -1)
            {
                HandleError("E7");
            }

            string result = ClientConnect.GetString(resp);

            if (string.IsNullOrEmpty(result))
                return;

            string r1 = result.Substring(0,1);

            if(r1 == "N" || r1 == "T")
            {
                switchplay(r1);
                return;
            }

            if (r1 == "E")
            {
                HandleError(result);
                return;
            }

            string winresult = ClientConnect.GetString(resp).Substring(0, 3);
            
            if (winresult == "WIN" || winresult == "LOS")
            {
                HandleEnd(winresult);
                return;
            }

            //The first message contains either an error or The last played cards
            char[] delimiter1 = { '|', '|', '+' };
            String[] messages1 = result.Split(delimiter1);

            Char delimiter = ',';
            String[] messages = result.Substring(messages1[0].Length + 3, (result.Length - (messages1[0].Length + 4)) ).Split(delimiter);
            
            Drawc(messages, messages1);
        }

        //Data needed to draw images 
        public struct DrawInfo
        {
            public int x { get; set; }
            public int y { get; set; }
            public Bitmap img { get; set; }
        }

        /**/
        /*
         private void Play_Click(object sender, EventArgs e)       
        
         NAME
            private void Play_Click(object sender, EventArgs e) - Checks which cards the user wants to play and builds up message
                                                                  to send to server.
        
        SYNOPSIS
            private void Play_Click(object sender, EventArgs e)
         
            object sender - required arguments for event handler
            EventArgs e - ""
         
         DESCRIPTION
            Check TransParentMessagePanels add active panels to message (since that indicates card will be played).
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/

        delegate void eDelegate(object sender, EventArgs e);

        private void Play_Click(object sender, EventArgs e)
        {
            //If the card is selected put it in the deck (find out which are selected by looking at
            //the panels. Find out which card is on the panel by looking at Carddraw.)

            if (this.InvokeRequired == false)
            {
                if (C1.active == true) Settmp(0);
                if (C2.active == true) Settmp(1);
                if (C3.active == true) Settmp(2);
                if (C4.active == true) Settmp(3);
                if (C5.active == true) Settmp(4);
                if (C6.active == true) Settmp(5);
                if (C7.active == true) Settmp(6);
                if (C8.active == true) Settmp(7);
                if (C9.active == true) Settmp(8);
                if (C10.active == true) Settmp(9);
                if (C11.active == true) Settmp(0);
                if (C12.active == true) Settmp(11);
                if (C13.active == true) Settmp(12);
                if (C14.active == true) Settmp(13);
                if (C15.active == true) Settmp(14);
                if (C16.active == true) Settmp(15);
                if (C17.active == true) Settmp(16);
                if (C18.active == true) Settmp(17);
                if (C19.active == true) Settmp(18);
                if (C20.active == true) Settmp(19);
                if (C21.active == true) Settmp(20);
                if (C22.active == true) Settmp(21);
                if (C23.active == true) Settmp(22);
                if (C24.active == true) Settmp(23);
                if (C25.active == true) Settmp(24);
                if (C26.active == true) Settmp(25);


                Clear();

                if (connection.SendCards(ClientConnect.GetString(cards)) == -1)
                {
                    HandleError("E7");
                }

                Array.Clear(cards, 0, 4096);
            }
            else
            {
                 eDelegate pclick =
                  new eDelegate(Play_Click);
                this.Invoke(pclick, new object[] {sender, e});
            }
        }

        /**/
        /*
         private void Clear()     
        
         NAME
            private void Clear() - Redraws all messagepanels as not active to deselect card at location.
        
        SYNOPSIS
            private void Clear()
         
         DESCRIPTION
            Redraws each panel that was selected previously.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        delegate void nDelegate();

        private void Clear()
        {
            if (this.InvokeRequired == false)
            {
                if (C1.active == true) C1.Invalidate();
                if (C2.active == true) C2.Invalidate();
                if (C3.active == true) C3.Invalidate();
                if (C4.active == true) C4.Invalidate();
                if (C5.active == true) C5.Invalidate();
                if (C6.active == true) C6.Invalidate();
                if (C7.active == true) C7.Invalidate();
                if (C8.active == true) C8.Invalidate();
                if (C9.active == true) C9.Invalidate();
                if (C10.active == true) C10.Invalidate();
                if (C11.active == true) C11.Invalidate();
                if (C12.active == true) C12.Invalidate();
                if (C13.active == true) C13.Invalidate();
                if (C14.active == true) C14.Invalidate();
                if (C15.active == true) C15.Invalidate();
                if (C16.active == true) C16.Invalidate();
                if (C17.active == true) C17.Invalidate();
                if (C18.active == true) C18.Invalidate();
                if (C19.active == true) C19.Invalidate();
                if (C20.active == true) C20.Invalidate();
                if (C21.active == true) C21.Invalidate();
                if (C22.active == true) C22.Invalidate();
                if (C23.active == true) C23.Invalidate();
                if (C24.active == true) C24.Invalidate();
                if (C25.active == true) C25.Invalidate();
                if (C26.active == true) C26.Invalidate();
            }
            else 
            {
                nDelegate sclear =
                  new nDelegate(Clear);
                this.Invoke(sclear);
            }
        }

        /**/
        /*
         public static void Settmp(int index)    
        
         NAME
            public static void Settmp(int index)
        
        SYNOPSIS
            public static void Settmp(int index)
          
            int index - The index of the selected card to be added to the message.         
         
         DESCRIPTION
            Adds an index to the array which eventually is a message sent to the server.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public static void Settmp(int index)
        {
            string ostring = ClientConnect.GetString(cards);
            ostring = ostring.Replace("\0", string.Empty);
            byte[] ccard = ClientConnect.GetBytes(ostring);

            string str = index.ToString() + ',';
            byte[] strb = ClientConnect.GetBytes(str);
            System.Buffer.BlockCopy(strb, 0, cards, ccard.Length, strb.Length);
        }
        
        //Event handler for each transparent message panel to change color and property to active
        private void C1_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C1.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C1_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C2_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C2.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C2_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C3_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C3.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C3_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C4_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C4.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C4_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C5_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C5.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C5_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C6_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C6.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C6_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C7_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C7.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C7_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C8_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C8.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C8_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C9_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C9.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C9_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C10_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C10.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C10_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C11_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C11.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C11_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C12_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C12.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C12_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C13_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C13.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C13_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C14_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C14.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C14_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C15_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C15.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C15_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C16_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C16.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C16_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C17_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C17.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C17_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C18_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C18.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C18_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C19_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C19.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C19_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C20_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C20.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C20_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C21_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C21.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C21_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void C22_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C22.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C22_Click);
                this.Invoke(cclick, new object[] { sender, e });
            } 
        }

        private void C23_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C23.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C23_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }
        private void C24_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C24.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C24_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }
        private void C25_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C25.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C25_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }
        private void C26_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
                C26.ToggleActive();
            else
            {
                eDelegate cclick = new eDelegate(C26_Click);
                this.Invoke(cclick, new object[] { sender, e });
            }
        }

        private void Pass_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired == false)
            {
                if (connection.SendCards("Pass") == -1)
                {
                    HandleError("E7");
                }

                Clear();
            }
            else
            {
                eDelegate pclick = new eDelegate(Pass_Click);
                this.Invoke(pclick, new object[] { sender, e });
            }
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Exit()
        {
            Application.Exit();
        }
    }
}
