using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Circle : Point
    {
        private readonly double _radius;

        public Circle(int x, int y, bool isVisible, double radius) : base(x,y, isVisible)
        {
            _radius = radius;
        }

        public override double Square()
        {
            return Math.PI * _radius * _radius;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"\nRadius: {_radius}";
        }
    }
}
