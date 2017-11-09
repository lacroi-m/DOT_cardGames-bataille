using System;
using System.Collections;
using static ConsoleApp1.Color;
using static ConsoleApp1.Deck;

namespace ConsoleApp1
{
    public class Game
    {
        private Client _client1;

        private Client _client2;

        private Deck _deck;

        //Ctor
        public Game(Client f, Client s)
        {
            _deck = new Deck(true);
            _client1 = f;
            _client2 = s;
        }

        //Member
        public void Launch()
        {
            _deck.Mix();
            Distribute();
            LoopGame();
        }

        public void GiveCard(Client client, Card card)
        {
            //
        }

        public void GiveCards(Client client, ArrayList cards)
        {
            var i = 0;

            while (i < cards.Count)
            {
                GiveCard(client, (Card)cards[i]);
                i = i + 1;
            }
        }

        public void Distribute()
        {
            var i = 0;

            while (i < _deck.GetDeck().Count)
            {
                //if (i % 2 == 0)
                    //GiveCard(_client1, _deck.GetDeck()[i]);
                //else
                    //GiveCard(_client2, _deck.GetDeck()[i]);
                i = i + 1;
            }
        }

        public void LoopGame()
        {
            //var best = - 1;
            
            /*
            //while (_client1.GetHand().GetDeck().Count != 0 && _client2.GetHand().GetDeck().Count != 0)
            {
                var curr = new Turn();
                //faire jouer f : cardF = ..
                //curr.addCard(cardf);
                //faire jouer s : cardS = ..
                //curr.addCard(cards);
                //best = curr.wichIsBest(cardF, CardS);
                while (best == - 1)
                {
                    curr.GetDeck().GetDeck().Clear();
                    //faire jouer f;
                    //faire jouer s;
                    //faire jouer f;
                    //curr.addCard(cardF);
                    //faire jouer s;
                    //curr.addCard(cardS);
                    //best = curr.wichIsBest(cardF, CardS);
                }
                if (best == 0)
                {
                    //GiveCards(f, curr.GetDeck());
                }
                else
                {
                    //give_cards(s, curr.GetDeck());
                }
            }
            */
        }
    }
}