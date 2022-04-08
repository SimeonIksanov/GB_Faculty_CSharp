using System.Collections.Generic;
using ScannerEmulator;

namespace ScanApp
{
    public interface IPerfSaver
    {
        void Save(IEnumerable<IPerfData> perfData);
    }
}