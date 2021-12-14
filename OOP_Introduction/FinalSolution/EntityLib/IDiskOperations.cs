
namespace EntityLib
{
    public interface IDiskOperations
    {
        void ChangeDirectory(string path);
        void Copy(string src, string dst);
        void Delete(string path);
        FileAttributes GetAttributes(string path);
        string GetCurDir();
        IFileSystemItem[] GetFolderContent(string path);
        string GetFullPath(string path);
        ulong GetSizeOnDisk(string path);
        IFileSystemItemInfo GetItemInfo(string path);
        bool IsDirectoryExist(string path);
        void CreateDirectory(string path);
        void CreateFile(string path);
        void Move(string path, string newName);
        IFileSystemItem[] Find(string path,string filter);
        void SetAttribute(string path, FileAttributes attribute);
    }
}