using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Lesson05
{
    class Program
    {
        static void Main(string[] args)
        {
            Task01();
            Task02();
            Task03();
            Task04();
            Task05();
        }

        static void Task01()
        {
            //Ввести с клавиатуры произвольный набор данных и сохранить его в текстовый файл
            Console.Write("Введите произвольный набор данных: ");
            string inputLine = Console.ReadLine();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "sampleFile.txt");
            File.AppendAllText(filePath, inputLine);
        }

        static void Task02()
        {
            //Написать программу, которая при старте дописывает текущее время в файл «startup.txt».
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "startup.txt");
            File.AppendAllText(filePath, DateTime.Now.ToString());
        }

        static void Task03()
        {
            //Ввести с клавиатуры произвольный набор чисел (0...255) и записать их в бинарный файл.
            Console.Write("Ввести с клавиатуры произвольный набор чисел (0...255) через пробел: ");
            string bytesInString = Console.ReadLine();
            byte[] bytes = bytesInString.Split(' ').Select(Byte.Parse).ToArray();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetRandomFileName());
            File.WriteAllBytes(filePath, bytes);
        }

        static void Task04()
        {
            //Сохранить дерево каталогов и файлов по заданному пути в текстовый файл — с рекурсией и без.
            var path = Path.GetFullPath(Path.Combine("..", ".."));

            Console.WriteLine("### Non-reqursive way ###");
            Console.WriteLine(string.Join(Environment.NewLine, Task04_1(path)));
            Console.WriteLine("### end Non-reqursive way ###");

            Console.WriteLine();

            Console.WriteLine("### Reqursive way ###");
            List<string> tree = new List<string>();
            Task04_2(path, tree);
            Console.WriteLine(string.Join(Environment.NewLine, tree));
            Console.WriteLine("### end Reqursive way ###");
        }

        static string[] Task04_1(string path)
        {
            //string fullPath = Path.GetFullPath(path);
            return Directory.GetFileSystemEntries(
                path: path,
                searchPattern: "*",
                enumerationOptions: new EnumerationOptions()
                {
                    RecurseSubdirectories = true,
                    ReturnSpecialDirectories = false
                });
        }

        static void Task04_2(string path, List<string> result)
        {
            result.AddRange(Directory.GetFiles(path));

            foreach (var dir in Directory.GetDirectories(path))
            {
                result.Add(dir);
                Task04_2(dir, result);
            }
        }

        static void Task05()
        {
            //Список задач (ToDo-list)
            new ToDoList().Run();
        }
    }

    class ToDoList
    {
        private List<ToDo> todoList;

        public void Run()
        {
            Load();

            ShowMainMenu();

            Save();
        }

        private void Load(string filename = "tasks.json")
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                todoList = JsonSerializer.Deserialize<List<ToDo>>(json);
            }
            else
            {
                todoList = new List<ToDo>();
            }
        }

        private void Save(string filename = "tasks.json")
        {
            string json = JsonSerializer.Serialize(todoList);
            File.WriteAllText(filename, json);
        }

        private void SetDone(int n)
        {
            n--;
            if (n >= 0 && n < todoList.Count)
                todoList[n].IsDone = true;
        }

        private void AddNew(string title) => todoList.Add(new ToDo(title));

        private void ShowList()
        {
            for (int i = 0; i < todoList.Count; i++)
            {
                Console.WriteLine("{0}) {1}", i + 1, todoList[i]);
            }
        }

        private void ShowMainMenu()
        {
            int userChoice;
            while (true)
            {
                userChoice = int.MinValue;
                Console.Clear();

                Console.WriteLine("Перечень текущих дел:");
                ShowList();
                Console.WriteLine();
                Console.WriteLine("Введите порядковый номер задачи, чтобы пометить как выполнено, '0' для ввода новой задачи, '-1' для выхода: ");
                userChoice = ReadInt();

                switch (userChoice)
                {
                    case 0: { ShowCreateMenu(); break; }
                    case -1: return;
                    default: { SetDone(userChoice); break; }
                }
            }
        }

        private int ReadInt()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        private void ShowCreateMenu()
        {
            Console.WriteLine("Введите текст задачи: ");
            string title = Console.ReadLine();
            AddNew(title);
        }
    }

    class ToDo
    {
        public ToDo()
        {
        }

        public ToDo(string title)
        {
            Title = title;
            IsDone = false;
        }

        public string Title { get; set; }
        public bool IsDone { get; set; }

        public override string ToString() => (IsDone ? "[x]" : "[ ]") + Title;
    }
}
