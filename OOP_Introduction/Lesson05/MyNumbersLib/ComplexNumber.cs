using System;
namespace MyNumbersLib
{
    public class ComplexNumber : IEquatable<ComplexNumber>
    {
        public ComplexNumber(int real, int image)
        {
            Real = real;
            Image = image;
        }

        public int Real { get; private set; }

        public int Image { get; private set; }

        public override string ToString()
        {
            return $"{Real}+{Image}i";
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real + b.Real, a.Image + b.Image);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real - b.Real, a.Image - b.Image);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                real: a.Real * b.Real - a.Image * b.Image,
                image: a.Image * b.Real + a.Real * b.Image
                );
        }

        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not ComplexNumber)
            {
                return false;
            }

            return Equals((ComplexNumber)obj);
        }

        public bool Equals(ComplexNumber other)
        {
            return this.Real == other.Real && this.Image == other.Image;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Image);
        }
    }
}
