using Bogus;
using SpectreLibraryConsoleApp.Models;
using static Bogus.Randomizer;

namespace SpectreLibraryConsoleApp.Classes;

public class BogusOperations 
{
    public static List<Company> Companies(int count = 10)
    {
        Seed = new Random(338);
        int identifier = 1;
        Faker<Company> fakePerson = new Faker<Company>()
            .CustomInstantiator(f => new Company(identifier++))
            .RuleFor(p => p.Name, f => f.Company.CompanyName());

        return fakePerson.Generate(count).OrderBy(company => company.Name).ToList();

    }
}