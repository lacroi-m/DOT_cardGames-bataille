using System.Collections;

namespace ConsoleApp1
{
    public class Turn
    {
        Deck _deck;

        //Ctor
        public Turn()
        {
            _deck = new Deck(false);
        }

        //Member
        public void AddCard(Card card)
        {
            _deck.GetDeck().Add(card);
        }

        public int WichIsBest()
        {
            if (((Card)_deck.GetDeck()[0]).GetValue() > ((Card)_deck.GetDeck()[1]).GetValue())
                return (0);
            if (((Card)_deck.GetDeck()[1]).GetValue() > ((Card)_deck.GetDeck()[0]).GetValue())
                return (1);
            return (- 1);
        }

        //Setter
        public void SetDeck(Deck cards)
        {
            _deck = cards;
        }

        //Getter
        public Deck GetDeck()
        {
            return _deck;
        }
    }
}