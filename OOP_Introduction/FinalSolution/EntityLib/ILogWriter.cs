using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLib
{
    public interface ILogWriter
    {
        public void Log(string message);
    }
}
