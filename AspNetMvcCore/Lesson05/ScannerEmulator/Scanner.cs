using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScannerEmulator
{
    public class Scanner : IScannerEmulator
    {


        public IEnumerable<IPerfData> GetPerfData()
        {
            while (true)
                yield return new PerfData();
        }

        public Stream Scan()
        {
            const string loremIpsum = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Itaque ducimus quasi modi. Atque est accusamus ipsa laborum unde sint, nemo numquam, cum consequatur exercitationem vitae ut, delectus quos. Amet, totam!";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(loremIpsum));
            return stream;
        }
    }
}
