using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntityLib;

namespace FileSystemLib
{
    public class DiskOperations : IDiskOperations
    {
        public IFileSystemItem[] GetFolderContent(string path)
        {
            if (!Directory.Exists(path) || string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"Directory '{path}' not found or incorrect", nameof(path));
            }
            var diskItems = new DirectoryInfo(path).GetFileSystemInfos(
                                "*",
                                new EnumerationOptions() { ReturnSpecialDirectories = true });

            return diskItems.Where(i => i.Name != ".")
                     .Select(i => new FileSystemItem()
                     {
                         Name = i.Name,
                         Path = i.FullName,
                         Type = (i.Attributes & FileAttributes.Directory) == FileAttributes.Directory
                                ? FileSystemItemType.Directory
                                : FileSystemItemType.File
                     })
                     .ToArray();
        }

        public void Copy(string src, string dst)
        {
            if (src.Equals(dst) || string.IsNullOrWhiteSpace(src) || string.IsNullOrWhiteSpace(dst))
                throw new ArgumentException("Copy: Incorrect arguments");

            if (Directory.Exists(src))
            {
                DirectoryCopy(src, Path.Combine(dst, new DirectoryInfo(src).Name), true);
            }
            else if (File.Exists(src))
            {
                if (IsDirectoryExist(dst))
                    dst = Path.Combine(dst, new FileInfo(src).Name);
                File.Copy(src, dst, overwrite: true);
            }
            else
            {
                throw new ArgumentException($"Cannot copy '{src}', not such file or folder", nameof(src));
            }
        }

        public void Delete(string path)
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

        public ulong GetSizeOnDisk(string path)
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

        public string GetFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("GetFullPath: Argument is null or empty", nameof(path));

            return Path.GetFullPath(path);
        }

        public IFileSystemItemInfo GetItemInfo(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("GetItemInfo: Argument is null or empty", nameof(path));

            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                //return (di.CreationTime, di.LastAccessTime, di.LastWriteTime);
                return new ItemInfo()
                {
                    CreationTime = di.CreationTime,
                    LastAccessTime = di.LastAccessTime,
                    LastWriteTime = di.LastWriteTime,
                    Attributes = di.Attributes,
                    Path = di.FullName,
                    Size = GetSizeOnDisk(path),
                    TextFileInfo = null
                };
            }
            else if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                //return (fi.CreationTime, fi.LastAccessTime, fi.LastWriteTime);
                return new ItemInfo()
                {
                    CreationTime = fi.CreationTime,
                    LastAccessTime = fi.LastAccessTime,
                    LastWriteTime = fi.LastWriteTime,
                    Attributes = fi.Attributes,
                    Path = fi.FullName,
                    Size = GetSizeOnDisk(path),
                    TextFileInfo = GetTextFileInfo(path)
                };
            }
            else
                throw new ArgumentException($"GetItemInfo: '{path}' - No such file or directory");
        }

        private ITextFileInfo GetTextFileInfo(string path)
        {
            uint lineCount = 0, paragraphCount = 0, spaceCount = 0, wordCount = 0;

            IEnumerable<string>? allLines = File.ReadLines(path);
            foreach (string line in allLines)
            {
                lineCount++;
                paragraphCount += line.Length==0 ? 0U : 1U;

                foreach (char ch in line)
                {
                    if (Char.IsWhiteSpace(ch))
                    {
                        spaceCount++;
                    }
                }
                wordCount += (uint)line.Split(' ',StringSplitOptions.RemoveEmptyEntries).Count();
            }

            return new TextFileInfo()
            {
                LineCount = lineCount,ParagraphCount=paragraphCount,SpaceCount=spaceCount,WordCount=wordCount
            };
        }

        public FileAttributes GetAttributes(string path)
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

        public string GetCurDir()
            => Directory.GetCurrentDirectory();

        public bool IsDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public void ChangeDirectory(string path)
        {
            if (IsDirectoryExist(path))
                Directory.SetCurrentDirectory(path);
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
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

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void CreateFile(string path)
        {
            var newFile = File.Create(path);
            newFile.Close();
            
        }

        public void Move(string path, string newName)
        {
            string fullPath = GetFullPath(path);
            Directory.Move(fullPath, newName);
        }

        public IFileSystemItem[] Find(string path, string filter)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.GetFileSystemInfos(filter,SearchOption.AllDirectories)
                     .Select(fsi => new FileSystemItem()
                        {
                            Name = fsi.Name,
                            Path = fsi.FullName,
                            Type = (fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory
                                            ? FileSystemItemType.Directory
                                            : FileSystemItemType.File
                        })
                     .ToArray();
        }

        public void SetAttribute(string path, FileAttributes attribute)
        {
            string fullPath = GetFullPath(path);
            if (File.Exists(fullPath))
            {
                File.SetAttributes(fullPath, attribute);
            }
            
        }
    }
}
