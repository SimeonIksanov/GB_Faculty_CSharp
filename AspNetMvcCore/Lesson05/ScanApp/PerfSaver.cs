using System;
using System.Collections.Generic;
using System.IO;
using ScannerEmulator;

namespace ScanApp
{
    public class PerfSaver : IPerfSaver
    {
        public void Save(IEnumerable<IPerfData> perfData)
        {
            string fileName = DateTime.Now.ToUniversalTime().ToString("u") + ".txt";
            using var file = File.CreateText(fileName);

            foreach (PerfData line in perfData)
            {
                file.WriteLine(line.ToString());
            }
        }
    }
}
