using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Deck
    {
        [JsonProperty]
        public List<Card> _deck = new List<Card>();
        //Ctor

        public Deck(bool creat)
        {
            if (creat)              //true : crée un deck de 52 cartes
              Create();
                                    //false : deck vide
        }

        public Deck(List<Card> deck)
        {
            _deck = deck;
        }

        public void AddCard(Card card)
        {
            _deck.Add(card);
        }

        public int CardsInDeck()
        {
            return _deck.Capacity;
        }

        public void Create()
        {
            int value = 0;
            int color = 0;
            int id = 1;
            while (id <= 52)
            {
                while (++value < 14)
                {
                    if (++color == 4)
                        color = 0;
                    Card card = new Card(id++);
                    card.SetValue(value);
                    card.SetTheColor(color);
                    _deck.Add(card);
                }
                value = 0;
            }
        }

        //Getter
        public List<Card> GetDeck()
        {
            return (_deck);
        }
    }
}