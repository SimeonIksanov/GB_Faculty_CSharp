using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FileManager
{
    public class FMApp
    {
        static int pageSize = 15;
        const int width = 130;
        static string currentDirectory = FMLib.GetCurDir();
        static string currentInfoItem = FMLib.GetCurDir();
        static int page = 1;
        static string appDirectory = FMLib.GetCurDir();
        static List<string> history = new List<string>(); // saved commands typed by user

        public static void Run()
        {
            LoadSettings();

            string command = null;
            while (command != "exit")
            {
                Console.Clear();

                ContentPanel content = new ContentPanel();
                try
                {   //по идее это надо заталкать в конструктор, но мы их еще не проходили, поэтому оставлю тут
                    content.folderPath = FMLib.GetFullPath(currentDirectory);
                    content.folderContent = FormatDirectoryListing(content.folderPath, page);
                }
                catch (Exception ex)
                {
                    content.folderContent = new List<string>();
                    WriteEvent(ex.Message);
                }

                InfoPanel info = new InfoPanel() { path = currentInfoItem };
                try
                {   //и это тоже
                    info.attr = FMLib.GetAttributes(info.path);
                    info.dataTimes = FMLib.GetTimes(info.path);
                    info.size = FMLib.GetSizeOnDisk(info.path);
                }
                catch
                {
                    info = null;
                }

                ShowGUI(content, info);
                //command = Console.ReadLine();
                command = ReadCommand();
                ParseCommand(command);
            }

            SaveSettings();
        }

        private static void SaveSettings()
        {
            var roaming = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = roaming.FilePath };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (config.HasFile)
            {
                if (config.AppSettings.Settings.AllKeys.Contains("currentDirectory"))
                    config.AppSettings.Settings["currentDirectory"].Value = currentDirectory;
                else
                    config.AppSettings.Settings.Add("currentDirectory", currentDirectory);

                if (config.AppSettings.Settings.AllKeys.Contains("currentInfoItem"))
                    config.AppSettings.Settings["currentInfoItem"].Value = currentInfoItem;
                else
                    config.AppSettings.Settings.Add("currentInfoItem", currentInfoItem);

                if (config.AppSettings.Settings.AllKeys.Contains("page"))
                    config.AppSettings.Settings["page"].Value = page.ToString();
                else
                    config.AppSettings.Settings.Add("page", page.ToString());
            }
            else
            {
                config.AppSettings.Settings.Add("currentDirectory", currentDirectory);
                config.AppSettings.Settings.Add("currentInfoItem", currentInfoItem);
                config.AppSettings.Settings.Add("page", page.ToString());
            }
            config.Save();
        }

        private static void LoadSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.HasFile)
            {
                pageSize = Convert.ToInt32(config.AppSettings.Settings["pageSize"].Value);
            }
            var roaming = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = roaming.FilePath };
            config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (config.HasFile)
            {
                if (config.AppSettings.Settings.AllKeys.Contains("currentDirectory")
                    && FMLib.isDirectoryExist(config.AppSettings.Settings["currentDirectory"].Value))
                {
                    currentDirectory = config.AppSettings.Settings["currentDirectory"].Value;
                    FMLib.ChangeDirectory(config.AppSettings.Settings["currentDirectory"].Value);
                }

                if (config.AppSettings.Settings.AllKeys.Contains("currentInfoItem"))
                    currentInfoItem = config.AppSettings.Settings["currentInfoItem"].Value;

                if (config.AppSettings.Settings.AllKeys.Contains("page"))
                    page = Convert.ToInt32(config.AppSettings.Settings["page"].Value);
            }
        }

        public static List<string> FormatDirectoryListing(string path, int page = 1)
        {
            List<string> list = new List<string>();
            foreach (var item in FMLib.GetFolderContent(path))
            {
                if (item.Name == ".") continue;
                if ((item.Attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
                    list.Add("[" + item.Name + "]");
                else
                    list.Add(item.Name);
            }
            list.Sort();

            if (page - 1 > list.Count / pageSize)
                page = 1;
            int start = (page - 1) * pageSize;
            int end = Math.Min(page * pageSize, list.Count);

            var retValue = new List<string>();
            for (int i = start; i < end; i++)
            {
                retValue.Add(list[i]);
            }
            return retValue;
        }

        public static void ShowGUI(ContentPanel cp, InfoPanel ip)
        {
            //const int width = 130;
            string hBorder = "+" + new string('-', width) + "+";
            string vBorder = "|" + new string(' ', width) + "|";

            Console.WriteLine(hBorder);
            Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);

            for (int i = 0; i < pageSize + 1; i++)
                Console.WriteLine(vBorder);

            Console.WriteLine(hBorder);
            Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);
            for (int i = 0; i < 5; i++)
                Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);
            Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);

            // Fill panels
            var bottom = Console.GetCursorPosition();
            if (cp != null)
            {
                Console.SetCursorPosition(left: 4, top: 1); Console.Write(cp.folderPath);
                for (int i = 0; i < cp.folderContent.Count; i++)
                {
                    Console.SetCursorPosition(left: 4, top: 3 + i);
                    Console.Write(cp.folderContent[i]);
                }
            }

            Console.SetCursorPosition(left: 4, top: bottom.Top - 10);
            Console.Write("Information:");

            if (ip != null)
            {
                Console.SetCursorPosition(left: 4, top: bottom.Top - 8);
                Console.Write("Item: {0}", ip.path);
                Console.SetCursorPosition(left: 4, top: bottom.Top - 7);
                Console.Write("CreationTime: {0}", ip.dataTimes.creationTime);
                Console.SetCursorPosition(left: 4, top: bottom.Top - 6);
                Console.Write("LastAccessTime: {0}\tLastWriteTime: {1}", ip.dataTimes.lastAccessTime, ip.dataTimes.lastWriteTime);
                Console.SetCursorPosition(left: 4, top: bottom.Top - 5);
                Console.Write("Attributes: {0}", ip.attr);
                Console.SetCursorPosition(left: 4, top: bottom.Top - 4);
                Console.Write("Size in bytes: {0}", ip.size);

                Console.SetCursorPosition(left: 0, top: bottom.Top);
            }

            Console.SetCursorPosition(left: 4, top: bottom.Top - 2);
            Console.Write("> ");
        }

        public static void ParseCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return;

            string[] commandParts = command.Split(' ');
            if (commandParts.Length >= 2 && commandParts[0].ToLower() == "ls" && FMLib.isDirectoryExist(commandParts[1]))
            {
                currentDirectory = FMLib.GetFullPath(commandParts[1]);
                if (commandParts.Length == 4 && commandParts[2] == "-p" && Int32.TryParse(commandParts[3], out int p))
                    page = p;
                else page = 1;
            }
            if (commandParts.Length >= 2 && commandParts[0].ToLower() == "cd" && FMLib.isDirectoryExist(commandParts[1]))
            {
                currentDirectory = FMLib.GetFullPath(commandParts[1]);
                FMLib.ChangeDirectory(FMLib.GetFullPath(commandParts[1]));
                page = 1;
            }
            if (commandParts.Length == 2 && commandParts[0].ToLower() == "info")
            {
                currentInfoItem = FMLib.GetFullPath(commandParts[1]);
            }
            if (commandParts.Length == 3 && commandParts[0].ToLower() == "copy")
            {
                try
                {
                    FMLib.Copy(commandParts[1], commandParts[2]);
                }
                catch (Exception ex)
                {
                    WriteEvent(ex.Message);
                }
            }
            if (commandParts.Length == 2 && commandParts[0].ToLower() == "delete")
            {
                try
                {
                    FMLib.Delete(commandParts[1]);
                }
                catch (Exception ex)
                {
                    WriteEvent(ex.Message);
                }
            }
        }

        public static void WriteEvent(string message)
        {
            var errorDir = Path.Combine(appDirectory, "errors");
            Directory.CreateDirectory(errorDir);
            File.AppendAllText(Path.Combine(errorDir, "exceptions.txt"), message + "\n");
        }

        public static string ReadCommand()
        {
            //return Console.ReadLine();

            (int Left, int Top) initialCursorPosition = Console.GetCursorPosition();
            StringBuilder command = new StringBuilder();
            int historyIndex = history.Count - 1;

            while (true)
            {
                ConsoleKeyInfo KeyInfoPressed = Console.ReadKey(true);

                switch (KeyInfoPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (historyIndex >= 0 && historyIndex < history.Count)
                        {
                            command.Clear();
                            command.Append(history.ElementAt(historyIndex));
                            if (historyIndex > 0)
                                historyIndex--;
                            ClearConsole(command.ToString(), initialCursorPosition);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (historyIndex >= 0 && historyIndex < history.Count)
                        {
                            command.Clear();
                            command.Append(history.ElementAt(historyIndex));
                            if (historyIndex < history.Count - 1)
                                historyIndex++;
                            ClearConsole(command.ToString(), initialCursorPosition);
                        }

                        break;

                    case ConsoleKey.LeftArrow:
                        if (Console.CursorLeft > initialCursorPosition.Left)
                        {
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (Console.CursorLeft < initialCursorPosition.Left + command.Length)
                        {
                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        }
                        break;

                    case ConsoleKey.Backspace:
                        if (Console.CursorLeft > initialCursorPosition.Left)
                        {
                            int l = Console.CursorLeft - 1;
                            command = command.Remove(Console.CursorLeft - initialCursorPosition.Left - 1, 1);
                            ClearConsole(command.ToString(), initialCursorPosition);
                            Console.CursorLeft = l;
                        }
                        break;

                    case ConsoleKey.Delete:
                        if (Console.CursorLeft < command.Length)
                        {
                            int l = Console.CursorLeft;
                            command = command.Remove(Console.CursorLeft - initialCursorPosition.Left, 1);
                            ClearConsole(command.ToString(), initialCursorPosition);
                            Console.CursorLeft = l;
                        }
                        break;

                    default:
                        command.Append(KeyInfoPressed.KeyChar.ToString());
                        ClearConsole(command.ToString(), initialCursorPosition);
                        break;

                    case ConsoleKey.Enter:
                        history.Add(command.ToString());
                        return command.ToString();
                }
            }
        }

        private static void ClearConsole(string str, (int Left, int Top) initialCursorPosition)
        {
            Console.SetCursorPosition(initialCursorPosition.Left, initialCursorPosition.Top);
            Console.Write(new String(' ', width - 2 - initialCursorPosition.Left));
            Console.SetCursorPosition(initialCursorPosition.Left, initialCursorPosition.Top);
            Console.Write(str);
        }
    }

    public class ContentPanel
    {
        public string folderPath;
        public List<string> folderContent;
    }

    public class InfoPanel
    {
        public string path;
        public (DateTime creationTime, DateTime lastAccessTime, DateTime lastWriteTime) dataTimes;
        public FileAttributes attr;
        public ulong size;
    }
}
