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

    public static class Game
    {
        //Add function to determine move type (enum) 

        static Game() 
        {
            //Carddraw = new List<DrawInfo>();
            masterdeck = new Deck();
            AIdeck = new Deck();
            pdeck = new Deck();
            lpdeck = new Deck();

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

        private static Deck masterdeck;
        private static Deck AIdeck;
        private static Deck pdeck;
        private static Deck lpdeck;
    }
}
