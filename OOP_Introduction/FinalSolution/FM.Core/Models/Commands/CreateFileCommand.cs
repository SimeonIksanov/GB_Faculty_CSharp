namespace FM.Core.Models.Commands
{
    public class CreateFileCommand : UserCommand
    {
        public CreateFileCommand(string path)
        {
            Path = path;
        }

        public string Path { get;}
    }
}
