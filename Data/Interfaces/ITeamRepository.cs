using Data.Repository;
using Models.Entities;

namespace Data.Interfaces;

public interface ITeamRepository : IRepositoryGeneric<Team>
{
    void Update(Team team);
}