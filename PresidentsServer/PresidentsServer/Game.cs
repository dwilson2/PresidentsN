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

        //VerifyHand which calls Checkhand
        //tdeck.empty();

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
            tdeck2.SetCard(tdeck.GetCard(2).CV, tdeck.GetCard(0).SV);
            tdeck2.SetCard(tdeck.GetCard(3).CV, tdeck.GetCard(0).SV);
            tdeck2.SetCard(tdeck.GetCard(4).CV, tdeck.GetCard(0).SV);

            if (Multiples(tdeck2) && Multiples(tdeck3))
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
                System.Buffer.BlockCopy(decktouse.GetCard(i).name.ToCharArray(), 0, cards, size * i, size);
            }

                return cards;
        }

        public byte[] BuildmessageLP()
        {
            byte[] cards = new byte[4096];

            int size = lpdeck.GetCard(0).name.ToCharArray().Length;

            for (int i = 0; i < lpdeck.size(); i++)
            {
                System.Buffer.BlockCopy(lpdeck.GetCard(i).name.ToCharArray(), 0, cards, size * i, size);
            }

            return cards;
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
    }
}
