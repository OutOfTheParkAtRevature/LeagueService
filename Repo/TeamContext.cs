using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class TeamContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Role> Roles { get; set; }

        public TeamContext() { }
        public TeamContext(DbContextOptions<TeamContext> options) : base(options) { }
    }
}