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
            Carddraw = new List <DrawInfo>();
            lhdeck = new Deck();
            CardImages = new Dictionary<string, Bitmap>();
            ErrorCodes = new Dictionary<int, string>()
            {
                {0,"No cards played."},
                {1, "Incorrect number of cards played."},
                {2, "Invalid hand. Is your hand a 'poker' hand?"},
                {3, "Your hand is not better than your opponents."},
                {4, "Sorry your time expired so you automatically pass."}
            };

            this.DoubleBuffered = true;

            PopulateCardVP();
            mainf();
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

        //Format the Graphics object correctly
        public static void DrawPlayerCards(Deck pdeck)
        {
            //Draw the player's cards on the screen
            int xco = 43;
            int yco = 229;


            DrawInfo cardpos = new DrawInfo();

           for (int i = 0; i < pdeck.size(); i++)
            {

                if (i == 13) yco = 347;
                if (xco > 967) xco = 43;

                cardpos.x = xco;
                cardpos.y = yco;
                cardpos.img = pdeck.GetCard(i).img;

                Carddraw.Add(cardpos);

                xco += 77;
            }

            ActiveForm.Invalidate();
        }

        public static void DrawPlayedCards(Deck lhdeck)
        {
            //Draw the played cards on the screen
            int xco = 31;
            int yco = 35;


            DrawInfo cardpos = new DrawInfo();

            for (int i = 0; i < lhdeck.size(); i++)
            {
                cardpos.x = xco;
                cardpos.y = yco;
                cardpos.img = lhdeck.GetCard(i).img;

                Carddraw.Add(cardpos);

                xco += 77;
            }

            ActiveForm.Invalidate();
        }

        static void Shuffle(Deck a, Deck AI, Deck Player)
        {
           Random x = new Random();
           int turn = 0; 

         while(a.size() > 0) {

               int cardvalue = x.Next(1, 14);

               if (turn == 0)
               {
                   //AI
                   if ( a.DealCard(cardvalue, AI) )
                   {
                       turn = 1;
                   }
               }
               else
               {
                   //Player
                   if ( a.DealCard(cardvalue, Player) )
                   {
                       turn = 0;
                   }
               }
           }
        }

        //static void mainf()
        public void mainf()
        {
            //Create a deck of of all the cards and one for the player and the AI
            Deck a = new Deck(); 
            Deck AIdeck = new Deck();
            pdeck = new Deck();

            //Note: 1 - Ace, Royals are 11,12,13
            //Populates a deck with one card of each suite
            for (int i = 1; i <= 13; i++)
            {
                a.SetCard(i, Suite.diamonds);
                a.SetCard(i, Suite.clubs);
                a.SetCard(i, Suite.hearts);
                a.SetCard(i, Suite.spades);
            }

            //Distribute the cards evenly across the player and AI Decks (If 1v1 should be 26 each)
            Shuffle(a, AIdeck, pdeck);
            
            //Put the players cards in order (not totally necessary but makes the game easier to play(
            pdeck.OrderCards();

            //Draw the players' cards to the screen (would want to set up some kind of do-while (pdeck > 0 || AIdeck.size > 0) )
            DrawPlayerCards(pdeck);
        }

        //0-3 Value for the Suites
        public enum Suite
        {
            diamonds,
            clubs,
            hearts,
            spades
        };

        public struct Card {

            public int CV { get; set; }
            public Suite SV { get; set; }
            public Bitmap img { get; set; }
            public string name { get; set; }
        }

    public class Deck : Object
    {
        public Deck()
        {
            deck = new List<Card>();
        }

        public void SetCard( int i, Suite s)
        {
           
            Card cCard = new Card();
            cCard.CV = i;
            cCard.SV = s;

            string cname = cCard.SV.ToString().Substring(0, 1) + cCard.CV.ToString();

            try
            {
                object O = PresidentsGame.Properties.Resources.ResourceManager.GetObject(cname);
                cCard.img = new Bitmap((Image)O);
            }
            catch
            {
                Console.WriteLine("ERROR: Bad param - " + cname); 
            }

            this.deck.Add(cCard);
        }

        public void RemoveCard(int value, Suite s)
        {
            Predicate<Card> T = (Card x) => { return (x.CV == value) && (x.SV == s); };

            //If a card of this type exists we get the first one
            this.deck.Remove(this.deck.Find(T));
        }

        public Card GetCard(int index)
        {
                Card nullc = new Card();
                
                if (index > this.size()) return nullc;

                Card[] cardarray = deck.ToArray();

                return cardarray[index];
        }

        public bool DealCard(int i, Deck b)
        {
            Card cCard = new Card();        
            Predicate<Card> T = (Card x) => { return x.CV == i; };

            //If a card of this type exists we get the first one
           cCard = this.deck.Find( T );

            //If we actually found a card with the random number put it in either the player or AI decks
            if ( !cCard.CV.Equals(0))
            {
                b.SetCard(cCard.CV, cCard.SV);
                this.deck.Remove(cCard);
                return true;
            }

            return false;
        }

        public void OrderCards()
        {
            deck.Sort(CompareCards);
        }

        private static int CompareCards(Card c1, Card c2)
        {
            if (c1.CV > c2.CV || (c1.CV == c2.CV && c1.SV > c2.SV) )
            {
                return 1;
            }
            else
                return -1;
                
        }

   
        public int size()
        {
            return deck.Count();
        }

        List<Card> deck;
    }
  
        private void SoloForm_Load(object sender, EventArgs ev)
        {
        }

        private void Deal_Click(object sender, EventArgs e)
        {
            mainf();
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
                e.Graphics.DrawImage(cardpos.img, cardpos.x, cardpos.y);
            }
        }

        //Global objects
        public static List<DrawInfo> Carddraw;
        public static Deck lhdeck;
        public static Deck pdeck;
        public static Dictionary<string, Bitmap> CardImages;
        public static Dictionary<int,string> ErrorCodes;

        public struct DrawInfo
        {
            public int x { get; set; }
            public int y { get; set; }
            public Bitmap img { get; set; }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Deck tmph = new Deck(); //tmph - temporary hand since we must verify the move

            //If the card is selected put it in the deck (find out which are selected by looking at
            //the panels. Find out which card is on the panel by looking at Carddraw.)
            if (C1.active == true) Settmp(tmph, 0);
            if (C2.active == true) Settmp(tmph, 1);
            if (C3.active == true) Settmp(tmph, 2);
            if (C4.active == true) Settmp(tmph, 3);
            if (C5.active == true) Settmp(tmph, 4);
            if (C6.active == true) Settmp(tmph, 5);
            if (C7.active == true) Settmp(tmph, 6);
            if (C8.active == true) Settmp(tmph, 7);
            if (C9.active == true) Settmp(tmph, 8);
            if (C10.active == true) Settmp(tmph, 9);
            if (C11.active == true) Settmp(tmph, 10);
            if (C12.active == true) Settmp(tmph, 11);
            if (C13.active == true) Settmp(tmph, 12);
            if (C14.active == true) Settmp(tmph, 13);
            if (C15.active == true) Settmp(tmph, 14);
            if (C16.active == true) Settmp(tmph, 15);
            if (C17.active == true) Settmp(tmph, 16);
            if (C18.active == true) Settmp(tmph, 17);
            if (C19.active == true) Settmp(tmph, 18);
            if (C20.active == true) Settmp(tmph, 19);
            if (C21.active == true) Settmp(tmph, 20);
            if (C22.active == true) Settmp(tmph, 21);
            if (C23.active == true) Settmp(tmph, 22);
            if (C24.active == true) Settmp(tmph, 23);
            if (C25.active == true) Settmp(tmph, 24);
            if (C26.active == true) Settmp(tmph, 25);

            if (VerifyHand(tmph)) 
            {
                //Needed to clear out the previously played hand
                lhdeck = new Deck();

                for (int i = 0; i < tmph.size(); i++)
                {
                    lhdeck.SetCard(tmph.GetCard(i).CV, tmph.GetCard(i).SV);
                }

                DrawPlayedCards(lhdeck);

                PrevH.Visible = true;

                //Get rid of played cards from player hand
                for (int i = 0; i < lhdeck.size(); i++)
                {
                    pdeck.RemoveCard(lhdeck.GetCard(i).CV, lhdeck.GetCard(i).SV);
                }

                DrawPlayerCards(pdeck);

               
            }
            
            Clear();
        }

        public static bool VerifyHand(Deck Temp)
        {
            if (Temp.size() > 5 )
            {
                MessageBox.Show("INVALID MOVE: Five cards maximum per hand");
                return false;
            }

            if ( lhdeck.size()!= 0 && Temp.size() != lhdeck.size() )
            {
                MessageBox.Show("INVALID MOVE: Previous hand had " + lhdeck.size() + "cards");
                return false;
            }

            return true;
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

        public static void Settmp(Deck temp, int index)
        {
            temp.SetCard(pdeck.GetCard(index).CV, pdeck.GetCard(index).SV);
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

    }
}
