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

namespace PresidentsGame
{

    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            CardImages = new Dictionary<string, Bitmap>();
            Carddraw = new List<DrawInfo>();
            ErrorCodes = new Dictionary<int, string>()
            {
                {1,"No cards played."},
                {2, "Incorrect number of cards played."},
                {3, "Invalid hand. Is your hand a 'poker' hand?"},
                {4, "Your hand is not better than your opponents."},
                {5, "Sorry your time expired so you automatically pass."}
            };

            this.DoubleBuffered = true;

            PopulateCardVP();
            connection = new ClientConnect();
            cards = new byte[4096];

            main();
        }

        public void main()
        {
            byte[] resp = new byte[4096];
            connection.RecvResponse(resp);

            string result = System.Text.Encoding.UTF8.GetString(resp).Substring(0, resp.Length);
            result = result.Replace(" ", string.Empty);

            //The first message contains either an error or The last played cards
            char delimiter = ',';
            String[] pcards = result.Split(delimiter);

            DrawPlayerCards(pcards);

            byte[] turnp = new byte[sizeof(char)];
            connection.RecvResponse(turnp);

            char[] chars = new char[sizeof(char)];
            System.Buffer.BlockCopy(turnp, 0, chars, 0, sizeof(char));
            string charst = new string(chars);

            if (charst != "T")
                Play.Enabled = false;

            while(true) {

                HandleResponse();
            }

        }

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

        //This function will change to instead taking in
        //a vector of strings and using the cardImages dictionary to figure out which images 
        //should be drawn
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

            ActiveForm.Invalidate();
        }

        //If a move is not an error move a player's selected cards into the last played
        //From last played they simply get overridden, after a players turn,
        //draw the cards they have remaining in their hand and wait to recieve messages
        //indicating what their opponent played
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

            ActiveForm.Invalidate();
        }

        private void SoloForm_Load(object sender, EventArgs ev)
        {
        }

        private void Deal_Click(object sender, EventArgs e)
        {
        }
 
        private void SoloForm_Paint(object sender, PaintEventArgs e)
        {
            DrawInfo cardpos = new DrawInfo();
            DrawInfo[] DrawArray = Carddraw.ToArray();
            int count = Carddraw.Count;

            //Erase the list after the cards have been drawn to the screen as the list will be repopulated before future draws (after each hand)
            Carddraw.RemoveRange(0, Carddraw.Count);


            for (int i = 0; i < count; i++)
            {
                cardpos = DrawArray[i];
                //reduce width and height by 5%
                float rwidth = 0.95F * this.C1.Width;
                float rheight = 0.98F * this.C1.Height;

               e.Graphics.DrawImage(cardpos.img, cardpos.x, cardpos.y,rwidth,rheight);
            }
        }

        private void HandleError(String[] messages)
        {
            //Single character ( 0-4 indicating error code)
            string code = messages[0].Substring(1,1);
            
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
            else
            {
                message = "Incorrect error recevied";
            }

            MessageBox.Show(message);
        }

        private void HandleResponse()
        {
            byte[] resp = new byte [4096];
            connection.RecvResponse(resp);

            string winresult = System.Text.Encoding.UTF8.GetString(resp).Substring(0, 3);
            
            if (winresult == "WIN")
            {
                //Display some kind of win form
                //ClientForm C1 = new ClientForm();
                //C1.Show();
                //this.Hide();
                return;
            }

            string result = System.Text.Encoding.UTF8.GetString(resp).Substring(0, resp.Length);
            result = result.Replace(" ", string.Empty);

            //The first message contains either an error or The last played cards
            char[] delimiter1 = { '|', '|', '+' };
            String[] messages1 = result.Split(delimiter1);

            Char delimiter = ',';
            String[] messages = result.Substring(messages1[0].Length,result.Length).Split(delimiter);

            if (messages1[0].Substring(0, 1) == "E" && Play.Enabled == true)
            {
                HandleError(messages1);
            }
            else
            {
                DrawPlayerCards(messages);
                DrawPlayedCards(messages1[0].Split(delimiter));

                if (Play.Enabled == false)
                    Play.Enabled = true;
                else
                    Play.Enabled = false;
            }
        }

        //Global objects
        public static List<DrawInfo> Carddraw;
        public static Dictionary<string, Bitmap> CardImages;
        public static Dictionary<int,string> ErrorCodes;
        public static ClientConnect connection;
        public static byte[] cards;

        public struct DrawInfo
        {
            public int x { get; set; }
            public int y { get; set; }
            public Bitmap img { get; set; }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            //If the card is selected put it in the deck (find out which are selected by looking at
            //the panels. Find out which card is on the panel by looking at Carddraw.)
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

           
            PrevH.Visible = true;
            Clear();

            connection.SendCards(cards.ToString());
            Array.Clear(cards, 0, 4096);
        }

        private void Clear()
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

        public static void Settmp(int index)
        {
            cards.SetValue(BitConverter.GetBytes(index)+",",cards.Length);
        }
 
        private void C1_Click(object sender, EventArgs e)
        {
            C1.Invalidate();
        }

        private void C2_Click(object sender, EventArgs e)
        {
            C2.Invalidate();
        }

        private void C3_Click(object sender, EventArgs e)
        {
            C3.Invalidate();
        }

        private void C4_Click(object sender, EventArgs e)
        {
            C4.Invalidate();
        }

        private void C5_Click(object sender, EventArgs e)
        {
            C5.Invalidate();
        }

        private void C6_Click(object sender, EventArgs e)
        {
            C6.Invalidate();
        }

        private void C7_Click(object sender, EventArgs e)
        {
            C7.Invalidate();
        }

        private void C8_Click(object sender, EventArgs e)
        {
            C8.Invalidate();
        }

        private void C9_Click(object sender, EventArgs e)
        {
            C9.Invalidate();
        }

        private void C10_Click(object sender, EventArgs e)
        {
            C10.Invalidate();
        }

        private void C11_Click(object sender, EventArgs e)
        {
            C11.Invalidate();
        }

        private void C12_Click(object sender, EventArgs e)
        {
            C12.Invalidate();
        }

        private void C13_Click(object sender, EventArgs e)
        {
            C13.Invalidate();
        }

        private void C14_Click(object sender, EventArgs e)
        {
            C14.Invalidate();
        }

        private void C15_Click(object sender, EventArgs e)
        {
            C15.Invalidate();
        }

        private void C16_Click(object sender, EventArgs e)
        {
            C16.Invalidate();
        }

        private void C17_Click(object sender, EventArgs e)
        {
            C17.Invalidate();
        }

        private void C18_Click(object sender, EventArgs e)
        {
            C18.Invalidate();
        }

        private void C19_Click(object sender, EventArgs e)
        {
            C19.Invalidate();
        }

        private void C20_Click(object sender, EventArgs e)
        {
            C20.Invalidate();
        }

        private void C21_Click(object sender, EventArgs e)
        {
            C21.Invalidate();
        }

        private void C22_Click(object sender, EventArgs e)
        {
            C22.Invalidate(); 
        }

        private void C23_Click(object sender, EventArgs e)
        {
            C23.Invalidate();
        }
        private void C24_Click(object sender, EventArgs e)
        {
            C24.Invalidate();
        }
        private void C25_Click(object sender, EventArgs e)
        {
            C25.Invalidate();
        }
        private void C26_Click(object sender, EventArgs e)
        {
            C26.Invalidate();
        }

        private void Pass_Click(object sender, EventArgs e)
        {
            connection.SendCards("Pass");
        }

    }
}
