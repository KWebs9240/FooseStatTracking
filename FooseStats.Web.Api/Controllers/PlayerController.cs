using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooseStats.Data.FooseStats.Data.Ef;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerDA _playerService;

        public PlayerController(IPlayerDA playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            List<Player> rtnList = _playerService.GetPlayers().ToList();

            return rtnList;
        }

        [HttpPost]
        public Player AddPlayer([FromBody]Player player)
        {
            if (player.PlayerId != null &&
                _playerService.GetPlayers(x => x.PlayerId.Equals(player.PlayerId)).Any())
            {
                throw new InvalidOperationException("A Player with that Id already exist.");
            }

            if(player.PlayerId.Equals(Guid.Empty))
            {
                player.PlayerId = Guid.NewGuid();
            }

            return _playerService.SaveorUpdatePlayer(player);
        }

        [HttpPut]
        public Player UpdatePlayer([FromBody]Player player)
        {
            if(!_playerService.GetPlayers(x => x.PlayerId.Equals(player.PlayerId)).Any())
            {
                throw new InvalidOperationException("Player to update does not already exist.");
            }

            return _playerService.SaveorUpdatePlayer(player);
        }
    }
}
