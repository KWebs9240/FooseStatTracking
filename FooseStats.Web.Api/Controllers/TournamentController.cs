using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.FooseStats.Data.Ef.Helpers;
using FooseStats.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FooseStats.Data.Dto;
using AutoMapper;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class TournamentController : Controller
    {
        private readonly IBaseDA<TournamentHeader> _tournamentHeaderService;
        private readonly IBaseDA<TournamentRelation> _tournamentRelationService;
        private readonly IBaseDA<Match> _matchService;

        public TournamentController(IBaseDA<TournamentHeader> tournamentHeaderService,
            IBaseDA<TournamentRelation> tournamentRelationService,
            IBaseDA<Match> matchService)
        {
            _tournamentHeaderService = tournamentHeaderService;
            _tournamentRelationService = tournamentRelationService;
            _matchService = matchService;
        }

        //GetTournament
        [HttpGet]
        public IEnumerable<TournamentHeader> GetTournamentMeta()
        {
            IEnumerable<TournamentHeader> rtnList = _tournamentHeaderService.Get();

            return rtnList;
        }

        //GetCompleteTournament
        [HttpGet]
        [Route("Complete")]
        public TournamentDto GetTournament([FromQuery]Guid tournamentId)
        {
            Dictionary<Guid, TournamentRelation> relationDict = null;
            Dictionary<Guid, Match> matchDict = null;

            relationDict = _tournamentRelationService.Get(x => x.TournamentHeaderId.Equals(tournamentId)).ToDictionary(x => x.ChildMatchId, x => x);

            var tournyMatchesHashSet = relationDict.Values.Select(x => x.ChildMatchId);
            matchDict = _matchService.Get(x => tournyMatchesHashSet.Contains(x.MatchId)).ToDictionary(x => x.MatchId, x => x);

            TournamentDto rtnDto = Mapper.Map<TournamentDto>(_tournamentHeaderService.Get(x => x.TournamentId.Equals(tournamentId)).Single());

            rtnDto.TournamentMatch = rtnDto.RecursiveBuildTournamentDtoMatches(rtnDto.HeadMatchId, relationDict, matchDict);

            return rtnDto;
        }

        //BuildTournament
        [HttpPost]
        [Route("Complete")]
        public TournamentDto CreateTournament([FromBody]TournamentCreationDto creationDto)
        {
            TournamentHeader svHeader = Mapper.Map<TournamentHeader>(creationDto);

            svHeader.HeadMatchId = Guid.NewGuid();

            _tournamentHeaderService.SaveorUpdate(svHeader);

            //Create relations
            //Breadth first "search" to fill in all the games?
            //One per player except for the first
            Queue<TournamentRelation> relationProcessQueue = new Queue<TournamentRelation>();
            relationProcessQueue.Enqueue(new TournamentRelation());

            int numParticipants = creationDto.Participants.Count;
            int numGamesToAdd = numParticipants - 1;
            List<Match> matchSaveList = new List<Match>();
            List<TournamentRelation> relationSaveList = new List<TournamentRelation>();

            while(numGamesToAdd > 0)
            {
                TournamentRelation currentRelation = relationProcessQueue.Dequeue();
                currentRelation.TournamentRelationId = Guid.NewGuid();
                currentRelation.TournamentHeaderId = svHeader.TournamentId;
                if (currentRelation.ChildMatchId == null || currentRelation.ChildMatchId.Equals(Guid.Empty))
                {
                    numGamesToAdd--;
                    if(matchSaveList.Count <= 0)
                    {
                        currentRelation.ChildMatchId = svHeader.HeadMatchId;
                    }
                    else
                    {
                        currentRelation.ChildMatchId = Guid.NewGuid();
                    }
                    matchSaveList.Add(new Match() { MatchId = currentRelation.ChildMatchId });
                }

                relationSaveList.Add(currentRelation);

                if(numGamesToAdd > 0)
                {
                    numGamesToAdd--;
                    currentRelation.LeftParentMatchId = Guid.NewGuid();
                    Match leftMatch = new Match() { MatchId = currentRelation.LeftParentMatchId };
                    matchSaveList.Add(leftMatch);
                    relationProcessQueue.Enqueue(new TournamentRelation()
                    {
                        ChildMatchId = currentRelation.LeftParentMatchId
                    });

                    if((numParticipants % 2 != 0) && ((((decimal)numParticipants)/2 - 0.5m) == numGamesToAdd))
                    {
                        Player leftPlayer2 = creationDto.GetRandomPlayer();
                        leftMatch.Player2Id = leftPlayer2.PlayerId;
                        creationDto.Participants.Remove(leftPlayer2);
                    }
                    else if(((decimal)numParticipants)/2 >= numGamesToAdd)
                    {
                        Player leftPlayer1 = creationDto.GetRandomPlayer();
                        leftMatch.Player1Id = leftPlayer1.PlayerId;
                        creationDto.Participants.Remove(leftPlayer1);

                        Player leftPlayer2 = creationDto.GetRandomPlayer();
                        leftMatch.Player2Id = leftPlayer2.PlayerId;
                        creationDto.Participants.Remove(leftPlayer2);
                    }
                }

                if(numGamesToAdd > 0)
                {
                    numGamesToAdd--;
                    currentRelation.RightParentMatchId = Guid.NewGuid();
                    Match rightMatch = new Match() { MatchId = currentRelation.RightParentMatchId };
                    matchSaveList.Add(rightMatch);
                    relationProcessQueue.Enqueue(new TournamentRelation()
                    {
                        ChildMatchId = currentRelation.RightParentMatchId
                    });

                    if ((numParticipants % 2 != 0) && ((((decimal)numParticipants) / 2 - 0.5m) == numGamesToAdd))
                    {
                        Player rightPlayer2 = creationDto.GetRandomPlayer();
                        rightMatch.Player2Id = rightPlayer2.PlayerId;
                        creationDto.Participants.Remove(rightPlayer2);
                    }
                    else if (((decimal)numParticipants) / 2 >= numGamesToAdd)
                    {
                        Player rightPlayer1 = creationDto.GetRandomPlayer();
                        rightMatch.Player1Id = rightPlayer1.PlayerId;
                        creationDto.Participants.Remove(rightPlayer1);

                        Player rightPlayer2 = creationDto.GetRandomPlayer();
                        rightMatch.Player2Id = rightPlayer2.PlayerId;
                        creationDto.Participants.Remove(rightPlayer2);
                    }
                }
            }

            //Save all of it
            _tournamentRelationService.SaveorUpdateEnum(relationSaveList);
            _matchService.SaveorUpdateEnum(matchSaveList);

            var rtnDto = Mapper.Map<TournamentDto>(svHeader);
            rtnDto.TournamentMatch = new TournamentMatchDto();
            rtnDto.TournamentMatch = rtnDto.RecursiveBuildTournamentDtoMatches(svHeader.HeadMatchId, relationSaveList.ToDictionary(x => x.ChildMatchId, x => x), matchSaveList.ToDictionary(x => x.MatchId, x => x));

            return rtnDto;
        }

        //UpdateTournament
        [HttpPut]
        [Route("Complete")]
        public TournamentDto UpdateTournamentGames([FromBody]TournamentDto tournamentDtoToUpdate)
        {
            //This one is essentially just updating any games on the tournament.

            _matchService.SaveorUpdateEnum(tournamentDtoToUpdate.RecursiveGetAllMatches());

            return tournamentDtoToUpdate;
        }

        //For now no deleting because it's likely going to be a pain
        //DeleteMatch
        //[HttpPost]
        //[Route("Delete")]
        //public int DeleteLocation([FromBody]Location locationToDelete)
        //{
        //    return _locationService.Delete(locationToDelete);
        //}
    }
}
