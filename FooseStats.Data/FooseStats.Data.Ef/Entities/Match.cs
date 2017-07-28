using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class Match
    {
        [Key]
        public Guid MatchId { get; set; }
        public Guid Player1Id { get; set; }
        public Guid Player2Id { get; set; }
        public Guid Player3Id { get; set; }
        public Guid Player4Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public bool IsDoubles { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
