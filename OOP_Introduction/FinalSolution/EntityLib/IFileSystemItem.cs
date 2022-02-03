using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLib
{
    public interface IFileSystemItem
    {
        string Name { get; set; }
        string Path { get; set; }
        FileSystemItemType Type { get; set; }
        string ToString();
    }
}
