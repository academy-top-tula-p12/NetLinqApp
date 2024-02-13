using NetLinqApp;

//Examples.Welcome();

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


bool result;

// All
result = users.All(u => u.Age > 20);
Console.WriteLine(result);

result = users.All(u => u.Company?.Title == "Yandex");
Console.WriteLine(result);

// Any
result = users.Any(u => u.Company?.Title == "Yandex");
Console.WriteLine(result);

// Contains
result = users.Contains(new() { Name = "Bob", 
                                Age = 25, 
                                Company = companies[0] },
                        new UserComparer());
//var user = users.FirstOrDefault();
//result = users.Contains(user);
Console.WriteLine(result);