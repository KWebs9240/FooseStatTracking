using FooseStats.Data.FooseStats.Data.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class TournamentMatchDto
    {
        public Match CurrentMatch { get; set; }
        public TournamentMatchDto LeftMatch { get; set; }
        public TournamentMatchDto RightMatch { get; set; }
    }
}
