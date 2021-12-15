using System;
using System.Text;

namespace CoderLib
{
    public class BCoder : ICoder
    {
        public string Decode(string input)
        {
            return Transform(input);
        }

        public string Encode(string input)
        {
            return Transform(input);
        }

        private string Transform(string input)
        {
            var sb = new StringBuilder(input.Length);
            int start, end;
            foreach (var symbol in input)
            {
                if (char.IsLetter(symbol))
                {
                    SetParams(symbol, out start, out end);
                    sb.Append((char)(start + end - symbol));
                }
            }
            return sb.ToString();
        }

        private void SetParams(char symbol, out int start, out int end)
        {
            if (symbol >= 97 && symbol <= 122)
            {
                start = 97;
                end = 122;
            }
            else if (symbol >= 65 && symbol <= 90)
            {
                start = 65;
                end = 90; ;
            }
            else if (symbol >= 1072 && symbol <= 1103)
            {
                start = 1072;
                end = 1103;
            }
            else if (symbol >= 1040 && symbol <= 1071)
            {
                start = 1040;
                end = 1071; ;
            }
            else
            {
                start = 97;
                end = 122;
            }
        }
    }
}
