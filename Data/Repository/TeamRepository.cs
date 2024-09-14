using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TeamRepository : RepositoryGeneric<Team>, ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Team team)
        {
            var teamDb = _context.Teams.FirstOrDefault(t => t.Id == t.Id);

            if (teamDb != null)
            {
                teamDb.Name = team.Name;
                teamDb.Image = team.Image;
                _context.SaveChanges();
            }
        }
    }
}