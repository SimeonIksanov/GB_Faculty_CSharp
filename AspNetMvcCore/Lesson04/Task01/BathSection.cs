using System;
using System.Collections.Generic;

namespace Task01
{
    public class BathSection
    {
        public List<BathProduct> Products { get; set; }
    }

    public class BathProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
    }
}
