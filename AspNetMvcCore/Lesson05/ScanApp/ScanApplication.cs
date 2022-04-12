using System;
using System.Linq;
using ScannerEmulator;

namespace ScanApp
{
    public class ScanApplication
    {
        private readonly IScannerEmulator _scanner;

        public ScanApplication(IScannerEmulator scanner)
        {
            _scanner = scanner;
        }

        public void ScanToFile(ISaveStrategy saveStrategy)
        {
            using (var stream = _scanner.Scan())
            {
                saveStrategy.Save(stream);
            }
        }

        public void SavePerfDate(IPerfSaver perfSaver)
        {
            perfSaver.Save(_scanner.GetPerfData().Take(10));
        }
    }
}
