using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager
{
    public class FMLib
    {
        public static FileSystemInfo[] GetFolderContent(string path)
        {
            if (!Directory.Exists(path) || string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"Directory '{path}' not found or incorrect", nameof(path));

            return new DirectoryInfo(path).GetFileSystemInfos("*", new EnumerationOptions() { ReturnSpecialDirectories = true });
        }

        public static void Copy(string src, string dst)
        {
            if (src.Equals(dst) || string.IsNullOrWhiteSpace(src) || string.IsNullOrWhiteSpace(dst))
                throw new ArgumentException("Copy: Incorrect arguments");

            if (Directory.Exists(src))
            {
                DirectoryCopy(src, Path.Combine(dst, new DirectoryInfo(src).Name), true);
            }
            else if (File.Exists(src))
            {
                if (isDirectoryExist(dst))
                    dst = Path.Combine(dst, new FileInfo(src).Name);
                File.Copy(src, dst, overwrite: true);
            }
            else
            {
                throw new ArgumentException($"Cannot copy '{src}', not such file or folder", nameof(src));
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "DirectoryCopy: Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, overwrite: true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public static void Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Delete: Argument is null or empty", nameof(path));

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                throw new ArgumentException($"Cannot delete '{path}' : No such file or directory");
            }
        }

        public static ulong GetSizeOnDisk(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("GetSizeOnDisk: Argument is null or empty", nameof(path));

            ulong size = 0;
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach (FileInfo fi in dir.GetFiles("*", SearchOption.AllDirectories))
                {
                    size += (ulong)fi.Length;
                }
            }
            else if (File.Exists(path))
            {
                size = (ulong)new FileInfo(path).Length;
            }
            else
            {
                throw new ArgumentException($"GetSizeOnDisk: '{path}' - No such file or directory");
            }

            return size;
        }

        public static string GetFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("GetFullPath: Argument is null or empty", nameof(path));

            return Path.GetFullPath(path);
        }

        public static (DateTime creationTime, DateTime lastAccessTime, DateTime lastWriteTime) GetTimes(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("GetTimes: Argument is null or empty", nameof(path));

            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                return (di.CreationTime, di.LastAccessTime, di.LastWriteTime);
            }
            else if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                return (fi.CreationTime, fi.LastAccessTime, fi.LastWriteTime);
            }
            else
                throw new ArgumentException($"GetTimes: '{path}' - No such file or directory");
        }

        public static FileAttributes GetAttributes(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("GetAttributes: Argument is null or empty", nameof(path));

            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                return di.Attributes;
            }
            else if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                return fi.Attributes;
            }
            else
                throw new ArgumentException($"GetAttributes: '{path}' - No such file or directory");
        }

        public static string GetCurDir()
            => Directory.GetCurrentDirectory();

        public static bool isDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public static void ChangeDirectory(string path)
        {
            if (isDirectoryExist(path))
                Directory.SetCurrentDirectory(path);
        }
    }
}
