using FooseStats.Data.FooseStats.Data.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class MatchDto
    {
        public Guid MatchId { get; set; }
        public Guid Player1Id { get; set; }
        public Guid Player2Id { get; set; }
        public Guid Player3Id { get; set; }
        public Guid Player4Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public bool IsDoubles { get; set; }
        public Guid MatchTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public PlayerDto Player1 { get; set; }
        public PlayerDto Player2 { get; set; }
        public PlayerDto Player3 { get; set; }
        public PlayerDto Player4 { get; set; }

        public MatchType MatchType { get; set; }
    }
}
