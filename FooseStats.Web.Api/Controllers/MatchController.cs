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
    public class MatchController : Controller
    {
        private readonly IBaseDA<Match> _matchService;

        public MatchController(IBaseDA<Match> matchService)
        {
            _matchService = matchService;
        }

        //GetMatches
        [HttpGet]
        public IEnumerable<Match> GetMatches()
        {
            IEnumerable<Match> rtnList = _matchService.Get();

            return rtnList;
        }

        //AddMatch
        [HttpPost]
        public Match AddMatch([FromBody]Match matchToAdd)
        {
            if (matchToAdd.MatchId != null &&
                _matchService.Get(x => x.MatchId.Equals(matchToAdd.MatchId)).Any())
            {
                throw new InvalidOperationException("A match with that Id already exist.");
            }

            if (matchToAdd.MatchId.Equals(Guid.Empty))
            {
                matchToAdd.MatchId = Guid.NewGuid();
            }

            return _matchService.SaveorUpdate(matchToAdd);
        }

        //UpdateMatch
        [HttpPut]
        public Match UpdateMatch([FromBody]Match matchToUpdate)
        {
            if(matchToUpdate.MatchId == null
                || !_matchService.Get(x => x.MatchId.Equals(matchToUpdate.MatchId)).Any())
            {
                throw new InvalidOperationException("Match to update does not already exist.");
            }

            return _matchService.SaveorUpdate(matchToUpdate);
        }

        //DeleteMatch
        [HttpPost]
        [Route("Delete")]
        public int DeleteMatch([FromBody]Match matchToDelete)
        {
            return _matchService.Delete(matchToDelete);
        }
    }
}
