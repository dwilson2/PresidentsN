using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Collections;

namespace PresidentsServer
{
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
            cCard.name = cname;

            this.deck.Add(cCard);
        }

        public void RemoveCard(int value, Suite s)
        {
            Predicate<Card> T = (Card x) => { return (x.CV == value) && (x.SV == s); };

            //If a card of this type exists we get the first one
            this.deck.Remove(this.deck.Find(T));
        }

        //True if the deck has the card, false otherwise
        public bool FindCard(int value, Suite s)
        {
            Predicate<Card> T = (Card x) => { return (x.CV == value) && (x.SV == s); };

            if (this.deck.Find(T).CV != value || this.deck.Find(T).SV != s)
                return false;

            return true;
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

        public void empty()
        {
            deck.RemoveRange(0, size());
        }

        List<Card> deck;
    }

    public class Game
    {
        public enum Hand
        {
            error,
            high_card,
            pair,
            three_kind,
            straight,
            flush,
            full_house,
            four_kind,
            straight_flush
        };

        public int VerifyHand(string[] cards, int turn)
        {
            Deck decktouse = (turn == 0 ? AIdeck : pdeck);
            int retval = 0;

            for (int i = 0; i < cards.Length; i++)
            {
                int cardi = Convert.ToInt32(cards[i],10);
                Card cc = decktouse.GetCard(cardi);
                tdeck.SetCard(cc.CV, cc.SV);
            }

            Hand res = Checkhand();

            if (cards.Length == 0)
                retval = 1;
            else if ((tdeck.size() != lpdeck.size()) && lpdeck.size() > 0)
                retval = 2;
            else if (res == Hand.error)
                retval = 3;
            else if (res < lpdeckh && lpdeck.size() > 0)
                retval = 4;

            if (res == lpdeckh && lpdeck.size() > 0)
            {
                //tiebreaker
                if (!Breaktie(res))
                    retval = 4;
            }

            if (retval == 0)
            {
                for (int i = 0; i < tdeck.size(); i++)
                    decktouse.RemoveCard(tdeck.GetCard(i).CV, tdeck.GetCard(i).SV);
            }


            return retval;
        }

        public bool Breaktie(Hand tdeckh)
        {
            lpdeck.OrderCards();
            tdeck.OrderCards(); 
            
            bool retval = false;
            Card thigh = highcard(tdeck);
            Card lphigh = highcard(lpdeck);

            if (tdeckh == Hand.straight_flush)
            {
                //Played cards are higher values straight flush
                if (tdeck.GetCard(0).SV == lpdeck.GetCard(0).SV &&
                    tdeck.GetCard(0).CV > lpdeck.GetCard(0).CV || tdeck.GetCard(0).SV > lpdeck.GetCard(0).SV)
                        retval = true;
            }
            else if (tdeckh == Hand.four_kind)
            {
                //Played cards are higher values
                if (tdeck.GetCard(0).CV > lpdeck.GetCard(0).CV)
                    retval = true;
            }
            else if (tdeckh == Hand.full_house)
            {
                Deck ntriple = new Deck();
                //0,1,2 are the "triple"
                if (tdeck.GetCard(0).CV == tdeck.GetCard(1).CV && tdeck.GetCard(1).CV == tdeck.GetCard(2).CV)
                {
                    for (int i = 0; i < 3; i++)
                        ntriple.SetCard(tdeck.GetCard(i).CV, tdeck.GetCard(i).SV);
                }
                //2,3,4 are the "triple"
                else 
                {
                    for (int i = 2; i < 5; i++)
                        ntriple.SetCard(tdeck.GetCard(i).CV, tdeck.GetCard(i).SV);
                }

                Deck otriple = new Deck();
                //0,1,2 are the "triple"
                if (lpdeck.GetCard(0).CV == lpdeck.GetCard(1).CV && lpdeck.GetCard(1).CV == lpdeck.GetCard(2).CV)
                {
                    for (int i = 0; i < 3; i++)
                        otriple.SetCard(lpdeck.GetCard(i).CV, lpdeck.GetCard(i).SV);
                }
                //2,3,4 are the "triple"
                else
                {
                    for (int i = 2; i < 5; i++)
                        otriple.SetCard(lpdeck.GetCard(i).CV, lpdeck.GetCard(i).SV);
                }

                if (ntriple.GetCard(0).CV > otriple.GetCard(0).CV)
                    retval = true;
            }
            else if (tdeckh == Hand.flush)
            {
                if (tdeck.GetCard(0).SV > lpdeck.GetCard(0).SV
                    || tdeck.GetCard(0).SV == lpdeck.GetCard(0).SV && 
                       thigh.CV > lphigh.CV)
                    retval = true;
            }
            else if (tdeckh == Hand.straight)
            {
                if (thigh.CV > lphigh.CV
                    || thigh.CV == lphigh.CV &&
                       thigh.SV > lphigh.SV)
                    retval = true;
            }
            else if (tdeckh == Hand.three_kind)
            {
                if (thigh.CV > lphigh.CV)
                    retval = true;
            }
            else
            {
                if (thigh.CV > lphigh.CV || thigh.CV == lphigh.CV && thigh.SV > lphigh.SV)
                    retval = true;
            }

            return retval;
        }

        Card highcard(Deck deck)
        {
            Card hc = deck.GetCard(0);

            for (int i = 1; i < deck.size(); i++)
            {
                if (deck.GetCard(i).CV > hc.CV ||
                    deck.GetCard(i).CV == hc.CV &&
                    deck.GetCard(i).SV > hc.SV)
                        hc = deck.GetCard(i);
            }

            return hc;
        }

        public bool isFlush()
        {
            Card cc = tdeck.GetCard(0);

            for (int i = 1; i < tdeck.size(); i++)
            {
                if (tdeck.GetCard(i).SV != cc.SV)
                    return false;

                cc = tdeck.GetCard(i);
            }

            return true;
        }

        public bool isStraight()
        {
            int val = tdeck.GetCard(0).CV;

            for (int i = 1; i < tdeck.size(); i++)
            {
                if (tdeck.GetCard(i).CV != ++val)
                    return false;
            }

            return true;
        }

        public bool Multiples(Deck deck)
        {
            int val = deck.GetCard(0).CV;

            for (int i = 1; i < deck.size(); i++)
            {
                if (deck.GetCard(i).CV != val)
                    return false;
            }

            return true;
        }

        public bool isFullHouse()
        {
            tdeck.OrderCards();

            Deck tdeck2 = new Deck();
            tdeck2.SetCard(tdeck.GetCard(0).CV, tdeck.GetCard(0).SV);
            tdeck2.SetCard(tdeck.GetCard(1).CV, tdeck.GetCard(0).SV);

            Deck tdeck3 = new Deck();
            tdeck3.SetCard(tdeck.GetCard(2).CV, tdeck.GetCard(0).SV);
            tdeck3.SetCard(tdeck.GetCard(3).CV, tdeck.GetCard(0).SV);
            tdeck3.SetCard(tdeck.GetCard(4).CV, tdeck.GetCard(0).SV);

            Deck tdeck4 = new Deck();
            tdeck4.SetCard(tdeck.GetCard(0).CV, tdeck.GetCard(0).SV);
            tdeck4.SetCard(tdeck.GetCard(1).CV, tdeck.GetCard(0).SV);
            tdeck4.SetCard(tdeck.GetCard(2).CV, tdeck.GetCard(0).SV);

            Deck tdeck5 = new Deck();
            tdeck5.SetCard(tdeck.GetCard(3).CV, tdeck.GetCard(0).SV);
            tdeck5.SetCard(tdeck.GetCard(4).CV, tdeck.GetCard(0).SV);

            if (Multiples(tdeck2) && Multiples(tdeck3) || Multiples(tdeck4) && Multiples(tdeck5))
                return true;

            return false;
        }

        //Add function to determine move type (enum) 
        public Hand Checkhand()
        {
            bool straight, flush, fullhouse;
            Hand result = Hand.error;

            if (tdeck.size() == 5)
            {
                straight = isStraight();
                flush = isFlush();
                fullhouse = isFullHouse();

                if (straight && flush)
                    result = Hand.straight_flush;

                else if (fullhouse)
                    result = Hand.full_house;

                else if (flush)
                    result = Hand.flush;

                else if (straight)
                    result = Hand.straight;
            }
            else if (tdeck.size() == 4)
            {
                if (Multiples(tdeck))
                    result = Hand.four_kind;
            }
            else if (tdeck.size() == 3)
            {
                if (Multiples(tdeck))
                    result = Hand.three_kind;
            }
            else if (tdeck.size() == 2)
            {
                if (Multiples(tdeck))
                    result = Hand.pair;
            }
            else if (tdeck.size() == 1)
                result = Hand.high_card;

            return result;
        }

        public void PlayCards()
        {
            lpdeckh = Checkhand();
            lpdeck.empty();

            for (int i = 0; i < tdeck.size(); i++)
            {
                tdeck.DealCard(i,lpdeck);
            }

            tdeck.empty();
        }

        static Game() 
        {
            //Carddraw = new List<DrawInfo>();
            masterdeck = new Deck();
            AIdeck = new Deck();
            pdeck = new Deck();
            lpdeck = new Deck();
            tdeck = new Deck();

            Initializegame();

        }

        static void Initializegame()
        {
            //Note: 1 - Ace, Royals are 11,12,13
            //Populates a deck with one card of each suite
            for (int i = 1; i <= 13; i++)
            {
                masterdeck.SetCard(i, Suite.diamonds);
                masterdeck.SetCard(i, Suite.clubs);
                masterdeck.SetCard(i, Suite.hearts);
                masterdeck.SetCard(i, Suite.spades);
            }

            //Distribute the cards evenly across the player and AI Decks (If 1v1 should be 26 each)
            Shuffle();

            //Put the players cards in order (not totally necessary but makes the game easier to play)
            pdeck.OrderCards();
            AIdeck.OrderCards();
        }

        static void Shuffle()
        {
            Random x = new Random();
            int turn = 0;

            while (masterdeck.size() > 0)
            {

                int cardvalue = x.Next(1, 14);

                if (turn == 0)
                {
                    //AI
                    if (masterdeck.DealCard(cardvalue, AIdeck))
                    {
                        turn = 1;
                    }
                }
                else
                {
                    //Player
                    if (masterdeck.DealCard(cardvalue, pdeck))
                    {
                        turn = 0;
                    }
                }
            }
        }

        public byte[] Buildmessagecards(int turn)
        {
            byte[] cards = new byte[4096];

            Deck decktouse = (turn == 0  ? AIdeck : pdeck);

            int size = decktouse.GetCard(0).name.ToCharArray().Length;

            for (int i = 0; i < decktouse.size(); i++)
            {
                string cardstring = decktouse.GetCard(i).name + ',';
                System.Buffer.BlockCopy(cardstring.ToCharArray(), 0, cards, size * i, cardstring.Length);
            }

                return cards;
        }

        public byte[] BuildmessageLP()
        {
            byte[] cards = new byte[4096];

            int size = lpdeck.GetCard(0).name.ToCharArray().Length;

            for (int i = 0; i < lpdeck.size(); i++)
            {
                string cardstring = lpdeck.GetCard(i).name + ',';
                System.Buffer.BlockCopy(cardstring.ToCharArray(), 0, cards, size * i, cardstring.Length);
            }

            System.Buffer.BlockCopy("||+".ToCharArray(), 0, cards, size + 1 * lpdeck.size(), "||+".ToCharArray().Length);

            return cards;
        }

        public void ResetLP()
        {
            lpdeck.empty();
            lpdeckh = Hand.error;
        }

        public bool Checkwin()
        {
            if (AIdeck.size() == 0 || pdeck.size() == 0)
                return true;
            else
                return false;
        }

        

        public static Deck masterdeck;
        public static Deck AIdeck;
        public static Deck pdeck;
        public static Deck lpdeck;
        public static Deck tdeck;
        public static Hand lpdeckh;
    }
}
