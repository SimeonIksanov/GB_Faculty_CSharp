using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public abstract class Figure
    {
        protected bool _isVisible;
        protected int _x;
        protected int _y;

        public ConsoleColor Color { get; set; }
        public bool IsVisible => _isVisible;

        public override string ToString()
        {
            return $"Figure {GetType().Name}\n" +
                $"Location at ({_x},{_y})\n" +
                $"Color: {Color.ToString()}\n" +
                $"IsVisible: {_isVisible}\n" +
                $"Has area: {Area}";
        }

        public void MoveHorizontal(int step)
        {
            _x += step;
        }

        public void MoveVertical(int step)
        {
            _y += step;
        }

        public abstract double Area { get; }
    }
}
