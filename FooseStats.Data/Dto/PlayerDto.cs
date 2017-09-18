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

        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }
}
