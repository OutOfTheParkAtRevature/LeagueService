using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;


namespace Repository
{
    public class Repo
    {
        private readonly LeagueContext _LeagueContext;
        private readonly ILogger _logger;
        public DbSet<Team> Teams;
        public DbSet<Sport> Sports;
        public DbSet<League> Leagues;
        //public DbSet<Role> Roles;


        public Repo(LeagueContext teamContext, ILogger<Repo> logger)
        {
            _LeagueContext = teamContext;
            _logger = logger;
            //this.Roles = _teamContext.Roles;
            this.Teams = _LeagueContext.Teams;
            this.Sports = _LeagueContext.Sports;
            this.Leagues = _LeagueContext.Leagues;
        }
        // Access SaveChangesAsync from Logic class
        public async Task CommitSave()
        {
            await _LeagueContext.SaveChangesAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await Teams.FindAsync(id);
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await Teams.ToListAsync();
        }
        //public async Task<Role> GetRoleById(int id)
        //{
        //    return await Roles.FindAsync(id);
        //}
        //public async Task<IEnumerable<Role>> GetRoles()
        //{
        //    return await Roles.ToListAsync();
        //}
    }
}
