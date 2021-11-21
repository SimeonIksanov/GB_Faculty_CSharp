using System;
using BuildingLib;

namespace BuldingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Homework for lesson 04");

            Creator.CreateBuiling();
            Creator.CreateBuiling(5);
            Creator.CreateBuiling(5, 3);

            Console.WriteLine($"Построено {Creator.AllBuildings.Count} дома");

            foreach (var b in Creator.AllBuildings.Values)
            {
                Console.WriteLine(b);
            }

            Creator.Remove(2);
            Console.WriteLine($"Одно здание рухнуло, осталось {Creator.AllBuildings.Count}");
        }
    }
}
