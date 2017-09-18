using System;
using FooseStats.Web.Api.Controllers;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using Xunit;
using XUnitFooseTests.Mocks;
using Moq;
using FooseStats.Data.Interfaces;

namespace XUnitFooseTests
{
    public class PlayerControllerTests
    {
        private MockPlayerService _mockPlayerService = null;
        private PlayerController _playerController = null;
        private Mock<IMatchDA> _moqMatchService = new Mock<IMatchDA>();

        public PlayerControllerTests()
        {
            _mockPlayerService = new MockPlayerService();
            _playerController = new PlayerController(_mockPlayerService, _moqMatchService.Object);
        }

        [Fact]
        public void CallGet()
        {
            _playerController.GetPlayers();

            Assert.True(_mockPlayerService.GetPlayerSuccess);
        }

        [Fact]
        public void CallAdd()
        {
            _playerController.AddPlayer(new Player() { FirstName = "Test", LastName = "User2", NickName = "Computer", UpdateDate = DateTime.Now });

            Assert.True(_mockPlayerService.SaveorUpdatePlayerSuccess);
        }

        [Fact]
        public void CallAddDuplicate()
        {
            bool duplicateExceptionThrown = false;

            Assert.True(_mockPlayerService.InitializeFakePlayers());

            try
            {
                _playerController.AddPlayer(new Player() { PlayerId = new Guid("0b3f7e1c-cd09-4df1-9e07-d03887e0e522"), FirstName = "Kyle", LastName = "Webster", NickName = "Chimp9240", UpdateDate = DateTime.Now });
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Equals("A Player with that Id already exist."))
                {
                    duplicateExceptionThrown = true;
                }
            }

            Assert.True(duplicateExceptionThrown);
        }

        [Fact]
        public void CallUpdate()
        {
            Assert.True(_mockPlayerService.InitializeFakePlayers());

            Player playerToUpdate = new Player() { PlayerId = new Guid("0b3f7e1c-cd09-4df1-9e07-d03887e0e522"), FirstName = "Kyle", LastName = "Webster", NickName = "Chimp9240", UpdateDate = DateTime.Now };

            _playerController.UpdatePlayer(playerToUpdate);

            Assert.True(_mockPlayerService.SaveorUpdatePlayerSuccess);
        }

        [Fact]
        public void CallUpdateNonExisting()
        {
            bool invalidExceptionThrown = false;

            Assert.True(_mockPlayerService.InitializeFakePlayers());

            Player playerToUpdate = new Player() { PlayerId = new Guid("ee345f7f-5275-48a1-8751-0c5a82646422"), FirstName = "New", LastName = "Guy", NickName = "Chimp9240", UpdateDate = DateTime.Now };

            try
            {
                _playerController.UpdatePlayer(playerToUpdate);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Equals("Player to update does not already exist."))
                {
                    invalidExceptionThrown = true;
                }
            }

            Assert.True(invalidExceptionThrown);
        }

        [Fact]
        public void CreateReturnsNonNullCreated()
        {
            Player playerToUpdate = new Player() { PlayerId = new Guid("ee345f7f-5275-48a1-8751-0c5a82646422"), FirstName = "New", LastName = "Guy", NickName = "Chimp9240", UpdateDate = DateTime.Now };
        }
    }
}
