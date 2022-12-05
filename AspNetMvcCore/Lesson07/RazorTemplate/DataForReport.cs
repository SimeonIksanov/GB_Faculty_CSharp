using System.Collections.Generic;
using System.Linq;

namespace RazorTemplate
{
    public class DataForReport
    {
        public IEnumerable<Employee> Employees { get; set; }
        public string CompanyName { get; set; }

        public DataForReport()
        {
            CompanyName = "LLC Coca-Cola";
            Employees = Enumerable
                .Range(1, 5)
                .Select(i => new Employee
                {
                    Name = $"Firstname{i}",
                    Lastname = $"Lastname{i}",
                    Grade = (byte)(i + 5)
                });
        }
    }
}
