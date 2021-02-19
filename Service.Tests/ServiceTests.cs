using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using System;
using Xunit;
using System.Net.Http;

namespace Service.Tests
{
    public class ServiceTests
    {
        /// <summary>
        /// Tests the GetTeams() method of Logic
        /// </summary>
<<<<<<< Updated upstream
        [Fact]
        public async void TestForGetTeams()
        {
            //for coverage
            var dbContext = new LeagueContext();
            var logicClass = new Logic();

            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
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

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
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
        /// Tests the GetTeamByName() method of Logic
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

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
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
                var listOfTeams = await logic.GetTeamByName(team.Name);
                Assert.True(listOfTeams.Wins.Equals(2));
            }
        }

        /// <summary>
        /// Tests the AddTeam() method of Logic
        /// </summary>
        [Fact]
        public async void TestForAddTeam()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
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

                var newTeam = await logic.AddTeam(team);
                Assert.Contains<Team>(newTeam, context.Teams);
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

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
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
        /// Tests the GetVendors() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetVendors()
        {
            //for coverage
            var dbContext = new LeagueContext();
            var logicClass = new Logic();

            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "hotdog",
                    VendorName = "weinerhut"
                };

                r.Vendors.Add(vendor);
                await r.CommitSave();
                var listOfVendors = await logic.GetVendors();
                Assert.NotNull(listOfVendors);
            }
        }

        /// <summary>
        /// Tests the GetVendorById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetVendorById()
        {
            //for coverage
            var dbContext = new LeagueContext();
            var logicClass = new Logic();

            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "hotdog",
                    VendorName = "weinerhut"
                };

                r.Vendors.Add(vendor);
                await r.CommitSave();
                var getVendor = await logic.GetVendorById(vendor.VendorID);
                Assert.True(getVendor.VendorInfo.Equals("hotdog"));
            }
        }

        /// <summary>
        /// Tests the GetVendorByName() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetVendorByName()
        {
            //for coverage
            var dbContext = new LeagueContext();
            var logicClass = new Logic();

            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "hotdog",
                    VendorName = "weinerhut"
                };

                r.Vendors.Add(vendor);
                await r.CommitSave();
                var getVendor = await logic.GetVendorByName(vendor.VendorName);
                Assert.True(getVendor.VendorInfo.Equals("hotdog"));
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

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
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

        /// <summary>
        /// Tests the AddLeague() method of Logic
        /// </summary>
        [Fact]
        public async void TestForAddLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var sport = new Sport
                {
                    SportID = 35,
                    SportName = "basketball"
                };

                r.Sports.Add(sport);
                await r.CommitSave();

                var leagueDto = new CreateLeagueDto
                {
                    LeagueName = "sports",
                    SportName = sport.SportName
                };

                var newLeague = await logic.AddLeague(leagueDto);
                Assert.Contains<League>(newLeague, context.Leagues);
            }
        }

        /// <summary>
        /// Tests the LeagueExists() method of Logic
        /// </summary>
        [Fact]
        public async void TestForLeagueExists()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 35,
                    LeagueName = "sports"
                };

                r.Leagues.Add(league);
                await r.CommitSave();
                var leagueExists = await logic.LeagueExists(league.LeagueName);
                Assert.True(leagueExists);
            }
        }

        /// <summary>
        /// Tests the SportExists() method of Logic
        /// </summary>
        [Fact]
        public async void TestForSportExists()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var sport = new Sport
                {
                    SportID = 35,
                    SportName = "basketball"
                };

                r.Sports.Add(sport);
                await r.CommitSave();
                var sportExists = await logic.SportExists(sport.SportName);
                Assert.True(sportExists);
            }
        }

        /// <summary>
        /// Tests the TeamExists(id) method of Logic
        /// </summary>
        [Fact]
        public async void TestForTeamExistsId()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var team = new Team
                {
                    LeagueID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Wins = 6,
                    Losses = 2,
                    Name = "seals"
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var teamExists = await logic.TeamExists(team.TeamID);
                Assert.True(teamExists);
            }
        }

        /// <summary>
        /// Tests the TeamExists(name) method of Logic
        /// </summary>
        [Fact]
        public async void TestForTeamExists()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                var team = new Team
                {
                    LeagueID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Wins = 6,
                    Losses = 2,
                    Name = "seals"
                };

                r.Teams.Add(team);
                await r.CommitSave();
                var teamExists = await logic.TeamExists(team.Name);
                Assert.True(teamExists);
            }
        }
