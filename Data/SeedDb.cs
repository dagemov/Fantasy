using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();//Create or Update

        await CheckCountriesAsync();
        await CheckTeamAsync();
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            var basePath = AppContext.BaseDirectory;
            var filePath = Path.Combine(basePath, "SqlResources", "Countries.sql");

            var countriesSQLScript = File.ReadAllText(filePath);
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckTeamAsync()
    {
        if (!_context.Teams.Any())
        {
            foreach (var country in _context.Countries)
            {
                _context.Teams.Add(new Team { Name = country.Name, Country = country });
                if (country.Name == "Colombia")
                {
                    _context.Teams.Add(new Team { Name = "Medellin", Country = country });
                    _context.Teams.Add(new Team { Name = "Nacional", Country = country });
                    _context.Teams.Add(new Team { Name = "Caldas", Country = country });
                    _context.Teams.Add(new Team { Name = "Junior", Country = country });
                    _context.Teams.Add(new Team { Name = "Millonarios", Country = country });
                }
                else if (country.Name == "United States")
                {
                    _context.Teams.Add(new Team { Name = "Connecticut LG", Country = country });
                    _context.Teams.Add(new Team { Name = "NYF", Country = country });
                    _context.Teams.Add(new Team { Name = "Tiggers", Country = country });
                    _context.Teams.Add(new Team { Name = "Wilton School", Country = country });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}