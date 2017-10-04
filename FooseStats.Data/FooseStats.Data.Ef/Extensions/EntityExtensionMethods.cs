using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;

namespace FooseStats.Data.FooseStats.Data.Ef.Extensions
{
    public static class EntityExtensionMethods
    {
        public static void CopyProperties(this Player source, Player copy)
        {
            source.FirstName = copy.FirstName;
            source.LastName = copy.LastName;
            source.NickName = copy.NickName;
            source.UpdateDate = DateTime.Now;
        }

        public static void CopyProperties(this MatchType source, MatchType copy)
        {
            source.MatchTypeDescription = copy.MatchTypeDescription;
            source.UpdateDate = copy.UpdateDate;
            source.UpdateDate = DateTime.Now;
        }
    }
}
