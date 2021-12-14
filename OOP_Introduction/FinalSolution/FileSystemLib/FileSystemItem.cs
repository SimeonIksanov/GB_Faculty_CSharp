using EntityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemLib
{
    public class FileSystemItem : IFileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public FileSystemItemType Type { get; set; }

        public override string ToString()
        {
            return Type==FileSystemItemType.Directory 
                ? $"[{Name}]"
                : Name;
        }
    }
}
