using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FooseStats.Data.Dto;
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
        private readonly IMatchDA _matchService;

        public PlayerController(IPlayerDA playerService, IMatchDA matchService)
        {
            _playerService = playerService;
            _matchService = matchService;
        }

        [HttpGet]
        public IEnumerable<PlayerDto> GetPlayers([FromQuery] bool LoadGamesInfo = false, [FromQuery] bool LoadPointInfo = false)
        {
            List<PlayerDto> rtnList = Mapper.Map<List<PlayerDto>>(_playerService.GetPlayers());
            List<Match> qryMatches = null;

            if (LoadGamesInfo || LoadPointInfo)
            {
                qryMatches = _matchService.GetMatches().ToList();

                foreach (PlayerDto matchPlayer in rtnList)
                {
                    matchPlayer.GamesPlayed = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.ToList().Count(y =>
                    {
                        return y.Player1Id.Equals(matchPlayer.PlayerId)
                            || y.Player2Id.Equals(matchPlayer.PlayerId)
                            || y.Player3Id.Equals(matchPlayer.PlayerId)
                            || y.Player4Id.Equals(matchPlayer.PlayerId);
                    }));
                }
            }

            if (LoadGamesInfo)
            {
                foreach(PlayerDto matchPlayer in rtnList)
                {
                    matchPlayer.GamesWon = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.ToList().Count(y =>
                    {
                        return ((y.Player1Id.Equals(matchPlayer.PlayerId) || y.Player3Id.Equals(matchPlayer.PlayerId)) && (y.Team1Score > y.Team2Score))
                            || ((y.Player2Id.Equals(matchPlayer.PlayerId) || y.Player4Id.Equals(matchPlayer.PlayerId)) && (y.Team1Score < y.Team2Score));
                    }));

                    foreach (Guid matchTypeGuid in matchPlayer.GamesPlayed.Keys)
                    {
                        if (matchPlayer.GamesWon.ContainsKey(matchTypeGuid) && (matchPlayer.GamesPlayed[matchTypeGuid] > 0))
                        {
                            matchPlayer.GamesWonPct.Add(matchTypeGuid, (((decimal)matchPlayer.GamesWon[matchTypeGuid] / matchPlayer.GamesPlayed[matchTypeGuid]) * 100));
                        }
                        else
                        {
                            matchPlayer.GamesWonPct.Add(matchTypeGuid, 0);
                        }
                    }
                }
            }

            if (LoadPointInfo)
            {
                foreach(PlayerDto matchPlayer in rtnList)
                {
                    matchPlayer.TotalPointsScored = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.Sum(y =>
                    {
                        if (y.Player1Id.Equals(matchPlayer.PlayerId)) { return y.Team1Score; }
                        else if (y.Player2Id.Equals(matchPlayer.PlayerId)) { return y.Team2Score; }
                        else return 0;
                    }));

                    matchPlayer.TotalPointsAllowed = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.Sum(y =>
                    {
                        if (y.Player1Id.Equals(matchPlayer.PlayerId)) { return y.Team2Score; }
                        else if (y.Player2Id.Equals(matchPlayer.PlayerId)) { return y.Team1Score; }
                        else return 0;
                    }));

                    foreach (Guid matchTypeGuid in matchPlayer.GamesPlayed.Keys)
                    {
                        if (matchPlayer.TotalPointsScored.ContainsKey(matchTypeGuid) && (matchPlayer.GamesPlayed[matchTypeGuid] > 0))
                        {
                            matchPlayer.PointsPerGame.Add(matchTypeGuid, ((decimal)matchPlayer.TotalPointsScored[matchTypeGuid] / matchPlayer.GamesPlayed[matchTypeGuid]));
                        }
                        else
                        {
                            matchPlayer.PointsPerGame.Add(matchTypeGuid, 0);
                        }
                    }
                }
            }

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

        [HttpPost]
        [Route("Delete")]
        public int DeleteMatch([FromBody]Player playerToDelete)
        {
            return _playerService.DeletePlayer(playerToDelete);
        }
    }
}
