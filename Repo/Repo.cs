using System;
using System.Collections.Generic;
using System.Linq;
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
        /// returns a team based on name parameter passed in
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Team>> GetTeamsByName(string name)
        {
            return await Teams.Where(x => x.Name == name).ToListAsync();
        }
        /// <summary>
        /// returns a list of all teams
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await Teams.ToListAsync();
        }


        public async Task<IEnumerable<Team>> GetTeamsByLeague(Guid id)
        {
            return await Teams.Where(x => x.LeagueID == id).ToListAsync();
        }


        public async Task<Team> GetTeamByNameAndLeague(string teamName)
        {
            return await Teams.FirstOrDefaultAsync(x => x.Name == teamName);
        }

        /// <summary>
        /// returns a vendor based on id parameter passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Vendor> GetVendorById(Guid id)
        {
            return await Vendors.FirstOrDefaultAsync(x => x.VendorID == id);
        }
        /// <summary>
        /// returns a vendor based on id parameter passed in
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Vendor> GetVendorByName(string name)
        {
            return await Vendors.FirstOrDefaultAsync(x => x.VendorName == name);
        }
        /// <summary>
        /// returns a list of all vendors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await Vendors.ToListAsync();
        }

        //public async Task SeedLeague()
        //{
        //    League league = new League()
        //    {
        //        LeagueID = Guid.NewGuid(),
        //        LeagueName = "",
        //        SportID = 0
        //    };
        //    await Leagues.AddAsync(league);
        //    await CommitSave();
        //}

        //public async Task SeedSports()
        //{
        //    int[] sportids = { 1, 2, 3, 4, 5, 6 };
        //    string[] sports = { "", "", "", "", "", "" };
        //    for (int i = 0; i < sports.Length; i++)
        //    {
        //        Sport sport = new Sport()
        //        {
        //            SportID = sportids[i],
        //            SportName = sports[i]
        //        };
        //        await Sports.AddAsync(sport);
        //    }
        //    await CommitSave();            
        //}
        
        //public async Task SeedTeams()
        //{
        //    string[] teams = { "Tigers", "Bears", "Lions" };
        //    List<League> leagueList = await Leagues.ToListAsync();
        //    for (int i = 0; i < teams.Length; i++)
        //    {
        //        Team team = new Team()
        //        {
        //            TeamID = Guid.NewGuid(),
        //            LeagueID = leagueList[0].LeagueID,
        //            CarpoolID = Guid.NewGuid(),
        //            StatLineID = Guid.NewGuid(),
        //            Name = teams[i]
        //        };
        //        await Teams.AddAsync(team);
        //    }
        //    await CommitSave();
        //}
    }
}
