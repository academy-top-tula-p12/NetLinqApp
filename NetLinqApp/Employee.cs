using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqApp
{
    internal class Employee
    {
        public string Name { set; get; }
        public int Age { set; get; }
        public decimal Salary { set; get; }

        public override bool Equals(object? obj)
        {
            if (obj is Employee empl)
                return this.Name == empl.Name;
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    class Department
    {
        public string Title { set; get; }
        public List<Employee> Employees { set; get; } = new();
    }

    class Manager : Employee { }
    class Developer : Employee { }
}
