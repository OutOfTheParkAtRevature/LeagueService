using LeagueService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeagueService.Tests
{
    public class TeamControllerTests
    {

        /// <summary>
        /// Tests the CreateTeam() method of TeamController
        /// TODO: deal with httpcontext token
        /// </summary>
        [Fact]
        public async void TestForCreateLeague()
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
                TeamController teamController = new TeamController(logic);
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

                //var createTeam = await teamController.CreateTeam(team.Name);
                //Assert.IsAssignableFrom<Team>((createTeam as OkObjectResult).Value);
                //var createTeam2 = await teamController.CreateTeam(team.Name);
                //Assert.IsAssignableFrom<string>((createTeam2 as ConflictObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetAllTeams() method of TeamController
        /// </summary>
        [Fact]
        public async void TestForGetAllTeams()
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
                TeamController teamController = new TeamController(logic);
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 21,
                    Losses = 33
                };
                r.Teams.Add(team);
                var team2 = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Dolphins",
                    Wins = 25,
                    Losses = 23
                };
                r.Teams.Add(team2);
                await r.CommitSave();

                var getTeams = await teamController.GetAllTeams();
                Assert.IsAssignableFrom<IEnumerable<Team>>((getTeams as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of TeamController
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
                TeamController teamController = new TeamController(logic);
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 21,
                    Losses = 33
                };

                var getTeam = await teamController.GetTeamById(team.TeamID);
                Assert.IsAssignableFrom<string>((getTeam as NotFoundObjectResult).Value);

                r.Teams.Add(team);
                await r.CommitSave();

                var getTeam2 = await teamController.GetTeamById(team.TeamID);
                Assert.IsAssignableFrom<Team>((getTeam2 as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetTeamByName() method of TeamController
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
                Logic logic = new Logic(r, new NullLogger<Repo>());
                TeamController teamController = new TeamController(logic);
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 21,
                    Losses = 33
                };

                var getTeam = await teamController.GetTeamByName(team.Name);
                Assert.IsAssignableFrom<string>((getTeam as NotFoundObjectResult).Value);

                r.Teams.Add(team);
                await r.CommitSave();

                var getTeam2 = await teamController.GetTeamByName(team.Name);
                Assert.IsAssignableFrom<Team>((getTeam2 as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the EditTeam() method of TeamController
        /// TODO: deal with claimsidentity
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
                TeamController teamController = new TeamController(logic);
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 21,
                    Losses = 33
                };
                var teamDto = new EditTeamDto
                {
                    Name = "Pirates",
                    Wins = 22,
                    Losses = 11
                };
                //var editTeam = await teamController.EditTeam(team.TeamID, teamDto);
                //Assert.IsAssignableFrom<string>((editTeam as NotFoundObjectResult).Value);

                //r.Teams.Add(team);
                //await r.CommitSave();

                //var editTeam2 = await teamController.EditTeam(team.TeamID, teamDto);
                //Assert.IsAssignableFrom<Team>((editTeam2 as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the DeleteTeam() method of TeamController
        /// </summary>
        [Fact]
        public async void TestForDeleteTeam()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3TeamController")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                TeamController teamController = new TeamController(logic);
                var team = new Team
                {
                    TeamID = Guid.NewGuid(),
                    CarpoolID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid(),
                    Name = "Broncos",
                    Wins = 21,
                    Losses = 33
                };

                var deleteTeam = await teamController.DeleteTeam(team.TeamID);
                Assert.IsAssignableFrom<string>((deleteTeam as NotFoundObjectResult).Value);

                r.Teams.Add(team);
                await r.CommitSave();

                var deleteTeam2 = await teamController.DeleteTeam(team.TeamID);
                Assert.IsAssignableFrom<bool>((deleteTeam2 as OkObjectResult).Value);
            }
        }
    }
}
