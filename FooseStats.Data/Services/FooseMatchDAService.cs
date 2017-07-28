using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;

namespace FooseStats.Data.Services
{
    public class FooseMatchDAService : IMatchDA
    {
        public int DeleteMatch(Match matchToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                db.Matches.Remove(matchToDelete);

                return db.SaveChanges();
            }
        }

        public int DeleteMatchEnum(IEnumerable<Match> matchEnumToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                db.Matches.RemoveRange(matchEnumToDelete);

                return db.SaveChanges();
            }
        }

        public IEnumerable<Match> GetMatches(Func<Match, bool> filterFunction = null)
        {
            using (var db = new FooseStatsContext())
            {
                return db.Matches.Where(filterFunction).ToList();
            }
        }

        public Match SaveorUpdateMatches(Match matchToSave)
        {
            matchToSave.UpdateDate = DateTime.Now;

            using (var db = new FooseStatsContext())
            {
                Match updtMatch = db.Matches.FirstOrDefault(x => x.MatchId.Equals(matchToSave.MatchId));

                if (updtMatch == null)
                {
                    db.Matches.Add(matchToSave);
                }
                else
                {
                    updtMatch = matchToSave;
                }

                db.SaveChanges();

                return updtMatch;
            }
        }

        public IEnumerable<Match> SaveorUpdateMatchesEnum(IEnumerable<Match> matchEnumToSave)
        {
            matchEnumToSave.ToList().ForEach(x => x = SaveorUpdateMatches(x));

            return matchEnumToSave;
        }
    }
}
