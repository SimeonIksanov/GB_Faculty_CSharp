using System;
using System.Linq;
using System.Text;

namespace CoderLib
{
    public class ACoder : ICoder
    {
        public string Decode(string input)
        {
            return Transform(input, (s) => s - 1);
        }

        public string Encode(string input)
        {
            return Transform(input, (s) => s + 1);
        }

        private string Transform(string input, Func<char, int> cipher)
        {
            var sb = new StringBuilder(input.Length);
            char newSymbol;
            int shift, alphabetLength;

            foreach (var symbol in input)
            {
                SetParams(symbol, out shift, out alphabetLength);
                newSymbol = (char)((cipher(symbol) - shift + alphabetLength) % alphabetLength + shift);
                sb.Append(newSymbol);
            }
            return sb.ToString();
        }

        private void SetParams(char symbol, out int shift, out int alphabetLength)
        {
            if (symbol >= 97 && symbol <= 122)
            {
                shift = 97;
                alphabetLength = 26;
            }
            else if (symbol >= 65 && symbol <= 90)
            {
                shift = 65;
                alphabetLength = 26;
            }
            else if (symbol >= 1072 && symbol <= 1103)
            {
                shift = 1072;
                alphabetLength = 32;
            }
            else if (symbol >= 1040 && symbol <= 1071)
            {
                shift = 1040;
                alphabetLength = 32;
            }
            else
            {
                shift = 0;
                alphabetLength = 10000;
            }
        }
    }
}
