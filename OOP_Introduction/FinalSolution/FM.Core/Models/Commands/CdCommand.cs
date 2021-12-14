namespace FM.Core.Models.Commands
{
    public class CdCommand : UserCommand
    {
        public CdCommand(string path)
        {
            Path = path;
        }

        public string Path { get; }

    }
}
