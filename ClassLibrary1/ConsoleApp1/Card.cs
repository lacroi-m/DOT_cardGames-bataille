using Newtonsoft.Json;

namespace ConsoleApp1
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Card
    {
        //Vars
        [JsonProperty]
        private int _value { get; set; }
        [JsonProperty]
        private EColor _color { get; set; }
        [JsonProperty]
        public int _id { get; set; }

        //Ctor
        public Card() { }
        public Card(int id)
        {
            _id = id;
        }

        //Setter
        public void SetValue(int value)
        {
            _value = value;
        }

        public void SetTheColor(int nbr)
        {
           if (nbr == 0)
                _color = EColor.Carreau;
            else if (nbr == 1)
                _color = EColor.Coeur;
            else if (nbr == 2)
                _color = EColor.Pique;
            else if (nbr == 4)
                _color = EColor.Trefle;
            else
                _color = EColor.None;
        }
        //Getter
        public int GetValue()
        {
            return (_value);
        }

        public EColor GetColor()
        {
            return (_color);
        }
    }
}