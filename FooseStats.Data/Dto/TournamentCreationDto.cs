using FooseStats.Data.FooseStats.Data.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class TournamentCreationDto
    {
        public Guid TournamentId { get; set; }
        public string TournamentName { get; set; }
        public Guid HeadMatchId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public List<Player> Participants { get; set; }

        public Player GetRandomPlayer()
        {
            Random rand = new Random();
            return Participants[rand.Next(Participants.Count - 1)];
        }
    }
}
