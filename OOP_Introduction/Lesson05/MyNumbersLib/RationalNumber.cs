using System;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace MyNumbersLib
{
    public class RationalNumber : IEquatable<RationalNumber>
    {
        public RationalNumber(int numerator, int denominator = 1)
        {
            if (numerator == 0 && denominator != 0)
            {
                Numerator = 0;
                Denominator = 1;
            }
            else
            {
                Numerator = numerator;
                Denominator = denominator;
                MakeCorrect(this);
            }
        }

        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            if (a.IsNan || b.IsNan)
                return new RationalNumber(1, 0);

            return new RationalNumber(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            if (a.IsNan || b.IsNan)
                return new RationalNumber(1, 0);

            var r = new RationalNumber(a.Numerator * b.Denominator, a.Denominator * b.Numerator);

            return r;
        }

        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            if (a.IsNan || b.IsNan)
                return new RationalNumber(1, 0);
            else if (a.Denominator == b.Denominator)
                return new RationalNumber(a.Numerator + b.Numerator, a.Denominator);
            else
            {
                return new RationalNumber(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
            }
        }

        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            if (a.IsNan || b.IsNan)
                return new RationalNumber(1, 0);
            else if (a.Denominator == b.Denominator)
                return new RationalNumber(a.Numerator - b.Numerator, a.Denominator);
            else
            {
                return new RationalNumber(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
            }
        }

        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(RationalNumber a, RationalNumber b)
        {
            return !a.Equals(b);
        }

        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            if (a.IsNan || b.IsNan)
            {
                throw new ArgumentException("Both arguments have to be correct rationals");
            }

            if (a.Denominator == b.Denominator)
            {
                return a.Numerator < b.Numerator;
            }
            else
            {
                return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
            }
        }

        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            if (a.IsNan || b.IsNan)
            {
                throw new ArgumentException("Both arguments have to be correct rationals");
            }

            if (a.Denominator == b.Denominator)
            {
                return a.Numerator > b.Numerator;
            }
            else
            {
                return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
            }
        }

        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            return a < b || a.Equals(b);
        }

        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            return a > b || a.Equals(b);
        }

        public static RationalNumber operator %(RationalNumber a, RationalNumber b)
        {
            throw new NotImplementedException();
            // не понял как это реализовывать для рациональных чисел
            // (и можно ли это сделать в принципе)
        }

        public static RationalNumber operator ++(RationalNumber number)
        {
            number.Numerator += number.Denominator;
            return number;
        }

        public static RationalNumber operator --(RationalNumber number)
        {
            number.Numerator -= number.Denominator;
            return number;
        }

        public static implicit operator float(RationalNumber rational)
        {
            if (rational.Denominator == 0)
                return float.NaN;
            return rational.Numerator / (float)rational.Denominator;
        }

        public static implicit operator RationalNumber(int a)
        {
            return new RationalNumber(a);
        }

        public static explicit operator int(RationalNumber rational)
        {
            if (rational.Numerator % rational.Denominator != 0)
                throw new Exception();
            return rational.Numerator / rational.Denominator;
        }

        public int Numerator { get; private set; }

        public int Denominator { get; private set; }

        public bool IsNan
        {
            get { return Denominator == 0; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not RationalNumber)
            {
                return false;
            }

            return Equals((RationalNumber)obj);
        }

        public bool Equals(RationalNumber other)
        {
            bool bothAreValidRationals = other.IsNan == false && this.IsNan == false;

            return bothAreValidRationals && Numerator == other.Numerator && Denominator == other.Denominator;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        private static int getGreatestCommonDivisor(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private void Reduce(RationalNumber a)
        {
            //наибольший общий делитель
            var nod = getGreatestCommonDivisor(a.Numerator, a.Denominator);

            if (nod != 1 && nod != 0)
            {
                a.Numerator /= nod;
                a.Denominator /= nod;
            }
        }

        private void MakeCorrect(RationalNumber a)
        {
            Reduce(a);

            if (a.Denominator < 0)
            {
                a.Numerator *= -1;
                a.Denominator *= -1;
            }
        }
    }
}
