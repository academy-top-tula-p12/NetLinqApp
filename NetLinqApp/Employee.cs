using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Salary: {Salary}";
        }
    }

    class Department
    {
        public string Title { set; get; }
        public List<Employee> Employees { set; get; } = new();
    }
    class Manager : Employee { }
    class Developer : Employee { }

    class User
    {
        public string? Name { set; get; }
        public int Age { set; get; }
        public Company? Company { set; get; }
    }

    class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            return x?.Name == y?.Name 
                && x?.Age == y?.Age 
                && x?.Company?.Title == y?.Company?.Title;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    class Company
    {
        public string? Title { set; get; }
        public string? City { set; get; }
        public override string ToString()
        {
            return Title;
        }
    }
}
