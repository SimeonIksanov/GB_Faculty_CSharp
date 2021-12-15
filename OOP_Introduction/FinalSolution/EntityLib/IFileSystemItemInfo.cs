using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLib
{
    public interface IFileSystemItemInfo
    {
        public string? Path { get; set; }
        public ulong Size { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public FileAttributes Attributes { get; set; }
        public ITextFileInfo? TextFileInfo { get; set; }

    }
}
