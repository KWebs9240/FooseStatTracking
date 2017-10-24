using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FooseStats.Data.FooseStats.Data.Ef;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.FooseStats.Data.Ef.Extensions;
using FooseStats.Data.Interfaces;

namespace FooseStats.Data.Services
{
    public class FooseMatchTypeDAService : IMatchTypeDA
    {
        public int DeleteMatchType(MatchType matchTypeToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                db.MatchTypes.Remove(matchTypeToDelete);

                return db.SaveChanges();
            }
        }

        public int DeleteMatchTypeEnum(IEnumerable<MatchType> matchTypeEnumToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                db.MatchTypes.RemoveRange(matchTypeEnumToDelete);

                return db.SaveChanges();
            }
        }

        public IEnumerable<MatchType> GetMatchTypes(Func<MatchType, bool> filterFunction = null)
        {
            using (var db = new FooseStatsContext())
            {
                if (filterFunction == null)
                {
                    return db.MatchTypes.ToList();
                }
                else
                {
                    return db.MatchTypes.Where(filterFunction).ToList();
                }
            }
        }

        public MatchType SaveorUpdateMatchTypes(MatchType matchTypeToSave)
        {
            matchTypeToSave.UpdateDate = DateTime.Now;

            using (var db = new FooseStatsContext())
            {
                MatchType updtMatchType = db.MatchTypes.FirstOrDefault(x => x.MatchTypeId.Equals(matchTypeToSave.MatchTypeId));

                if (updtMatchType == null)
                {
                    matchTypeToSave.CreatedDate = DateTime.Now;
                    db.MatchTypes.Add(matchTypeToSave);
                    updtMatchType = matchTypeToSave;
                }
                else
                {
                    updtMatchType.CopyProperties(matchTypeToSave);
                }

                db.SaveChanges();

                return updtMatchType;
            }
        }

        public IEnumerable<MatchType> SaveorUpdateMatchTypesEnum(IEnumerable<MatchType> matchTypeEnumToSave)
        {
            matchTypeEnumToSave.ToList().ForEach(x => x = SaveorUpdateMatchTypes(x));

            return matchTypeEnumToSave;
        }
    }
}
