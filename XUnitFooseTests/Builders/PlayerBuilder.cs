using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;

namespace XUnitFooseTests.Builders
{
    public class PlayerBuilder
    {
        private string _firstName { get; set; }
        private string _lastName { get; set; }
        private string _nickName { get; set; }

        public PlayerBuilder()
        {
            _firstName = "Kyle";
            _lastName = "Webster";
            _nickName = "Default";
        }

        public Player Build()
        {
            return new Player()
            {
                PlayerId = Guid.NewGuid(),
                FirstName = _firstName,
                LastName = _lastName,
                NickName = _nickName,
                UpdateDate = DateTime.Now
            };
        }

        public List<Player> BuildList()
        {
            List<Player> rtnList = new List<Player>();

            rtnList.Add(Build());

            _firstName = "William";
            _lastName = "Hodges";

            rtnList.Add(Build());

            return rtnList;
        }
    }
}
