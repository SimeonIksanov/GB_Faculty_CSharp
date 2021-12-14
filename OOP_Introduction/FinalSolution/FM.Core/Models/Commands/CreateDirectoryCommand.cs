namespace FM.Core.Models.Commands
{
    public class CreateDirectoryCommand : UserCommand
    {
        public CreateDirectoryCommand(string path)
        {
            Path = path;
        }

        public string Path { get;}
    }
}
