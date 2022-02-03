namespace FM.Core.Models.Commands
{
    public class MoveCommand : UserCommand
    {
        public MoveCommand(string path, string newName)
        {
            Path = path;
            NewName = newName;
        }

        public string Path { get; }
        public string NewName { get; }
    }
}
