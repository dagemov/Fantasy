using Data.Interfaces;

namespace Data.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly DataContext _context;
        public ICountryRepository CountryRepository { get; set; }

        public UnitWork(DataContext context)
        {
            _context = context;
            CountryRepository = new CountryRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}