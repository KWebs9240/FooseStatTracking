using FooseStats.Data.FooseStats.Data.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class TournamentDto
    {
        public Guid TournamentId { get; set; }
        public string TournamentName { get; set; }
        public Guid HeadMatchId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Match CurrentMatch { get; set; }
        public Match RightMatch { get; set; }
        public Match LeftMatch { get; set; }
    }
}
