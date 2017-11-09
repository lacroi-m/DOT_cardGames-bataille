namespace ConsoleApp1
{
    public class Card
    {
        //Vars
        private int _value;
        private Color _color;

        //Ctor
        public Card()
        {

        }

        //Setter
        public void SetValue(int value)
        {
            _value = value;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        //Getter
        public int GetValue()
        {
            return (_value);
        }

        public Color GetColor()
        {
            return (_color);
        }
    }
}