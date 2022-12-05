using System.IO;

namespace ScanApp
{
    public interface ISaveStrategy
    {
        void Save(Stream stream);
    }
}