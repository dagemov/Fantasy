namespace Data.Interfaces;

public interface IUnitWork : IDisposable
{
    //  IAddressRepository AddressRepository { get; }
    ICountryRepository CountryRepository { get; }

    Task Save();
}