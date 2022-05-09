using System;
using System.Collections.Generic;

namespace Task01
{
    public class MeatSection
    {
        public List<MeatProduct> Goods { get; set; }
    }

    public class MeatProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public MeatType Type { get; set; }
    }

    public enum MeatType
    {
        Beef,
        Mutton,
        Chicken
    }
}
