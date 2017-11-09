using System.Diagnostics.Tracing;

namespace ConsoleApp1
{
    public class Color
    {
        private EColor _color;

        //Ctor
        public Color()
        {
            _color = EColor.None;
        }

        //Getter
        public EColor GetColor()
        {
            return (_color);
        }

        //Setter
        public void SetColor(EColor color)
        {
            _color = color;
        }
    }
}