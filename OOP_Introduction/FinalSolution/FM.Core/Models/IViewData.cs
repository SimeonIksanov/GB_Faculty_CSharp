using EntityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Core.Models
{
    public interface IViewData
    {
        public string Path { get; set; }
        public IFileSystemItem[] DirectoryListing { get; set; }
        public IFileSystemItemInfo FileSystemItemInfo { get; set; }
        //public string InfoPanelPath { get; set; }
        //public DateTime InfoPanelCreationTime { get; set; }
        //public DateTime InfoPanelLastAccessTime { get; set; }
        //public DateTime InfoPanelLastWriteTime { get; set; }
        //public FileAttributes InfoPanelFileAttributes { get; set; }
        //public ulong InfoPanelDiskSize { get; set; }
        //public bool IsInfoPanelEnabled { get; set; }
    }
}
