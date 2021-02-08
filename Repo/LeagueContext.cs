using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class LeagueContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<League> Leagues { get; set; }
        //public DbSet<Role> Roles { get; set; }

        public LeagueContext() { }
        public LeagueContext(DbContextOptions<LeagueContext> options) : base(options) { }
    }
}
