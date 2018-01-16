using FooseStats.Data.FooseStats.Data.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Helpers
{
    public static class TournamentRelationExtensions
    {
        public static Match AddMatchToRelation (this Guid source)
        {
            source = Guid.NewGuid();
            return new Match() { MatchId = source };
        }
    }
}
