using System;
using System.Text;

namespace Task02
{
    public static class MyExtensionClass
    {
        public static string ReverseString(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Sting is Null or empty");
            
            StringBuilder sb = new StringBuilder();
            for (int i = input.Length-1; i >=0; i--)
            {
                sb.Append(input[i]);
            }
            return sb.ToString();
        }
    }
}
