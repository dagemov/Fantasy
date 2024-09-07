using Data.Interfaces;

namespace Data.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly DataContext _context;

        public UnitWork(DataContext context)
        {
            _context = context;
            // public IAddressRepository AddressRepository { get; set; }
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