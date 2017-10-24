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
    public class FoosePlayerDAService : IPlayerDA
    {
        public int DeletePlayer(Player playerToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                db.Players.Remove(playerToDelete);

                return db.SaveChanges();
            }
        }

        public int DeletePlayerEnum(IEnumerable<Player> playerEnumToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                db.Players.RemoveRange(playerEnumToDelete);

                return db.SaveChanges();
            }
        }

        public IEnumerable<Player> GetPlayers(Func<Player, bool> filterFunction = null)
        {
            using (var db = new FooseStatsContext())
            {
                if (filterFunction == null)
                {
                    return db.Players.ToList();
                }
                else
                {
                    return db.Players.Where(filterFunction).ToList();
                }
            }
        }

        public Player SaveorUpdatePlayer(Player playerToSave)
        {
            playerToSave.UpdateDate = DateTime.Now;

            using (var db = new FooseStatsContext())
            {
                Player updtPlayer = db.Players.FirstOrDefault(x => x.PlayerId.Equals(playerToSave.PlayerId));
                
                if(updtPlayer == null)
                {
                    playerToSave.CreatedDate = DateTime.Now;
                    db.Players.Add(playerToSave);
                    updtPlayer = playerToSave;
                }
                else
                {
                    updtPlayer.CopyProperties(playerToSave);
                }

                db.SaveChanges();

                return updtPlayer;
            }
        }

        public IEnumerable<Player> SaveorUpdatePlayerEnum(IEnumerable<Player> playerEnumToSave)
        {
            playerEnumToSave.ToList().ForEach(x => x = SaveorUpdatePlayer(x));

            return playerEnumToSave;
        }
    }
}
