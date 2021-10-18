using System.IO;

namespace Task03
{
    class AddressBookProcessor
    {
        public void Process(string inputFileName, string outputFileName)
        {
            string inputFullPath = Path.GetFullPath(inputFileName);
            string outputFullPath = Path.GetFullPath(outputFileName);

            if (!File.Exists(inputFullPath))
            {
                //throw new FileNotFoundException("file not found", nameof(inputFileName));
                System.Console.WriteLine("Input file not found");
                return;
            }

            using StreamReader inputFile = File.OpenText(inputFullPath);

            Directory.CreateDirectory(Path.GetDirectoryName(outputFullPath));
            using StreamWriter outputFile = new StreamWriter(outputFullPath);

            string line = inputFile.ReadLine();
            while(line != null)
            {
                SearchMail(ref line);
                outputFile.WriteLine(line);
                line = inputFile.ReadLine();
            }
        }

        public void SearchMail(ref string line)
        {
            string[] lines = line.Split('&',2);
            if (lines.Length == 2)
            {
                line = lines[1].Trim();
            }
            else
            {
                line = "";
            }
        }
    }
}
