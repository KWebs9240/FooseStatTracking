﻿using FooseStats.Data.FooseStats.Data.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooseStats.Data.Dto
{
    public class TournamentDto
    {
        public Guid TournamentId { get; set; }
        public string TournamentName { get; set; }
        public Guid HeadMatchId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public TournamentMatchDto TournamentMatch { get; set; }

        public TournamentMatchDto RecursiveBuildTournamentDtoMatches(Guid currentMatch, Dictionary<Guid, TournamentRelation> relationDictionary, Dictionary<Guid, Match> matchDictionary, TournamentMatchDto matchToFill = null)
        {
            matchToFill = matchToFill ?? this.TournamentMatch;

            if (matchDictionary.ContainsKey(currentMatch))
            {
                matchToFill.CurrentMatch = matchDictionary[currentMatch];
            }

            if(relationDictionary.ContainsKey(currentMatch))
            {
                var currentRel = relationDictionary[currentMatch];

                if(currentRel.LeftParentMatchId != null)
                {
                    RecursiveBuildTournamentDtoMatches(currentRel.LeftParentMatchId, relationDictionary, matchDictionary, matchToFill.LeftMatch);
                }
                if(currentRel.RightParentMatchId != null)
                {
                    RecursiveBuildTournamentDtoMatches(currentRel.RightParentMatchId, relationDictionary, matchDictionary, matchToFill.RightMatch);
                }
            }

            return matchToFill;
        }

        public List<Match> RecursiveGetAllMatches(TournamentMatchDto MatchToAdd = null)
        {
            MatchToAdd = MatchToAdd ?? this.TournamentMatch;

            List<Match> rtnList = new List<Match>();

            rtnList.Add(MatchToAdd.CurrentMatch);

            if(MatchToAdd.LeftMatch != null)
            {
                rtnList.AddRange(RecursiveGetAllMatches(MatchToAdd.LeftMatch));
            }
            if(MatchToAdd.RightMatch != null)
            {
                rtnList.AddRange(RecursiveGetAllMatches(MatchToAdd.RightMatch));
            }

            return rtnList;
        }
    }
}
