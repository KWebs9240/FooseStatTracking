using FooseStats.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class MatchType : IUpdatable
    {
        public Guid MatchTypeId { get; set; }
        public string MatchTypeDescription { get; set; }
        public int MaxPoints { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
