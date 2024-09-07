namespace Data.Interfaces;

public interface IUnitWork : IDisposable
{
    //  IAddressRepository AddressRepository { get; }
    Task Save();
}