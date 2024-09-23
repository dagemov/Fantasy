using Models.Entities;

namespace Data.Interfaces;

public interface ITeamsRepository : IRepositoryGeneric<Team>
{
    void Update(Team team);
}