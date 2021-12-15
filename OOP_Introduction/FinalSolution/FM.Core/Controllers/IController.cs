using FM.Core.Models.Commands;

namespace FM.Core.Controllers
{
    public interface IController
    {
        void Execute(UserCommand? cmd);
        public int PageSize { get; set; }
    }
}