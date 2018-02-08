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
    public class MatchController : Controller
    {
        private readonly IBaseDA<Match> _matchService;
        private readonly IBaseDA<Player> _playerService;
        private readonly IBaseDA<MatchType> _matchTypeService;

        public MatchController(IBaseDA<Match> matchService,
            IBaseDA<Player> playerService,
            IBaseDA<MatchType> matchTypeService)
        {
            _matchService = matchService;
            _playerService = playerService;
            _matchTypeService = matchTypeService;
        }

        //GetMatches
        [HttpGet]
        public IEnumerable<MatchDto> GetMatches([FromQuery] bool LoadPlayerInfo = false, [FromQuery] bool LoadMatchTypeInfo = false)
        {
            List<MatchDto> rtnList = Mapper.Map<List<MatchDto>>(_matchService.Get());

            if (LoadPlayerInfo)
            {
                rtnList.AddPlayerInfo(_playerService);
            }

            if (LoadMatchTypeInfo)
            {
                var matchTypeDict = _matchTypeService.Get().ToDictionary(x => x.MatchTypeId, x => x);

                foreach(MatchDto matDto in rtnList)
                {
                    matDto.MatchType = matchTypeDict[matDto.MatchTypeId];
                }
            }

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
