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
            var idToDownload = Enumerable.Range(4, 10).ToArray();
            using (var grabber = new Grabber())

                await saver.SaveToFileAsync(
                    "result.txt",
                    await grabber.DownloadAsync(idToDownload, cts.Token));
        }
    }
}
