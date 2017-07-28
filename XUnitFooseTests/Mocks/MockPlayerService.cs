using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;

namespace XUnitFooseTests.Mocks
{
    public class MockPlayerService : IPlayerDA
    {
        private bool _deletePlayerSuccess = false;
        private bool _deletePlayerEnumSuccess = false;
        private bool _getPlayersSuccess = false;
        private bool _saveorUpdatePlayerSuccess = false;
        private bool _saveorUpdatePlayerEnumSuccess = false;

        public bool DeletePlayerSuccess { get { return _deletePlayerSuccess; } }
        public bool DeletePlayerEnumSuccess { get { return _deletePlayerEnumSuccess; } }
        public bool GetPlayerSuccess { get { return _getPlayersSuccess; } }
        public bool SaveorUpdatePlayerSuccess { get { return _saveorUpdatePlayerSuccess; } }
        public bool SaveorUpdatePlayerEnumSuccess { get { return _saveorUpdatePlayerEnumSuccess; } }

        private List<Player> _fakePlayers = null;

        public bool InitializeFakePlayers()
        {
            _fakePlayers = new List<Player>();
            _fakePlayers.Add(new Player() { PlayerId = new Guid("0b3f7e1c-cd09-4df1-9e07-d03887e0e522"), FirstName = "Kyle", LastName = "Webster", NickName = "Chimp9240", UpdateDate = DateTime.Now });
            _fakePlayers.Add(new Player() { PlayerId = new Guid("39454e08-1afd-46ba-80d5-bf74b17e0356"), FirstName = "Matt", LastName = "VanNatten", NickName = "Rampage", UpdateDate = DateTime.Now });
            _fakePlayers.Add(new Player() { PlayerId = new Guid("1e075ded-ba34-410f-941e-f1c01942f096"), FirstName = "Michael", LastName = "Columbus", NickName = "The Artist", UpdateDate = DateTime.Now });
            _fakePlayers.Add(new Player() { PlayerId = new Guid("385e86ad-35bc-440d-b57a-91d8a2be4b72"), FirstName = "William", LastName = "Hodges", NickName = "Management", UpdateDate = DateTime.Now });

            return true;
        }

        public int DeletePlayer(Player playerToDelete)
        {
            if (playerToDelete != null)
            {
                _deletePlayerSuccess = true;
                return 1;
            }

            else return 0;
        }

        public int DeletePlayerEnum(IEnumerable<Player> playerEnumToDelete)
        {
            int fakeDeleteCount = 0;

            foreach(Player pl in playerEnumToDelete)
            {
                if(pl != null)
                {
                    fakeDeleteCount++;
                }
                else
                {
                    return -1;
                }
            }

            _deletePlayerEnumSuccess = true;
            return fakeDeleteCount;
        }

        public IEnumerable<Player> GetPlayers(Func<Player, bool> filterFunction = null)
        {
            List<Player> rtnPlayers = new List<Player>();

            if(_fakePlayers != null)
            {
                if (filterFunction != null)
                {
                    rtnPlayers = _fakePlayers.Where(filterFunction).ToList();
                }
                else
                {
                    rtnPlayers = _fakePlayers.ToList();
                }
            }

            _getPlayersSuccess = true;
            return rtnPlayers;
        }

        public Player SaveorUpdatePlayer(Player playerToSave)
        {
            if(playerToSave != null)
            {
                _saveorUpdatePlayerSuccess = true;
                return playerToSave;
            }

            return playerToSave;
        }

        public IEnumerable<Player> SaveorUpdatePlayerEnum(IEnumerable<Player> playerEnumToSave)
        {
            List<Player> rtnList = new List<Player>();

            foreach(Player pl in playerEnumToSave)
            {
                if(pl == null)
                {
                    return null;
                }

                rtnList.Add(pl);
            }

            _saveorUpdatePlayerEnumSuccess = true;

            return rtnList;
        }
    }
}
