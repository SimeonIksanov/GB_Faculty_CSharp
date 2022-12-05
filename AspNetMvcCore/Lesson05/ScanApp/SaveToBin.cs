using System.IO;

namespace ScanApp
{
    public class SaveToBin : ISaveStrategy
    {
        private readonly string _filePath;

        public SaveToBin(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(Stream stream)
        {
            using var br = new BinaryReader(stream);
            using var bw = new BinaryWriter(new FileStream(_filePath, FileMode.Create));

            byte[] buffer = new byte[100];
            int count;
            while ((count = br.Read(buffer, 0, 100)) > 0)
            {
                bw.Write(buffer, 0, count);
            }
        }
    }
}
