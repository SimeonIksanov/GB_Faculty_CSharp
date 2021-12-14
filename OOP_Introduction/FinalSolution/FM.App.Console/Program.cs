using FileSystemLib;
using FM.Core.Controllers;
using FM.Core.Models;
using FM.Core.Models.Commands;

namespace FM.App.Cmd
{
    class MyClass
    {
        public static void Main()
        {
            SetConsoleWindowsSize();

            IConfiguration config = new Configuration();

            UserCommand userCommand;

            var controller = new Controller(
                view: new UI(),
                diskOperation: new DiskOperations(),
                pageSize: 15
                );
            
            controller.Execute(null);// Draw UI for first time
            
            while (true)
            {
                userCommand = ReadCommand();
                controller.Execute(userCommand);
            }
        }

        private static void SetConsoleWindowsSize()
        {
            if (Environment.OSVersion.Platform.ToString().StartsWith("Win32NT"))
            {
                Console.SetWindowSize(140, 35);
            }
        }
        private static UserCommand ReadCommand()
        {
            string input = String.Empty;

            input = Console.ReadLine();
            var cmd = ParseCommand(input);
            return cmd;
        }
        private static UserCommand ParseCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            string[] splittedInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            splittedInput[0] = splittedInput[0].ToLower();
            if (splittedInput[0] == "ls")
            {
                if (splittedInput.Length == 4 && splittedInput[2] == "-p" && Int32.TryParse(splittedInput[3], out int page))
                {
                    return new ListCommand( splittedInput[1], page);
                }
                else if (splittedInput.Length == 2)
                {
                    return new ListCommand( splittedInput[1], 1);
                }
                else if (splittedInput.Length == 1)
                {
                    return new ListCommand(".", 1);
                }
            }
            else if (splittedInput[0] == "exit")
            {
                return new ExitCommand();
            }
            else if(splittedInput[0] == "copy")
            {
                if (splittedInput.Length == 3)
                {
                    return new CopyCommand( splittedInput[1], splittedInput[2] );
                }
            }
            else if(splittedInput[0] == "delete")
            {
                if (splittedInput.Length == 2)
                {
                    return new DeleteCommand(splittedInput[1]);
                }
            }
            else if(splittedInput[0] == "cd")
            {
                if (splittedInput.Length == 2)
                {
                    return new CdCommand(splittedInput[1]);
                }
            }
            else if(splittedInput[0] == "mkdir")
            {
                if (splittedInput.Length == 2)
                {
                    return new CreateDirectoryCommand(splittedInput[1]);
                }
            }
            else if(splittedInput[0] == "touch")
            {
                if (splittedInput.Length == 2)
                {
                    return new CreateFileCommand(splittedInput[1]);
                }
            }
            else if(splittedInput[0]== "info")
            {
                if (splittedInput.Length==2)
                {
                    return new InfoCommand(splittedInput[1] );
                }
            }
            else if(splittedInput[0]=="rename" || splittedInput[0] == "move")
            {
                if (splittedInput.Length == 3)
                {
                    return new MoveCommand(splittedInput[1],splittedInput[2]);
                }
            }
            else if (splittedInput[0] == "find")
            {
                if (splittedInput.Length == 2)
                {
                    return new FindCommand(splittedInput[1]);
                }
            }
            else if(splittedInput[0]== "setattr")
            {
                if (splittedInput.Length==3 && Int32.TryParse(splittedInput[2],out int attr))
                {
                    return new SetAttributeCommand(splittedInput[1], attr );
                }
            }

            return null;
        }
    }
}