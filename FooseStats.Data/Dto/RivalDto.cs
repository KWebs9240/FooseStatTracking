using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class RivalDto
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
        public Dictionary<Guid, int> RivalGamesPlayed { get; set; }
        public Dictionary<Guid, int> RivalGamesWon { get; set; }
        public Dictionary<Guid, decimal> RivalGamesWonPct { get; set; } = new Dictionary<Guid, decimal>();

        //Points Info
        public Dictionary<Guid, int> RivalTotalPointsScored { get; set; }
        public Dictionary<Guid, int> RivalTotalPointsAllowed { get; set; }
        public Dictionary<Guid, decimal> RivalPointsPerGame { get; set; } = new Dictionary<Guid, decimal>();

        //Games Info
        public Dictionary<Guid, int> PlayerGamesPlayed { get; set; }
        public Dictionary<Guid, int> PlayerGamesWon { get; set; } = new Dictionary<Guid, int>();
        public Dictionary<Guid, decimal> PlayerGamesWonPct { get; set; } = new Dictionary<Guid, decimal>();

        //Points Info
        public Dictionary<Guid, int> PlayerTotalPointsScored { get; set; }
        public Dictionary<Guid, int> PlayerTotalPointsAllowed { get; set; }
        public Dictionary<Guid, decimal> PlayerPointsPerGame { get; set; } = new Dictionary<Guid, decimal>();
    }
}
