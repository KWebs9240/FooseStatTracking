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
        public DateTime UpdateDate { get; set; }
        public string HexColor { get; set; }

        //Games Info
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }

        //Points Info
        public int TotalPointsScored { get; set; }
        public int TotalPointsAllowed { get; set; }
        public double PointsPerGame { get; set; }
    }
}
