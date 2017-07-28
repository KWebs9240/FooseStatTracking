using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;

namespace FooseStats.Data.Interfaces
{
    public interface IPlayerDA
    {
        //Currently just accepting what linq uses.  If I need to I can probably modify to my needs?
        IEnumerable<Player> GetPlayers(Func<Player, bool> filterFunction = null);

        Player SaveorUpdatePlayer(Player playerToSave);
        IEnumerable<Player> SaveorUpdatePlayerEnum(IEnumerable<Player> playerEnumToSave);

        int DeletePlayer(Player playerToDelete);
        int DeletePlayerEnum(IEnumerable<Player> playerEnumToDelete);
    }
}
