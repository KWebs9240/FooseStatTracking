using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
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
            _tournamentHeaderService.SaveorUpdate(svHeader);

            //Create relations
            //One per player except for the first
            //Num Relations = Num players - 1

            //Relate them to eachother

            //Create the required matches for each relation

            //Assign default players to the bottom matches

            //Save all of it

            //Build the tournament Dto: ideally it's already built
            //Return it
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

        //DeleteMatch
        [HttpPost]
        [Route("Delete")]
        public int DeleteLocation([FromBody]Location locationToDelete)
        {
            return _locationService.Delete(locationToDelete);
        }
    }
}
