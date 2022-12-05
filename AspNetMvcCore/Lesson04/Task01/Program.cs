using System;
using System.Collections.Generic;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {

            var bath = new BathSection();
            bath.Products = new List<BathProduct>()
            {
                new BathProduct
                {
                    Name="toothBrush",
                    Color="red",
                    Price = 10,
                }
            };

            var meat = new MeatSection();
            meat.Goods = new List<MeatProduct>()
            {
                new MeatProduct
                {
                    Name="shahslik",
                    Price=99,
                    Type= MeatType.Beef,
                }
            };

            var mm = new MegaMarket(bath, meat);
            foreach (var item in mm)
            {
                Console.WriteLine(item);
            }
        }
    }
}
