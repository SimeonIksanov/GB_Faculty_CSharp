using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Platform.ToString().StartsWith("Win32NT"))
                Console.SetWindowSize(140, 35);

            FMApp.Run();
        }
    }
}
