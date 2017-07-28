using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;

namespace FooseStats.Data.Interfaces
{
    public interface IMatchDA
    {
        //Currently just accepting what linq uses.  If I need to I can probably modify to my needs?
        IEnumerable<Match> GetMatches(Func<Match, bool> filterFunction = null);

        Match SaveorUpdateMatches(Match matchToSave);
        IEnumerable<Match> SaveorUpdateMatchesEnum(IEnumerable<Match> matchEnumToSave);

        int DeleteMatch(Match matchToDelete);
        int DeleteMatchEnum(IEnumerable<Match> matchEnumToDelete);
    }
}
