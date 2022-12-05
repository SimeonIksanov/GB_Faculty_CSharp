using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Task01
{
    public class MegaMarket : IEnumerable<IUnifiedProducts>
    {
        private readonly BathSection _bathSection;
        private readonly MeatSection _meatSection;

        public MegaMarket(
            BathSection bathSection,
            MeatSection meatSection)
        {
            _bathSection = bathSection;
            _meatSection = meatSection;
        }

        public IEnumerator<IUnifiedProducts> GetEnumerator()
        {
            foreach (var item in _bathSection.Products)
            {
                yield return new UnifiedProducts
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = string.Format("Color: {0}", item.Color),
                };
            }

            foreach (var item in _meatSection.Goods)
            {
                yield return new UnifiedProducts
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = string.Format("Meat: {0}", item.Type.ToString()),
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface IUnifiedProducts
    {
        string Name { get; set; }
        double Price { get; set; }
        string Description { get; set; }
    }

    public class UnifiedProducts : IUnifiedProducts
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}\nPrice: {1}\nDesc: {2}", Name, Price.ToString(), Description);
        }
    }
}
