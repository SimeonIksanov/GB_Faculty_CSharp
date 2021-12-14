using EntityLib;
using FM.Core.Models;
using FM.Core.Models.Commands;
using FM.Core.Views;

namespace FM.Core.Controllers
{
    public class Controller
    {
        private string _currentDirectory;
        private int _pageSize;
        private IDiskOperations _diskOperations;
        private IView _view { get; set; }
        private IViewData _viewData;

        public Controller(IView view, IDiskOperations diskOperation, int pageSize)
        {
            _view = view;
            _viewData = new ViewData();
            _diskOperations = diskOperation;
            _currentDirectory = _diskOperations.GetCurDir();
            _pageSize = pageSize;
        }

        public void Execute(UserCommand cmd)
        {
            if (cmd is ExitCommand)
            {
                Environment.Exit(0);
            }
            else if (cmd is ListCommand listCommand)
            {
                ListContent(listCommand.Path, listCommand.Page);
            }
            else if (cmd is CdCommand cdCommand)
            {
                ChangeDirectory(cdCommand.Path);
                ListContent(_currentDirectory, 1);
            }
            else if (cmd is CopyCommand copyCommand)
            {
                Copy(copyCommand.Source, copyCommand.Destination);
                ListContent(_currentDirectory, 1);
            }
            else if (cmd is DeleteCommand deleteCommand)
            {
                _diskOperations.Delete(deleteCommand.Path);
                ListContent(_currentDirectory, 1);
            }
            else if (cmd is CreateDirectoryCommand createDirectoryCommand)
            {
                CreateDirectory(createDirectoryCommand.Path);
                ListContent(_currentDirectory, 1);
            }
            else if (cmd is CreateFileCommand createFileCommand)
            {
                CreateFile(createFileCommand.Path);
                ListContent(_currentDirectory, 1);
            }
            else if (cmd is InfoCommand infoCommand)
            {
                var info = Info(infoCommand.Path);
                _viewData.FileSystemItemInfo = info;
            }
            else if (cmd is MoveCommand moveCommand)
            {
                Move(moveCommand.Path, moveCommand.NewName);
                ListContent(_currentDirectory, 1);
            }
            else if (cmd is FindCommand findCommand)
            {
                Find(findCommand.Filter);
            }
            else if (cmd is SetAttributeCommand setAttributeCommand)
            {
                SetAttribute(setAttributeCommand.Path, setAttributeCommand.Attribute);
                Execute(new InfoCommand(setAttributeCommand.Path));
            }
            else
            {
                ListContent(_currentDirectory);
            }
            _view.Display(_viewData);
        }

        private void SetAttribute(string path, int attribute)
        {
            _diskOperations.SetAttribute(path, (FileAttributes)attribute);
        }

        private void Find(string filter)
        {
            _viewData.DirectoryListing = _diskOperations.Find(_currentDirectory,filter);
        }

        private void Move(string path, string newName)
        {
            _diskOperations.Move(path, newName);
        }

        private IFileSystemItemInfo Info(string path)
        {
            return _diskOperations.GetItemInfo(path);
        }

        private void CreateFile(string path)
        {
            _diskOperations.CreateFile(path);
        }

        private void CreateDirectory(string path)
        {
            _diskOperations.CreateDirectory(path);
        }

        private void Copy(string source, string destination)
        {
            _diskOperations.Copy(source, destination);
        }

        private void ChangeDirectory(string path)
        {
            string fullPath = _diskOperations.GetFullPath(path);
            _currentDirectory = fullPath;
            _diskOperations.ChangeDirectory(_currentDirectory);
        }

        private void ListContent(string path, int page=1)
        {
            string fullPath = _diskOperations.GetFullPath(path);
            _viewData.Path = fullPath;
            _viewData.DirectoryListing = _diskOperations.GetFolderContent(fullPath)
                                                        .Skip(_pageSize*(page-1))
                                                        .Take(_pageSize)
                                                        .ToArray();
        }
    }
}