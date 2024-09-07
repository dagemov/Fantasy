using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repository;

public class CountryRepository : RepositoryGeneric<Country>, ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Country country)
    {
        var countryDb = _context.Countries.FirstOrDefault(c => c.Id == country.Id);

        if (countryDb != null)
        {
            countryDb.Name = country.Name;
            _context.SaveChanges();
        }
    }
}