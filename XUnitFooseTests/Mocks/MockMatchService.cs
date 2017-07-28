using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;

namespace XUnitFooseTests.Mocks
{
    public class MockMatchService : IMatchDA
    {
        private bool _deleteMatchSuccess = false;
        private bool _deleteMatchEnumSuccess = false;
        private bool _getMatchesSuccess = false;
        private bool _saveorUpdateMatchSuccess = false;
        private bool _saveorUpdateMatchEnumSuccess = false;

        public bool DeleteMatchSuccess { get { return _deleteMatchSuccess; } }
        public bool DeleteMatchEnumSuccess { get { return _deleteMatchEnumSuccess; } }
        public bool GetMatchSuccess { get { return _getMatchesSuccess; } }
        public bool SaveorUpdateMatchSuccess { get { return _saveorUpdateMatchSuccess; } }
        public bool SaveorUpdateMatchEnumSuccess { get { return _saveorUpdateMatchEnumSuccess; } }

        private List<Match> _fakeMatches = null;

        public bool InitializeFakePlayers()
        {
            Guid fakePlayer1Id = Guid.NewGuid();
            Guid fakePlayer2Id = Guid.NewGuid();
            Guid fakePlayer3Id = Guid.NewGuid();

            _fakeMatches = new List<Match>();
            _fakeMatches.Add(new Match() { Player1Id = fakePlayer1Id, Player2Id = fakePlayer2Id, IsDoubles = false, Team1Score = 2, Team2Score = 8, UpdateDate = DateTime.Now });
            _fakeMatches.Add(new Match() { Player1Id = fakePlayer1Id, Player2Id = fakePlayer3Id, IsDoubles = false, Team1Score = 8, Team2Score = 2, UpdateDate = DateTime.Now });
            _fakeMatches.Add(new Match() { Player1Id = fakePlayer2Id, Player2Id = fakePlayer3Id, IsDoubles = false, Team1Score = 4, Team2Score = 8, UpdateDate = DateTime.Now });
            _fakeMatches.Add(new Match() { Player1Id = fakePlayer3Id, Player2Id = fakePlayer2Id, IsDoubles = false, Team1Score = 8, Team2Score = 7, UpdateDate = DateTime.Now });

            return true;
        }

        public int DeleteMatch(Match matchToDelete)
        {
            if (matchToDelete != null)
            {
                _deleteMatchSuccess = true;
                return 1;
            }

            else return 0;
        }

        public int DeleteMatchEnum(IEnumerable<Match> matchEnumToDelete)
        {
            int fakeDeleteCount = 0;

            foreach (Match pl in matchEnumToDelete)
            {
                if (pl != null)
                {
                    fakeDeleteCount++;
                }
                else
                {
                    return -1;
                }
            }

            _deleteMatchEnumSuccess = true;
            return fakeDeleteCount;
        }

        public IEnumerable<Match> GetMatches(Func<Match, bool> filterFunction = null)
        {
            List<Match> rtnPlayers = new List<Match>();

            if (_fakeMatches != null)
            {
                if (filterFunction != null)
                {
                    rtnPlayers = _fakeMatches.Where(filterFunction).ToList();
                }
                else
                {
                    rtnPlayers = _fakeMatches.ToList();
                }
            }

            _getMatchesSuccess = true;
            return rtnPlayers;
        }

        public Match SaveorUpdateMatches(Match matchToSave)
        {
            if (matchToSave != null)
            {
                _saveorUpdateMatchSuccess = true;
                return matchToSave;
            }

            return matchToSave;
        }

        public IEnumerable<Match> SaveorUpdateMatchesEnum(IEnumerable<Match> matchEnumToSave)
        {
            List<Match> rtnList = new List<Match>();

            foreach (Match pl in matchEnumToSave)
            {
                if (pl == null)
                {
                    return null;
                }

                rtnList.Add(pl);
            }

            _saveorUpdateMatchEnumSuccess = true;

            return rtnList;
        }
    }
}
