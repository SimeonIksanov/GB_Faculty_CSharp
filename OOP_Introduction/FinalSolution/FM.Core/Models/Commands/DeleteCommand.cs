namespace FM.Core.Models.Commands
{
    public class DeleteCommand : UserCommand
    {
        public DeleteCommand(string path)
        {
            Path = path;
        }

        public String Path { get; }
    }
}