=======
        //[Fact]
        //public async void TestForGetTeams()
        //{
        //    //for coverage
        //    var dbContext = new LeagueContext();
        //    var logicClass = new Logic();

        //    var options = new DbContextOptionsBuilder<LeagueContext>()
        //    .UseInMemoryDatabase(databaseName: "p3LeagueService")
        //    .Options;

        //    using (var context = new LeagueContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        var team = new Team
        //        {
        //            TeamID = Guid.NewGuid(),
        //            CarpoolID = Guid.NewGuid(),
        //            LeagueID = Guid.NewGuid(),
        //            StatLineID = Guid.NewGuid(),
        //            Name = "Broncos",
        //            Wins = 2,
        //            Losses = 1
        //        };

        //        r.Teams.Add(team);
        //        await r.CommitSave();
        //        var listOfTeams = await logic.GetTeams();
        //        Assert.NotNull(listOfTeams);
        //    }
        //}

        ///// <summary>
        ///// Tests the GetTeamById() method of Logic
        ///// </summary>
        //[Fact]
        //public async void TestForGetTeamById()
        //{
        //    var options = new DbContextOptionsBuilder<LeagueContext>()
        //    .UseInMemoryDatabase(databaseName: "p3LeagueService")
        //    .Options;

        //    using (var context = new LeagueContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        var team = new Team
        //        {
        //            TeamID = Guid.NewGuid(),
        //            CarpoolID = Guid.NewGuid(),
        //            LeagueID = Guid.NewGuid(),
        //            StatLineID = Guid.NewGuid(),
        //            Name = "Broncos",
        //            Wins = 2,
        //            Losses = 1
        //        };

        //        r.Teams.Add(team);
        //        await r.CommitSave();
        //        var listOfTeams = await logic.GetTeamById(team.TeamID);
        //        Assert.True(listOfTeams.Equals(team));
        //    }
        //}

        ///// <summary>
        ///// Tests the EditTeam method of Logic
        ///// </summary>
        //[Fact]
        //public async void TestForEditTeam()
        //{
        //    var options = new DbContextOptionsBuilder<LeagueContext>()
        //    .UseInMemoryDatabase(databaseName: "p3LeagueService")
        //    .Options;

        //    using (var context = new LeagueContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        var team = new Team()
        //        {
        //            TeamID = Guid.NewGuid(),
        //            CarpoolID = Guid.NewGuid(),
        //            LeagueID = Guid.NewGuid(),
        //            StatLineID = Guid.NewGuid(),
        //            Name = "Dirty Donkies",
        //            Wins = 0,
        //            Losses = 5
        //        };

        //        r.Teams.Add(team);
        //        await r.CommitSave();

        //        var team2 = new EditTeamDto()
        //        {
        //            Name = "Bad Broncoes",
        //            Wins = 0,
        //            Losses = 5
        //        };

        //        var editedTeam = await logic.EditTeam(team.TeamID, team2);
        //        Assert.Equal(editedTeam.Name, context.Teams.Find(team.TeamID).Name);
        //    }
        //}

        ///// <summary>
        ///// Tests the AddVendor() method of Logic
        ///// </summary>
        //[Fact]
        //public async void TestForAddVendor()
        //{
        //    var options = new DbContextOptionsBuilder<LeagueContext>()
        //    .UseInMemoryDatabase(databaseName: "p3LeagueService")
        //    .Options;

        //    using (var context = new LeagueContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        var vendor = new Vendor
        //        {
        //            VendorID = Guid.NewGuid(),
        //            VendorInfo = "hotdog",
        //            VendorName = "weinerhut"
        //        };

        //        var newVendor = await logic.AddVendor(vendor.VendorName, vendor.VendorInfo);
        //        Assert.Contains<Vendor>(newVendor, context.Vendors);
        //    }
        //}
>>>>>>> Stashed changes
    }
}
