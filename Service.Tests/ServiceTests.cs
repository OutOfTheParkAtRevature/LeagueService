using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using System;
using Xunit;
using System.Collections.Generic;

namespace Service.Tests
{
    public class ServiceTests
    {
        //----------------------------Start of League Tests------------------------------------------


        /// <summary>
        /// Tests the LeagueExists(id) method of Logic
        /// </summary>
        [Fact]
        public async void TestForLeagueExistsById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicLeagueExistsId")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 35,
                    LeagueName = "sports"
                };

                var leagueExists = await logic.LeagueExistsID(league.LeagueID);
                Assert.False(leagueExists);

                r.Leagues.Add(league);
                await r.CommitSave();
                var leagueExists2 = await logic.LeagueExistsID(league.LeagueID);
                Assert.True(leagueExists2);
            }
        }

        /// <summary>
        /// Tests the LeagueExists(name) method of Logic
        /// </summary>
        [Fact]
        public async void TestForLeagueExistsByName()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicLeagueExistsName")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 35,
                    LeagueName = "sports"
                };

                var leagueExists = await logic.LeagueExists(league.LeagueName);
                Assert.False(leagueExists);

                r.Leagues.Add(league);
                await r.CommitSave();
                var leagueExists2 = await logic.LeagueExists(league.LeagueName);
                Assert.True(leagueExists2);
            }
        }

        /// <summary>
        /// Tests the GetLeagues() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetLeagues()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetLeagues")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 35,
                    LeagueName = "basketball"
                };
                r.Leagues.Add(league);
                var league2 = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 43,
                    LeagueName = "baseball"
                };
                r.Leagues.Add(league2);
                await r.CommitSave();
                var getLeagues = await logic.GetLeagues();
                var convertedList = (List<League>)getLeagues;
                Assert.True(convertedList.Count == 2);
                Assert.True(convertedList[0].LeagueName.Equals("basketball"));
                Assert.True(convertedList[1].LeagueName.Equals("baseball"));
            }
        }

        /// <summary>
        /// Tests the GetLeagueById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetLeagueById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetLeagueById")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 35,
                    LeagueName = "basketball"
                };
                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeagues = await logic.GetLeagueById(league.LeagueID);
                Assert.Equal(35, getLeagues.SportID);
                Assert.Equal("basketball", getLeagues.LeagueName);
            }
        }

        /// <summary>
        /// Tests the AddLeague() method of Logic
        /// </summary>
        [Fact]
        public async void TestForAddLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicAddLeague")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
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
        /// Tests the EditLeague() method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicEditLeague")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 29,
                    LeagueName = "football"
                };
                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeague = await logic.GetLeagueById(league.LeagueID);
                Assert.Equal("football", getLeague.LeagueName);
                var editLeague = await logic.EditLeague(league.LeagueID, "baseball");
                Assert.Equal("baseball", editLeague.LeagueName);
            }
        }

        /// <summary>
        /// Tests the DeleteLeague() method of Logic
        /// </summary>
        [Fact]
        public async void TestForDeleteLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicDeleteLeague")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 29,
                    LeagueName = "football"
                };
                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeague = await logic.GetLeagueById(league.LeagueID);
                Assert.NotNull(getLeague);
                await logic.DeleteLeague(league);
                var getLeague2 = await logic.GetLeagueById(league.LeagueID);
                Assert.Null(getLeague2);
            }
        }

        //-----------------------------End of League Tests------------------------------------------

        //-----------------------------Start of Team Tests------------------------------------------

        /// <summary>
        /// Tests the TeamExists(id) method of Logic
        /// </summary>
        [Fact]
        public async void TestForTeamExistsById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicTeamExistsId")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
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

                var teamExists = await logic.TeamExistsID(team.TeamID);
                Assert.False(teamExists);

                r.Teams.Add(team);
                await r.CommitSave();
                var teamExists2 = await logic.TeamExistsID(team.TeamID);
                Assert.True(teamExists2);
            }
        }

        /// <summary>
        /// Tests the TeamExists(name) method of Logic
        /// </summary>
        [Fact]
        public async void TestForTeamExistsByName()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicTeamExistsName")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
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

                var teamExists = await logic.TeamExists(team.Name);
                Assert.False(teamExists);

                r.Teams.Add(team);
                await r.CommitSave();
                var teamExists2 = await logic.TeamExists(team.Name);
                Assert.True(teamExists2);
            }
        }

        /// <summary>
        /// Tests the GetTeams() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetTeams()
        {
            //for coverage
            var dbContext = new LeagueContext();
            var logicClass = new Logic();

            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetTeams")
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
                var convertedList = (List<Team>)listOfTeams;
                Assert.NotNull(listOfTeams);
                Assert.Equal(2, convertedList[0].Wins);
                Assert.Equal(1, convertedList[0].Losses);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetTeamById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetTeamById")
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
        /// Tests the GetTeamByName() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetTeamByName()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetTeamByName")
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
                var listOfTeams = await logic.GetTeamByName(team.Name);
                Assert.True(listOfTeams.Wins.Equals(2));
            }
        }

        /// <summary>
        /// Tests the AddTeam() method of Logic
        /// TODO: figure out how to handle tokens
        /// </summary>
        [Fact]
        public async void TestForAddTeam()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicAddTeam")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());

                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    SportID = 33,
                    LeagueName = "NBA"
                };
                r.Leagues.Add(league);
                await r.CommitSave();

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

                //string token = "access_token";
                //var newTeam = await logic.AddTeam(team.Name, token);
                //Assert.Contains<Team>(newTeam, context.Teams);
            }
        }

        /// <summary>
        /// Tests the EditTeam method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditTeam()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicEditTeam")
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
        /// Tests the DeleteTeam method of Logic
        /// </summary>
        [Fact]
        public async void TestForDeleteTeam()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicDeleteTeam")
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

                var getTeam = await logic.GetTeamById(team.TeamID);
                Assert.NotNull(getTeam);
                await logic.DeleteTeam(team);
                var getTeam2 = await logic.GetTeamById(team.TeamID);
                Assert.Null(getTeam2);
            }
        }

        //------------------------------End of Team Tests------------------------------------------

        //----------------------------Start of Vendor Tests----------------------------------------

        /// <summary>
        /// Tests the VendorExists(name) method of Logic
        /// </summary>
        [Fact]
        public async void TestForVendorExists()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicVendorExists")
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

                var vendorExists = await logic.VendorExists(vendor.VendorName);
                Assert.False(vendorExists);

                r.Vendors.Add(vendor);
                await r.CommitSave();
                var vendorExists2 = await logic.VendorExists(vendor.VendorName);
                Assert.True(vendorExists2);
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
            .UseInMemoryDatabase(databaseName: "p3LogicGetVendors")
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

                r.Vendors.Add(vendor);
                await r.CommitSave();
                var listOfVendors = await logic.GetVendors();
                var convertedList = (List<Vendor>)listOfVendors;
                Assert.NotNull(listOfVendors);
                Assert.Equal("hotdog", convertedList[0].VendorInfo);
                Assert.Equal("weinerhut", convertedList[0].VendorName);
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
            .UseInMemoryDatabase(databaseName: "p3LogicGetVendorById")
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
            .UseInMemoryDatabase(databaseName: "p3LogicGetVendorByName")
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
            .UseInMemoryDatabase(databaseName: "p3LogicAddVendor")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var vendorDto = new CreateVendorDto
                {
                    VendorInfo = "hotdog",
                    VendorName = "weinerhut"
                };

                var newVendor = await logic.AddVendor(vendorDto);
                Assert.Contains<Vendor>(newVendor, context.Vendors);
            }
        }

        /// <summary>
        /// Tests the EditVendor() method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditVendor()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicEditVendor")
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

                r.Vendors.Add(vendor);
                await r.CommitSave();

                var getVendor = await logic.GetVendorById(vendor.VendorID);
                Assert.Equal("hotdog", getVendor.VendorInfo);
                Assert.Equal("weinerhut", getVendor.VendorName);

                var vendorDto = new CreateVendorDto
                {
                    VendorInfo = "hamburger",
                    VendorName = "wendys"
                };

                var editVendor = await logic.EditVendor(vendor.VendorID, vendorDto);
                Assert.Equal("hamburger", editVendor.VendorInfo);
                Assert.Equal("wendys", editVendor.VendorName);
            }
        }

        /// <summary>
        /// Tests the DeleteVendor() method of Logic
        /// </summary>
        [Fact]
        public async void TestForDeleteVendor()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicDeleteVendor")
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

                r.Vendors.Add(vendor);
                await r.CommitSave();

                var getVendor = await logic.GetVendorById(vendor.VendorID);
                Assert.Equal("hotdog", getVendor.VendorInfo);
                Assert.Equal("weinerhut", getVendor.VendorName);

                bool deleteVendor = await logic.DeleteVendor(vendor);
                Assert.True(deleteVendor);
            }
        }

        //------------------------------End of Vendor Tests-----------------------------------------

        //----------------------------Start of Sports Tests-----------------------------------------

        /// <summary>
        /// Tests the SportExists() method of Logic
        /// </summary>
        [Fact]
        public async void TestForSportExists()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicSportExists")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var sport = new Sport
                {
                    SportID = 35,
                    SportName = "basketball"
                };

                var sportExists = await logic.SportExists(sport.SportName);
                Assert.False(sportExists);

                r.Sports.Add(sport);
                await r.CommitSave();
                var sportExists2 = await logic.SportExists(sport.SportName);
                Assert.True(sportExists2);
            }
        }

        //-------------------------------End of Sports Tests----------------------------------------
    }
}
