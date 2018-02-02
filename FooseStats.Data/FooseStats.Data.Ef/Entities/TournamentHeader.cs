using FooseStats.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class TournamentHeader : IUpdatable
    {
        [Key]
        public Guid TournamentId { get; set; }
        public string TournamentName { get; set; }
        public Guid HeadMatchId { get; set; }
        public Guid MatchTypeId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
