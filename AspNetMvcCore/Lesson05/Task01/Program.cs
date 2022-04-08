using System;
using System.IO;
using System.Linq;
using ScanApp;
using ScannerEmulator;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            IScannerEmulator scanner = new Scanner();
            var app = new ScanApplication(scanner);

            app.ScanToFile(new SaveToTextFile("SavedScan.txt"));
            //app.ScanToFile(new SaveToBin("SavedScan.bin"));

            app.SavePerfDate(new PerfSaver());
        }
    }
}
