namespace Data.Interfaces;

public interface IUnitWork : IDisposable
{
    IRepositoryGeneric<T> GetRepository<T>() where T : class;

    ICountryRepository CountryRepository { get; }
    ITeamRepository TeamRepository { get; }

    Task Save();
}