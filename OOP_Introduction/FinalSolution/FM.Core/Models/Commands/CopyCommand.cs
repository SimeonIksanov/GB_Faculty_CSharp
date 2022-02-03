namespace FM.Core.Models.Commands
{
    public class CopyCommand : UserCommand
    {
        public CopyCommand(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }

        public string Source { get;}
        public string Destination { get;}
    }
}
