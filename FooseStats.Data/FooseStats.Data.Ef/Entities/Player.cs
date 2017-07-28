using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooseStats.Data.FooseStats.Data.Ef.Entities
{
    public class Player
    {
        [Key]
        public Guid PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
