using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Web.Api.Controllers;
using Xunit;
using XUnitFooseTests.Mocks;

namespace XUnitFooseTests
{
    public class MatchControllerTests
    {
        private MockMatchService _mockMatchService = null;
        private MatchController _matchController = null;

        public MatchControllerTests()
        {
            _mockMatchService = new MockMatchService();
            _matchController = new MatchController(_mockMatchService);
        }

        [Fact]
        public void CallGet()
        {
            _matchController.GetMatches();

            Assert.True(_mockMatchService.GetMatchSuccess);
        }

        [Fact]
        public void CallAdd()
        {
            _matchController.AddMatch(new Match() { Player1Id = Guid.NewGuid(), Player2Id = Guid.NewGuid(), IsDoubles = false, Team1Score = 2, Team2Score = 8, UpdateDate = DateTime.Now });

            Assert.True(_mockMatchService.SaveorUpdateMatchSuccess);
        }
    }
}
