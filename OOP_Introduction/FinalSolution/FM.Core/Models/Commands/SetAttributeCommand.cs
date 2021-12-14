using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Core.Models.Commands
{
    public class SetAttributeCommand : UserCommand
    {
        public SetAttributeCommand(string path, int attribute)
        {
            Path = path;
            Attribute = attribute;
        }

        public string Path { get; }
        public int Attribute { get; }
    }
}
