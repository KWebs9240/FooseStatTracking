using FooseStats.Data.FooseStats.Data.Ef.Entities;
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
        public Guid MatchTypeId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public TournamentMatchDto TournamentMatch { get; set; }

        public TournamentMatchDto RecursiveBuildTournamentDtoMatches(Guid currentMatch, Dictionary<Guid, TournamentRelation> relationDictionary, Dictionary<Guid, MatchDto> matchDictionary, TournamentMatchDto matchToFill = null)
        {
            matchToFill = matchToFill ?? this.TournamentMatch;

            if (matchDictionary.ContainsKey(currentMatch))
            {
                matchToFill.CurrentMatch = matchDictionary[currentMatch];
            }

            if(relationDictionary.ContainsKey(currentMatch))
            {
                var currentRel = relationDictionary[currentMatch];

                if(currentRel.LeftParentMatchId != null && !currentRel.LeftParentMatchId.Equals(Guid.Empty))
                {
                    matchToFill.LeftMatch = new TournamentMatchDto();
                    matchToFill.LeftMatch = RecursiveBuildTournamentDtoMatches(currentRel.LeftParentMatchId, relationDictionary, matchDictionary, matchToFill.LeftMatch);
                }
                if(currentRel.RightParentMatchId != null && !currentRel.RightParentMatchId.Equals(Guid.Empty))
                {
                    matchToFill.RightMatch = new TournamentMatchDto();
                    matchToFill.RightMatch = RecursiveBuildTournamentDtoMatches(currentRel.RightParentMatchId, relationDictionary, matchDictionary, matchToFill.RightMatch);
                }
            }

            return matchToFill;
        }

        public List<MatchDto> RecursiveGetAllMatches(TournamentMatchDto MatchToAdd = null)
        {
            MatchToAdd = MatchToAdd ?? this.TournamentMatch;

            List<MatchDto> rtnList = new List<MatchDto>();

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
