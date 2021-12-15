using System;
using System.Text;

namespace CoderLib
{
    public interface ICoder
    {
        string Encode(string input);

        string Decode(string input);
    }
}
