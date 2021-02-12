using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using System;
using Xunit;

namespace Service.Tests
{
    public class ServiceTests
    {
        /// <summary>
        /// Tests the GetTeams() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetTeams()
        {
            //for coverage
            var dbContext = new LeagueContext();
            var logic = new Logic();

            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var listOfTeams = await logic.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of Logic
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
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var listOfTeams = await logic.GetTeamById(team.TeamID);
                Assert.True(listOfTeams.Equals(team));
            }
        }

        /// <summary>
        /// Tests the EditTeam method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditTeam()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var team = new Team()
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Dirty Donkies",
                    Wins = 0,
                    Losses = 5
                };

                r.Teams.Add(team);
                await r.CommitSave();

                var team2 = new EditTeamDto()
                {
                    Name = "Bad Broncoes",
                    Wins = 0,
                    Losses = 5
                };

                var editedTeam = await logic.EditTeam(team.TeamID, team2);
                Assert.Equal(editedTeam.Name, context.Teams.Find(team.TeamID).Name);
            }
        }

        /// <summary>
        /// Tests the AddVendor() method of Logic
        /// </summary>
        [Fact]
        public async void TestForAddVendor()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "hotdog",
                    VendorName = "weinerhut"
                };

                var newVendor = await logic.AddVendor(vendor.VendorName, vendor.VendorInfo);
                Assert.Contains<Vendor>(newVendor, context.Vendors);
            }
        }
    }
}
