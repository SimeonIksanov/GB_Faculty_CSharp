using System.Collections.Generic;
using System.IO;

namespace ScannerEmulator
{
    public interface IScannerEmulator
    {
        Stream Scan();
        IEnumerable<IPerfData> GetPerfData();
    }
}
