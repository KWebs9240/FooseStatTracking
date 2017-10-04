using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class MatchTypeController : Controller
    {
        private readonly IMatchTypeDA _matchTypeService;

        public MatchTypeController(IMatchTypeDA matchTypeService)
        {
            _matchTypeService = matchTypeService;
        }

        //GetMatches
        [HttpGet]
        public IEnumerable<MatchType> GetMatchTypes()
        {
            IEnumerable<MatchType> rtnList = _matchTypeService.GetMatchTypes();

            return rtnList;
        }

        //AddMatch
        [HttpPost]
        public MatchType AddMatchType([FromBody]MatchType matchTypeToAdd)
        {
            if (matchTypeToAdd.MatchTypeId != null &&
                _matchTypeService.GetMatchTypes(x => x.MatchTypeId.Equals(matchTypeToAdd.MatchTypeId)).Any())
            {
                throw new InvalidOperationException("A match type with that Id already exist.");
            }

            if (matchTypeToAdd.MatchTypeId.Equals(Guid.Empty))
            {
                matchTypeToAdd.MatchTypeId = Guid.NewGuid();
            }

            return _matchTypeService.SaveorUpdateMatchTypes(matchTypeToAdd);
        }

        //UpdateMatch
        [HttpPut]
        public MatchType UpdateMatchType([FromBody]MatchType matchTypeToUpdate)
        {
            //if (matchTypeToUpdate.MatchTypeId == null
            //    || !_matchTypeService.GetMatchTypes(x => x.MatchTypeId.Equals(matchTypeToUpdate.MatchTypeId)).Any())
            //{
            //    throw new InvalidOperationException("Match type to update does not already exist.");
            //}

            return _matchTypeService.SaveorUpdateMatchTypes(matchTypeToUpdate);
        }

        //DeleteMatch
        [HttpPost]
        [Route("Delete")]
        public int DeleteMatchType([FromBody]MatchType matchTypeToDelete)
        {
            return _matchTypeService.DeleteMatchType(matchTypeToDelete);
        }
    }
}
