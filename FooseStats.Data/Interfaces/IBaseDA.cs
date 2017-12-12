using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Interfaces
{
    public interface IBaseDA<T>
    {
        //Currently just accepting what linq uses.  If I need to I can probably modify to my needs?
        IEnumerable<T> Get(Func<T, bool> filterFunction = null);

        T SaveorUpdateMatches(T toSave);
        IEnumerable<T> SaveorUpdateMatchesEnum(IEnumerable<T> enumToSave);

        int Delete(T toDelete);
        int DeleteEnum(IEnumerable<T> enumToDelete);
    }
}
