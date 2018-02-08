using FooseStats.Data.Dto;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace FooseStats.Data.FooseStats.Data.Ef.Helpers
{
    public static class MatchDtoExtensions
    {
        public static void AddPlayerInfo(this List<MatchDto> source, IBaseDA<Player> playerService)
        {
            var playerIdList = new HashSet<Guid>(new List<Guid>()
                    .Union(source.Select(x => x.Player1Id))
                    .Union(source.Select(x => x.Player2Id))
                    .Union(source.Select(x => x.Player3Id))
                    .Union(source.Select(x => x.Player4Id)));

            Dictionary<Guid, Player> playerDict = playerService.Get(x => playerIdList.Contains(x.PlayerId)).ToDictionary(x => x.PlayerId, x => x);

            foreach (MatchDto matDto in source)
            {
                matDto.Player1 = matDto.Player1Id != Guid.Empty ? Mapper.Map<PlayerDto>(playerDict[matDto.Player1Id]) : null;
                matDto.Player2 = matDto.Player2Id != Guid.Empty ? Mapper.Map<PlayerDto>(playerDict[matDto.Player2Id]) : null;
                matDto.Player3 = matDto.Player3Id != Guid.Empty ? Mapper.Map<PlayerDto>(playerDict[matDto.Player3Id]) : null;
                matDto.Player4 = matDto.Player4Id != Guid.Empty ? Mapper.Map<PlayerDto>(playerDict[matDto.Player4Id]) : null;
            }
        }
    }
}
