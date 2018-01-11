using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FooseStats.Data.Dto;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class TournamentController : Controller
    {
        private readonly IBaseDA<TournamentHeader> _tournamentHeaderService;
        private readonly IBaseDA<TournamentRelation> _tournamentRelationService;

        public TournamentController(IBaseDA<TournamentHeader> tournamentHeaderService,
            IBaseDA<TournamentRelation> tournamentRelationService)
        {
            _tournamentHeaderService = tournamentHeaderService;
            _tournamentRelationService = tournamentRelationService;
        }

        //GetTournament
        [HttpGet]
        public IEnumerable<TournamentHeader> GetTournamentMeta()
        {
            IEnumerable<TournamentHeader> rtnList = _tournamentHeaderService.Get();

            return rtnList;
        }

        //GetFullTournament

        //AddMatch
        [HttpPost]
        public Location AddLocation([FromBody]Location locationToAdd)
        {
            if (locationToAdd.LocationId != null &&
                _locationService.Get(x => x.LocationId.Equals(locationToAdd.LocationId)).Any())
            {
                throw new InvalidOperationException("A match type with that Id already exist.");
            }

            if (locationToAdd.LocationId.Equals(Guid.Empty))
            {
                locationToAdd.LocationId = Guid.NewGuid();
            }

            return _locationService.SaveorUpdate(locationToAdd);
        }

        //UpdateMatch
        [HttpPut]
        public Location UpdateMatchType([FromBody]Location locationToUpdate)
        {
            //if (matchTypeToUpdate.MatchTypeId == null
            //    || !_matchTypeService.GetMatchTypes(x => x.MatchTypeId.Equals(matchTypeToUpdate.MatchTypeId)).Any())
            //{
            //    throw new InvalidOperationException("Match type to update does not already exist.");
            //}

            return _locationService.SaveorUpdate(locationToUpdate);
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
