using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using Service;
using System;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace LeagueService.Tests
{
    public class ControllerTests
    {
        /// <summary>
        /// Tests the CreateLeague() method of Logic
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

                HttpClient http = new HttpClient();
                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>(), http);
                LeagueController leagueController = new LeagueController();
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

                //var newLeague = await leagueController.CreateLeague(leagueDto);
                //var getLeague = await context.Leagues.FirstOrDefaultAsync(x => x.LeagueName == leagueDto.LeagueName);
                //Assert.True(getLeague.SportID == 35);
            }
        }
    }
}
