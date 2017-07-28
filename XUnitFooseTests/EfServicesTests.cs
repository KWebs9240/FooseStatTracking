using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Services;
using Xunit;

namespace XUnitFooseTests
{
    public class EfServicesTests
    {
        private List<string> _firstNames = new List<string>()
        {
            "Kyle",
            "William",
            "Matt",
            "Michael",
            "Jeff",
            "Jared",
            "Brittnee",
            "Travis"
        };

        private List<string> _lastNames = new List<string>()
        {
            "Webster",
            "Hodges",
            "VanNatten",
            "Columbus",
            "LaFavors",
            "Friedman",
            "Keller",
            "Barnes"
        };

        private List<string> _nickNames = new List<string>()
        {
            "Chimp9240",
            "Management",
            "Yankee",
            "The Artist",
            "Tampa",
            "JuhJay",
            "Rando",
            "Giggem"
        };

        //This is a test to make sure the Ef Sqlite stuff is working as well as create a db file I can use with plenty of dummy data
        [Fact]
        public void GenerateDummyData()
        {
            Random randomNumberThing = new Random();

            HashSet<string> alreadyAddedCombos = new HashSet<string>();

            List<Player> AddedPlayers = new List<Player>();

            FoosePlayerDAService playerService = new FoosePlayerDAService();

            for(int i = 0; i < 100; i++)
            {
                Player playerToAdd = new Player();
                playerToAdd.PlayerId = Guid.NewGuid();
                playerToAdd.FirstName = _firstNames[randomNumberThing.Next(7)];
                playerToAdd.LastName = _lastNames[randomNumberThing.Next(7)];
                playerToAdd.NickName = _nickNames[randomNumberThing.Next(7)];

                string playerNameKey = string.Format("{0}|{1}|{2}", playerToAdd.FirstName, playerToAdd.LastName, playerToAdd.NickName);
                if(!alreadyAddedCombos.Contains(playerNameKey))
                {
                    alreadyAddedCombos.Add(playerNameKey);
                    playerService.SaveorUpdatePlayer(playerToAdd);
                    AddedPlayers.Add(playerToAdd);
                }
            }

            FooseMatchDAService matchService = new FooseMatchDAService();

            for (int i = 0; i < 500; i++)
            {
                Match matchToAdd = new Match();
                matchToAdd.MatchId = Guid.NewGuid();
                matchToAdd.Player1Id = AddedPlayers[randomNumberThing.Next(AddedPlayers.Count)].PlayerId;

                do
                {
                    matchToAdd.Player2Id = AddedPlayers[randomNumberThing.Next(AddedPlayers.Count)].PlayerId;
                } while (matchToAdd.Player2Id.Equals(matchToAdd.Player1Id));

                //If greater than 0, let team 1 win
                if(randomNumberThing.Next(2) > 0)
                {
                    matchToAdd.Team1Score = 8;
                    matchToAdd.Team2Score = randomNumberThing.Next(8);
                }
                else
                {
                    matchToAdd.Team1Score = randomNumberThing.Next(8);
                    matchToAdd.Team2Score = 8;
                }

                //If we get a 1, go ahead and make it a doubles match
                if(randomNumberThing.Next(2) > 0)
                {
                    matchToAdd.IsDoubles = true;
                    do
                    {
                        matchToAdd.Player3Id = AddedPlayers[randomNumberThing.Next(AddedPlayers.Count)].PlayerId;
                    } while (matchToAdd.Player3Id.Equals(matchToAdd.Player1Id) || matchToAdd.Player3Id.Equals(matchToAdd.Player2Id));

                    do
                    {
                        matchToAdd.Player4Id = AddedPlayers[randomNumberThing.Next(AddedPlayers.Count)].PlayerId;
                    } while (matchToAdd.Player4Id.Equals(matchToAdd.Player1Id) || matchToAdd.Player4Id.Equals(matchToAdd.Player2Id) || matchToAdd.Player4Id.Equals(matchToAdd.Player3Id));
                }

                matchService.SaveorUpdateMatches(matchToAdd);
            }
        }
    }
}
