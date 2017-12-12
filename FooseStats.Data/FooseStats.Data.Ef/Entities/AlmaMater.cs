using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class AlmaMater
    {
        [Key]
        public Guid AlmaMaterId { get; set; }
        public string AlmaMaterCode { get; set; }
        public string AlmaMaterDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
