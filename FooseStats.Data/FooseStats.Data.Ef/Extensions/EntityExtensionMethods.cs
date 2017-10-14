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

        public static void CopyProperties(this Match source, Match copy)
        {
            source.Player1Id = copy.Player1Id;
            source.Player2Id = copy.Player2Id;
            source.Player3Id = copy.Player3Id;
            source.Player4Id = copy.Player4Id;
            source.Team1Score = copy.Team1Score;
            source.Team2Score = copy.Team2Score;
        }

        public static void CopyProperties(this MatchType source, MatchType copy)
        {
            source.MatchTypeDescription = copy.MatchTypeDescription;
            source.UpdateDate = copy.UpdateDate;
            source.UpdateDate = DateTime.Now;
        }
    }
}
