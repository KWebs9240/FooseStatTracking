using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;

namespace FooseStats.Data.Interfaces
{
    public interface IMatchTypeDA
    {
        //Currently just accepting what linq uses.  If I need to I can probably modify to my needs?
        IEnumerable<MatchType> GetMatchTypes(Func<MatchType, bool> filterFunction = null);

        MatchType SaveorUpdateMatchTypes(MatchType matchTypeToSave);
        IEnumerable<MatchType> SaveorUpdateMatchTypesEnum(IEnumerable<MatchType> matchTypeEnumToSave);

        int DeleteMatchType(MatchType matchTypeToDelete);
        int DeleteMatchTypeEnum(IEnumerable<MatchType> matchTypeEnumToDelete);
    }
}
