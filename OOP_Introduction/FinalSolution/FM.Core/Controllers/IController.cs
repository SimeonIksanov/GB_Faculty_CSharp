using EntityLib;
using FM.Core.Models.Commands;

namespace FM.Core.Controllers
{
    public interface IController
    {
        void Execute(UserCommand? cmd);
        void AddLogger(ILogWriter logger);

        public int PageSize { get; set; }
    }
}