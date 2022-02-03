namespace FM.Core.Models.Commands
{
    public class ListCommand : UserCommand
    {
        public ListCommand(string path, int page)
        {
            Path = path;
            Page = page;
        }

        public string Path { get;}
        public int Page { get;}
    }
}
