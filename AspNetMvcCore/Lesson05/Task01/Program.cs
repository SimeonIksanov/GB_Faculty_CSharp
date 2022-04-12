using System;
using Autofac;
using ScanApp;
using ScannerEmulator;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<PerfData>()
                .As<IPerfData>();

            builder
                .RegisterType<Scanner>()
                .As<IScannerEmulator>();

            builder
                .Register(c => new SaveToTextFile(string.Format("{0}.txt", DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ssZ"))))
                .As<ISaveStrategy>();

            builder
                .Register(c => new SaveToBin(string.Format("{0}.bin", DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ssZ"))))
                .As<ISaveStrategy>();

            builder
                .RegisterType<PerfSaver>()
                .As<IPerfSaver>();

            builder.RegisterComposite<CompositeSaver, ISaveStrategy>();

            var container = builder.Build();


            IScannerEmulator scanner = container.Resolve<IScannerEmulator>();
            var compositeSaver = container.Resolve<ISaveStrategy>();

            var app = new ScanApplication(scanner);

            app.ScanToFile(compositeSaver);

            app.SavePerfDate(container.Resolve<IPerfSaver>());
        }
    }
}
