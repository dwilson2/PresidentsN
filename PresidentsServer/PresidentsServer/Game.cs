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

//
//Defines all the rules and functionality for the Presidents Game
//
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
        
        //Custom data type representing the various pieces of
        //data associated with a card.
        public struct Card {

            public int CV { get; set; }
            public Suite SV { get; set; }
            public string name { get; set; }
        }

    //Note this class is probably more accurately named player hand (subset of entire deck)
    public class Deck : Object
    {
        /**/
        /*
        public Deck()
        
        NAME
            public Deck() - Creates a deck object - List<Card>
        
        SYNOPSIS
             public Deck()
                  
         DESCRIPTION
            Initializes a new list of cards to later be populated.
         
         RETURNS
            Deck object - constructor
         
         AUTHOR
            David Wilson
         */
        /**/
        public Deck()
        {
            deck = new List<Card>();
        }

        /**/
        /*
        public void SetCard( int i, Suite s)
        
        NAME
            public void SetCard( int i, Suite s) - Adds the specified card to the deck object (*this)
        
        SYNOPSIS
             public void SetCard( int i, Suite s)
              
             int i - the numeric value of the card to be added
             Suite s - the suite of the card to be added
                  
         DESCRIPTION
            Adds a card into the deck.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void SetCard( int i, Suite s)
        {
           
            Card cCard = new Card();
            cCard.CV = i;
            cCard.SV = s;
           
            string cname = cCard.SV.ToString().Substring(0, 1) + cCard.CV.ToString();
            cCard.name = cname;

            this.deck.Add(cCard);
        }

        /**/
        /*
        public void RemoveCard(int value, Suite s)
        
        NAME
            RemoveCard - Removes a card from the deck if found
        
        SYNOPSIS
             public void RemoveCard(int value, Suite s)
              
             int i - the numeric value of the card to be removed
             Suite s - the suite of the card to be removed
                  
         DESCRIPTION
            Removes the specified card from the deck.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void RemoveCard(int value, Suite s)
        {
            Predicate<Card> T = (Card x) => { return (x.CV == value) && (x.SV == s); };

            //If a card of this type exists we get the first one
            this.deck.Remove(this.deck.Find(T));
        }

        /**/
        /*
        public bool FindCard(int value, Suite s)
        
        NAME
            FindCard - Checks to see if the specified card is in the deck (*this)
        
        SYNOPSIS
             public bool FindCard(int value, Suite s)
              
             int i - the numeric value of the card to be located
             Suite s - the suite of the card to be located
                  
         DESCRIPTION
            Checks if the specified card is in the deck
         
         RETURNS
            bool - True means the card is in the deck
         
         AUTHOR
            David Wilson
         */
        /**/
        public bool FindCard(int value, Suite s)
        {
            Predicate<Card> T = (Card x) => { return (x.CV == value) && (x.SV == s); };

            //Verify the card "found" matches the one searched for (rather than the default)
            if (this.deck.Find(T).CV != value || this.deck.Find(T).SV != s)
                return false;

            return true;
        }

        /**/
        /*
        public Card GetCard(int index)
        
        NAME
            GetCard - Returns the card at a given index (0 - 1st card in the list)
        
        SYNOPSIS
             public Card GetCard(int index)
              
             int index - the position in the deck where the card is located.
                  
         DESCRIPTION
            Returns a card at a given position from the deck.
         
         RETURNS
            Card at specified location.
         
         AUTHOR
            David Wilson
         */
        /**/
        public Card GetCard(int index)
        {
                Card nullc = new Card();

                if (index > deck.ToArray().Length -1) return nullc;

                Card[] cardarray = deck.ToArray();

                return cardarray[index];
        }

        /**/
        /*
        public bool DealCard(int i, Deck b)
        
        NAME
            public bool DealCard(int i, Deck b) - Put card from the deck (*this) into deck b 
            if (*this) has any card with numeric value i.
        
        SYNOPSIS
             public bool DealCard(int i, Deck b)
              
             int i - The numeric value of the card being searched for.
             deck b - The deck in which to place the card.
                  
         DESCRIPTION
            Takes a card from the current deck and puts it into b, if current deck has a card with value i.
         
         RETURNS
            bool - indicating success or failure
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        public void OrderCards()
        
        NAME
             public void OrderCards() - Sort the cards first by number value then by suite.
        
        SYNOPSIS
             public void OrderCards()
              
         DESCRIPTION
            Sorts cards by their value within the game.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void OrderCards()
        {
            deck.Sort(CompareCards);
        }

        /**/
        /*
        private static int CompareCards(Card c1, Card c2)
        
        NAME
             private static int CompareCards(Card c1, Card c2) - Comparison function used to sort cards in a Deck.
        
        SYNOPSIS
             private static int CompareCards(Card c1, Card c2)
         
            Card c1, c2 The two cards to be compared.
              
         DESCRIPTION
           Tells sort function how to compare the Card datatype.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        private static int CompareCards(Card c1, Card c2)
        {
            if (c1.CV > c2.CV || (c1.CV == c2.CV && c1.SV > c2.SV) )
            {
                return 1;
            }
            else
                return -1;    
        }


        /**/
        /*
        public int size()
        
        NAME
             public int size() - Returns number of elements in Deck list
        
        SYNOPSIS
            public int size()
         
              
         DESCRIPTION
           An easier to remember way to obtain the number of elements in a Deck's deck.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public int size()
        {
            return deck.Count;
        }

        /**/
        /*
        public void empty()
        
        NAME
             public void empty() - Remove all elements from the list.
        
        SYNOPSIS
            public void empty()
         
         DESCRIPTION
           An easier to remember way to clear out a list (deck).
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void empty()
        {
            deck.Clear();
        }

        List<Card> deck;
    }

    public class Game
    {
        //All the possible valid hands a client could play
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

        /**/
        /*
        public int VerifyHand(string[] cards, int turn)
        
        NAME
             public int VerifyHand(string[] cards, int turn) - Checks that a valid hand was played (one of the above defined),
             the same number of cards as the previous player played, and a better hand.
        
        SYNOPSIS
            public int VerifyHand(string[] cards, int turn)
            
            string[] cards - An array containing the indices of the cards being played
            turn - Identification of which client played a move.
         
         DESCRIPTION
            Function which either validates a move and takes the cards away from a player or reports an error.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public int VerifyHand(string[] cards, int turn, ref Deck clientdeck)
        {
            int retval = 0;

            clientdeck.OrderCards();

            Card nullc = new Card();

            for (int i = 0; i < cards.Length; i++)
            {
                int cardi = Convert.ToInt32(cards[i],10);
                Card cc = clientdeck.GetCard(cardi);
                tdeck.SetCard(cc.CV, cc.SV);

                if (cc.CV == nullc.CV && cc.SV == nullc.SV)
                    retval = 3;
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
                    clientdeck.RemoveCard(tdeck.GetCard(i).CV, tdeck.GetCard(i).SV);
            }

            return retval;
        }

        /**/
        /*
        public bool Breaktie(Hand tdeckh)
        
        NAME
             public bool Breaktie(Hand tdeckh) - Compares the current hand (in tdeck) with the 
             hand played by the last player in (lpdeck) to determine if the new hand is better.
        
        SYNOPSIS
            public bool Breaktie(Hand tdeckh)
            
           tdeckh - Reveals the type of hand being played
         
         DESCRIPTION
            Compare the last played hand with the current hand according to the rules of Presidents
            Generally speaking if the number is higher or suite is better, it's a better hand.
         
         RETURNS
            bool - True if current hand is better and therefore legal.
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        Card highcard(Deck deck)
        
        NAME
             Card highcard(Deck deck) - Iterate through the cards in a deck and return the highest.
        
        SYNOPSIS
            Card highcard(Deck deck)
            
            Deck deck - The deck to search through 
         
         DESCRIPTION
            Find the highest card in a deck.
         
         RETURNS
            Card - The best card in a deck (highest number and best suite).
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        public bool isFlush()
        
        NAME
             public bool isFlush() - Determines if a played hand is a flush
        
        SYNOPSIS
            public bool isFlush()
         
         DESCRIPTION
           Determines if a played hand is a flush by looking at the suite values of the cards.
         
         RETURNS
            bool - true - The played hand is a flush.
         
         AUTHOR
            David Wilson
         */
        /**/
        public bool isFlush()
        {
            Card cc = tdeck.GetCard(0);

            for (int i = 1; i < tdeck.size(); i++)
            {
                if (tdeck.GetCard(i).SV != cc.SV)
                    return false;
            }

            return true;
        }

        /**/
        /*
        public bool isStraight()
        
        NAME
             public bool isStraight() - Determines if a played hand is a straight.
        
        SYNOPSIS
            public bool isStraight()
         
         DESCRIPTION
           Determines if a played hand is a straight by looking at the numeric values of the cards.
         
         RETURNS
            bool - true - The played hand is a straight.
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        public bool Multiples(Deck deck)
        
        NAME
             public bool Multiples(Deck deck) - Determines if all cards in a played hand are the same numeric value.
        
        SYNOPSIS
            public bool Multiples(Deck deck)
            
            Deck deck - The deck to iterate through.
         
         DESCRIPTION
           Determines if a played hand is a multiples hand (pair, three kind, four kind, full house) by looking at the numeric values of the cards.
         
         RETURNS
            bool - true - The played hand is a multiple.
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        public bool isFullHouse()
        
        NAME
             public bool isFullHouse() - Determines if a plyed hand is a full house (3-kind and a pair)
        
        SYNOPSIS
            public bool isFullHouse()
            
            Deck deck - The deck to iterate through.
         
         DESCRIPTION
           Determines if a played hand is a full house by looking at the numeric values of the cards.
         
            Note the three of a kind could be 0,1,2 or 2,3,4
         
         RETURNS
            bool - true - The played hand is a fullhouse.
         
         AUTHOR
            David Wilson
         */
        /**/
        public bool isFullHouse()
        {
            tdeck.OrderCards();

            Deck tdeck2 = new Deck();
            tdeck2.SetCard(tdeck.GetCard(0).CV, tdeck.GetCard(0).SV);
            tdeck2.SetCard(tdeck.GetCard(1).CV, tdeck.GetCard(1).SV);

            Deck tdeck3 = new Deck();
            tdeck3.SetCard(tdeck.GetCard(2).CV, tdeck.GetCard(2).SV);
            tdeck3.SetCard(tdeck.GetCard(3).CV, tdeck.GetCard(3).SV);
            tdeck3.SetCard(tdeck.GetCard(4).CV, tdeck.GetCard(4).SV);

            Deck tdeck4 = new Deck();
            tdeck4.SetCard(tdeck.GetCard(0).CV, tdeck.GetCard(0).SV);
            tdeck4.SetCard(tdeck.GetCard(1).CV, tdeck.GetCard(1).SV);
            tdeck4.SetCard(tdeck.GetCard(2).CV, tdeck.GetCard(2).SV);

            Deck tdeck5 = new Deck();
            tdeck5.SetCard(tdeck.GetCard(3).CV, tdeck.GetCard(3).SV);
            tdeck5.SetCard(tdeck.GetCard(4).CV, tdeck.GetCard(4).SV);

            if (Multiples(tdeck2) && Multiples(tdeck3) || Multiples(tdeck4) && Multiples(tdeck5))
                return true;

            return false;
        }

        /**/
        /*
        public Hand Checkhand()
        
        NAME
             public Hand Checkhand() - Determines what type of move a client wants to play.
        
        SYNOPSIS
            public Hand Checkhand()
                     
         DESCRIPTION
           Uses the number of cards the user is playing to determine what possible hands the move could be. 
           Tests from best hand to worst so that a hand is always identified as its strongest possible hand.
            Ex. Checks if a hand is a straight_flush before testing if it's a straight or flush.
         
         RETURNS
            Hand - The type of hand that is being played.
         
         AUTHOR
            David Wilson
         */
        /**/
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

        /**/
        /*
        public void PlayCards()
        
        NAME
             public void PlayCards() - "Plays" the clients cards
        
        SYNOPSIS
            public void PlayCards()
                     
         DESCRIPTION
            Moves cards from the temporary tdeck to the last played lpdeck so that the cards can be sent to both clients. 
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void PlayCards()
        {
            lpdeckh = Checkhand();
            lpdeck.empty();

            for (int i = 0; i < tdeck.size(); i++)
            {
                lpdeck.SetCard(tdeck.GetCard(i).CV, tdeck.GetCard(i).SV);
            }

            tdeck.empty();
        }

        /**/
        /*
        static Game()
        
        NAME
            static Game() - Creates a new game object (initializes a bunch of new decks)
        
        SYNOPSIS
            static Game() 
                     
         DESCRIPTION
            Various decks 
            master - Intially holds the entire deck (52 cards)
            AI - Client 1s deck
            pdeck - Client 2s deck
            lpdeck - Deck that holds a move immediately after it is played
            tdeck - Deck that holds a move while it's being evaluated to ensure it can be legally played
         
         RETURNS
            A deck object with all of the above created
         
         AUTHOR
            David Wilson
         */
        /**/
        public Game() 
        {
            masterdeck = new Deck();
            AIdeck = new Deck();
            pdeck = new Deck();
            lpdeck = new Deck();
            tdeck = new Deck();

            Initializegame();

        }

        /**/
        /*
        static void Initializegame()
        
        NAME
           static void Initializegame() - Deals out entire deck amongst the AI and played decks
        
        SYNOPSIS
            static void Initializegame() 
                     
         DESCRIPTION
            Creates cards for Ace-King of each suite and puts them in the master deck.
            Randomly distributes the cards amongst the client decks.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void Initializegame()
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

        /**/
        /*
        static void Shuffle()
        
        NAME
           static void Shuffle() - Deals out entire deck amongst the AI and played decks
        
        SYNOPSIS
            static void Shuffle()
                     
         DESCRIPTION
            Randomly distributes the cards amongst the client decks.
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void Shuffle()
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

        /**/
        /*
        public byte[] Buildmessagecards(int turn)
        
        NAME
           public byte[] Buildmessagecards(int turn) - Builds up a message of the player's cards to be sent out
        
        SYNOPSIS
            public byte[] Buildmessagecards(int turn)
         
            turn - A number referencing which client deck to use.
                     
         DESCRIPTION
            Creates a message of the player's cards (coma seperated)
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public byte[] Buildmessagecards(int turn, Deck clientdeck)
        {
            byte[] cards = new byte[4096];

            int offset = 0;

            clientdeck.OrderCards();

            for (int i = 0; i < clientdeck.size(); i++)
            {
                string cardstring = clientdeck.GetCard(i).name + ',';
                byte[] card = Program.GetBytes(cardstring);
                System.Buffer.BlockCopy(card, 0, cards, offset, card.Length);
                offset += card.Length;
            }

                return cards;
        }

        /**/
        /*
        public byte[] BuildmessageLP() 
        
        NAME
           public byte[] BuildmessageLP() - Build up byte array to send to clients containing last played cards 
        
        SYNOPSIS
            public byte[] BuildmessageLP()
                     
         DESCRIPTION
            Iterates through lpdeck building up a message with its contents (comma-seperated)
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public byte[] BuildmessageLP()
        {
            byte[] cards = new byte[4096];

            int offset = 0;

            for (int i = 0; i < lpdeck.size(); i++)
            {
                string cardstring = lpdeck.GetCard(i).name + ',';
                byte[] card = Program.GetBytes(cardstring);
                System.Buffer.BlockCopy(card, 0, cards, offset, card.Length);
                offset += card.Length;
            }

            byte[] sep = Program.GetBytes("||+");
            System.Buffer.BlockCopy(sep, 0, cards, offset, sep.Length);

            return cards;
        }

        public byte[] Buildmessage(int clientnum, Deck clientdeck)
        {
            byte[] lp = BuildmessageLP();
            byte[] p = Buildmessagecards(clientnum, clientdeck);

            byte[] pf = new byte[lp.Length + p.Length];
            System.Buffer.BlockCopy(lp, 0, pf, 0, lp.Length);
            System.Buffer.BlockCopy(p, 0, pf, lp.Length, p.Length);

            return pf;
        }

        /**/
        /*
        public void ResetLP()
        
        NAME
           public void ResetLP() - Clear out last played deck so next person can put anything they want (new round)!
        
        SYNOPSIS
            public void ResetLP()
                     
         DESCRIPTION
            Clears out lpdeck
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void ResetLP()
        {
            lpdeck.empty();
            lpdeckh = Hand.error;
        }

        /**/
        /*
        public void ResetTD()
        
        NAME
           public void ResetTD() - Clear out last played cards so person can play a non-error hand
        
        SYNOPSIS
            public void ResetLP()
                     
         DESCRIPTION
            Clears out tdeck
         
         RETURNS
            N/A
         
         AUTHOR
            David Wilson
         */
        /**/
        public void ResetTD()
        {
            tdeck.empty();
        }

        /**/
        /*
        public bool Checkwin()
        
        NAME
           public bool Checkwin() - Determine if any player has played all of his cards.
        
        SYNOPSIS
            public bool Checkwin()
                     
         DESCRIPTION
            Check sizes of client card arrays to see if someone has won
         
         RETURNS
            bool - true if someone has won
         
         AUTHOR
            David Wilson
         */
        /**/
        public bool Checkwin()
        {
            if (AIdeck.size() == 0 || pdeck.size() == 0)
                return true;
            else
                return false;
        }

        public Deck masterdeck;
        public Deck AIdeck;
        public Deck pdeck;
        public Deck lpdeck;
        public Deck tdeck;
        public Hand lpdeckh;
    }
}
