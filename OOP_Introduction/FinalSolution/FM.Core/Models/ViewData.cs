using EntityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Core.Models
{
    public class ViewData : IViewData
    {
        public string Path { get; set; } = String.Empty;
        public IFileSystemItem[] DirectoryListing { get; set; } = new IFileSystemItem[0];
        public IFileSystemItemInfo? FileSystemItemInfo { get; set; }
    }
}
