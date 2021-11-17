using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Rectangle : Point
    {
        private readonly int _length;
        private readonly int _height;

        public Rectangle(int x, int y, bool isVisible, int length, int height) : base(x, y, isVisible)
        {
            _length = length;
            _height = height;
        }
        public override double Square()
        {
            return _length * _height;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"\nLength: {_length}" +
                $"\nHeight: {_height}";
        }
    }
}
