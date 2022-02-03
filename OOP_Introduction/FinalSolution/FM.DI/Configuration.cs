using ConsoleUI;
using EntityLib;
using FileSystemLib;
using FM.Core.Controllers;
using SimpleInjector;

namespace FM.DI
{
    public class Configuration
    {
        public Configuration()
        {
            Container = new Container();
            Setup();
        }

        public Container Container { get; }

        private void Setup()
        {
            Container.Register<IDiskOperations, DiskOperations>(Lifestyle.Singleton);
            Container.Register<IController, Controller>(Lifestyle.Singleton);
            Container.Register<IView, UI>(Lifestyle.Singleton);
            Container.Register<ILogWriter, LogWriter>(Lifestyle.Singleton);
        }
    }
}
