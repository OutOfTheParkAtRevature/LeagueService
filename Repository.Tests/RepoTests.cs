using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using System;
using Xunit;

namespace Repository.Tests
{
    public class RepoTests
    {
        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        [Fact]
        public async void TestForCommitSave()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var sport = new Sport
                {
                    SportID = 8,
                    SportName = "football"
                };

                r.Sports.Add(sport);
                await r.CommitSave();
                Assert.NotEmpty(context.Sports);
            }
        }

        /// <summary>
        /// Tests the GetTeams() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeams()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var listOfTeams = await r.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var team = new Team
                {
                    TeamID = Guid.NewGuid(), 
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var listOfTeams = await r.GetTeamById(team.TeamID);
                Assert.True(listOfTeams.Equals(team));
            }
        }

        /// <summary>
        /// Tests the GetVendors() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetVendors()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "hotdog",
                    VendorName = "weinerhut"
                };

                r.Vendors.Add(vendor);
                await r.CommitSave();
                var listOfTeams = await r.GetVendors();
                Assert.NotNull(listOfTeams);
            }
        }
    }
}
