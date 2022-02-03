using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            var saver = new DataSaver();
            var grabber = new Grabber();
            var idToDownload = Enumerable.Range(4, 10).ToArray();

            await saver.SaveToFileAsync(
                "result.txt",
                await grabber.DownloadAsync(idToDownload));
        }
    }
}
