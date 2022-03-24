using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_list
{
    internal class MyList<T> : List<T> , IList<T>
    {
        public new void Add(T item)
        {
            lock (this)
            {
                base.Add(item);
            }
        }

        public new void Remove(T item)
        {
            lock (this)
            {
                base.Remove(item);
            }
        }
    }
}
