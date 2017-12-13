using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class PlayerDto
    {
        public Guid PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string HexColor { get; set; }

        public Guid LocationId { get; set; }
        public Guid AlmaMaterId { get; set; }

        //Games Info
        public Dictionary<Guid, int> GamesPlayed { get; set; }
        public Dictionary<Guid, int> GamesWon { get; set; }
        public Dictionary<Guid, decimal> GamesWonPct { get; set; } = new Dictionary<Guid, decimal>();

        //Points Info
        public Dictionary<Guid, int> TotalPointsScored { get; set; }
        public Dictionary<Guid, int> TotalPointsAllowed { get; set; }
        public Dictionary<Guid, decimal> PointsPerGame { get; set; } = new Dictionary<Guid, decimal>();
    }
}
