using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace LeagueService.Tests
{
    public class LeagueControllerTests
    {
        /// <summary>
        /// Tests the CreateLeague() method of LeagueController
        /// </summary>
        [Fact]
        public async void TestForCreateLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueControllerCreateLeague")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                LeagueController leagueController = new LeagueController(logic);
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

                var createLeague = await leagueController.CreateLeague(leagueDto);
                Assert.IsAssignableFrom<League>((createLeague as OkObjectResult).Value);
                var createLeague2 = await leagueController.CreateLeague(leagueDto);
                Assert.IsAssignableFrom<string>((createLeague2 as ConflictObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetLeagues() method of LeagueController
        /// </summary>
        [Fact]
        public async void TestForGetLeagues()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueControllerGetLeagues")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                LeagueController leagueController = new LeagueController(logic);
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    LeagueName = "louge",
                    SportID = 51
                };

                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeagues = await leagueController.GetLeagues();

                Assert.IsAssignableFrom<IEnumerable<League>>((getLeagues as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetLeagueById() method of LeagueController
        /// </summary>
        [Fact]
        public async void TestForGetLeagueById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueControllerGetLeagueById")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                LeagueController leagueController = new LeagueController(logic);
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    LeagueName = "louge",
                    SportID = 51
                };

                var getLeague = await leagueController.GetLeagueById(league.LeagueID);
                Assert.IsAssignableFrom<string>((getLeague as NotFoundObjectResult).Value);

                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeague2 = await leagueController.GetLeagueById(league.LeagueID);
                Assert.IsAssignableFrom<League>((getLeague2 as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the EditLeague() method of LeagueController
        /// </summary>
        [Fact]
        public async void TestForEditLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueControllerEditLeague")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                LeagueController leagueController = new LeagueController(logic);
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    LeagueName = "louge",
                    SportID = 51
                };

                var getLeague = await leagueController.EditLeague(league.LeagueID, "tennis");
                Assert.IsAssignableFrom<string>((getLeague as NotFoundObjectResult).Value);

                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeague2 = await leagueController.EditLeague(league.LeagueID, "tennis");
                Assert.IsAssignableFrom<League>((getLeague2 as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the DeleteLeague() method of LeagueController
        /// </summary>
        [Fact]
        public async void TestForDeleteLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueControllerDeleteLeague")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                LeagueController leagueController = new LeagueController(logic);
                var league = new League
                {
                    LeagueID = Guid.NewGuid(),
                    LeagueName = "louge",
                    SportID = 51
                };

                var getLeague = await leagueController.DeleteLeague(league.LeagueID);
                Assert.IsAssignableFrom<string>((getLeague as NotFoundObjectResult).Value);

                r.Leagues.Add(league);
                await r.CommitSave();

                var getLeague2 = await leagueController.DeleteLeague(league.LeagueID);
                Assert.IsAssignableFrom<bool>((getLeague2 as OkObjectResult).Value);
            }
        }
    }
}
