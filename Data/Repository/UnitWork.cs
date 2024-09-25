using Data.Helpers.Interfaces;
using Data.Interfaces;

namespace Data.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public ICountryRepository CountryRepository { get; set; }
        public ITeamRepository TeamRepository { get; set; }

        public UnitWork(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
            CountryRepository = new CountryRepository(_context);
            TeamRepository = new TeamRepository(_context, _fileStorage);
        }

        public IRepositoryGeneric<T> GetRepository<T>() where T : class
        {
            return new RepositoryGeneric<T>(_context);
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