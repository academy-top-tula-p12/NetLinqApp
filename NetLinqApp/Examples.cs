using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetLinqApp
{
    class StringLengthComparer : IComparer<String>
    {
        public int Compare(string? x, string? y)
        {
            int xLen = x?.Length ?? 0;
            int yLen = y?.Length ?? 0;
            return xLen - yLen;
        }
    }
    static class Examples
    {
        static public void Welcome()
        {
            string[] employees = { "Bob", "Sam", "Ben", "Jim", "Tom", "Joe" };

            List<string> selectedEmployees = new List<string>();

            foreach (var e in employees)
            {
                if (e.ToUpper().StartsWith("B"))
                    selectedEmployees.Add(e);
            }

            selectedEmployees.Sort();

            foreach (var e in selectedEmployees)
                Console.WriteLine(e);

            // query
            var queryEmployees = from e in employees
                                 where e.ToUpper().StartsWith("B")
                                 orderby e
                                 select e;

            foreach (var e in queryEmployees)
                Console.WriteLine(e);

            // methods
            var methodsEmployees = employees.Where(e => e.ToUpper().StartsWith("B"))
                                            .OrderBy(e => e);

            foreach (var e in methodsEmployees)
                Console.WriteLine(e);
        }

        static public void BaseSelect()
        {
            var employees = new List<Employee>()
            {
                new(){ Name = "Bob", Age = 25, Salary = 60000 },
                new(){ Name = "Tom", Age = 34, Salary = 70000 },
                new(){ Name = "Jim", Age = 19, Salary = 65000 },
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 },
                new(){ Name = "Ken", Age = 40, Salary = 40000 },
            };

            List<string> projects = new() { "DataBase Framework", "Mobile App", "Printer driver" };

            List<Department> departmets = new()
            {
                new()
                {
                    Title = "Managers",
                    Employees = new()
                    {
                        new(){ Name = "Bob", Age = 25, Salary = 60000 },
                        new(){ Name = "Tom", Age = 34, Salary = 70000 },
                        new(){ Name = "Jim", Age = 19, Salary = 65000 },
                    }
                },
                new()
                {
                    Title = "Developers",
                    Employees = new()
                    {
                        new(){ Name = "Sam", Age = 29, Salary = 50000 },
                        new(){ Name = "Leo", Age = 31, Salary = 100000 },
                        new(){ Name = "Ken", Age = 40, Salary = 40000 },
                    }
                }
            };






            // query
            var names = from e in employees
                        select e.Name;

            foreach (var n in names)
                Console.WriteLine(n);
            Console.WriteLine();

            // methods
            var names2 = employees.Select(e => e.Name);
            foreach (var n in names2)
                Console.WriteLine(n);
            Console.WriteLine();


            // select anonim objects
            // query
            var qAnonim = from e in employees
                          select new
                          {
                              FirstName = e.Name,
                              //Year = DateTime.Now.AddYears(-e.Age)
                              Year = DateTime.Now.Year - e.Age
                          };

            foreach (var e in qAnonim)
                Console.WriteLine($"Name: {e.FirstName}, Year: {e.Year}");
            Console.WriteLine();

            // methods
            var mAnonim = employees.Select(e => new
            {
                FirstName = e.Name,
                Year = DateTime.Now.Year - e.Age
            });

            foreach (var e in mAnonim)
                Console.WriteLine($"Name: {e.FirstName}, Year: {e.Year}");
            Console.WriteLine();


            // decart values
            // query

            var qDecart = from e in employees
                          from p in projects
                          select new
                          {
                              Employee = e.Name,
                              Project = p
                          };

            foreach (var o in qDecart)
                Console.WriteLine($"Employee: {o.Employee}, Project: {o.Project}");
            Console.WriteLine();


            // union departments
            // query

            var qUnion = from d in departmets
                         from empls in d.Employees
                         select empls;

            foreach (var e in qUnion)
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            Console.WriteLine();


            // methods

            var mUnion = departmets.SelectMany(d => d.Employees);
            foreach (var e in mUnion)
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            Console.WriteLine();


            // union departs with title
            // query

            var qUnionT = from d in departmets
                          from empl in d.Employees
                          select new
                          {
                              FirstName = empl.Name,
                              Depart = d.Title
                          };

            foreach (var e in qUnionT)
                Console.WriteLine($"Name: {e.FirstName}, Depart: {e.Depart}");
            Console.WriteLine();

            // methods
            var mUnionT = departmets.SelectMany(
                d => d.Employees,
                (d, empl) => new
                {
                    FirstName = empl.Name,
                    Depart = d.Title
                }
                );


            foreach (var e in mUnionT)
                Console.WriteLine($"Name: {e.FirstName}, Depart: {e.Depart}");
            Console.WriteLine();
        }

        static public void Filter()
        {
            var employees = new List<Employee>()
            {
                new(){ Name = "Bob", Age = 25, Salary = 60000 },
                new(){ Name = "Tom", Age = 34, Salary = 70000 },
                new(){ Name = "Jim", Age = 19, Salary = 65000 },
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 },
                new(){ Name = "Ken", Age = 40, Salary = 40000 },
            };

            List<string> projects = new() { "DataBase Framework", "Mobile App", "Printer driver" };

            List<Department> departmets = new()
            {
                new()
                {
                    Title = "Managers",
                    Employees = new()
                    {
                        new(){ Name = "Bob", Age = 25, Salary = 60000 },
                        new(){ Name = "Tom", Age = 34, Salary = 70000 },
                        new(){ Name = "Jim", Age = 19, Salary = 65000 },
                    }
                },
                new()
                {
                    Title = "Developers",
                    Employees = new()
                    {
                        new(){ Name = "Sam", Age = 29, Salary = 50000 },
                        new(){ Name = "Leo", Age = 31, Salary = 100000 },
                        new(){ Name = "Ken", Age = 40, Salary = 40000 },
                        new(){ Name = "Joe", Age = 26, Salary = 80000 },
                    }
                }
            };

            List<Employee> empls = new()
            {
                new Developer(){ Name = "Developer1" },
                new Employee(){ Name = "Emploee" },
                new Manager(){ Name = "Manager" },
                new Developer(){ Name = "Developer2" },
            };


            // select age
            // query

            var qAge = from e in employees
                       where e.Age <= 30
                       select e;
            foreach (var e in qAge)
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            Console.WriteLine();

            // methods
            var mAge = employees.Where(e => e.Age <= 30);
            foreach (var e in mAge)
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            Console.WriteLine();


            // select big departs and age
            // query

            var qDepAge = from d in departmets
                          from e in d.Employees
                          where d.Employees.Count > 2
                          where e.Age <= 30
                          select e;

            foreach (var e in qDepAge)
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            Console.WriteLine();

            // methods
            var mDepAge = departmets.SelectMany(
                                        d => d.Employees,
                                        (d, e) => new { dep = d, empl = e })
                                    .Where(newc => newc.dep.Employees.Count > 2
                                                    && newc.empl.Age <= 30)
                                    .Select(newc => newc.empl);
            foreach (var e in mDepAge)
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            Console.WriteLine();

            // select of type
            var devops = empls.OfType<Employee>();

            foreach (var dev in devops)
                Console.WriteLine($"Name: {dev.Name}");
            Console.WriteLine();
        }

        static public void Orders()
        {
            var employees = new List<Employee>()
            {
                new(){ Name = "Bob", Age = 25, Salary = 60000 },
                new(){ Name = "Tom", Age = 34, Salary = 70000 },
                new(){ Name = "Jim", Age = 19, Salary = 65000 },
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 },
                new(){ Name = "Jim", Age = 39, Salary = 85000 },
                new(){ Name = "Ken", Age = 40, Salary = 40000 },
                new(){ Name = "Sam", Age = 18, Salary = 55000 }
            };

            List<string> projects = new() { "Mobile App", "DataBase Framework", "Printer driver", };


            // sort by name
            // query

            var qSortName = from e in employees
                                //orderby e.Name
                                //orderby e.Name descending
                            orderby e.Name descending, e.Age
                            select e;
            foreach (var e in qSortName)
                Console.WriteLine($"Name {e.Name}, Age: {e.Age}");
            Console.WriteLine();

            // method

            //var mSortAge = employees.OrderBy(e => e.Age);
            //var mSortAge = employees.OrderByDescending(e => e.Age);
            var mSortAge = employees.OrderByDescending(e => e.Name).ThenBy(e => e.Age);
            foreach (var e in mSortAge)
                Console.WriteLine($"Name {e.Name}, Age: {e.Age}");
            Console.WriteLine();

            // sort other critery
            // method

            foreach (var p in projects)
                Console.WriteLine(p);
            Console.WriteLine();

            var pAlpha = projects.OrderBy(p => p);
            foreach (var p in pAlpha)
                Console.WriteLine(p);
            Console.WriteLine();

            var pLength = projects.OrderBy(p => p, new StringLengthComparer());
            foreach (var p in pLength)
                Console.WriteLine(p);
            Console.WriteLine();
        }

        static public void SetOpers()
        {
            var yandex = new List<Employee>()
            {
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 },
                new(){ Name = "Joe", Age = 39, Salary = 85000 },
                new(){ Name = "Ken", Age = 40, Salary = 40000 },
                new(){ Name = "Ben", Age = 18, Salary = 55000 },
            };

                        var ozon = new List<Employee>()
            {
                new(){ Name = "Bob", Age = 25, Salary = 60000 },
                new(){ Name = "Tom", Age = 34, Salary = 70000 },
                new(){ Name = "Jim", Age = 19, Salary = 65000 },
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 }
            };

            var union = yandex.Union(ozon);

            foreach (var e in union)
                Console.WriteLine($"Name {e.Name}, Age: {e.Age}, Salary: {e.Salary}");
            Console.WriteLine();


            var inters = yandex.Intersect(ozon);
            foreach (var e in inters)
                Console.WriteLine($"Name {e.Name}, Age: {e.Age}, Salary: {e.Salary}");
            Console.WriteLine();

            var except = yandex.Except(ozon);
            foreach (var e in except)
                Console.WriteLine($"Name {e.Name}, Age: {e.Age}, Salary: {e.Salary}");
            Console.WriteLine();
        }

        static public void Agregates()
        {
            var employees = new List<Employee>()
            {
                new(){ Name = "Bob", Age = 25, Salary = 60000 },
                new(){ Name = "Tom", Age = 34, Salary = 70000 },
                new(){ Name = "Jim", Age = 19, Salary = 65000 },
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 },
                new(){ Name = "Ken", Age = 40, Salary = 40000 },
            };

            List<string> projects = new() { "DataBase Framework", "Mobile App", "Printer driver" };

            //int count = employees.Where(e => e.Age >= 30).Count();
            int count = employees.Count(e => e.Age >= 30);
            Console.WriteLine(count);

            Console.WriteLine(employees.Min(e => e.Age));
            Console.WriteLine(employees.Min(e => e.Salary));
            Console.WriteLine(employees.Max(e => e.Age));

            Console.WriteLine(employees.Average(e => e.Salary));

            Console.WriteLine(employees.Sum(e => e.Salary));

            var result = employees.Aggregate(100000M, (acc, next) => acc - next.Salary);
            Console.WriteLine(result);

            var longTitle = projects.Aggregate("", (res, next) => (res.Length < next.Length) ? next : res);
            Console.WriteLine(longTitle);
        }

        static public void SkipTake()
        {
            var employees = new List<Employee>()
            {
                new(){ Name = "Bob", Age = 25, Salary = 60000 },
                new(){ Name = "Tom", Age = 34, Salary = 70000 },
                new(){ Name = "Jim", Age = 19, Salary = 65000 },
                new(){ Name = "Sam", Age = 29, Salary = 50000 },
                new(){ Name = "Leo", Age = 31, Salary = 100000 },
                new(){ Name = "Ken", Age = 40, Salary = 40000 },
                new(){ Name = "Ben", Age = 33, Salary = 60000 },
                new(){ Name = "Joe", Age = 28, Salary = 70000 },
                new(){ Name = "Sid", Age = 17, Salary = 65000 },
                new(){ Name = "Max", Age = 42, Salary = 50000 },
                new(){ Name = "Poul", Age = 29, Salary = 100000 },
                new(){ Name = "Ann", Age = 36, Salary = 40000 },
            };

            List<string> projects = new() { "DataBase Framework", "Mobile App", "Printer driver" };


            var emplSkip3 = employees.Skip(2);
            foreach (var e in emplSkip3)
                Console.WriteLine(e);
            Console.WriteLine();

            var emplWhile50 = employees.SkipWhile(e => e.Salary > 50000);
            foreach (var e in emplWhile50)
                Console.WriteLine(e);
            Console.WriteLine();

            var emplTake3 = employees.Skip(2).Take(3);
            foreach (var e in emplTake3)
                Console.WriteLine(e);
            Console.WriteLine();

            var emplTakeWhile50 = employees.TakeWhile(e => e.Salary > 50000);
            foreach (var e in emplTakeWhile50)
                Console.WriteLine(e);
            Console.WriteLine();


            int pageCount = 3;
            int pageCurrent = 0;

            while (employees.Count / pageCount >= pageCurrent)
            {
                var emplResult = employees.Skip(pageCurrent * pageCount).Take(pageCount);
                foreach (var e in emplResult)
                    Console.WriteLine(e);
                Console.WriteLine();
                Console.ReadKey();
                pageCurrent++;
            }
        }

        static public void Groups()
        {
            List<Company> companies = new List<Company>()
            {
                new Company{ Title = "Yandex" },
                new Company{ Title = "Ozon" },
                new Company{ Title = "Mail Group" },
            };

            List<User> users = new List<User>()
            {
                new User{ Name = "Bob", Age = 34, Company = companies[0] },
                new User{ Name = "Sam", Age = 26, Company = companies[1] },
                new User{ Name = "Joe", Age = 19, Company = companies[2] },
                new User{ Name = "Tim", Age = 42, Company = companies[1] },
                new User{ Name = "Leo", Age = 38, Company = companies[0] },
                new User{ Name = "Max", Age = 25, Company = companies[2] },
                new User{ Name = "Jim", Age = 31, Company = companies[1] },
                new User{ Name = "Tom", Age = 29, Company = companies[0] },
            };

            // query
            var compsQuery = from u in users
                             group u by u.Company;

            foreach (var company in compsQuery)
            {
                Console.WriteLine(company.Key);
                foreach (var u in company)
                    Console.WriteLine("\t" + u.Name);
                Console.WriteLine();
            }


            var compsNewQuery = from u in users
                                group u by u.Company into c
                                select new
                                {
                                    Title = c.Key.Title,
                                    Count = c.Count()
                                };
            foreach (var c in compsNewQuery)
                Console.WriteLine($"Title: {c.Title}, Size: {c.Count}");



            // method
            Console.WriteLine();
            Console.WriteLine();
            var compsMethod = users.GroupBy(u => u.Company);
            foreach (var company in compsMethod)
            {
                Console.WriteLine(company.Key);
                foreach (var u in company)
                    Console.WriteLine("\t" + u.Name);
                Console.WriteLine();
            }


            var compsNewMethod = users.GroupBy(u => u.Company)
                                      .Select(c => new
                                      {
                                          Title = c.Key.Title,
                                          Count = c.Count()
                                      });
            foreach (var c in compsNewMethod)
                Console.WriteLine($"Title: {c.Title}, Size: {c.Count}");

            // inners queries
            // query
            Console.WriteLine("\n\n" + new string('-', 30));
            var compUsersQuery = from u in users
                                 group u by u.Company into c
                                 select new
                                 {
                                     CompanyTitle = c.Key.Title,
                                     CompanySize = c.Count(),
                                     //CompanyUsers = from ui in c
                                     //               select ui
                                     CompanyUsers = from ui in c
                                                    select new
                                                    {
                                                        UserName = ui.Name
                                                    }
                                 };
            foreach (var c in compUsersQuery)
            {
                Console.WriteLine($"Title: {c.CompanyTitle}, size: {c.CompanySize}");
                foreach (var u in c.CompanyUsers)
                    //Console.WriteLine($"\tUser name: {u.Name}, age: {u.Age}");
                    Console.WriteLine($"\tUser name: {u.UserName}");
            }

            // method
            Console.WriteLine("\n\n" + new string('-', 30));
            var compUsersMethod = users.GroupBy(u => u.Company)
                                      .Select(c => new
                                      {
                                          CompanyTitle = c.Key.Title,
                                          CompanySize = c.Count(),
                                          //CompanyUsers = c.Select(ui => ui)
                                          CompanyUsers = c.Select(ui => new
                                          {
                                              UserName = ui.Name
                                          })
                                      });

        }

        static public void Joins()
        {
            List<Company> companies = new List<Company>()
            {
                new(){ Title = "Yandex", City = "Kaliningrad" },
                new(){ Title = "Mail Group", City = "Moscow" },
                new(){ Title = "TechArt", City = "Tula" },
            };

            List<User> users = new List<User>()
            {
                new(){ Name = "Bob", Age = 25, Company = companies[0] },
                new(){ Name = "Jim", Age = 31, Company = companies[1] },
                new(){ Name = "Tom", Age = 19, Company = companies[2] },
                new(){ Name = "Leo", Age = 42, Company = companies[0] },
                new(){ Name = "Max", Age = 28, Company = companies[1] },
                new(){ Name = "Sam", Age = 33, Company = companies[0] },
                new(){ Name = "Joe", Age = 26, Company = companies[1] },
                new(){ Name = "Tim", Age = 18, Company = companies[0] },
                new(){ Name = "Ben", Age = 43, Company = companies[2] },
            };

            // join
            // query
            var dataFullQuery = from u in users
                                join c in companies
                                    on u.Company equals c
                                select new
                                {
                                    Company = c.Title,
                                    FullName = u.Name,
                                    City = c.City,
                                    Age = u.Age
                                };

            foreach (var item in dataFullQuery)
                Console.WriteLine($"City: {item.City}, " +
                                  $"Company: {item.Company}, " +
                                  $"User: {item.FullName} ({item.Age})");
            Console.WriteLine();

            // method
            var dataFullMethod = users.Join(companies,
                                            u => u.Company,
                                            c => c,
                                            (u, c) => new
                                            {
                                                Company = c.Title,
                                                FullName = u.Name,
                                                City = c.City,
                                                Age = u.Age
                                            });
            foreach (var item in dataFullMethod)
                Console.WriteLine($"City: {item.City}, " +
                                  $"Company: {item.Company}, " +
                                  $"User: {item.FullName} ({item.Age})");


            // group join
            var dataFullGroupQuery = from c in companies
                                     join u in users
                                        on c equals u.Company into item
                                     select new
                                     {
                                         Title = c.Title,
                                         City = c.City,
                                         Users = item
                                     };

            foreach (var comp in dataFullGroupQuery)
            {
                Console.WriteLine($"Company: {comp.Title} from {comp.City}");
                foreach (var user in comp.Users)
                    Console.WriteLine($"\tUser: {user.Name} ({user.Age})");
            }
            Console.WriteLine();

            // method
            var dataFullGroupMethod = companies
                                .GroupJoin(users,
                                           c => c,
                                           u => u.Company,
                                           (c, item) => new
                                           {
                                               Title = c.Title,
                                               City = c.City,
                                               Users = item
                                           });
            foreach (var comp in dataFullGroupMethod)
            {
                Console.WriteLine($"Company: {comp.Title} from {comp.City}");
                foreach (var user in comp.Users)
                    Console.WriteLine($"\tUser: {user.Name} ({user.Age})");
            }
        }
    }

}
