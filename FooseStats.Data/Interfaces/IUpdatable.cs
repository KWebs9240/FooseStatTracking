using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Interfaces
{
    public interface IUpdatable
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdateDate { get; set; }
    }
}
