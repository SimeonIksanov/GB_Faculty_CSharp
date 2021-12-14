namespace FM.Core.Models.Commands
{
    public class InfoCommand : UserCommand
    {
        public InfoCommand(string path)
        {
            Path = path;
        }

        public string Path { get;}
    }
}
