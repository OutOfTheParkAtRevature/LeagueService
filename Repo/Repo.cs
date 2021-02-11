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
        private readonly LeagueContext _leagueContext;
        private readonly ILogger _logger;
        public DbSet<Team> Teams;
        public DbSet<Sport> Sports;
        public DbSet<League> Leagues;
        public DbSet<Vendor> Vendors;

        public Repo(LeagueContext leagueContext, ILogger<Repo> logger)
        {
            _leagueContext = leagueContext;
            _logger = logger;
            this.Teams = _leagueContext.Teams;
            this.Sports = _leagueContext.Sports;
            this.Leagues = _leagueContext.Leagues;
            this.Vendors = _leagueContext.Vendors;
        }
        /// <summary>
        /// Saves Changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitSave()
        {
            await _leagueContext.SaveChangesAsync();
        }
        /// <summary>
        /// returns a team based on id parameter passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Team> GetTeamById(Guid id)
        {
            return await Teams.FindAsync(id);
        }
        /// <summary>
        /// returns a list of all teams
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await Teams.ToListAsync();
        }
        /// <summary>
        /// returns a list of all vendors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await Vendors.ToListAsync();
        }
    }
}
