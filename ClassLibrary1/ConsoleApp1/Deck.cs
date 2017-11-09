using System;
using System.Collections;

namespace ConsoleApp1
{
    public class Deck
    {
        private ArrayList _deck;

        //Ctor
        public Deck(bool creat)
        {
            //true : crée un deck de 52 cartes
            //false : deck vide
            if (creat == true)
                Create();
        }

        public void Create()
        {
            var color = new Color();
            var value = - 1;
            var i = 0;
            var j = 0;
            var k = 0;

            while (i < 4)
            {

                k = 0;
                while (k < 13)
                {
                    var card = new Card();

                    if (k == 1)
                        value = 14;
                    if (k != 1)
                        value = k;
                    color = GetEColor(i);
                    card.SetValue(value);
                    card.SetColor(color);
                    _deck.Add(card);
                    k = k + 1;
                }
                i = i + 1;
            }
        }

        public Deck(ArrayList deck)
        {
            _deck = deck;
        }

        //Member
        public Color GetEColor(int i)
        {
            var color = new Color();
            var col = color.GetColor();

            switch (i)
            {
                case 0:
                    col = EColor.Coeur;
                    break;
                case 1:
                    col = EColor.Pique;
                    break;
                case 2:
                    col = EColor.Trefle;
                    break;
                case 3:
                    col = EColor.Carreau;
                    break;
                default:
                    col = EColor.None;
                    break;
            }
            color.SetColor(col);
            return (color);
        }

        public int GetId(Deck added, int nb)
        {
            var i = 0;
            var j = 0;

            while (i < _deck.Count)
            {
                if (!added.GetDeck().Contains(GetDeck()[i]))
                {
                    j = j + 1;
                    if (j == nb)
                        return (i);
                }
                i = i + 1;
            }
            return (- 1);
        }

        public Deck Mix()
        {
            var rnd = new Random();
            var bis = new Deck(false);
            var tmp = new Deck(_deck);
            var i = _deck.Count - 1;
            var nb = 0;

            while (tmp.GetDeck().Count != _deck.Count)
            {
                nb = rnd.Next(0, i);
                bis.GetDeck().Add(tmp.GetDeck()[nb]);
                tmp.GetDeck().Remove(tmp.GetDeck()[nb]);
                i = i - 1;
            }
            return (bis);
        }

        //Setter
        public void SetDeck(ArrayList deck)
        {
            _deck = deck;
        }

        //Getter
        public ArrayList GetDeck()
        {
            return (_deck);
        }
    }
}