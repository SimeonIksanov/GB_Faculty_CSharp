using System.IO;
using System.Text;

namespace ScanApp
{
    public class SaveToTextFile : ISaveStrategy
    {
        private readonly string _filePath;

        public SaveToTextFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(Stream stream)
        {
            string allText;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                allText = Encoding.UTF8.GetString(ms.ToArray());
            }

            File.WriteAllText(_filePath, allText);
        }
    }
}
