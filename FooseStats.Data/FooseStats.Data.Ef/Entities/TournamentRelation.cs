using FooseStats.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class TournamentRelation : IUpdatable
    {
        [Key]
        public Guid TournamentRelationId { get; set; }
        public Guid LeftParentMatchId { get; set; }
        public Guid RightParentMatchId { get; set; }
        public Guid ChildMatchId { get; set; }
        public Guid TournamentHeaderId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
