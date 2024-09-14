using Data.Interfaces;

namespace Data.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly DataContext _context;

        public UnitWork(DataContext context)
        {
            _context = context;
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