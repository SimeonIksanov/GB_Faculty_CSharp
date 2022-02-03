using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLib
{
    public interface ITextFileInfo
    {
        public uint WordCount { get; set; }
        public uint LineCount { get; set; }
        public uint ParagraphCount { get; set; }
        public uint SpaceCount { get; set; }
    }
}
