using EntityLib;

namespace ConsoleUI
{
    public class LogWriter : ILogWriter
    {
        private string _filePath = "errors.log";

        public LogWriter()
        {
            _filePath = Path.GetFullPath(_filePath);
        }

        public void Log(string message)
        {
            File.AppendAllText(_filePath, message+"\n");
        }
    }
}
