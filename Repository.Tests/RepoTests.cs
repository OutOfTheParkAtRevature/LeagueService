using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using System;
using System.Collections.Generic;
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
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
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
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
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
        /// Tests the GetTeamsByName() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamsByName()
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
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var getTeam = await r.GetTeamsByName(team.Name);
                var convertedList = (List<Team>)getTeam;
                Assert.True(convertedList[0].Wins.Equals(2));
            }
        }

        /// <summary>
        /// Tests the GetTeamsByLeague() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamsByLeague()
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
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var getTeam = await r.GetTeamsByLeague(team.LeagueID);
                var convertedList = (List<Team>)getTeam;
                Assert.True(convertedList[0].Wins.Equals(2));
            }
        }

        /// <summary>
        /// Tests the GetTeamByNameAndLeague() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamByName()
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
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var getTeam = await r.GetTeamByNameAndLeague(team.Name);
                Assert.True(getTeam.Wins.Equals(2));
                Assert.True(getTeam.Losses.Equals(1));
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

        /// <summary>
        /// Tests the GetVendorById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetVendorById()
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
                var getTeam = await r.GetVendorById(vendor.VendorID);
                Assert.True(getTeam.VendorName.Equals("weinerhut"));
            }
        }

        /// <summary>
        /// Tests the GetVendorByName() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetVendorByName()
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
                var getTeam = await r.GetVendorByName(vendor.VendorName);
                Assert.True(getTeam.VendorInfo.Equals("hotdog"));
            }
        }
    }
}
