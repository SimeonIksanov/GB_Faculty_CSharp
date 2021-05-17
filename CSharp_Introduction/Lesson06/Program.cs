using System;
using System.Diagnostics;

namespace Lesson06
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Написать консольное приложение Task Manager,
             * которое выводит на экран запущенные процессы
             * и позволяет завершить указанный процесс. Предусмотреть 
             * возможность завершения процессов с помощью указания 
             * его ID или имени процесса.
             */

            if (args.Length == 1 && args[0].ToLower() == "list")
            {
                TaskManager.ShowProcesses();
            }
            else if (args.Length == 2 && args[0].ToLower() == "kill")
            {
                if (Int32.TryParse(args[1], out int id))
                {
                    TaskManager.Kill(id);
                }
                else
                {
                    TaskManager.Kill(args[1]);
                }
            }
            else
                ShowHelpMessage();
        }

        static void ShowHelpMessage()
        {
            Console.WriteLine("Command usage:");
            Console.WriteLine("\tlist - show running processes");
            Console.WriteLine("\tkill <id/name> - kill process with <id/name>");
        }
    }

    class TaskManager
    {
        public static void ShowProcesses()
        {
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                Console.WriteLine($"{process.Id} - {process.ProcessName}");
            }
        }

        public static void Kill(int id)
        {
            try
            {
                Process process = Process.GetProcessById(id);
                Kill(process);
            }
            catch (ArgumentException ex) //process not found
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Kill(string name)
        {
            try
            {
                Process[] process = Process.GetProcessesByName(name);
                Kill(process);
            }
            catch (ArgumentException ex) //process not found
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Kill(params Process[] processes)
        {
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(process.ProcessName, " : ", ex.Message);
                }
            }
        }
    }
}
