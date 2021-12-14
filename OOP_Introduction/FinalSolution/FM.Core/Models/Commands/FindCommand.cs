using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Core.Models.Commands
{
    public class FindCommand : UserCommand
    {
        public FindCommand(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; }
    }
}
