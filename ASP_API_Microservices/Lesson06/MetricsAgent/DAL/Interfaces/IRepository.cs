using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        public IList<T> GetAll();

        public T GetById(int id);

        public void Create(T item);

        public void Update(T item);

        public void Delete(int id);

        public IList<T> GetByTimePeriod(DateTime from, DateTime to);
    }
}
