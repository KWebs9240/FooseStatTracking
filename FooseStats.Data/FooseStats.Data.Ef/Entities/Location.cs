using FooseStats.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class Location : IUpdatable
    {
        [Key]
        public Guid LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
