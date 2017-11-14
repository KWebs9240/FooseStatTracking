using AutoMapper;
using FooseStats.Data.Dto;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class RivalsController
    {
        private readonly IPlayerDA _playerService;
        private readonly IMatchDA _matchService;

        public RivalsController(IPlayerDA playerService, IMatchDA matchService)
        {
            _playerService = playerService;
            _matchService = matchService;
        }

        [HttpGet]
        public IEnumerable<RivalDto> GetPlayerDetails([FromQuery] Guid playerId, [FromQuery] bool LoadPointInfo = false)
        {
            List<RivalDto> rtnList = Mapper.Map<List<RivalDto>>(_playerService.GetPlayers(x => !x.PlayerId.Equals(playerId)));

            List<Match> qryMatches = _matchService.GetMatches(x =>
            {
                return x.Player1Id.Equals(playerId)
                        || x.Player2Id.Equals(playerId)
                        || x.Player3Id.Equals(playerId)
                        || x.Player4Id.Equals(playerId);
            }).ToList();

            foreach (RivalDto rivalPlayer in rtnList)
            {
                rivalPlayer.RivalGamesPlayed = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.ToList().Count(y =>
                {
                    return y.Player1Id.Equals(rivalPlayer.PlayerId)
                        || y.Player2Id.Equals(rivalPlayer.PlayerId)
                        || y.Player3Id.Equals(rivalPlayer.PlayerId)
                        || y.Player4Id.Equals(rivalPlayer.PlayerId);
                }));

                rivalPlayer.PlayerGamesPlayed = rivalPlayer.RivalGamesPlayed;

                rivalPlayer.RivalGamesWon = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.ToList().Count(y =>
                {
                    return ((y.Player1Id.Equals(rivalPlayer.PlayerId) || y.Player3Id.Equals(rivalPlayer.PlayerId)) && (y.Team1Score > y.Team2Score))
                        || ((y.Player2Id.Equals(rivalPlayer.PlayerId) || y.Player4Id.Equals(rivalPlayer.PlayerId)) && (y.Team1Score < y.Team2Score));
                }));

                foreach (Guid matchTypeGuid in rivalPlayer.RivalGamesPlayed.Keys)
                {
                    rivalPlayer.PlayerGamesWon.Add(matchTypeGuid, rivalPlayer.PlayerGamesPlayed[matchTypeGuid] - rivalPlayer.RivalGamesWon[matchTypeGuid]);

                    if (rivalPlayer.RivalGamesWon.ContainsKey(matchTypeGuid) && (rivalPlayer.RivalGamesPlayed[matchTypeGuid] > 0))
                    {
                        rivalPlayer.RivalGamesWonPct.Add(matchTypeGuid, (((decimal)rivalPlayer.RivalGamesWon[matchTypeGuid] / rivalPlayer.RivalGamesPlayed[matchTypeGuid]) * 100));
                        rivalPlayer.PlayerGamesWonPct.Add(matchTypeGuid, (100 - rivalPlayer.RivalGamesWonPct[matchTypeGuid]));
                    }
                    else
                    {
                        rivalPlayer.RivalGamesWonPct.Add(matchTypeGuid, 0);
                        rivalPlayer.PlayerGamesWonPct.Add(matchTypeGuid, 0);
                    }
                }

                rivalPlayer.RivalTotalPointsScored = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.Sum(y =>
                {
                    if (y.Player1Id.Equals(rivalPlayer.PlayerId)) { return y.Team1Score; }
                    else if (y.Player2Id.Equals(rivalPlayer.PlayerId)) { return y.Team2Score; }
                    else return 0;
                }));

                rivalPlayer.RivalTotalPointsAllowed = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.Sum(y =>
                {
                    if (y.Player1Id.Equals(rivalPlayer.PlayerId)) { return y.Team2Score; }
                    else if (y.Player2Id.Equals(rivalPlayer.PlayerId)) { return y.Team1Score; }
                    else return 0;
                }));

                rivalPlayer.PlayerTotalPointsScored = qryMatches.GroupBy(x => x.MatchTypeId).ToDictionary(x => x.Key, x => x.Sum(y =>
                {
                    if (y.Player1Id.Equals(rivalPlayer.PlayerId)) { return y.Team2Score; }
                    else if (y.Player2Id.Equals(rivalPlayer.PlayerId)) { return y.Team1Score; }
                    else return 0;
                }));

                foreach (Guid matchTypeGuid in rivalPlayer.RivalGamesPlayed.Keys)
                {
                    if (rivalPlayer.RivalTotalPointsScored.ContainsKey(matchTypeGuid) && (rivalPlayer.RivalGamesPlayed[matchTypeGuid] > 0))
                    {
                        rivalPlayer.RivalPointsPerGame.Add(matchTypeGuid, ((decimal)rivalPlayer.RivalTotalPointsScored[matchTypeGuid] / rivalPlayer.RivalGamesPlayed[matchTypeGuid]));
                    }
                    else
                    {
                        rivalPlayer.RivalPointsPerGame.Add(matchTypeGuid, 0);
                    }
                }

                foreach (Guid matchTypeGuid in rivalPlayer.PlayerGamesPlayed.Keys)
                {
                    if (rivalPlayer.PlayerTotalPointsScored.ContainsKey(matchTypeGuid) && (rivalPlayer.PlayerGamesPlayed[matchTypeGuid] > 0))
                    {
                        rivalPlayer.PlayerPointsPerGame.Add(matchTypeGuid, ((decimal)rivalPlayer.PlayerTotalPointsScored[matchTypeGuid] / rivalPlayer.PlayerGamesPlayed[matchTypeGuid]));
                    }
                    else
                    {
                        rivalPlayer.PlayerPointsPerGame.Add(matchTypeGuid, 0);
                    }
                }
            }

            return rtnList;
        }
    }
}
