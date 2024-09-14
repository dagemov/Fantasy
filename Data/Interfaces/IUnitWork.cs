namespace Data.Interfaces;

public interface IUnitWork : IDisposable
{
    IRepositoryGeneric<T> GetRepository<T>() where T : class;

    Task Save();
}